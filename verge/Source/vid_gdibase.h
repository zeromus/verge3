/// The VERGE 3 Project is originally by Ben Eirich and is made available via
///  the BSD License.
///
/// Please see LICENSE in the project's root directory for the text of the
/// licensing agreement.  The CREDITS file in the same directory enumerates the
/// folks involved in this public build.
///
/// If you have altered this source file, please log your name, date, and what
/// changes you made below this line.


#ifndef DDBASE_H
#define DDBASE_H

void ddwin_Flip();
void dd_Flip();
void dd_Fallback();
int dd_SetMode(int xres, int yres, int bpp, bool windowflag);
void dd_Close();
void dd_RegisterBlitters();

class gdi_Window
{
public:
	BITMAPINFO bmi;
	HDC hdc, backDC;
	HANDLE backOld;
	HBITMAP backSurface;
	void* backBuffer;

	image* img;
	int imgHandle;
	HWND hwnd;
	bool bGameWindow;
	bool bActive;
	int handle;
	int xres, yres;
	bool bVisible;

	int winx, winy;
	int winw, winh;


	gdi_Window(bool bGameWindow);
	void dispose();
	void flip_win();
	void flip_fullscreen();
	int set_win(int w, int h, int bpp);
	void shutdown_win();
	int set_fullscreen(int w, int h, int bpp);
	void deactivate();
	void activate();
	void createWindow();
	void setupDummyImage();
	void adjust(int w, int h, RECT* r);

	int getHandle();
	int getImageHandle();
	image* getImage();

	bool bMouseInside;
};


#endif