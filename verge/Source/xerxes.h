/// The VERGE 3 Project is originally by Ben Eirich and is made available via
///  the BSD License.
///
/// Please see LICENSE in the project's root directory for the text of the
/// licensing agreement.  The CREDITS file in the same directory enumerates the
/// folks involved in this public build.
///
/// If you have altered this source file, please log your name, date, and what
/// changes you made below this line.

#ifndef XERXES_H
#define XERXES_H

#define ALLOW_SCRIPT_COMPILATION

#ifdef __APPLE__
#include "TargetConditionals.h"
	#if defined(TARGET_OS_IPHONE) || defined(TARGET_IPHONE_SIMULATOR)
	#define __IPHONE__
	#else
	#define __OSX__
	#endif
#endif

//version info macros
#define DEF_VERSION "Verge 3.2 2009.08.01"
#define DEF_BUILD "20090801"
#define DEF_OS "win"

#include <math.h>
#include <vector>

#ifdef WIN32
#define __WIN32__ 1
#endif

//sound configurations
#ifdef __WIN32__
#define SND_USE_FMOD
#endif

//mbg 9/5/05 determine whether to use asm in ddblit
//and other configs too
//CRIPPLED is set if the goddamn cpu cant do unaligned
//reads of shorts and ints.
//for example, psp is CRIPPLED.
//CRIPPLED code so far is assuming little endian since
//we only use it for psp
//isnt it also necessary for mac? or is mac big endian but uncrippled?
#ifdef __APPLE__
#define CDECL _cdecl
#define BLITTER_32BPP
#define BLITTER_16BPP
#define BLITTER_15BPP
#elif __LINUX__
#define CDECL
#define BLITTER_32BPP
#define BLITTER_16BPP
#define BLITTER_15BPP
#elif __PSP__
#define CDECL
#define NOSPLASHSCREEN
#define NOTIMELESS
#define CRIPPLED
#define BLITTER_61BPP
#define BLITTER_16BPP
#elif __WII__
#define CDECL
#define BLITTER_32BPP
//#define BLITTER_16BPP
//#define BLITTER_15BPP
#else
#define DDBLIT_ASM
#define CDECL _cdecl
#define BLITTER_32BPP
#define BLITTER_16BPP
#define BLITTER_15BPP
//nominmax prevents windows.h from including lame min/max macros
#define NOMINMAX
#endif

/* xerxes system types */
#define MYTYPES 1
typedef unsigned int   quad;
typedef unsigned short word;
typedef unsigned char  byte;

/* macros */

#define APPNAME "verge3"
#define LOGFILE "v3.log"
#define ORDER_INT(a,b) { if (a>b) { a=a-b; b=a+b; a=b-a; } }
#define SWAP(a,b) { a=a-b; b=a+b; a=b-a; }
#define FORCEINLINE inline

#ifdef __WIN32__
#undef FORCEINLINE
#define FORCEINLINE __forceinline
#endif

/* system includes */



#ifdef __APPLE__
#include <stdlib.h>
#include <stdio.h>
#include <strings.h>
#include <stdarg.h>
#include <unistd.h>

	#ifndef __IPHONE__
	#include "SDL.h"
	#endif

#include "../corona/corona.h"
#elif __LINUX__
#include <ctype.h>
#include <stdlib.h>
#include <stdio.h>
#include <stdarg.h>
#include <strings.h>
#include <unistd.h>
#include <SDL.h>
#include <fmod.h>
#include <corona.h>
#include <gtk/gtk.h>

//mbg 9/5/05 adding psp support
#elif __PSP__
#include <stdlib.h>
#include <stdio.h>
#include <stdarg.h>
#include <corona.h>
#undef quad

#elif __WIN32__
#define WIN32_LEAN_AND_MEAN
#define _WIN32_WINNT 0x0500
#include <crtdbg.h>
#include <windows.h>
#undef TRANSPARENT
#undef MessageBox
#include <mmsystem.h>
#include <stdlib.h>
#include <stdio.h>
#include <stdarg.h>
#include <io.h>
#include "../fmod/fmod.h"
#include "../corona/corona.h"
#define strlwr _strlwr
#define strupr _strupr
#define snprintf _snprintf

//mbg 04-nov-08 adding wii support
#elif __WII__
#include "wiisys.h"
#endif

//xerxes includes

#include "a_handle.h"
#include "a_image.h"
#include "vid_manager.h"
#include "vid_sysfont.h"
#include "a_common.h"
#include "a_config.h"
#include "a_string.h"
#include "a_codec.h"
#include "a_dict.h"

#ifdef __IPHONE__
#include "vid_ddbase.h"
#include "iphone_platform.h"

#elif __APPLE__
#include "vid_macbase.h"
#include "mac_joystick.h"
#include "mac_network.h"
#include "mac_keyboard.h"
#include "mac_mouse.h"
#include "mac_system.h"
#include "mac_timer.h"
#include "mac_movie.h"

#elif __LINUX__
#include "vid_macbase.h"
#include "mac_joystick.h"
#include "mac_network.h"
#include "mac_keyboard.h"
#include "mac_mouse.h"
#include "mac_system.h"
#include "mac_timer.h"
#include "mac_movie.h"

//mbg 9/5/05 adding psp support
#elif __PSP__
#include "vid_ddbase.h"
#include "psp_system.h"
#include "psp_timer.h"
#include "psp_sound.h"
#include "psp_keyboard.h"
#include "psp_mouse.h"
#include "psp_joystick.h"
#include "psp_movie.h"
#include "psp_network.h"
#include "psp.h"

//mbg 02-nov-08 adding wii support
#elif __WII__
#include "vid_ddbase.h"
#include "wii_platform.h"
#else

#include "vid_gdibase.h"
#include "win_joystick.h"
#include "win_network.h"
#include "win_keyboard.h"
#include "win_mouse.h"
#include "win_system.h"
#include "win_timer.h"

#endif

#include "g_script.h"
#include "g_controls.h"
#include "g_chr.h"
#include "g_entity.h"
#include "g_font.h"
#include "g_vsp.h"
#include "g_map.h"
#include "g_sprites.h"
#include "g_engine.h"
#include "g_sound.h"
#include "vc_compiler.h"
#include "vc_core.h"
#include "g_startup.h"
#include "emufile.h"

//prototypes

void xmain(int argc, char *argv[]);
void err(const char *s, ...);
void HandleMessages();

int getInitialWindowXres();
int getInitialWindowYres();

#define ARRAY_SIZE(a) (sizeof(a) / sizeof((a)[0]))
#ifndef CTASSERT
#define	CTASSERT(x)		typedef char __assert ## y[(x) ? 1 : -1]
#endif

inline void fread_le(int* dest, FILE *fp) { fread(dest,1,sizeof(int),fp); }
inline void fread_le(int* dest, EMUFILE *fp) { fp->fread(dest,sizeof(int)); }
inline void vread(void* dst, int size, FILE* inf) { fread(dst,1,size,inf); }
inline void vseek(FILE* inf, int offset, int origin) { fseek(inf,offset,origin); }
inline char* vgets(char* buf, int maxcount, FILE*inf) { return fgets(buf,maxcount,inf); }
inline int veof(FILE* inf) { return feof(inf); }
inline int vtell(FILE* inf) { return ftell(inf); }
inline int filesize(FILE* inf) {
	int temp = ftell(inf);
	fseek(inf,0,SEEK_END);
	int len = ftell(inf);
	fseek(inf,0,SEEK_SET);
	return len;
}

inline bool Exist(char* fname) {
	FILE* inf = fopen(fname,"rb");
	if(!inf) return false;
	fclose(inf);
	return true;
}

inline std::string normalize_path(const std::string& str)
{
#ifdef __WIN32__
	std::string temp = str;
	boost::algorithm::replace_all(temp, "/", "\\");
	return temp;
#endif
	return str;
}
#endif
