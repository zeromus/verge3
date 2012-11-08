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
 * verge3: g_entity.cpp                                           *
 * copyright (c) 2002 vecna                                       *
 ******************************************************************/

#include "xerxes.h"

/****************************** data ******************************/

/****************************** code ******************************/

Entity::Entity(int x1, int y1, const char *chrfn)
{
	flags = 0;
	sortkey = 0;
	follower = 0;
	follow = 0;
	delay = 0;
	lucent = 0;
	wdelay = 75;
	setxy(x1, y1);
	setspeed(256);
	speedct = 0;
	chr = RequestCHR(chrfn);
	hotw = chr->hw;
	hoth = chr->hh;
	visible = true;
	active = true;
	specframe = 0;
	custanim = -1;
	movecode = 0;
	moveofs = 0;
	foot = 0;
	continuous = false;
	framect = 0;
	frame = 0;
	face = SOUTH;
	hookrender = empty_string;
	script = empty_string;
	description = empty_string;
	memset(movestr, 0, 256);
	obstructable = 0;
	obstruction = 0;
	last_tx = -1;
	last_ty = -1;
	for (int i=0; i<FOLLOWDISTANCE; i++)
	{
		pathx[i] = x;
		pathy[i] = y;
		pathf[i] = SOUTH;
	}
	facePending = false;
}

Entity::~Entity() { } // dont delete chr anymore due to chr cacher/garbage collection
int Entity::getx() { return x / 65536; }
int Entity::gety() { return y / 65536; }

void Entity::setface(int d) { 
  // bounds-check this since it's easy to set an invalid value in vc (janus)
  if ((d > 0) && (d <= 4)) {
    face = d; 
  } else {
    err("Entity::setface(%d), facing values must be within the range [1, 4]", d);
  }
}

void Entity::warp(int x1, int y1)
{
	setxy(x1,y1);
	//suppress zones
	last_tx = x1/16;
	last_ty = y1/16;
}

void Entity::setxy(int x1, int y1)
{
	x = x1 * 65536;
	y = y1 * 65536;
	if (follower) follower->setxy(x1, y1);
	set_waypoint(x1, y1);
	for (int i=0; i<FOLLOWDISTANCE; i++)
		pathx[i] = x,
		pathy[i] = y,
		pathf[i] = SOUTH;
}

int Entity::getspeed() { return speed; }
void Entity::setspeed(int s)
{
	speed = s;
    // We don't reset the speedct here, because
    // 1) Is is keeping track of distance already moved but not acted on
    //    (ie any partial movement made but not turned into a tick)
    // 2) If we reset speedct, setting the speed frequently will slow
    //    the character down by discarding the partial bits

	if (follower) follower->setspeed(s);
}

int Entity::get_waypointx() { return waypointx; }
int Entity::get_waypointy() { return waypointy; }

void Entity::set_waypoint(int x1, int y1)
{
	waypointx = x1;
	waypointy = y1;

	switch (sgn(y1-gety()))
	{
		case -1: face = NORTH; break;
		case 0:  break;
		case 1:  face = SOUTH; break;
	}
	switch (sgn(x1-getx()))
	{
		case -1: face = WEST; break;
		case 0:  break;
		case 1:  face = EAST; break;
	}
}

void onStep();
void Entity::set_waypoint_relative_player(int x1, int y1, bool changeface)
{
	//if((flags&EFLAG_PCANIM) && !foot && !continuous)
	//{
	//	framect = 16;
	//}
	set_waypoint_relative(x1, y1, changeface);
	event_tx = waypointx/16;
	event_ty = waypointy/16;
	event_entity = myself->index;
	event_zone = current_map->zone(event_tx,event_ty);
	foot ^= 1;
	myself->continuous = true;
	onStep();
}

void Entity::set_waypoint_relative(int x1, int y1, bool changeface)
{
	waypointx += x1;
	waypointy += y1;
	if(changeface) setFaceFromDxDy(x1,y1);
}

void Entity::setFaceFromDxDy(int dx, int dy)
{
	switch (sgn(dy))
	{
		case -1: face = NORTH; break;
		case 0:  break;
		case 1:  face = SOUTH; break;
	}
	switch (sgn(dx))
	{
		case -1: face = WEST; break;
		case 0:  break;
		case 1:  face = EAST; break;
	}
}

bool Entity::ready() { 
	
	return (x/65536 == waypointx && y/65536 == waypointy); 
}

bool Entity::leaderidle(){

	if (follow) return follow->leaderidle();
	return (x/65536 == waypointx && y/65536 == waypointy);
}

// called to sync up with leader's frame
// of course, if the two people have different-
// length walk cycles, they might have the same framect,
// but they won't sync visuall, which is OK
int Entity::get_leader_framect()
{
    if(follow) return follow->get_leader_framect();
    return framect;
}

void Entity::set_framect_follow(int f)
{
    if(follower) {
        follower->set_framect_follow(f);
    }
    framect = f;
}

void Entity::stalk(Entity *e)
{
	follow = e;
	e->follower = this;
	for (int i=0; i<FOLLOWDISTANCE; i++)
		pathx[i] = follow->pathx[FOLLOWDISTANCE-1],
		pathy[i] = follow->pathy[FOLLOWDISTANCE-1],
		pathf[i] = SOUTH;
	x = follow->pathx[FOLLOWDISTANCE-1];
	y = follow->pathy[FOLLOWDISTANCE-1];
	set_waypoint(x/65536, y/65536);
    movecode = 0;
	obstruction = false;
	obstructable = false;
    // clear delay info from wandering
    delay = 0;
    // sync our (and followers') framect with the leader
    set_framect_follow(get_leader_framect());
}

// This is called when we are going to change
// to a kind of movement that isn't stalking to
// ensure we are not trying to stalk at the same time
void Entity::clear_stalk()
{
    if(follow) {
        follow->follower = 0;
        follow = 0;
    }
}

void Entity::move_tick()
{
	int dx = waypointx - getx();
	int dy = waypointy - gety();

	if(facePending)
	{
		setFaceFromDxDy(dx,dy);
		facePending = false;
	}

	if (this != myself && !follow && obstructable)
	{
		// do obstruction checking */

		switch (face)
		{
			case NORTH: if (ObstructDirTick(NORTH)) return; break;
			case SOUTH: if (ObstructDirTick(SOUTH)) return; break;
			case WEST: if (ObstructDirTick(WEST)) return; break;
			case EAST: if (ObstructDirTick(EAST)) return; break;
			default: err("Entity::move_tick() - bad face value!!");
		}
	}
	framect++;

	// update pathxy for following
	for (int i=FOLLOWDISTANCE-2; i>=0; i--)
		pathx[i+1] = pathx[i],
		pathy[i+1] = pathy[i],
		pathf[i+1] = pathf[i];
	pathx[0] = x;
	pathy[0] = y;
	pathf[0] = face;

	// if following, grab new position from leader
    // We now keep track of our own framect, (rather
    // than using the leader's framect)
    // which is synced with the leader in stalk(),
    // but then runs free after that so animations
    // of different lengths are ok in a stalking chain.
	if (follow)
	{
		x = follow->pathx[FOLLOWDISTANCE-1];
		y = follow->pathy[FOLLOWDISTANCE-1];
		face = follow->pathf[FOLLOWDISTANCE-1];
		set_waypoint(x/65536, y/65536);
		if (follower)
			follower->move_tick();
		return;
	}

	// else move
	//mbg 21-jan-2012 - changed to add * speed / 256 to delink move speed from anim speed
	if (dx)
		x += sgn(dx) * 65536 * speed / 256;

	if (dy)
		y += sgn(dy) * 65536 * speed / 256;

/*
	if (dx &&!dy)
		x += sgn(dx) * 65536;
	if (!dx && dy)
		y += sgn(dy) * 65536;

	if (dx && dy)
	{
		x += sgn(dx) * 46340;
		y += sgn(dy) * 46340;
	}
*/
	if (follower)
		follower->move_tick();
}

static bool stop_point(int framect)
{
	return (framect%12!=9 &&framect%12!=0);
	//return (framect%12!=0);
}

void Entity::think()
{
	if (!active) return;

	bool postAnim = ready() && (flags&EFLAG_PCANIM) && stop_point(framect);
	bool ticktime = delay>systemtime;

	if (ticktime || postAnim)
	{
		if(custanim != -1 || postAnim)
			framect++;
		else framect = 0;
		if(ticktime) return;
	}

	//speedct += speed;
	speedct += 256; //mbg 21-jan-2012 for now, anim speed is always 1x. this should not be related to their move speed
	int num_ticks = speedct / 256;
	speedct -= num_ticks*256;

	while (num_ticks)
	{
		num_ticks--;

		if (ready())
		{
			switch (movecode)
			{
			case 0: if (this == myself && !invc) ProcessControls(); break;
			case 1: case 4: do_wanderzone(); break;
			case 2: do_wanderbox(); break;
			case 3: do_movescript(); break;
			default: err("Entity::think(), unknown movecode value");
			}
		}

		//mbg 06-jan-2012 - added delay <= systemtime to fix a glitch with wandering entities where they would move a pixel in their next direction
		//wandering still sucks the big donkey dick (the direction is decided and if you get in the way then the npc will be stuck)
		if (!ready() && delay <= systemtime)
			move_tick();
	}
}

int ignoreEntCollisionId = -1;
bool Entity::ObstructDirTick(int d)
{
	__grue_actor_index = this->index;

	int x, y;
	int ex = getx();
	int ey = gety();

	if (!obstructable) return false;

	ignoreEntCollisionId = index;
	switch (d)
	{
		case NORTH:
			for (x=ex; x<ex+hotw; x++)
				if (ObstructAt(x, ey-1)) { ignoreEntCollisionId=-1; return true; }
			break;
		case SOUTH:
			for (x=ex; x<ex+hotw; x++)
				if (ObstructAt(x, ey+hoth)) { ignoreEntCollisionId=-1;  return true; }
			break;
		case WEST:
			for (y=ey; y<ey+hoth; y++)
				if (ObstructAt(ex-1, y)) { ignoreEntCollisionId=-1;  return true; }
			break;
		case EAST:
			for (y=ey; y<ey+hoth; y++)
				if (ObstructAt(ex+hotw, y))  { ignoreEntCollisionId=-1; return true; }
			break;
	}
	ignoreEntCollisionId=-1;
	return false;
}

bool Entity::ObstructDir(int d)
{
	__grue_actor_index = this->index;

	int i, x, y;
	int ex = getx();
	int ey = gety();

	if (!obstructable) return false;
	switch (d)
	{
		case NORTH:
			for (i=0; i<hoth; i++)
				for (x=ex; x<ex+hotw; x++)
					if (ObstructAt(x, ey-i-1)) return true;
			break;
		case SOUTH:
			for (i=0; i<hoth; i++)
				for (x=ex; x<ex+hotw; x++)
					if (ObstructAt(x, ey+i+hoth)) return true;
			break;
		case WEST:
			for (i=0; i<hotw; i++)
				for (y=ey; y<ey+hoth; y++)
					if (ObstructAt(ex-i-1, y)) return true;
			break;
		case EAST:
			for (i=0; i<hotw; i++)
				for (y=ey; y<ey+hoth; y++)
					if (ObstructAt(ex+hotw+i, y)) return true;
			break;
	}
	return false;
}

void Entity::do_wanderzone()
{
	bool ub=false, db=false, lb=false, rb=false;
	int ex = getx()/16;
	int ey = gety()/16;
	int myzone = current_map->zone(ex, ey);

	//movecode 1 - wanderzone
	//movecode 4 - antiwanderzone
	if (ObstructDir(EAST)  || (movecode==1 && current_map->zone(ex+1, ey) != myzone) || (movecode==4 && current_map->zone(ex+1, ey) == myzone)) rb=true;
	if (ObstructDir(WEST)  || (movecode==1 && current_map->zone(ex-1, ey) != myzone) || (movecode==4 && current_map->zone(ex-1, ey) == myzone)) lb=true;
	if (ObstructDir(SOUTH) || (movecode==1 && current_map->zone(ex, ey+1) != myzone) || (movecode==4 && current_map->zone(ex, ey+1) == myzone)) db=true;
	if (ObstructDir(NORTH) || (movecode==1 && current_map->zone(ex, ey-1) != myzone) || (movecode==4 && current_map->zone(ex, ey-1) == myzone)) ub=true;

	if (rb && lb && db && ub) return; // Can't move in any direction

	delay = systemtime + wdelay;
	while (1)
	{
		int i = rnd(0,3);
		switch (i)
		{
			case 0:
				if (rb) break;
				set_waypoint_relative(16, 0, false);
				facePending = true;
				return;
			case 1:
				if (lb) break;
				set_waypoint_relative(-16, 0, false);
				facePending = true;
				return;
			case 2:
				if (db) break;
				set_waypoint_relative(0, 16, false);
				facePending = true;
				return;
			case 3:
				if (ub) break;
				set_waypoint_relative(0, -16, false);
				facePending = true;
				return;
		}
	}

}

void Entity::do_wanderbox()
{
	bool ub=false, db=false, lb=false, rb=false;
	int ex = getx()/16;
	int ey = gety()/16;

	if (ObstructDir(EAST) || ex+1 > wx2) rb=true;
	if (ObstructDir(WEST) || ex-1 < wx1) lb=true;
	if (ObstructDir(SOUTH) || ey+1 > wy2) db=true;
	if (ObstructDir(NORTH) || ey-1 < wy1) ub=true;

	if (rb && lb && db && ub) return; // Can't move in any direction

	delay = systemtime + wdelay;
	while (1)
	{
		int i = rnd(0,3);
		switch (i)
		{
			case 0:
				if (rb) break;
				set_waypoint_relative(16, 0);
				return;
			case 1:
				if (lb) break;
				set_waypoint_relative(-16, 0);
				return;
			case 2:
				if (db) break;
				set_waypoint_relative(0, 16);
				return;
			case 3:
				if (ub) break;
				set_waypoint_relative(0, -16);
				return;
		}
	}
}

void Entity::do_movescript()
{
	static char vc2me[] = { 2, 1, 3, 4 };
	int arg;

	// movements factors
	// These are set to -1,0 or 1 to signify in
	// which directions movement should occur
	int vertfac = 0, horizfac = 0;


    // reset to tile-based at the start of a movestring
    if(moveofs == 0) {
        movemult = 16;
    }

	while ((movestr[moveofs] >= '0' && movestr[moveofs] <= '9') || movestr[moveofs] == ' ' || movestr[moveofs] == '-')
		moveofs++;

REPEAT:
	int done = 0;
	int found_move = 0; // number of LRUD letters we found
	while(!done && found_move < 2) {
		switch(toupper(movestr[moveofs]))
		{
			case 'C':
				//moveofs++;
				done = 1;
				break;

			case 'L':
				if(!found_move && face != WEST) setface(WEST);
				moveofs++;
				horizfac = -1;
				found_move++;
				break;
			case 'R':
				if(!found_move && face != EAST) setface(EAST);
				moveofs++;
				horizfac = 1;
				found_move++;
				break;
			case 'U':
				if(!found_move && face != NORTH) setface(NORTH);
				moveofs++;
				vertfac = -1;
				found_move++;
				break;
			case 'D':
				if(!found_move && face != SOUTH) setface(SOUTH);
				moveofs++;
				vertfac = 1;
				found_move++;
				break;
			default:
				done = 1;
		}
	}

	if(found_move) {
		custanim = -1;
		arg = atoi(&movestr[moveofs]);
		// we've already set facing, don't do it again
		set_waypoint_relative(horizfac*arg*movemult, vertfac*arg*movemult, false);
	} else {
NEXT:
		// no directions, check other possible letters:
		char code = toupper(movestr[moveofs]);
		switch(code) {
			case 'S': 
				moveofs++;
				setspeed(atoi(&movestr[moveofs]));
				break;
			case 'W': 
				moveofs++;
				delay = systemtime + atoi(&movestr[moveofs]);
				break;
			case 'F': 
				moveofs++;
				setface(vc2me[atoi(&movestr[moveofs])]);
				custanim = -1;
				break;
			case 'B': 
				moveofs = 0; 
				custanim = -1;
				goto REPEAT;
			case 'X': 
				moveofs++;
				arg = atoi(&movestr[moveofs]);
				set_waypoint(arg*16, gety());
				custanim = -1;
				break;
			case 'Y': 
				moveofs++;
				arg = atoi(&movestr[moveofs]);
				set_waypoint(getx(), arg*16);
				custanim = -1;
				break;
			case 'Z': 
				moveofs++;
				specframe = atoi(&movestr[moveofs]);
				custanim = -1;
				break;
			case 'P': 
				movemult = 1;
				moveofs++;
				break;
			case 'C':
			case 'A':
				{
					int temp = atoi(&movestr[moveofs+1]);
					if(code == 'C') temp += 9; //custom
					if(temp != custanim) { /*reset anim*/ }
					custanim = temp;
				}
				moveofs+=2;
				goto NEXT;
			case 'T': 
				movemult = 16;
				moveofs++;
				break;
			case 'H':
			case 0:  
				movemult = 0; moveofs = 0; movecode = 0; framect = 0; 
				//switch back to player control if we just ended a custom movescript and we're the player (not sure if we like this though)
				//if(myself == this) movecode = 0;
				return;
			default: err("Entity::do_movescript(), unidentify movescript command");
		}
	}
}

void Entity::set_chr(CStringRef fname)
{
    chr = RequestCHR(fname.c_str());
	specframe = 0;
	framect = 0;
	frame = 0;
	custanim = -1;
}

void Entity::draw()
{
	if (!visible) return;

	bool idle = ready();
	if(idle && (flags&EFLAG_PCANIM) && stop_point(framect))
			idle = false;

    // if we're idle, reset the framect
	if ((!follow && idle) || (follow && leaderidle()))
	{
		if(custanim == -1)
			framect = 0;
	}

	if (specframe)
		frame = specframe;
	else if(custanim != -1)
		frame = chr->GetFrame(custanim, framect);
	else
	{
		if (!follow)
		{
			if (idle) frame = chr->idle[face];
			else
			{

					frame = chr->GetFrame(face, framect);
			}
		}
		else
		{
			if (leaderidle()) frame = chr->idle[face];
			else frame = chr->GetFrame(face, framect);
		}
	}

	int zx = getx() - xwin;
	int zy = gety() - ywin;

	if (hookrender.length())
	{
		event_entity = index;
		se->ExecuteFunctionString(hookrender.c_str());
		return;
	}

	if (chr)
	{
		if(flags&EFLAG_YBUMP)
			zy -= 2;
		chr->render(zx, zy, frame, rtarget);
	}
	else
		DrawRect(zx, zy, zx + 15, zy + 15, MakeColor(255,255,255), screen);
}

void Entity::SetWanderZone()
{
	clear_stalk();
	set_waypoint(getx(), gety());
	movecode = 1;
}

void Entity::SetWanderBox(int x1, int y1, int x2, int y2)
{
	clear_stalk();
	set_waypoint(getx(), gety());
	wx1 = x1;
	wy1 = y1;
	wx2 = x2;
	wy2 = y2;
	movecode = 2;
}

void Entity::SetMoveScript(const char *s)
{
	clear_stalk();
	set_waypoint(getx(), gety());
	strcpy(movestr, s);
	moveofs = 0;
	movecode = 3;
	delay = 0;
}

void Entity::SetWanderDelay(int n)
{
	wdelay = n;
}

void Entity::SetMotionless()
{
	clear_stalk();
	set_waypoint(getx(), gety());
	movecode = 0;
	delay = 0;
	//mbg 04-dec-2012 - added this to make animations reset when the entity is stopped.
	//this was for the overworld arrows
	//was this a good idea?
	framect = 0;
	frame = 0;
}
