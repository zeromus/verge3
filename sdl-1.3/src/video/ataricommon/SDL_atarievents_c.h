/*
    SDL - Simple DirectMedia Layer
    Copyright (C) 1997-2009 Sam Lantinga

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA

    Sam Lantinga
    slouken@libsdl.org
*/
#include "SDL_config.h"

/*
 *	Atari keyboard events manager
 *
 *	Patrice Mandin
 */

#ifndef _SDL_ATARI_EVENTS_H_
#define _SDL_ATARI_EVENTS_H_

#include "../SDL_sysvideo.h"

#define ATARIBIOS_MAXKEYS 128

/* Special keys state */
#ifndef K_RSHIFT
enum
{
    K_RSHIFT = 0,
    K_LSHIFT,
    K_CTRL,
    K_ALT,
    K_CAPSLOCK,
    K_CLRHOME,
    K_INSERT
};
#endif

extern void (*SDL_Atari_ShutdownEvents) (void);

extern void SDL_Atari_PumpEvents(_THIS);

extern void SDL_Atari_InitInternalKeymap(_THIS);

/* Atari to Unicode charset translation table */
extern Uint16 SDL_AtariToUnicodeTable[256];
SDL_keysym *SDL_Atari_TranslateKey(int scancode, SDL_keysym * keysym,
                                   SDL_bool pressed);

#endif /* _SDL_ATARI_EVENTS_H_ */
/* vi: set ts=4 sw=4 expandtab: */
