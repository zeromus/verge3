using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace winmaped2
{
	public sealed class Render
	{
		public enum PixelOp
		{
			Src = 1,
			Dest = 2,
			Alpha50 = 3,
			DestInvert = 4,
			Alpha75 = 5,
		}

		private Render() { }

		public static Bitmap createBitmap(int width, int height)
		{
			return new Bitmap(width, height, PixelFormat.Format32bppArgb);
		}

		private static int HsbIntermediateValuesToRgbComponent(float m1, float m2, float h)
		{
			if (h < 0)
			{
				h++;
			}
			else if (h > 1)
			{
				h--;
			}

			if (h * 6 < 1) return (int)(255 * (m1 + (m2 - m1) * h * 6));
			if (h * 2 < 1) return (int)(255 * m2);
			if (h * 3 < 2) return (int)(255 * (m1 + (m2 - m1) * (2.0f / 3 - h) * 6));
			return (int)(255 * m1);
		}

		private static int HsbToColor(float h, float s, float b)
		{
			float m1;
			float m2;

			h = h / 360;
			m2 = (b <= 0.5) ? b * (s + 1) : b + s - b * s;
			m1 = b * 2 - m2;

			int r = HsbIntermediateValuesToRgbComponent(m1, m2, h + 1.0f / 3);
			int g = HsbIntermediateValuesToRgbComponent(m1, m2, h);
			int b2 = HsbIntermediateValuesToRgbComponent(m1, m2, h - 1.0f / 3);

			return (unchecked((int)0xFF000000) | (r << 16) | (g << 8) | b2);
		}

		public static int makeColor(int r, int g, int b)
		{
			return (int)((0xFF000000) | ((uint)r << 16) | ((uint)g << 8) | ((uint)b));
		}

		private unsafe static bool clip(ref int x0, ref int y0, ref int xlen, ref int ylen, ref int* s, ref int* d, int spitch, int dpitch, int cx1, int cx2, int cy1, int cy2)
		{
			if (x0 >= cx2 || y0 >= cy2 || x0 + xlen <= cx1 || y0 + ylen <= cy1)
			{
				return true;
			}

			if (x0 + xlen > cx2)
			{
				xlen = cx2 - x0;
			}
			if (y0 + ylen > cy2)
			{
				ylen = cy2 - y0;
			}

			if (x0 < cx1)
			{
				s += (cx1 - x0);
				xlen -= (cx1 - x0);
				x0 = cx1;
			}
			if (y0 < cy1)
			{
				s += (cy1 - y0) * spitch;
				ylen -= (cy1 - y0);
				y0 = cy1;
			}

			d += (y0 * dpitch) + x0;

			return false;
		}

		private static void handlePixel(int src, ref int dest, int op, bool mixFlag, bool drawZero)
		{
			if (mixFlag)
			{
				src = mixPixel(src, dest, op);
			}
			if (drawZero)
			{
				dest = src;
			}
			else
			{
				if ((src & 0x00FFFFFF) != 0x00FF00FF)
				{
					dest = src;
				}
			}
		}

		public static int mixPixel(int src, int dest, int op)
		{
			const int MASK = unchecked((int)0xFF000000);
			switch (op)
			{
				case 3:
					{
						int r = (((src & 0x00FF0000) * 3 + (dest & 0x00FF0000)) >> 2) & 0x00FF0000;
						int g = (((src & 0x0000FF00) * 3 + (dest & 0x0000FF00)) >> 2) & 0x0000FF00;
						int b = (((src & 0x000000FF) * 3 + (dest & 0x000000FF)) >> 2) & 0x000000FF;
						return (MASK) | (r | g | b);
					}
				case 5:
					{
						int r = ((src & 0x00FF0000) + (dest & 0x00FF0000)) & 0x01FE0000;
						int g = ((src & 0x0000FF00) + (dest & 0x0000FF00)) & 0x0001FE00;
						int b = ((src & 0x000000FF) + (dest & 0x000000FF)) & 0x000001FE;
						return (MASK) | ((r | g | b) >> 1);
					}
				case 1:
					return src;

				case 2:
					return dest;

				case 4:
					return (int)((~(uint)dest) | 0xFF000000);

				default:
					return 0;
			}
		}

		public unsafe static void render(pr2.IRenderImage dest, int x, int y, pr2.IRenderImage src, bool drawZero)
		{
			int xlen = src.Width;
			int ylen = src.Height;
			int* s = src.Buffer;
			int* d = dest.Buffer;

			int spitch = xlen;
			int dpitch = dest.Pitch;

			if (clip(ref x, ref y, ref xlen, ref ylen, ref s, ref d, spitch, dpitch, 0, dest.Width, 0, dest.Height))
			{
				return;
			}

			for (; ylen != 0; ylen--)
			{
				for (int xx = 0; xx < xlen; xx++)
				{
					handlePixel(s[xx], ref d[xx], 0, false, drawZero);
				}

				s += spitch;
				d += dpitch;
			}
		}


		public static void handlePixelAlpha(int src, ref int dest, int opacity, bool drawZero)
		{
			if (drawZero)
			{
				int r = (opacity * (((src >> 16) & 0xff) - ((dest >> 16) & 0xff)) / 100 + ((dest >> 16) & 0xff));
				int g = (opacity * (((src >> 8) & 0xff) - ((dest >> 8) & 0xff)) / 100 + ((dest >> 8) & 0xff));
				int b = (opacity * ((src & 0xff) - (dest & 0xff)) / 100 + (dest & 0xff));

				dest = unchecked((r << 16) | (g << 8) | b | (int)0xFF000000);
			}
			else
			{
				if ((src & 0x00FFFFFF) != 0x00FF00FF)
				{
					int r = (opacity * (((src >> 16) & 0xff) - ((dest >> 16) & 0xff)) / 100 + ((dest >> 16) & 0xff));
					int g = (opacity * (((src >> 8) & 0xff) - ((dest >> 8) & 0xff)) / 100 + ((dest >> 8) & 0xff));
					int b = (opacity * ((src & 0xff) - (dest & 0xff)) / 100 + (dest & 0xff));

					dest = unchecked((r << 16) | (g << 8) | b | (int)0xFF000000);
				}
			}
		}

		public unsafe static void renderAlpha(pr2.IRenderImage dest, int x, int y, pr2.IRenderImage src, int opacity, bool drawZero)
		{
			int xlen = src.Width;
			int ylen = src.Height;
			int* s = src.Buffer;
			int* d = dest.Buffer;

			int spitch = xlen;
			int dpitch = dest.Pitch;

			if (clip(ref x, ref y, ref xlen, ref ylen, ref s, ref d, spitch, dpitch, 0, dest.Width, 0, dest.Height))
			{
				return;
			}

			for (; ylen != 0; ylen--)
			{
				for (int xx = 0; xx < xlen; xx++)
				{
					handlePixelAlpha(s[xx], ref d[xx], opacity, drawZero);
				}

				s += spitch;
				d += dpitch;
			}
		}

		public static void renderBox(pr2.IRenderImage img, int x0, int y0, int w, int h, int color, Render.PixelOp op)
		{
			renderSolid(img, x0, y0, w, 1, color, op);
			renderSolid(img, x0, y0 + h - 1, w, 1, color, op);
			renderSolid(img, x0, y0, 1, h, color, op);
			renderSolid(img, x0 + w - 1, y0, 1, h, color, op);
		}

		public unsafe static void renderColoredStippleTile(pr2.IRenderImage img, int x0, int y0, int color1, int color2)
		{
			int xlen = Global.TILE_SIZE;
			int ylen = Global.TILE_SIZE;

			int* s = null;
			int* d = img.Buffer;

			int dpitch = img.Pitch;

			if (clip(ref x0, ref y0, ref xlen, ref ylen, ref s, ref d, 0, dpitch, 0, img.Width, 0, img.Height))
			{
				return;
			}

			for (; ylen > 0; ylen--)
			{
				for (int x = 0; x < xlen; x++)
				{
					if (((ylen ^ x) & 1) != 0)
					{
						d[x] = color1;
					}
					else
					{
						d[x] = color2;
					}
				}
				d += dpitch;
			}
		}

		public unsafe static void renderColoredTile(pr2.IRenderImage img, int x0, int y0, int color)
		{
			int xlen = Global.TILE_SIZE;
			int ylen = Global.TILE_SIZE;

			int* s = null;
			int* d = img.Buffer;

			int dpitch = img.Pitch;

			if (clip(ref x0, ref y0, ref xlen, ref ylen, ref s, ref d, 0, dpitch, 0, img.Width, 0, img.Height))
				return;

			for (; ylen > 0; ylen--)
			{
				for (int x = 0; x < xlen; x++)
				{
					d[x] = color;
				}
				d += dpitch;
			}
		}

		public unsafe static void renderColoredTile_50Alpha(pr2.IRenderImage img, int x0, int y0, int color)
		{
			int xlen = Global.TILE_SIZE;
			int ylen = Global.TILE_SIZE;

			int* s = null;
			int* d = img.Buffer;

			int dpitch = img.Pitch;

			if (clip(ref x0, ref y0, ref xlen, ref ylen, ref s, ref d, 0, dpitch, 0, img.Width, 0, img.Height))
			{
				return;
			}

			for (; ylen > 0; ylen--)
			{
				for (int x = 0; x < xlen; x++)
				{
					handlePixel(color, ref d[x], (int)Render.PixelOp.Alpha50, true, true);
				}

				d += dpitch;
			}

		}

		public unsafe static void renderColorPicker(pr2.IRenderImage img, float h)
		{
			int* dst = img.Buffer;
			for (int y = 0; y < 256; y++)
			{
				for (int x = 0; x < 256; x++)
				{
					*dst = HsbToColor(h, (float)x / 256, (float)y / 256);
					dst++;
				}
			}
		}

		public unsafe static int measureText(string str)
		{
			int ret = 0;
			char[] digits = str.ToCharArray();
			foreach (char c in digits)
			{
				byte[] ba = BiosFont.Glyph(c);
				int glyphWidth = ba[0];
				ret += glyphWidth+1;
			}
			ret -= 1;

			return ret;
		}

		public unsafe static void renderText(pr2.IRenderImage img, int x0, int y0, string str, int color)
		{
			int height = img.Height;
			int width = img.Width;
			int* pixels = img.Buffer;

			char[] digits = str.ToCharArray();
			foreach (char c in digits)
			{
				byte[] ba = BiosFont.Glyph(c);

				int glyphWidth = ba[0];
				int glyphHeight = (ba.Length - 1) / glyphWidth;

				for (int y = 0; y < glyphHeight; y++)
				{
					if (y0 + y < 0 || y0 + y >= height) continue;
					for (int x = 0; x < glyphWidth; x++)
					{
						if (x0 + x < 0 || x0 + x >= width) continue;
						if (ba[1 + (y * glyphWidth) + x] == 1)
						{
							pixels[(y0 + y) * width + (x0 + x)] = color;
						}
					}
				}
				x0 += glyphWidth + 1;
			}
		}

		public unsafe static void renderNumber(pr2.IRenderImage img, int x0, int y0, int number, int color)
		{
			renderText(img, x0, y0, number.ToString(), color);
		}

		public unsafe static void renderObsTile(pr2.IRenderImage img, int x0, int y0, pr2.IRenderImage src, bool clearbuf, int color)
		{
			int xlen = Global.TILE_SIZE;
			int ylen = Global.TILE_SIZE;

			const int WHITE = unchecked((int)0xFFFFFFFF);
			const int BLACK = unchecked((int)0xFF000000);

			int spitch = Global.TILE_SIZE;
			int dpitch = img.Pitch;

			int* ptr = src.Buffer;
			int* s = ptr;
			int* d = img.Buffer;

			if (clip(ref x0, ref y0, ref xlen, ref ylen, ref s, ref d, spitch, dpitch, 0, img.Width, 0, img.Height))
			{
				return;
			}

			if (clearbuf)
			{
				for (; ylen > 0; ylen--)
				{
					for (int x = 0; x < xlen; x++)
					{
						d[x] = (s[x] != 0) ? WHITE : BLACK;
					}
					s += spitch;
					d += dpitch;
				}
			}
			else
			{
				for (; ylen > 0; ylen--)
				{
					for (int x = 0; x < xlen; x++)
					{
						if (s[x] != 0)
						{
							handlePixel(color, ref d[x], (int)Render.PixelOp.Alpha50, true, true);
						}
					}

					s += spitch;
					d += dpitch;
				}
			}
		}

		public unsafe static void renderObsTileFast(pr2.IRenderImage img, int x0, int y0, pr2.IRenderImage src, bool clearbuf)
		{
			int xlen = Global.TILE_SIZE;
			int ylen = Global.TILE_SIZE;

			const int WHITE = unchecked((int)0xFFFFFFFF);
			const int BLACK = unchecked((int)0xFF000000);

			int* pixels = src.Buffer;
			int* s = pixels;
			int* d = img.Buffer;

			int spitch = Global.TILE_SIZE;
			int dpitch = img.Pitch;

			if (clip(ref x0, ref y0, ref xlen, ref ylen, ref s, ref d, spitch, dpitch, 0, img.Width, 0, img.Height))
			{
				return;
			}

			if (clearbuf)
			{
				for (; ylen > 0; ylen--)
				{
					for (int x = 0; x < xlen; x++)
					{
						d[x] = (s[x] != 0) ? WHITE : BLACK;
					}
					s += spitch;
					d += dpitch;
				}
			}
			else
			{
				for (; ylen > 0; ylen--)
				{
					for (int x = 0; x < xlen; x++)
					{
						if (s[x] != 0) d[x] = WHITE;
					}
					s += spitch;
					d += dpitch;
				}
			}
		}

		public unsafe static void renderSolid(pr2.IRenderImage img, int x0, int y0, int w, int h, int color, Render.PixelOp op)
		{
			int bw = img.Width;
			int bh = img.Height;
			int bp = img.Pitch;

			for (int y = 0; y < h; y++)
			{
				if (y + y0 >= 0 && y + y0 < bh)
				{
					for (int x = 0; x < w; x++)
					{
						if (x + x0 >= 0 && x + x0 < bw)
						{
							handlePixel(color, ref img.Buffer[(y0 + y) * bp + x0 + x], (int)op, true, true);
						}
					}
				}
			}
		}
	}
}
