/// The VERGE 3 Project is originally by Ben Eirich and is made available via
///  the BSD License.
///
/// Please see LICENSE in the project's root directory for the text of the
/// licensing agreement.  The CREDITS file in the same directory enumerates the
/// folks involved in this public build.
///
/// If you have altered this source file, please log your name, date, and what
/// changes you made below this line.


void cfwrite(void *inbuf, int size1, int size2, FILE *out);
void cfread(void *ptr, int size1, int size2, FILE *f);
#ifdef VFILE_H

void cfread(byte *dest, int len,1,f);
void cfread(word *dest, int len,1,f);
void cfread(quad *dest, int len,1,f);
void cfread(unsigned long *dest, int len,1,f);
void cfread(double *dest, int len,1,f);
void cfread(float *dest, int len,1,f);
void cfread(char *dest, int len,1,f);
void cfread(short *dest, int len,1,f);
void cfread(int *dest, int len,1,f);
void cfread(long *dest, int len,1,f);

void _cfread(void *ptr, int size,1,f);
#endif

