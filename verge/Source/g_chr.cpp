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
	g_chr.cpp
 ****************************************************************/

#include "xerxes.h"

/***************************** data *****************************/

#define CHR_SIGNATURE		5392451
#define CHR_VERSION			5

/***************************** code *****************************/

CHR::CHR(const char *fname)
{
	FILE *f = fopen(fname,"rb");
	if (!f) err("CHR::CHR(), couldn't open %s", fname);

	name = fname;

	int signature;
	fread(&signature, 4,1,f);
	if (signature != CHR_SIGNATURE)
		err("CHR::CHR(), %s is not a valid CHR file!", fname);

	int version;
	fread(&version, 4,1,f);
	if (version != CHR_VERSION)
		err("CHR::CHR(), %s is not the correct CHR version!", fname);

	int bpp;
	fread(&bpp, 4,1,f);

	int flags;
	fread(&flags, 4,1,f);

	int tcol;
	fread(&tcol, 4,1,f);   // FIXME properly support this
	fread(&hx, 4,1,f);
	fread(&hy, 4,1,f);
	fread(&hw, 4,1,f);
	fread(&hh, 4,1,f);
	fread(&fxsize, 4,1,f);
	fread(&fysize, 4,1,f);
	fread(&totalframes, 4,1,f);
	fread(&idle[SOUTH], 4,1,f);
	fread(&idle[NORTH], 4,1,f);
	fread(&idle[WEST], 4,1,f);
	fread(&idle[EAST], 4,1,f);
	int customscripts;
	fread(&customscripts, 4,1,f); // ignored for now

	char animbuf[255];
	int indexes[] = { 0, 2, 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

	int numAnims = 9 + customscripts;
	for (int b=1; b<numAnims; b++)
    {
		int n;
		fread(&n, 4,1,f);
		if (n>=254)
			err("Animation strand too long. %d", n);
		fread(animbuf, n+1,1,f);

		animsize[indexes[b]] = GetAnimLength(animbuf);
		anims[indexes[b]] = new int[animsize[indexes[b]]];
		ParseAnimation(indexes[b], animbuf);
	}


	int compression;
	fread(&compression, 4,1,f); // assume always zlib for now

	byte *raw = new byte[totalframes*fxsize*fysize*(bpp/8)];
	cfread(raw, totalframes*fxsize*fysize*(bpp/8),1,f);
	fclose(f);

	rawdata = ImageFrom32bpp(raw, fxsize, fysize*totalframes);
	container = new image(fxsize, fysize);
	container->delete_data();
	delete[] raw;
}

CHR::~CHR()
{
	for (int i=1; i<9; i++)
		delete[] anims[i];
	container->data = 0;
	delete container;
	delete rawdata;
}

// Overkill (2006-07-20): Saver functions!
void CHR::save(FILE *f)
{
	// Signature!
	int signature = CHR_SIGNATURE;
	fwrite(&signature, 1, 4, f);
	// Version!
	int version = CHR_VERSION;
	fwrite(&version, 1, 4, f);
	// BPP! Always 24.
	int bpp = 24;
	fwrite(&bpp, 1, 4, f);
	// ...Flags?
	int flags = 0;
	fwrite(&flags, 1, 4, f);
	// Total colors, always 2^bitdepth.
	int tcol = 16777216;
	fwrite(&tcol, 1, 4, f);
	// Hotspot
	fwrite(&hx, 1, 4, f);
	fwrite(&hy, 1, 4, f);
	fwrite(&hw, 1, 4, f);
	fwrite(&hh, 1, 4, f);
	// The frame dimensions
	fwrite(&fxsize, 1, 4, f);
	fwrite(&fysize, 1, 4, f);
	// Total frames.
	fwrite(&totalframes, 1, 4, f);
	// Idle frames.
	fwrite(&idle[SOUTH], 1, 4, f);
	fwrite(&idle[NORTH], 1, 4, f);
	fwrite(&idle[WEST], 1, 4, f);
	fwrite(&idle[EAST], 1, 4, f);
	// Animation
	int indexes[] = { 0, 2, 1, 3, 4, 5, 6, 7, 8 };
	for (int b=1; b<9; b++)
    {
		int n = animsize[indexes[b]];
		fwrite(&n, 1, 4, f);
		if (n>=254)
			err("Animation strand too long. %d", n);
		fwrite(&anims[indexes[b]], 1, n+1, f);
	}

	// Custom scripts! ignored for now ;_;
	int customscripts = 0;
	fwrite(&customscripts, 1, 4, f);
	// Compression: assume always zlib for now
	int compression = 0;
	fwrite(&compression, 1, 4, f);

	//byte *raw = rawchr;
	byte *raw = ImageTo24bpp(rawdata);
	cfwrite(raw, 1, totalframes*fxsize*fysize*(bpp/8), f);
}

void CHR::render(int x, int y, int frame, image *dest)
{
	x -= hx;
	y -= hy;

	if (frame <0 || frame >= totalframes)
		err("CHR::render(), frame requested is undefined (%d of %d)", frame, totalframes);
	container->data = (quad *) ((int) rawdata->data + (frame*fxsize*fysize*vid_bytesperpixel));
	TBlit(x, y, container, dest);
//SetLucent(50);
//Rect(x+hx, y+hy, x+hx+hw-1, y+hy+hh-1, 0, dest);
//SetLucent(0);
}

int CHR::GetFrame(int d, int &framect)
{
	//if (d<1 || d>4)
	//	err("CHR::GetFrame() - invalid direction %d", d);
	framect %= animsize[d];
	return anims[d][framect];
}

int CHR::GetFrameConst(int d, int framect)
{
	if (d<1 || d>4)
		err("CHR::GetFrame() - invalid direction %d", d);
	return anims[d][framect % animsize[d]];
}

void CHR::ParseAnimation(int d, char *anim)
{
	int frame, len, i, ofs=0;
	parsestr = anim;

	while (*parsestr)
	{
		switch (*parsestr)
		{
			case 'f':
			case 'F':
				parsestr++;
				frame = GetArg();
				break;
			case 'w':
			case 'W':
				parsestr++;
				len = GetArg();
				for (i=ofs; i<ofs+len; i++)
					anims[d][i] = frame;
				ofs += len;
				break;
			default:
				err("CHR::ParseAnimation() - invalid animscript command! %c", *parsestr);
		}
	}
}

int CHR::GetAnimLength(char *anim)
{
	int length = 0;
	parsestr = anim;

	while (*parsestr)
	{
		switch (*parsestr)
		{
			case 'f':
			case 'F':
				parsestr++;
				GetArg();
				break;
			case 'w':
			case 'W':
				parsestr++;
				length += GetArg();
				break;
			default:
				err("CHR::GetAnimLength() - invalid animscript command! %c", *parsestr);
		}
	}
	return length;
}

int CHR::GetArg()
{
	static char	token[10];

	while (*parsestr == ' ')
		parsestr++;

	int n = 0;
	while (*parsestr >= '0' && *parsestr <= '9')
		token[n++] = *parsestr++;
	token[n] = 0;

	return atoi(token);
}