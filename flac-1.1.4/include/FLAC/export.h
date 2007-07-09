/* libFLAC - Free Lossless Audio Codec library
 * Copyright (C) 2000,2001,2002,2003,2004,2005,2006,2007  Josh Coalson
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * - Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above copyright
 * notice, this list of conditions and the following disclaimer in the
 * documentation and/or other materials provided with the distribution.
 *
 * - Neither the name of the Xiph.org Foundation nor the names of its
 * contributors may be used to endorse or promote products derived from
 * this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE FOUNDATION OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#ifndef FLAC__EXPORT_H
#define FLAC__EXPORT_H

/** \file include/FLAC/export.h
 *
 *  \brief
 *  This module contains #defines and symbols for exporting function
 *  calls, and providing version information and compiled-in features.
 *
 *  See the \link flac_export export \endlink module.
 */

/** \defgroup flac_export FLAC/export.h: export symbols
 *  \ingroup flac
 *
 *  \brief
 *  This module contains #defines and symbols for exporting function
 *  calls, and providing version information and compiled-in features.
 *
 * \{
 */

#if defined(FLAC__NO_DLL) || !defined(_MSC_VER)
#define FLAC_API

#else

#ifdef FLAC_API_EXPORTS
#define	FLAC_API	_declspec(dllexport)
#else
#define FLAC_API	_declspec(dllimport)

#endif
#endif

/** These #defines will mirror the libtool-based library version number, see
 * http://www.gnu.org/software/libtool/manual.html#Libtool-versioning
 */
#define FLAC_API_VERSION_CURRENT 8
#define FLAC_API_VERSION_REVISION 1 /**< see above */
#define FLAC_API_VERSION_AGE 0 /**< see above */

#ifdef __cplusplus
extern "C" {
#endif

/** \c 1 if the library has been compiled with support for Ogg FLAC, else \c 0. */
extern FLAC_API int FLAC_API_SUPPORTS_OGG_FLAC;

#ifdef __cplusplus
}
#endif

/* \} */

#endif
