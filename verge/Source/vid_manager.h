/// The VERGE 3 Project is originally by Ben Eirich and is made available via
///  the BSD License.
///
/// Please see LICENSE in the project's root directory for the text of the
/// licensing agreement.  The CREDITS file in the same directory enumerates the
/// folks involved in this public build.
///
/// If you have altered this source file, please log your name, date, and what
/// changes you made below this line.


#ifndef VIDMANAGER_H
#define VIDMANAGER_H

#define MODE_NULL 0
#define MODE_SOFTWARE 1
#define MODE_D3D 2


extern int vid_bpp, vid_xres, vid_yres, transColor;
extern int vid_bytesperpixel;
extern bool vid_window;
extern image *screen, *rtarget;
extern int alpha, ialpha;

int dd_SetMode(int xres, int yres, int bpp, bool windowflag);
int vid_SetMode(int xres, int yres, int bpp, int window, int mode);
int SetLucent(int percent);



extern void   (*Flip) (void);
extern void   (*Blit) (int x, int y, image *src, image *dest);
extern void   (*TBlit) (int x, int y, image *src, image *dest);
extern void   (*AlphaBlit) (int x, int y, image *src, image *alpha, image *dest);
extern void   (*AdditiveBlit) (int x, int y, image *src, image *dest);
extern void   (*TAdditiveBlit) (int x, int y, image *src, image *dest);
extern void   (*SubtractiveBlit) (int x, int y, image *src, image *dest);
extern void   (*TSubtractiveBlit) (int x, int y, image *src, image *dest);
extern void   (*BlitTile) (int x, int y, char *src, image *dest);
extern void   (*TBlitTile) (int x, int y, char *src, image *dest);
void   Clear (int color, image *dest);
extern void   (*PutPixel) (int x, int y, int color, image *dest);
int    ReadPixel (int x, int y, image *dest);
extern void   (*Line) (int x, int y, int xe, int ye, int color, image *dest);
extern void   (*VLine) (int x, int y, int ye, int color, image *dest);
extern void   (*HLine) (int x, int y, int xe, int color, image *dest);
extern void   (*Box) (int x, int y, int xe, int ye, int color, image *dest);
extern void   (*DrawRect) (int x, int y, int xe, int ye, int color, image *dest);
extern void   (*Sphere) (int x, int y, int xradius, int yradius, int color, image *dest);
extern void   (*Circle) (int x, int y, int xradius, int yradius, int color, image *dest);
extern void   (*ScaleBlit) (int x, int y, int dw, int dh, image *src, image *dest);
extern void   (*TScaleBlit) (int x, int y, int dw, int dh, image *src, image *dest);
extern void   (*WrapBlit) (int x, int y, image *src, image *dst);
extern void   (*TWrapBlit) (int x, int y, image *src, image *dst);
extern void   (*Silhouette) (int x, int y, int c, image *src, image *dst);
extern void   (*RotScale) (int x, int y, float angle, float scale, image *src, image *dest);
extern void   (*Mosaic) (int xf, int yf, image *src);
extern void   (*BlitWrap) (int x, int y, image *src, image *dest);
extern void   (*ColorFilter) (int filter, image *img);
extern void   (*Triangle) (int x1, int y1, int x2, int y2, int x3, int y3, int c, image *dest);
extern void   (*FlipBlit) (int x, int y, int fx, int fy, image *src, image *dest);
extern image* (*ImageFrom8bpp) (byte *src, int width, int height, byte *pal);
extern image* (*ImageFrom15bpp) (byte *src, int width, int height);
extern image* (*ImageFrom16bpp) (byte *src, int width, int height);
extern image* (*ImageFrom24bpp) (byte *src, int width, int height);
extern image* (*ImageFrom32bpp) (byte *src, int width, int height);
extern void   (*vid_Close) (void);
extern void   GrabRegion(int sx1, int sy1, int sx2, int sy2, int dx, int dy, image *s, image *d);
// Overkill 2006-02-04
extern void   (*RectVGrad) (int x, int y, int xe, int ye, int color, int color2, image *dest);
// Overkill 2006-02-04
extern void   (*RectHGrad) (int x, int y, int xe, int ye, int color, int color2, image *dest);
// janus 2006-07-22
extern void   (*RectRGrad) (int x1, int y1, int x2, int y2, int color1, int color2, image *dest);
// janus 2006-07-22
extern void   (*Rect4Grad) (int x1, int y1, int x2, int y2, int color1, int color2, int color3, int color4, image *dest);

template<int BPP> FORCEINLINE int T_MakeColor(int r, int g, int b);
extern int (*MakeColor)(int r, int g, int b);

template<> FORCEINLINE int T_MakeColor<32>(int r, int g, int b)
{
	return ((r<<16)|(g<<8)|b);
}

template<int BPP> FORCEINLINE void T_GetColor(int c, int &r, int &g, int &b);
extern void (*GetColor)(int c, int &r, int &g, int &b);

template<> FORCEINLINE void T_GetColor<32>(int c, int &r, int &g, int &b)
{
	b = c & 0xff;
	g = (c >> 8) & 0xff;
	r = (c >> 16) & 0xff;
}


template<int BPP> FORCEINLINE void T_UnpackColor(int c, int &r, int &g, int &b);

template<> FORCEINLINE void T_UnpackColor<32>(int c, int &r, int &g, int &b)
{
	T_GetColor<32>(c,r,g,b);
}

template<int BPP> FORCEINLINE int T_PackColor(int r, int g, int b);

template<> FORCEINLINE int T_PackColor<32>(int r, int g, int b)
{ 
	return T_MakeColor<32>(r,g,b);
}

inline int ReadPixel(int x, int y, image *source)
{
	quad *ptr = (quad*)source->data;
	return ptr[(y * source->pitch) + x];
}


FORCEINLINE int BytesPerPixel(int bpp) {
	switch(bpp) {
		case 15: return 2;
		case 16: return 2;
		case 32: return 4;
		default: return -1;
	}
}
#endif
