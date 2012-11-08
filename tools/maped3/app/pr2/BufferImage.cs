using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace winmaped2.pr2
{
	public unsafe class BufferImage : RenderImage
	{
		public BufferImage(int width, int height) {
            this.width = width;
            this.height = height;
            this.buf = (int*)System.Runtime.InteropServices.Marshal.AllocHGlobal(width * height * 4);
						for (int i = 0; i < width * height; i++)
							this.buf[i] = 0;
            this.stride = width * 4;
            this.pitch = width;
        }

		public BufferImage(Bitmap bmp)
			: this(bmp.Width, bmp.Height)
		{
			int w = bmp.Width;
			int h = bmp.Height;

			BitmapData bmpd = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

			int dp = pitch;
			int sp = bmpd.Stride / 4;

			int* srcdata = (int*)bmpd.Scan0.ToPointer();
			int* destdata = (int*)Buffer;

			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					destdata[y * dp + x] = srcdata[y * sp + x];
				}
			}
			bmp.UnlockBits(bmpd);
		}

		public BufferImage(int width, int height, int[] pixels)
			: this(width, height)
		{
			if (width * height != pixels.Length)
			{
				throw new InvalidOperationException("Bad number of pixels passed to BufferImage(width, height, pixels)");
			}
			for (int i = 0; i < pixels.Length; i++)
			{
				this.buf[i] = pixels[i];
			}
		}

		public override void Dispose()
		{
			if (!isDisposed)
			{
				System.Runtime.InteropServices.Marshal.FreeHGlobal((IntPtr)buf);
				buf = null;
				isDisposed = true;
			}
		}

		~BufferImage()
		{
			Dispose();
		}

		private bool isDisposed = false;
	}
}
