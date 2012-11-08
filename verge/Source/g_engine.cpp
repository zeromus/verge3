/// The VERGE 3 Project is originally by Ben Eirich and is made available via
///  the BSD License.
///
/// Please see LICENSE in the project's root directory for the text of the
/// licensing agreement.  The CREDITS file in the same directory enumerates the
/// folks involved in this public build.
///
/// If you have altered this source file, please log your name, date, and what
/// changes you made below this line.


/******************************************************************
 * verge3: g_engine.cpp                                           *
 * copyright (c) 2001 vecna                                       *
 ******************************************************************/

#include "xerxes.h"

/****************************** data ******************************/

Entity	*entity[256];
Entity	*myself = 0;
int		entities;
int		player;
int		playerstep = 1;
int		playerdiagonals = 1;

int		xwin, ywin;
MAP		*current_map = 0;
int		px, py;

bool	done, inscroller=false;
int		cameraclamp=1;
int		cameratracking=1;
int     cameratracker=0;
int     lastplayerdir=0;

int     lastentitythink;
int     lastspritethink=0;
int		entitiespaused=false;

/****************************** code ******************************/

// CHR cache/requester/GC system

#define MAXCHRS		256

struct CHRnode
{
	CHR *chr;
	char fname[256];
	char active;
};

CHRnode chrnodes[MAXCHRS];

CHR *RequestCHR(const char *fname)
{
	int freeslot = -1;
	for (int i=0; i<MAXCHRS; i++)
	{
		if (!chrnodes[i].active)
		{
			if (freeslot == -1)
				freeslot = i;
			continue;
		}
		if (!strcasecmp(chrnodes[i].fname, fname))
			return chrnodes[i].chr;
	}

	if (freeslot==-1) {
		// try to cleanup; if we're successful,
		// make the request again
		if(CleanupCHRs()) {
			return RequestCHR(fname);
		} else  {
			err("Duuuuuuuuddddeeeeee.... you've run out of CHR cache slots!");
		}
	}

	// no existing chr found, so lets load it up
	CHR *t = new CHR(fname);
	chrnodes[freeslot].chr = t;
	strcpy(chrnodes[freeslot].fname, fname);
	chrnodes[freeslot].active = 1;
	return t;
}

bool CleanupCHRs()
{
	bool cleaned_one = false;

	// Do CHR 'garbage collection'
	for (int i=0; i<MAXCHRS; i++)
	{
		bool chrfound = false;
		if (!chrnodes[i].active) continue;
		for (int j=0; j<entities; j++)
		{
			if (entity[j]->chr == chrnodes[i].chr)
			{
				chrfound = true;
				break;
			}
		}
		// couldnt find any entity using this CHR.. toss it out!
		if (!chrfound)
		{
			cleaned_one = true;
			delete chrnodes[i].chr;
			chrnodes[i].active = 0;
		}
	}
	return cleaned_one;
}

/******************************************************************/

// main engine code

int AllocateEntity(int x, int y, const char *chr)
{
	entity[entities] = new Entity(x, y, chr);
	entity[entities]->index = entities;
	return entities++;
}

static int CDECL cmpent(const void* a, const void* b)
{
	int sa = entity[*(byte*)a]->sortkey;
	int sb = entity[*(byte*)b]->sortkey;
	int scmp = sa-sb;
	if(scmp!=0) return scmp;
	int ya = entity[*(byte*)a]->gety();
	int yb = entity[*(byte*)b]->gety();
	int ycmp = ya-yb;
	return ycmp;
}

void RenderEntities()
{
	static byte entidx[256];
	int entnum = 0;

	// Build a list of entities that are onscreen/visible.
	// FIXME: Make it actually only be entities that are onscreen
	int i;
	for (i=0; i<entities; i++)
		entidx[entnum++]=i;

	// Ysort that list, then draw.
	qsort(entidx, entnum, 1, cmpent);
	for (i=0; i<entnum; i++)
	{
		RenderSpritesBelowEntity(entidx[i]);
		SetLucent(entity[entidx[i]]->lucent);
		entity[entidx[i]]->draw();
		SetLucent(0);
		RenderSpritesAboveEntity(entidx[i]);
	}
}

void ProcessEntities()
{
	if (entitiespaused) return;
	for (int i=0; i<entities; i++)
		entity[i]->think();
}

int EntityAt(int x, int y)
{
	for (int i=0; i<entities; i++)
	{
		if (entity[i]->active &&
			x >= entity[i]->getx() && x < entity[i]->getx() + entity[i]->hotw &&
			y >= entity[i]->gety() && y < entity[i]->gety() + entity[i]->hoth)
			return i;
	}
	return -1;
}

int EntityObsAt(int x, int y)
{
	for( int i=0; i<entities; i++ )
	{
		if (entity[i]->active && entity[i]->obstruction &&
			x >= entity[i]->getx() && x < entity[i]->getx() + entity[i]->hotw &&
			y >= entity[i]->gety() && y < entity[i]->gety() + entity[i]->hoth)
			return i;
	}
	return -1;
}

int isEntityCollisionCapturing() {
	return !_trigger_onEntityCollide.empty();
}

int __obstructionHappened = 0;

extern int ignoreEntCollisionId;
bool ObstructAt(int x, int y)
{
	if( current_map->obstructpixel(x, y) ) {
		
		if( isEntityCollisionCapturing() ) {
			event_tx = x/16;
			event_ty = y/16;
			event_entity = __grue_actor_index;
			event_zone = current_map->zone(x/16, y/16);
			event_entity_hit = -1;
			onEntityCollision();
		}

		return true;
	}

	int ent_idx = EntityObsAt(x, y);

	if( ent_idx > -1 && ent_idx != ignoreEntCollisionId) {

		if( isEntityCollisionCapturing() ) {
			event_tx = x/16;
			event_ty = y/16;
			event_entity = __grue_actor_index;
			event_zone = -1;
			event_entity_hit = ent_idx;
			onEntityCollision();
		}

		return true;
	}

	return false;
}

// returns distance possible to move up to
// the first obstruction in the given direction
int MaxPlayerMove(int d, int max)
{
	__grue_actor_index = myself->index;

	int ex = myself->getx();
	int ey = myself->gety();

    // check to see if the player is obstructable at all
	if (!myself->obstructable) return max;

	bool old_obs = myself->obstruction;
	myself->obstruction = false;

	int ret = max;

	//mbg 03-dec-2011 - change to hardcoded ffmq motion
	switch (d)
	{
	case NORTH:
		if (ObstructAt(ex+8, ey-1)) ret = 0;
		if (ObstructAt(ex+8, ey)) ret = 0;
		break;
	case SOUTH:
		if (ObstructAt(ex+8, ey+16)) ret = 0;
		if (ObstructAt(ex+8, ey+15)) ret = 0;
		break;
	case EAST:
		if (ObstructAt(ex+16, ey+8)) ret = 0;
		if (ObstructAt(ex+15, ey+8)) ret = 0;
		break;
	case WEST:
		if (ObstructAt(ex-1, ey+8)) ret = 0;
		if (ObstructAt(ex, ey+8)) ret = 0;
		break;
	}

	myself->obstruction = old_obs;

	return ret;


	//for(int check = 1; check <= max+1; check++) {
	//	switch (d)
	//	{
	//		case NORTH:
	//			for (x=ex; x<ex+myself->hotw; x++)
	//				if (ObstructAt(x, ey-check)) return check-1;
	//			break;
	//		case SOUTH:
	//			for (x=ex; x<ex+myself->hotw; x++)
	//				if (ObstructAt(x, ey+myself->hoth+check-1)) return check-1;
	//			break;
	//		case WEST:
	//			for (y=ey; y<ey+myself->hoth; y++)
	//				if (ObstructAt(ex-check, y)) return check-1;
	//			break;
	//		case EAST:
	//			for (y=ey; y<ey+myself->hoth; y++)
	//				if (ObstructAt(ex+myself->hotw+check-1, y)) return check-1;
	//			break;
	//		case NW:
	//			for (x=ex; x<ex+myself->hotw; x++)
	//				if (ObstructAt(x-check, ey-check)) return check-1;
	//			for (y=ey; y<ey+myself->hoth; y++)
	//				if (ObstructAt(ex-check, y-check)) return check-1;
	//			break;
	//		case SW:
	//			for (x=ex; x<ex+myself->hotw; x++)
	//				if (ObstructAt(x-check, ey+myself->hoth+check-1)) return check-1;
	//			for (y=ey; y<ey+myself->hoth; y++)
	//				if (ObstructAt(ex-check, y+check)) return check-1;
	//			break;
	//		case NE:
	//			for (x=ex; x<ex+myself->hotw; x++)
	//				if (ObstructAt(x+check, ey-check)) return check-1;
	//			for (y=ey; y<ey+myself->hoth; y++)
	//				if (ObstructAt(ex+myself->hotw+check-1, y-check)) return check-1;
	//			break;
	//		case SE:
	//			for (x=ex; x<ex+myself->hoth; x++)
	//				if (ObstructAt(x+check, ey+myself->hoth+check-1)) return check-1;
	//			for (y=ey; y<ey+myself->hoth; y++)
	//				if (ObstructAt(ex+myself->hotw+check-1, y+check)) return check-1;
	//			break;
	//	}
	//}
	//return max;
}

void onStep() {
	if( !_trigger_onStep.empty() ) {
		se->ExecuteFunctionString( _trigger_onStep );
	}
}

void afterStep() {
	if( !_trigger_afterStep.empty() ) {
		se->ExecuteFunctionString( _trigger_afterStep );
	}
}

void afterPlayerMove() {
	if( !_trigger_afterPlayerMove.empty() ) {
		se->ExecuteFunctionString( _trigger_afterPlayerMove );
	}
}

void beforeEntityActivation() {
	if( !_trigger_beforeEntityScript.empty() ) {
		se->ExecuteFunctionString( _trigger_beforeEntityScript );
	}
}

void afterEntityActivation() {
	if( !_trigger_afterEntityScript.empty() ) {
		se->ExecuteFunctionString( _trigger_afterEntityScript );
	}
}

void onEntityCollision() {
	if( isEntityCollisionCapturing() ) {
		se->ExecuteFunctionString( _trigger_onEntityCollide );
	}
}

void ProcessControls()
{
	UpdateControls();
    // No player movement can be done if there's no ready player, or if there's a script active.
	if( !myself || !myself->ready() || invc ) {
		return;
	}

	//if( myself->movecode == 3 ) {
	//	ScriptEngine::PlayerEntityMoveCleanup();
	//}

	// kill contradictory input
	if (up && down) up = down = false;
	if (left && right) left = right = false;

	// if we're not supposed to be using diagonals,
	// prevent that, too.
	// We keep track of the last direction we moved in
	// and if we have diagonal input, we move along the same
	// axis of movement as before the conflict (horiz or vert.)
	// - Jesse 22-10-05
	if(!playerdiagonals) {
		if((up || down) && (left || right)) {
			if(lastplayerdir == WEST || lastplayerdir == EAST)
				up = down = false;
			else
				left = right = false;
		} else {
			if(left)
				lastplayerdir = WEST;
			else if(right)
				lastplayerdir = EAST;
			else if(up)
				lastplayerdir = NORTH;
			else if(down)
				lastplayerdir = SOUTH;
			else
				lastplayerdir = 0;
		}
	}

	// check four cardinal directions last
	if (up)
	{
		myself->setface(NORTH);
		int dist = MaxPlayerMove(NORTH, playerstep);
		if (dist)
		{
			myself->set_waypoint_relative_player(0, -1*dist);
			return;
		}
	}
	if (down)
	{
		myself->setface(SOUTH);
		int dist = MaxPlayerMove(SOUTH, playerstep);
		if (dist)
		{
			myself->set_waypoint_relative_player(0, dist);
			return;
		}
	}
	if (left)
	{
		myself->setface(WEST);
		int dist = MaxPlayerMove(WEST, playerstep);
		if (dist)
		{
			myself->set_waypoint_relative_player(-1*dist, 0);
			return;
		}
	}
	if (right)
	{
		myself->setface(EAST);
		int dist = MaxPlayerMove(EAST, playerstep);
		if (dist)
		{
			myself->set_waypoint_relative_player(dist, 0);
			return;
		}
	}
	myself->continuous = false;

	// Check for entity/zone activation
	if( b1 )
	{
		int ex, ey;
		UnB1();
		switch (myself->face)
		{
			case NORTH:
				ex = myself->getx()+(myself->hotw/2);
				ey = myself->gety()-1;
				break;
			case SOUTH:
				ex = myself->getx()+(myself->hotw/2);
				ey = myself->gety()+myself->hoth+1;
				break;
			case WEST:
				ex = myself->getx()-1;
				ey = myself->gety()+(myself->hoth/2);
				break;
			case EAST:
				ex = myself->getx()+myself->hotw+1;
				ey = myself->gety()+(myself->hoth/2);
				break;
		}

		int i = EntityAt(ex, ey);
		if (i != -1 && entity[i]->script.length())
		{
			if (entity[i]->autoface && entity[i]->ready())
			{
				switch( myself->face )
				{
					case NORTH: entity[i]->setface(SOUTH); break;
					case SOUTH: entity[i]->setface(NORTH); break;
					case WEST: entity[i]->setface(EAST); break;
					case EAST: entity[i]->setface(WEST); break;
					default: err("ProcessControls() - uwahh? invalid myself->face parameter");
				}
			}
			event_tx = entity[i]->getx()/16;
			event_ty = entity[i]->gety()/16;
			event_entity = i;
			int cur_timer = timer;
			beforeEntityActivation();
			se->ExecuteFunctionString(entity[i]->script.c_str());
			afterEntityActivation();
			timer = cur_timer;
			return;
		}

		int cz = current_map->zone(ex/16, ey/16);
		if (cz && current_map->zones[cz]->method)
		{
			int cur_timer = timer;

			event_zone = cz;
			event_tx = ex/16;
			event_ty = ey/16;
			event_entity = i;

			if(current_map->zones[cz]->script[0])
				se->ExecuteFunctionString(current_map->zones[cz]->script);
			else if(current_map->zones[cz]->name[0])
				se->ExecuteFunctionString(current_map->zones[cz]->name);
			else
				se->ExecuteCallback(zonefunc,true);

			timer = cur_timer;
		}
	}
}

void MapScroller()
{
	inscroller = true;
	int oldx = xwin;
	int oldy = ywin;
	int oldtimer = timer;
	int oldvctimer = vctimer;
	int oldcamera = cameratracking;
	cameratracking = 0;
	lastpressed = 0;

	while (lastpressed != 41)
	{
		if (keys[SCAN_UP]) ywin--;
		if (keys[SCAN_DOWN]) ywin++;
		if (keys[SCAN_LEFT]) xwin--;
		if (keys[SCAN_RIGHT]) xwin++;
		UpdateControls();
		Render();
		ShowPage();
	}

	lastpressed = 0;
	keys[41] = 0;
    cameratracking = oldcamera;
	vctimer = oldvctimer;
	timer = oldtimer;
	ywin = oldy;
	xwin = oldx;
	inscroller = false;
}

void Render()
{
	if(current_map)
	{
		if( cheats && !inscroller && lastpressed == 41 )
			MapScroller();

		int rmap = (current_map->mapwidth * 16);
		int dmap = (current_map->mapheight * 16);


		switch (cameratracking)
		{
		case 0:
			if( xwin + 200 >= rmap )
				xwin = rmap - screen->width;
			if( ywin + 120 >= dmap )
				ywin = dmap - screen->height;
			if( xwin < 0 ) xwin = 0;
			if( ywin < 0 ) ywin = 0;
			break;
		case 1:
			if( myself )
			{
				//xwin = (myself->getx() + myself->hotw/2) - (screen->width / 2);
				//ywin = (myself->gety() + myself->hoth/2) - (screen->height / 2);
				xwin = (myself->getx() + myself->hotw/2) - (200 / 2);
				ywin = (myself->gety() + myself->hoth/2) - (120 / 2);
			}
			else xwin=0, ywin=0;
			if(cameraclamp)
			{
				if (xwin + 200 >= rmap)
					xwin = rmap - screen->width;
				if (ywin + 120 >= dmap)
					ywin = dmap - screen->height;
				if (xwin < 0) xwin = 0;
				if (ywin < 0) ywin = 0;
			}
			break;
		case 2:
			if (cameratracker>=entities || cameratracker<0)
			{
				xwin = 0;
				ywin = 0;
			}
			else
			{
				Entity* target = entity[cameratracker];
				xwin = (target->getx() + target->hotw/2) - (200 / 2);
				ywin = (target->gety() + target->hoth/2) - (120 / 2);
			}
			if (xwin + 200 >= rmap)
				xwin = rmap - screen->width;
			if (ywin + 120 >= dmap)
				ywin = dmap - screen->height;
			if (xwin < 0) xwin = 0;
			if (ywin < 0) ywin = 0;
			break;
		}
	}

	if(realRenderFunc.IsValid())
	{
		se->ExecuteCallback(realRenderFunc,true);
	}
	else
	{
		if(current_map)
			current_map->render(xwin, ywin, screen);
	}
}

void CheckZone()
{
	int cur_timer = timer;
	int cz = current_map->zone(px, py);
	// the follwing line is probably now correct, since .percent is in [0,255]
	// and so the max rnd() will produce is 254, which will still always trigger
	// if .percent is 255, and the lowest is 0, which will never trigger, even if
	// .percent is 0
	if (rnd(0,254) < current_map->zones[cz]->percent)
	{
		event_zone = cz;
		if(current_map->zones[cz]->script[0])
			se->ExecuteFunctionString(current_map->zones[cz]->script);
		else if(current_map->zones[cz]->name[0])
			se->ExecuteFunctionString(current_map->zones[cz]->name);
		else
			se->ExecuteCallback(zonefunc,true);
	}
	timer = cur_timer;
}

void TimedProcessEntities()
{
	if (entitiespaused)
		return;

	while (lastentitythink < systemtime)
	{
		lastentitythink++;
		if (done) break;
		if (myself)
		{
			//px = (myself->getx()+15) / 16;
			//py = (myself->gety()+15) / 16;
		}
		ProcessEntities();
		if (myself && !invc)
		{
			int tx = myself->getx()/16;
			int ty = myself->gety()/16;
			int px = myself->getx() & 15;
			int py = myself->gety() & 15;
			bool ignore_x = myself->last_tx==-1;
			bool ignore_y = myself->last_ty==-1;
			bool xd = myself->last_tx != tx;
			bool yd = myself->last_ty != ty;
			bool ignore = ignore_x && xd || ignore_y && yd;
			
			if((xd || yd) && px==0 && py == 0)
			//if ((px != (myself->getx()+15) / 16) || (py != (myself->gety()+15) / 16))
			{
			/*	px = (myself->getx()+15) / 16;
				py = (myself->gety()+15) / 16;
				event_tx = px;
				event_ty = py;*/
				::px = myself->last_tx = tx;
				::py = myself->last_ty = ty;
				
				if(!ignore)
				{
					//onStep();
					CheckZone();
					afterStep();
				}
			}
		}

		//mbg 27-dec-2011 - moved this down below the other code to keep one step from happening in 
		//the same frame as a zone event
		if (!invc) ProcessControls();
	}
	//UGH
	//if (entitiespaused)
	//	return;

	//while (lastentitythink < systemtime)
	//{
	//	if (done) break;
	//	if (myself)
	//	{
	//		px = (myself->getx()+15) / 16;
	//		py = (myself->gety()+15) / 16;
	//	}
	//	ProcessEntities();
	//	if (!invc) ProcessControls();
	//	if (myself && !invc)
	//	{
	//		if ((px != (myself->getx()+15) / 16) || (py != (myself->gety()+15) / 16))
	//		{
	//			px = (myself->getx()+15) / 16;
	//			py = (myself->gety()+15) / 16;
	//			
	//			event_tx = px;
	//			event_ty = py;

	//			onStep();
	//			CheckZone();
	//			afterStep();
	//		}
	//	}
	//	lastentitythink++;
	//}
}

void TimedProcessSprites()
{
	while (lastspritethink < systemtime)
	{
		for (int i = 0; i < sprites.size(); i++)
		{
			if (!sprites[i].image) continue;
			if (sprites[i].wait)
			{
				sprites[i].wait--;
				continue;
			}
			sprites[i].timer++;
			sprites[i].thinkctr++;
			if (sprites[i].thinkctr > sprites[i].thinkrate)
			{
				sprites[i].thinkctr = 0;
				event_sprite = i;
				se->ExecuteFunctionString(sprites[i].thinkproc.c_str());
			}
		}
		lastspritethink++;
	}
}

void Engine_Start(char *mapname)
{
	entities = 0;
	player = -1;
	myself = 0;
	xwin = ywin = 0;
	done = false;
	die = 0;
	current_map = new MAP(mapname);
	CleanupCHRs();
	timer = 0;

	lastentitythink = systemtime;
	lastspritethink = systemtime;
	while (!done)
	{
		UpdateControls();
		TimedProcessEntities();
		if (!die)
		{
			Render();
			ShowPage();
			Sleep(1);
		}
	}
	delete current_map;
	current_map = 0;
	die = 0;
}
