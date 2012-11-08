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
 * verge3: g_map.cpp                                              *
 * copyright (c) 2001 vecna                                       *
 ******************************************************************/

#include "xerxes.h"


/****************************** data ******************************/

#define MAP_VERSION				2

char maptag[] = { 'V','3','M','A','P','\0' };

/****************************** code ******************************/
Layer::Layer(FILE *f)
{
	lucent = 0; width = height = 0;
	fread(layername, 256,1,f);
	fread(&parallax_x, 8,1,f);
	fread(&parallax_y, 8,1,f);
	fread(&width, 2,1,f);
	fread(&height, 2,1,f);
	fread(&lucent, 1,1,f);

	tiledata = new word[width*height];
	cfread(tiledata, width*height*2,1,f);
    x_offset = y_offset = 0; // no offset until parallax has changed
}

Layer::Layer(int width, int height)
{
	// Default lucency: not lucent.
	lucent = 0;
	// Setup th' width 'n height.
	this->width = width;
	this->height = height;
	// Default parallax: no parallax.
	parallax_x = 1;
	parallax_y = 1;
	// Instantiate tile data.
	tiledata = new word[width*height];
	// Fill 'er up with tile 0.
	memset(&tiledata, 0, sizeof(tiledata));
	// no offset until parallax has changed
	x_offset = y_offset = 0;
}

Layer::~Layer()
{
	delete[] tiledata;
}

// Overkill (2006-07-20): Saver functions!
void Layer::save(FILE *f)
{
	fwrite(layername, 1, 256, f);
	fwrite(&parallax_x, 1, 8, f);
	fwrite(&parallax_y, 1, 8, f);
	fwrite(&width, 1, 2, f);
	fwrite(&height, 1, 2, f);
	fwrite(&lucent, 1, 1, f);

	cfwrite(tiledata, 1, width*height*2, f);
}

void Layer::SetParallaxX(double p)
{
    // increase the x_offset to the current layer pos given the current parallax
    x_offset += (int) ((float) xwin * parallax_x);

    // then reduce it by what the parallax will be
    x_offset -= (int) ((float) xwin * p);

    // then we can set the parallax
    parallax_x = p;

}

void Layer::SetParallaxY(double p)
{
    // increase the x_offset to the current layer pos given the current parallax
    y_offset += (int) ((float) ywin * parallax_y);

    // then reduce it by what the parallax will be
    y_offset -= (int) ((float) ywin * p);

    // then we can set the parallax
    parallax_y = p;
}

MAP::MAP() {
}

MAP::MAP(char *fname)
{
	std::string fixed_fname = normalize_path(fname);
	fixed_fname = "./" + fixed_fname;
	fname = (char*)fixed_fname.c_str();

	FILE *f = fopen(fname,"rb");
	if (!f)
		err("MAP::MAP() - could not load map %s", fname);

	strcpy(mapfname, fname);
	strlwr(mapfname);

	char signature[8];
	fread(signature, 6,1,f);
	if (strcmp(signature, maptag))
		err("MAP::MAP() - %s is not a valid mapfile!", fname);

	int version;
	fread(&version, 4,1,f);
	//if (version != MAP_VERSION) err("MAP::MAP() - %s is not the correct MAP format version!", fname);

	int vcoffset;
	fread(&vcoffset, 4,1,f); // skip vc offset

	//skip notes
	if(version>=3)
	{
		int numNotes;
		fread(&numNotes,4,1,f);
		for(int i=0;i<numNotes;i++)
		{
			int len;
			fseek(f,4,SEEK_CUR); fseek(f,4,SEEK_CUR); //skip tx and ty
			fread(&len,4,1,f);
			fseek(f,len,SEEK_CUR);
		}
	}

	fread(mapname, 256,1,f);
	fread(vspname, 256,1,f);
	strcpy(savevspname, vspname); // Overkill 2006-05-21
	fread(musicname, 256,1,f);
	fread(renderstring, 256,1,f);
	fread(startupscript, 256,1,f);

	PlayMusic(musicname);

	startx = starty = 0;
	fread(&startx, 2,1,f);
	fread(&starty, 2,1,f);

	std::string s = std::string(fname);
	int offs = s.rfind('\\');
	if (offs >= 0)
	{
		s.replace(offs+1,strlen(fname),vspname);
		tileset = new VSP((char *)s.c_str());
	}
	else
	{
		offs = s.rfind('/');
		if (offs >= 0)
		{
			s.replace(offs+1,strlen(fname),vspname);
			tileset = new VSP((char *)s.c_str());
		}
		else
			tileset = new VSP(vspname);
	}

	fread(&numlayers, 4,1,f);

	//layers = new Layer*[numlayers];
	layers.resize(numlayers);
	int i;
	for (i=0; i<numlayers; i++)
		layers[i] = new Layer(f);

	mapwidth = layers[0]->width;
	mapheight = layers[0]->height;

	obslayer = new byte[mapwidth*mapheight];
	zonelayer = new word[mapwidth*mapheight];
	cfread(obslayer, mapwidth*mapheight,1,f);
	cfread(zonelayer, mapwidth*mapheight*2,1,f);

	fread(&numzones, 4,1,f);
	zones.resize(numzones);
	for (i=0; i<numzones; i++)
	{
		zones[i] = new Zone;
		zones[i]->percent = zones[i]->method = zones[i]->delay = 0;
		fread(zones[i]->name, 256,1,f);
		fread(zones[i]->script, 256,1,f);
		fread(&zones[i]->percent, 1,1,f);
		fread(&zones[i]->delay, 1,1,f);
		fread(&zones[i]->flags, 1,1,f);
	}

	fread(&mapentities, 4,1,f);

	for (i=0; i<mapentities; i++)
	{
		int t=0, x1=0, y1=0, x2=0, y2=0;
		char movescript[256], chrname[256], script[256], description[256];

		fread(&x1, 2,1,f);
		fread(&y1, 2,1,f);
		int o1 = vtell(f);
		vseek(f, 22, 1);
		fread(movescript, 256,1,f);
		fread(chrname, 256,1,f);
		fread(description, 256,1,f); // this is actually the description which we dont care about
		fread(script, 256,1,f); // this is the actual script
		vseek(f, o1, 0);

		int i = AllocateEntity(x1*16, y1*16, chrname);

		entity[i]->description = description;
		entity[i]->script = script;
		fread(&t, 1,1,f);
		if (!t) t = SOUTH;
		entity[i]->setface(t); t=0;
		fread(&t, 1,1,f);
		entity[i]->obstructable = t ? true : false; t=0;
		fread(&t, 1,1,f);
		entity[i]->obstruction = t ? true : false; t=0;
		fread(&t, 1,1,f);
		entity[i]->autoface = (t!=0); t=0;
		fread(&t, 2,1,f);
		entity[i]->setspeed(t); t=0;
		fread(&t, 1,1,f);
		// activation mode FIXME
		fread(&t, 1,1,f);

		fread(&x1, 2,1,f);
		fread(&y1, 2,1,f);
		fread(&x2, 2,1,f);
		fread(&y2, 2,1,f);
		switch(t)
		{
			case 0: entity[i]->SetMotionless(); break;
			case 1: entity[i]->SetWanderZone(); break;
			case 2: entity[i]->SetWanderBox(x1, y1, x2, y2); break; //FIXME
			case 3: entity[i]->SetMoveScript(movescript); break;
		}
		t=0; fread(&t, 2,1,f);
		entity[i]->SetWanderDelay(t);
		fread(&t, 4,1,f);
		vseek(f, 1024, 1);
		/*
		fread(&tlen, 4,1,f);
		fread(ename, tlen+1,1,f);
		// chr filename
		fread(&tlen, 4,1,f);
		fread(chrfn, tlen+1,1,f);
		// script
		fread(&tlen, 4,1,f);
		fread(escript, tlen+1,1,f);
		// movescript
		fread(&tlen, 4,1,f);
		fread(emovescript, tlen+1,1,f);

		fread(&tlen, 4,1,f);
		fread(&tlen, 4,1,f);
		int eface, speed, tx, ty;
		fread(&eface, 4,1,f);
		fread(&speed, 4,1,f);
		fread(&tx, 4,1,f);
		fread(&ty, 4,1,f);

		fread(&x1, 4,1,f);
		fread(&y1, 4,1,f);
		fread(&x2, 4,1,f);
		fread(&y2, 4,1,f);

		int idx = AllocateEntity(tx*16, ty*16, chrfn);
		entity[idx]->setface(eface);
		entity[idx]->setspeed(speed);
		entity[idx]->script = escript;
		entity[idx]->autoface = (eflags & ENT_AUTOFACE) ? true : false;
		entity[idx]->obstructable = (eflags & ENT_OBSTRUCTED) ? true : false;
		entity[idx]->obstruction = (eflags & ENT_OBSTRUCTS) ? true : false;

		if (method == ENT_MOVESCRIPT) entity[idx]->SetMoveScript(emovescript);
		if (method == ENT_WANDERZONE) entity[idx]->SetWanderZone();
		if (method == ENT_WANDERBOX) entity[idx]->SetWanderBox(x1, y1, x2, y2);*/
	}
	
	fclose(f);
	current_map = this;

//	std::string tmpstr = mapfname;
//#ifdef __WIN32__
//	boost::algorithm::replace_all(tmpstr, "/", "\\");
//#endif
//	FILE* vcf = fopen(tmpstr.c_str(),"rb");

	{
		std::string script_id = fname;
		boost::algorithm::to_lower(script_id);
		boost::algorithm::replace_all(script_id, "\\", "/");
		EMUFILE_MEMORY memf(&g_ScriptDatabase.scripts[script_id]->data);
		se->LoadMapScript(&memf, mapfname);
		se->ExecuteFunctionString(startupscript);
	}
}

MAP::~MAP()
{
	delete tileset;
	int i;
	for (i=0; i<numlayers; i++)
		delete layers[i];
	//delete[] layers;
	//delete[] zones;
	delete[] obslayer;
	delete[] zonelayer;

	for (i=0; i<entities; i++)
	{
		delete entity[i];
		entity[i]=0;
	}
	entities = 0;
}

// Overkill (2006-07-20): Saver functions!
void MAP::save(FILE *f)
{
	int i;
	char tempfn[16] = "$$v3map.tmp";
	FILE *out = fopen(tempfn, "wb");
	if (!out) err("Unable to open %s for writing!", tempfn);
	// Map signature.
	char signature[8];
	strcpy(signature, maptag);
	fwrite(&signature, 1, 6, f);

	// Version
	int version = MAP_VERSION;
	fwrite(&version, 1, 4, f);

	// USE OUTPUT BUFFER FROM THIS POINT ON.
	fwrite(mapname, 1, 256, out);
	//log("mapname: %s", mapname);
	// savevspname, not vspname because it's what the vsp file WILL be,
	// not necessarily what it IS at that moment.
	// So, you can set it then FileSaveVSP() down the line.
	fwrite(savevspname, 1, 256, out);
	//log("savevspname: %s", savevspname);
	fwrite(musicname, 1, 256, out);
	//log("musicname: %s", musicname);
	fwrite(renderstring, 1, 256, out);
	//log("renderstring: %s", renderstring);
	fwrite(startupscript, 1, 256, out);
	//log("startupscript: %s", startupscript);
	fwrite(&startx, 1, 2, out);
	fwrite(&starty, 1, 2, out);

	fwrite(&numlayers, 1, 4, out);
	for (i = 0; i < numlayers; i++)
	{
		layers[i]->save(out);
	}

	// Obs.
	cfwrite(obslayer, 1, mapwidth*mapheight, out);
	// Zones
	cfwrite(zonelayer, 1, mapwidth*mapheight*2, out);

	fwrite(&numzones, 1, 4, out);
	for (i=0; i<numzones; i++)
	{
		fwrite(zones[i]->name, 1, 256, out);
		fwrite(zones[i]->script, 1, 256, out);
		fwrite(&zones[i]->percent, 1, 1, out);
		fwrite(&zones[i]->delay, 1, 1, out);
		fwrite(&zones[i]->flags, 1, 1, out);
	}

	// ENTITIES! <3.
	fwrite(&entities, 1, 4, out);
	for (i=0; i<entities; i++)
	{
		//log("ENT %d of %d", i, entities);
		// Tile coords.
		int x = entity[i]->x / 16;
		int y = entity[i]->y / 16;
		fwrite(&x, 1, 2, out);
		fwrite(&y, 1, 2, out);
		fwrite(&entity[i]->face, 1, 1, out);
		fwrite(&entity[i]->obstructable, 1, 1, out);
		fwrite(&entity[i]->obstruction, 1, 1, out);
		fwrite(&entity[i]->autoface, 1, 1, out);
		fwrite(&entity[i]->speed, 1, 2, out);
		//log("HERE?");
		// FIXME: ACTIVATION MODE
		int activationMode = 0;
		fwrite(&activationMode, 1, 1, out);
		// Movecode and wandering.
		fwrite(&entity[i]->movecode, 1, 1, out);
		fwrite(&entity[i]->wx1, 1, 2, out);
		fwrite(&entity[i]->wy1, 1, 2, out);
		fwrite(&entity[i]->wx2, 1, 2, out);
		fwrite(&entity[i]->wy2, 1, 2, out);
		fwrite(&entity[i]->wdelay, 1, 2, out);
		//log("YAR?");
		// Expand! whatever, man.
		int expand = 0;
		fwrite(&expand, 1, 4, out);

		// h8 u STD::string.
		char tempchar[256];

		//log("movestr: %s", entity[i]->movestr);
		fwrite(entity[i]->movestr, 1, 256, out);
		strcpy(tempchar, entity[i]->chr->name.c_str());
		fwrite(tempchar, 1, 256, out);
		strcpy(tempchar, entity[i]->description.c_str());
		fwrite(tempchar, 1, 256, out); // description
		strcpy(tempchar, entity[i]->script.c_str());
		fwrite(tempchar, 1, 256, out); // The actual script position.
	}
	// NO LONGER NEED TO USE THE OUTPUT BUFFER
	fclose(out);
	
	// Why vopen instead? I... don't know!
	// Now that we've finished the buffer though, we can
	// use this to fill in the unknown map information.
	
	FILE *tmp = fopen(tempfn,"rb");
	if (!tmp) err("Could not open %s for reading!", tempfn);
	int bufferlength = filesize(tmp);
	byte* buf = new byte[bufferlength];
	vread(buf, bufferlength, tmp);
	fclose(tmp);
	// Delete the temporary buffer, we've gotten what we wanted.
	remove(tempfn);

	// Alright... finally, all this to get the offset for the VC stuffs.
	int vcofs = 14 + bufferlength;
	fwrite(&vcofs, 1, 4, f);
	// Write the buffer into the map now.
	fwrite(buf, 1, bufferlength, f);
	// VC funcs are waaay down here.
	int vcfuncs = 0; // number compiled should be none.
	//log("VCOFS: %d CURRENT POS: %d", vcofs, ftell(f));
	fwrite(&vcfuncs, 1, 4, f);
}


// Overkill (2006-07-20): Add a layer.
int MAP::addLayer(int width, int height)
{
	layers.push_back(new Layer(width, height));
	numlayers++;
	return numlayers - 1;
}

void MAP::SetZone(int x, int y, int t)
{
	if (x<0 || y<0 || x>=mapwidth || y>=mapheight) return;
	if (t>=numzones) return;
	zonelayer[(y*mapwidth)+x] = t;
}

void MAP::SetObs(int x, int y, int t)
{
	if (x<0 || y<0 || x>=mapwidth || y>=mapheight) return;
	if (t>=tileset->numobs) return;
	obslayer[(y*mapwidth)+x] = t;
}

template<bool TRANSPARENT>
void MAP::BlitLayer(int l, int tx, int ty, int xwin, int ywin, image *dest)
{
	Layer &layer = *layers[l];

	// we add offsets here because if the parallax changes while the
    // xwin and ywin are non-zero, we would jump unless we compensate
	int oxw = layer.x_offset + (int) ((float) xwin * layer.parallax_x);
	int oyw = layer.y_offset + (int) ((float) ywin * layer.parallax_y);
	int xofs = -(oxw & 15);
	int yofs = -(oyw & 15);
	int xtc = oxw >> 4;
	int ytc = oyw >> 4;

	if(TRANSPARENT)
		if (layer.lucent)
			SetLucent(layer.lucent);

	//tileset->UpdateAnimations();

    for (int y=0; y<ty; y++)
	{
		for (int x=0; x<tx; x++)
		{
			int c = layer.GetTile(xtc+x,ytc+y);
			if(!cameraclamp)
			{
				int x1 = xtc+x;
				int y1 = ytc+y;
				if(x1<0||y1<0||x1>=layer.width||y1>=layer.height)
				{
					if(TRANSPARENT)
						continue;
					else
						c=1;
				}
			}
			if(TRANSPARENT) {
				if(c)
					tileset->TBlit((x*16)+xofs, (y*16)+yofs, c, dest);
			}
			else
			{
				tileset->Blit((x*16)+xofs, (y*16)+yofs, c, dest);
			}
		}
	}
	if (dest == screen)
	{
		RenderLayerSprites(l);
	}
	
	if(TRANSPARENT)
		SetLucent(0);
}

void MAP::BlitObs(int tx, int ty, int xwin, int ywin, image *dest)
{
	int i, j, c;
	int oxw, oyw, xofs, yofs, xtc, ytc;

	oxw = xwin;
	oyw = ywin;
	xofs =- (oxw & 15);
	yofs =- (oyw & 15);
	xtc = oxw >> 4;
	ytc = oyw >> 4;

	SetLucent(50);
	for (i = 0; i < ty; i++)
		for (j = 0; j< tx; j++)
		{
			c = obslayer[(((ytc + i) * mapwidth) + xtc + j)];
			if (c) tileset->BlitObs((j * 16) + xofs, (i * 16) + yofs, c, dest);
		}
	SetLucent(0);
}
/*
void MAP::render(int x, int y, image *dest)
{
	char *src;
	int tx, ty;

//	curlayer = 0;
	src = renderstring;
	tx = (dest -> width / 16) + 2 ;
	ty = (dest -> height / 16) + 2;

	while (*src)
	{
		switch (*src)
		{
			case '1': BlitLayer(0, tx, ty, x, y, dest); break;
			case '2': TBlitLayer(1, tx, ty, x, y, dest); break;
			case '3': TBlitLayer(2, tx, ty, x, y, dest); break;
			case '4': TBlitLayer(3, tx, ty, x, y, dest); break;
			case '5': TBlitLayer(4, tx, ty, x, y, dest); break;
			case '6': TBlitLayer(5, tx, ty, x, y, dest); break;
			case 'E': RenderEntities();
					  break;
			case 'R': HookRetrace();
					  break;
	    }
		src++;
	}
//	BlitObs(tx, ty, x, y, dest);
	if (obszone)
	{
		BlitObs(dest, tx, ty, x, y);
		BlitZone(dest, tx, ty, x, y);
	}
}*/


void MAP::render(int x, int y, image *dest)
{
	bool first = true;
	int tx = (dest -> width / 16) + 2;
	int ty = (dest -> height / 16) + 2;
	char *src = renderstring;
	char token[10];

	while (*src)
	{
		int n=0;
		while (*src != 0 && *src !=',')
			token[n++] = *src++;
		if (*src == ',') src++;
		token[n] = 0;

		if (token[0] == 'E')
		{
			rtarget = dest;
			RenderEntities();
			rtarget = screen;
			continue;
		}
		if (token[0] == 'R')
		{
			HookRetrace();
			continue;
		}
		int layer = atoi(token)-1;
		if (first)
		{
			BlitLayer<false>(layer, tx, ty, x, y, dest);
			first = false;
			continue;
		}
		BlitLayer<true>(layer, tx, ty, x, y, dest);
	}
}
