/// The VERGE 3 Project is originally by Ben Eirich and is made available via
///  the BSD License.
///
/// Please see LICENSE in the project's root directory for the text of the
/// licensing agreement.  The CREDITS file in the same directory enumerates the
/// folks involved in this public build.
///
/// If you have altered this source file, please log your name, date, and what
/// changes you made below this line.


/****************************************************************
	xerxes engine
	a_codec.cpp
 ****************************************************************/

#include "xerxes.h"
#include "zlib.h"

/***************************** code *****************************/

void cfwrite(void *ptr, int size1, int size2, FILE *out)
{
	int size = size1 * size2;
	uLong comprLen = size+(size*20/100);
	if (comprLen<200) comprLen = 200;
    byte *outbuf = new byte[comprLen];
	byte *inbuf = (byte *) ptr;

    int myErr = compress(outbuf, &comprLen, inbuf, size);

    if (myErr != Z_OK)
    {
        if (myErr == Z_MEM_ERROR) err("Z_MEM_ERROR");
        if (myErr == Z_BUF_ERROR) err("Z_BUF_ERROR");
        if (myErr == Z_STREAM_ERROR) err("Z_STREAM_ERROR");
    }
    fwrite(&size, 1, 4, out);
    fwrite(&comprLen, 1, 4, out);
    fwrite(outbuf, 1, comprLen, out);
	delete[] outbuf;
}

void cfread(void *ptr, int size1, int size2, FILE *f)
{
	uLong size = size1 * size2, comprLen;
	unsigned int mysize;
	fread(&mysize, 1, 4, f);
	if (mysize != size) err("cfread(): block sizes do not match");
	fread(&comprLen, 1, 4, f);
	byte *inbuf = new byte[comprLen];
	byte *outbuf = (byte *) ptr;
	fread(inbuf, 1, comprLen, f);

	int myErr = uncompress(outbuf, &size, inbuf, comprLen);

	if (myErr != Z_OK)
    {
        if (myErr == Z_MEM_ERROR) err("Z_MEM_ERROR");
        if (myErr == Z_BUF_ERROR) err("Z_BUF_ERROR");
        if (myErr == Z_STREAM_ERROR) err("Z_STREAM_ERROR");
    }
	delete[] inbuf;
}

#ifdef VFILE_H
void cfread(word *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
	int num = len/sizeof(word);
	if(num == 0) num = 1;
	for(int i = 0; i < num; i++)
	{
		flip(dest+i, sizeof(word));
	}
}

void cfread(quad *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
	int num = len/sizeof(quad);
	if(num == 0) num = 1;
	for(int i = 0; i < num; i++)
	{
		flip(dest+i, sizeof(quad));
	}
}

void cfread(byte *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
}

void cfread(double *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
	int num = len/sizeof(double);
	if(num == 0) num = 1;
	for(int i = 0; i < num; i++)
		flip(dest+i, sizeof(double));
}

void cfread(float *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
	int num = len/sizeof(float);
	if(num == 0) num = 1;
	for(int i = 0; i < num; i++)
		flip(dest+i, sizeof(float));
}

void cfread(unsigned long *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
	int num = len/sizeof(unsigned long);
	if(num == 0) num = 1;
	for(int i = 0; i < num; i++)
		flip(dest+i, sizeof(unsigned long));
}

void cfread(long *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
	int num = len/sizeof(long);
	if(num == 0) num = 1;
	for(int i = 0; i < num; i++)
		flip(dest+i, sizeof(long));
}

void cfread(short *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
	int num = len/sizeof(short);
	if(num == 0) num = 1;
	for(int i = 0; i < num; i++)
	{
		flip(dest+i, sizeof(short));
	}
}

void cfread(int *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
	int num = len/sizeof(int);
	if(num == 0) num = 1;
	for(int i = 0; i < num; i++)
	{
		flip(dest+i, sizeof(int));
	}
}

void cfread(char *dest, int len,1,f)
{
	_cfread(dest, len,1,f);
}



void _cfread(void *ptr, int size1,1,f)
{
	uLong size = size1, comprLen;
	unsigned int mysize;
	fread(&mysize, 4,1,f);
	if (mysize != size) err("cvread(): block sizes do not match");
	fread(&comprLen, 4,1,f);
	byte *inbuf = new byte[comprLen];
	byte *outbuf = (byte *) ptr;
	fread(inbuf, comprLen,1,f);

	int myErr = uncompress(outbuf, &size, inbuf, comprLen);

	if (myErr != Z_OK)
    {
        if (myErr == Z_MEM_ERROR) err("Z_MEM_ERROR");
        if (myErr == Z_BUF_ERROR) err("Z_BUF_ERROR");
        if (myErr == Z_STREAM_ERROR) err("Z_STREAM_ERROR");
    }
	delete[] inbuf;
}

#endif
