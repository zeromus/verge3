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
 * verge3: g_startup.cpp                                          *
 * copyright (c) 2001 vecna                                       *
 ******************************************************************/

#include "xerxes.h"
#include "opcodes.h"

#include <memory>
#include <functional>

/****************************** data ******************************/

int v3_xres=320, v3_yres=240, v3_bpp;
int v3_window_xres=0, v3_window_yres = 0;
// Overkill (2010-04-29): Aspect ratio enforcing.
ScaleFormat v3_scale_win = SCALE_FORMAT_ASPECT, v3_scale_full = SCALE_FORMAT_STRETCH;

bool windowmode=true;
bool sound=true;
bool cheats=false;
char mapname[255];
bool releasemode=false;
bool automax = true;
bool decompile = false;
bool editcode = false;
int gamerate = 100;
int soundengine = 0;
bool use_lua = false;
bool vc_oldstring = false;

VCCore *vc;

#ifdef ALLOW_SCRIPT_COMPILATION
VCCompiler *vcc = 0;
#endif

/****************************** code ******************************/

void LoadConfig()
{
	cfg_Init("verge.cfg");
	cfg_SetDefaultKeyValue("startmap", "");

	if (cfg_KeyPresent("xres"))
		v3_xres = cfg_GetIntKeyValue("xres");
	if (cfg_KeyPresent("yres"))
		v3_yres = cfg_GetIntKeyValue("yres");
    // Overkill (2010-04-29): Scaling policies.
    if (cfg_KeyPresent("scalewin"))
    {
        int value = cfg_GetIntKeyValue("scalewin");
        if(value >= 0 && value < SCALE_FORMAT_COUNT)
        {
            v3_scale_win = (ScaleFormat) value;
        }
    }
    if (cfg_KeyPresent("scalefull"))
    {
        int value = cfg_GetIntKeyValue("scalefull");
        if(value >= 0 && value < SCALE_FORMAT_COUNT)
        {
            v3_scale_full = (ScaleFormat) value;
        }
    }
	if (cfg_KeyPresent("bpp"))
		v3_bpp = cfg_GetIntKeyValue("bpp");

	if (cfg_KeyPresent("window_x_res"))
		v3_window_xres = cfg_GetIntKeyValue("window_x_res");
	if (cfg_KeyPresent("window_y_res"))
		v3_window_yres = cfg_GetIntKeyValue("window_y_res");

	if (cfg_KeyPresent("windowmode"))
		windowmode = cfg_GetIntKeyValue("windowmode") ? true : false;
	if (cfg_KeyPresent("nosound"))
		sound = cfg_GetIntKeyValue("nosound") ? false : true;
	if (cfg_KeyPresent("soundengine"))
		soundengine = cfg_GetIntKeyValue("soundengine");
	if (cfg_KeyPresent("automax"))
		automax = cfg_GetIntKeyValue("automax") ? true : false;
	if (cfg_KeyPresent("startmap"))
		strcpy(mapname, cfg_GetKeyValue("startmap"));
	if (cfg_KeyPresent("vcverbose"))
		verbose = cfg_GetIntKeyValue("vcverbose");
//	if (cfg_KeyPresent("paranoid"))                FIXME
//		vc_paranoid = cfg_GetIntKeyValue("paranoid");
	if (cfg_KeyPresent("arraycheck"))
		vc_arraycheck = cfg_GetIntKeyValue("arraycheck");
	if (cfg_KeyPresent("appname"))
		setWindowTitle(cfg_GetKeyValue("appname"));
	if (cfg_KeyPresent("releasemode"))
		releasemode = cfg_GetIntKeyValue("releasemode") ? true : false;
	if (cfg_KeyPresent("gamerate"))
		gamerate = cfg_GetIntKeyValue("gamerate");
	if (cfg_KeyPresent("v3isuberlikethetens"))
		cheats = true;
	if (cfg_KeyPresent("decompile"))
		decompile = true;
	if (cfg_KeyPresent("editcode"))
		editcode = cfg_GetIntKeyValue("editcode") ? true : false;
	if (cfg_KeyPresent("logconsole"))
	{
		logconsole = true;
		initConsole();
	} else if (cfg_KeyPresent("logconsole-normalstdout")) {
		logconsole = true;
	}
    if (cfg_KeyPresent("oldstring"))
        vc_oldstring = true;

	void platform_ProcessConfig();
	platform_ProcessConfig();

	#ifndef ALLOW_SCRIPT_COMPILATION
	releasemode = true;
	editcode = false;
	#endif
}

int getInitialWindowXres() {
	return v3_window_xres;
}

int getInitialWindowYres() {
	return v3_window_yres;
}

void InitVideo()
{
	//initialize the image handles
	//allocate one dummy, one for screen, and one for the active vsp
	Handle::forceAlloc(HANDLE_TYPE_IMAGE,3);

	if (!windowmode)
	{
		int result = vid_SetMode(v3_xres, v3_yres, v3_bpp, 0, MODE_SOFTWARE);
		if (!result)
			result = vid_SetMode(v3_xres, v3_yres, v3_bpp, 1, MODE_SOFTWARE);
		if (!result)
			err("Could not set video mode!");
		return;
	}
	if (windowmode)
	{
		int result = vid_SetMode(v3_xres, v3_yres, v3_bpp, 1, MODE_SOFTWARE);
		if (!result)
			err("Could not set video mode!");
		return;
	}
}

extern int g_timer;
void ShowPage()
{
	int snap = g_timer;
	HookTimer();
	TimedProcessSprites();
	RenderSprites();
	HookShowPage();
	Flip();
	//Sleep(100);

	//wait for the time to advance.
	//geh
	while(snap==g_timer) Sleep(0);

	int todo=0;
	systemtime++;
	todo=1;
	//if (!engine_paused)
	{
		timer++;
		vctimer++;
		hooktimer++;
	}

	if(GetAsyncKeyState(VK_TAB))
	{
		todo = 8;
		for(int i=0;i<7;i++)
		{
			systemtime++;
			timer++;
			vctimer++;
			hooktimer++;
		}
	}

	if(current_map != NULL)
		current_map->tileset->UpdateAnimations();
}


#ifdef ALLOW_SCRIPT_COMPILATION
bool CompileMaps(const char *ext, MapScriptCompiler *compiler, char *directory = NULL)
{
	if (!directory)
		directory = ".";

	std::string pattern = std::string(directory);
	pattern.append("/*");

	std::vector<std::string> filenames;
	listFilePattern(filenames, pattern.c_str());
	for(std::vector<std::string>::iterator i = filenames.begin();
		i != filenames.end();
		i++)
	{
		if (ExtensionIs(i->c_str(),"map"))
		{
			std::string fullpath(directory);
			fullpath.append("/");
			fullpath.append(*i);

			char *s = stripext(fullpath.c_str());
			if (Exist(va("%s.%s", s,ext))) 
				if(!compiler->CompileMap(s))
					return false;
		}
		else if (i->at(0) != '.')
		{
			// for now, if it's not a .map then try to use it as a directory
			std::string newpath(directory);
			newpath.append("/");
			newpath.append(*i);
			CompileMaps(ext,compiler,(char *)newpath.c_str());
		}
	}


	//log ("");
	return true;
}
#endif


//---
void xmain(int argc, char *argv[])
{
	vc_initBuiltins();
	vc_initLibrary();

	Handle::init();

	strcpy(mapname,"");

	LoadConfig();
	if (argc == 2)
	{
		if (strlen(argv[1]) > 254)
			err("Mapname argument too long!");
		strcpy(mapname, argv[1]);
	}

	InitVideo();

	mouse_Init();
	InitKeyboard();
	joy_Init();
	InitScriptEngine();

	//---cross-platform plugins initialization
//	extern void p_datastructs();
	//p_datastructs();
	//---------

	if (sound) snd_Init(soundengine);

	ResetSprites();
	timer_Init(gamerate);

	#ifdef ALLOW_SCRIPT_COMPILATION
	if(!releasemode)
		vcc = new VCCompiler();
	#endif

	#ifdef ALLOW_SCRIPT_COMPILATION
	#endif

	#ifdef ALLOW_SCRIPT_COMPILATION
	if (!releasemode)
	{
		bool result = vcc->CompileAll();
		if (!result) err(vcc->errmsg);
		vcc->ExportSystemXVC();
		result = CompileMaps("vc",vcc);
		{
			EMUFILE_FILE mapf("maps.xvc","wb");
			g_ScriptDatabase.Save(&mapf);
		}
		if (!result) err(vcc->errmsg);
	}
	#endif

	se = vc = new VCCore();
	if(releasemode)
	{
			EMUFILE_FILE mapf("maps.xvc","rb");
			g_ScriptDatabase.Load(&mapf);
	}
	
	se->ExecAutoexec();

	while (true && strlen(mapname))
		Engine_Start(mapname);
	err("");
}
