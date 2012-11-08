using System;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace winmaped2
{

	/**************************************************/
	/* Abstract/Universal Classes                     */
	/**************************************************/

	public abstract class VspBase
	{
		public ArrayList Tiles;
		public int tileCount { get { return Tiles.Count; } }
		public ArrayList Animations;
		public FileInfo FileOnDisk;
		public abstract object CreateTile();
		public void removeAt(int index)
		{
			Tiles.RemoveAt(index);
		}
		public void insertAt(int index)
		{
			Tiles.Insert(index, CreateTile());
		}
	}

	public class VspAnimation
	{
		public int Start = 0;
		public int End = 0;
		public int Delay = 0;
		public int Mode = 0;
		public string Name = "";
		public int ID = 0;
		public int currTile = 0;
		public VspAnimation Clone()
		{
			VspAnimation va = new VspAnimation();

			va.Start = this.Start;
			va.End = this.End;
			va.Delay = this.Delay;
			va.Mode = this.Mode;
			va.ID = this.ID;
			va.Name = string.Copy(this.Name);

			return va;
		}
	}

	/**************************************************/
	/* 8-bit VSP Classes                              */
	/**************************************************/
	public class Vsp8 : VspBase
	{
		public override object CreateTile() { return new Vsp8Tile(this, new byte[Global.TILE_SIZE * Global.TILE_SIZE]); }
		public Vsp8Palette Palette;
		public Vsp8()
		{
			Tiles = new ArrayList();
			Animations = new ArrayList();
			Palette = new Vsp8Palette();
		}
		public byte[] getPixels(int i)
		{
			return ((Vsp8Tile)Tiles[i]).Pixels;
		}
	}
	public class Vsp8PaletteEntry
	{
		private byte _r = 0, _g = 0, _b = 0;
		public byte r { get { return _r; } set { _r = value; Calc(); } }
		public byte g { get { return _g; } set { _g = value; Calc(); } }
		public byte b { get { return _b; } set { _b = value; Calc(); } }
		public int IntColor;

		public byte[] ToByteArray()
		{
			return new byte[] { r, g, b };
		}
		private static int conv(int x)
		{
			return (x * 255) / 63;
		}
		private void Calc()
		{
			int nr = conv(r), ng = conv(g), nb = conv(b);
			unchecked { IntColor = ((int)0xFF000000 | (nr << 16) | (ng << 8) | (nb)); }
		}
	}
	public class Vsp8Palette
	{
		Vsp8PaletteEntry[] Colors = new Vsp8PaletteEntry[256];
		public Vsp8PaletteEntry this[int index]
		{
			get
			{
				return Colors[index];
			}
			set
			{
				Colors[index] = value;
			}
		}


		public int[] ToIntArray()
		{
			int[] ret = new int[Colors.Length];
			for (int i = 0; i < Colors.Length; i++)
				ret[i] = Colors[i].IntColor;
			return ret;
		}
	}
	public class Vsp8Tile
	{
		public byte[] Pixels;
		public Vsp8 parent;
		public Vsp8Tile(Vsp8 Parent, byte[] data)
		{
			Pixels = data;
			parent = Parent;
		}
		public void doAvg()
		{
			int cr = 0, cg = 0, cb = 0;
			int ca = 0;
			int count;
			for (int i = 0; i < Global.TILE_SIZE; i++)
			{
				for (int j = 0; j < Global.TILE_SIZE; j++)
				{
					int px = Pixels[i * Global.TILE_SIZE + j];
					if (px == 0) continue;
					ca+=256;
					cr += parent.Palette[px].r;
					cg += parent.Palette[px].g;
					cb += parent.Palette[px].b;
				}
			}
			unchecked { avg = (((ca >> 8)<<24) | ((cr >> 8) << 18) | ((cg >> 8) << 10) | ((cb >> 8) << 2)); }
		}
		protected int avg;
		public int ColorAverage
		{
			get
			{
				return avg;
			}
		}

	}


	/**************************************************/
	/* 24-bit VSP Classes                             */
	/**************************************************/
	public class Vsp24 : VspBase
	{
		public const int ANIMATION_FORWARD = 0;
		public const int ANIMATION_REVERSE = 1;
		public const int ANIMATION_RANDOM = 2;
		public const int ANIMATION_PINGPONG = 3;

		public override object CreateTile()
		{
			return new Vsp24Tile(this);
		}

		public string fname;
		private bool _bAltered;
		public bool bAltered { get { return _bAltered; } set { _bAltered = value; } }
		public void touch()
		{
			bAltered = true;
		}
		public ArrayList ObstructionTiles;
		public Vsp24()
		{
			Tiles = new ArrayList();
			ObstructionTiles = new ArrayList();
			Animations = new ArrayList();
		}
		public void Write()
		{
			InputOutput.WriteVsp(this.FileOnDisk, this);
		}
		public void Write(string fname)
		{
			FileOnDisk = new FileInfo(fname);
			Write();
		}
		public Vsp24Tile GetTile(int i)
		{
			return (Vsp24Tile)Tiles[i];
		}
		public static Vsp24 FromVsp8(Vsp8 src)
		{
			Vsp24 v24 = new Vsp24();
			foreach (Vsp8Tile v8t in src.Tiles)
			{
				int[] data = new int[256];
				for (int y = 0; y < Global.TILE_SIZE; y++)
				{
					for (int x = 0; x < Global.TILE_SIZE; x++)
					{
						if (v8t.Pixels[y * Global.TILE_SIZE + x] > 0)
						{
							data[y * Global.TILE_SIZE + x] = v8t.parent.Palette[v8t.Pixels[y * Global.TILE_SIZE + x]].IntColor;
						}
						else
						{
							unchecked
							{
								data[y * Global.TILE_SIZE + x] = ((int)0xFFFF00FF);
							}
						}
					}
				}

				Vsp24Tile v24t = new Vsp24Tile(v24, new pr2.BufferImage(Global.TILE_SIZE, Global.TILE_SIZE, data));
				v24.Tiles.Add(v24t);
			}
			v24.FileOnDisk = src.FileOnDisk;
			v24.Animations = src.Animations;
			return v24;
		}

		public void AddBasicObstructionTiles()
		{
			//int[] solid = new int[256];
			//for (int i = 0; i < 256; i++)
			//    solid[i] = 0x01;

			//int x, y;
			//int[] ld = new int[256];
			//for (x = 0, y = 0; x < 16; x++, y++)
			//    ld[y * 16 + x] = 0x01;

			//int[] rd = new int[256];
			//for (x = 15, y = 0; y < 16; x--, y++)
			//    rd[y * 16 + x] = 0x01;
			//int[] r_solid = (int[])solid.Clone();
			//r_solid[0] = 0;
			//r_solid[1] = 0;
			//r_solid[16] = 0;

			//r_solid[14] = 0;
			//r_solid[15] = 0;
			//r_solid[31] = 0;

			//r_solid[14 * 16 + 0] = 0;
			//r_solid[15 * 16 + 0] = 0;
			//r_solid[15 * 16 + 1] = 0;

			//r_solid[255] = 0;
			//r_solid[254] = 0;
			//r_solid[14 * 16 + 15] = 0;


			//ObstructionTiles.Add(new VspObstructionTile(this, new int[256]));
			//ObstructionTiles.Add(new VspObstructionTile(this, solid));
			//ObstructionTiles.Add(new VspObstructionTile(this, r_solid));
			//ObstructionTiles.Add(new VspObstructionTile(this, ld));
			//ObstructionTiles.Add(new VspObstructionTile(this, rd));

			for (int x = 0; x < 16; x++) //NUM_OBS
			{
				bool left = (x & 1) != 0;
				bool right = (x & 2) != 0;
				bool up = (x & 4) != 0;
				bool down = (x & 8) != 0;
				int[] tile = new int[256];
				if (left) for (int i = 0; i < 16; i++) tile[i * 16] = 1;
				if (right) for (int i = 0; i < 16; i++) tile[i * 16 + 15] = 1;
				if (up) for (int i = 0; i < 16; i++) tile[i] = 1;
				if (down) for (int i = 0; i < 16; i++) tile[i + 240] = 1;
				if (up && down && left && right) for (int i = 0; i < 256; i++) tile[i] = 1;
				//for (int i = 0; i < 256; i++) tile[i] = tile[i] == 1 ? 0 : 1;
				ObstructionTiles.Add(new VspObstructionTile(this, tile));
			}
		}
		public ArrayList GetTiles(int count)
		{
			// add count blank tiles
			ArrayList tiles = new ArrayList();
			for (int i = 0; i < count; i++)
			{
				Vsp24Tile vt = new Vsp24Tile(this);
				tiles.Add(vt);
			}
			return tiles;
		}
		public ArrayList GetTiles(Vsp24 vsp)
		{
			ArrayList tiles = new ArrayList();
			foreach (Vsp24Tile vt in vsp.Tiles)
			{
				Vsp24Tile vtt = vt.Clone();
				vtt.parent = this;
				tiles.Add(vtt);
			}
			return tiles;
		}

		public unsafe ArrayList ImportTiles(Bitmap bmp, int gridsize)
		{
			ArrayList tiles = new ArrayList();
			BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			int* ptr = (int*)bd.Scan0;
			int pitch = bd.Stride / sizeof(int*);
			int tw, th;

			try
			{
				tw = (bmp.Width - gridsize) / (Global.TILE_SIZE + gridsize);
				th = (bmp.Height - gridsize) / (Global.TILE_SIZE + gridsize);

				int xofs = Global.TILE_SIZE + gridsize;
				int yofs = Global.TILE_SIZE + gridsize;

				ptr += pitch * gridsize;

				int count = 0;

				for (int y = 0; y < th; y++)
				{
					for (int x = 0; x < tw; x++)
					{
						int[] t = new int[Global.TILE_SIZE * Global.TILE_SIZE];
						for (int yy = 0; yy < Global.TILE_SIZE; yy++)
						{
							for (int xx = 0; xx < Global.TILE_SIZE; xx++)
							{
								t[yy * Global.TILE_SIZE + xx] = ptr[gridsize + (x * xofs) + yy * pitch + xx];
							}
						}
						Vsp24Tile vt = new Vsp24Tile(this, new pr2.BufferImage(Global.TILE_SIZE, Global.TILE_SIZE, t));
						tiles.Add(vt);
						count++;
					}
					ptr += yofs * pitch;
				}
				return tiles;

			}
			finally
			{
				bmp.UnlockBits(bd);
			}
		}

		public ArrayList AddBlankObstructionTiles(int count)
		{
			ArrayList tiles = new ArrayList();
			// add count blank tiles
			for (int i = 0; i < count; i++)
			{
				VspObstructionTile vt = new VspObstructionTile(this, new int[Global.TILE_SIZE * Global.TILE_SIZE]);
				tiles.Add(vt);
			}
			return tiles;
		}
		public ArrayList ImportObstructionTiles(Vsp24 vsp)
		{
			ArrayList tiles = new ArrayList();
			foreach (VspObstructionTile vt in vsp.ObstructionTiles)
			{
				VspObstructionTile vot = vt.Clone();
				vot.parent = this;
				tiles.Add(vot);
			}
			return tiles;
		}
		private bool nonZero(int _c)
		{
			Color c = Color.FromArgb(_c);
			if (c.R == 0 && c.G == 0 && c.B == 0)
				return false;
			return true;
		}

		public unsafe ArrayList ImportObstructionTiles(Bitmap bmp, int gridsize)
		{
			ArrayList tiles = new ArrayList();
			BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			int* ptr = (int*)bd.Scan0;
			int pitch = bd.Stride / sizeof(int*);

			int tw = (bmp.Width - gridsize) / (Global.TILE_SIZE + gridsize);
			int th = (bmp.Height - gridsize) / (Global.TILE_SIZE + gridsize);

			int xofs = Global.TILE_SIZE + gridsize;
			int yofs = Global.TILE_SIZE + gridsize;

			try
			{
				ptr += pitch * gridsize;

				int count = 0;

				for (int y = 0; y < th; y++)
				{
					for (int x = 0; x < tw; x++)
					{
						int[] t = new int[Global.TILE_SIZE * Global.TILE_SIZE];
						for (int yy = 0; yy < Global.TILE_SIZE; yy++)
							for (int xx = 0; xx < Global.TILE_SIZE; xx++)
							{
								int c = ptr[gridsize + (x * xofs) + yy * pitch + xx];
								if (nonZero(c))
									t[yy * Global.TILE_SIZE + xx] = c;
							}
						VspObstructionTile vt = new VspObstructionTile(this, t);
						tiles.Add(vt);
						count++;
					}
					ptr += yofs * pitch;
				}
				return tiles;
			}
			finally
			{
				bmp.UnlockBits(bd);
			}
		}

		public Bitmap ExportToBitmap()
		{
			return ExportToBitmap(0);
		}

		public Bitmap ExportToBitmap(int GridSize)
		{
			int th = tileCount / Global.VSP_SIZE_TILES + 1;
			int h = th * (Global.TILE_SIZE + GridSize) + (GridSize);
			int w = Global.VSP_SIZE_PIXELS + (GridSize * (Global.VSP_SIZE_TILES + 1));

			Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);

			using (pr2.IRenderImage img = pr2.RenderImage.LockBitmap(bmp))
			{
				// render stuffs
				for (int y = 0; y < th; y++)
				{
					for (int x = 0; x < Global.VSP_SIZE_TILES && y * Global.VSP_SIZE_TILES + x < tileCount; x++)
					{
						Render.render(img, GridSize + x * (Global.TILE_SIZE + GridSize), GridSize + y * (Global.TILE_SIZE + GridSize), GetTile(y * Global.VSP_SIZE_TILES + x).Image, true);
					}
				}
			}

			return bmp;
		}

		private int m_rgba(Color c)
		{
			return unchecked(((int)(0xFF000000) | (c.R) | (c.G << 8) | (c.B << 16)));
		}
		public unsafe void ExportToClipboard()
		{
			ExportToClipboard(5);
		}
		public unsafe void ExportToClipboard(int GridSize)
		{
			int th = tileCount / Global.VSP_SIZE_TILES + 1;
			int h = th * (Global.TILE_SIZE + GridSize) + GridSize;
			int w = Global.VSP_SIZE_PIXELS + (GridSize * (Global.VSP_SIZE_TILES+1));
			Bitmap bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);

			using (pr2.IRenderImage img = pr2.RenderImage.LockBitmap(bmp))
			{
				// render stuffs
				for (int y = 0; y < th; y++)
				{
					for (int x = 0; x < Global.VSP_SIZE_TILES && y * Global.VSP_SIZE_TILES + x < tileCount; x++)
					{
						Render.render(img, GridSize + x * (Global.TILE_SIZE + GridSize), GridSize + y * (Global.TILE_SIZE + GridSize), GetTile(y * Global.VSP_SIZE_TILES + x).Image, true);
					}
				}
			}

			WindowsClipboard.setBitmap(bmp);
		}
	}

	public class VspObstructionTile
	{
		public Vsp24 parent;
		private pr2.IRenderImage image;

		public VspObstructionTile(Vsp24 parent, int[] data)
		{
			this.parent = parent;
			this.image = new pr2.BufferImage(Global.TILE_SIZE, Global.TILE_SIZE, data);
		}

		public VspObstructionTile Clone()
		{
			VspObstructionTile vot = new VspObstructionTile(parent, image.GetArray());
			return vot;
		}

		public pr2.IRenderImage Image
		{
			get
			{
				return image;
			}
		}
	}

	// TODO: maybe implement IDisposable -- andy 23 November 2007
	public class Vsp24Tile
	{
		public Vsp24 parent;
		private pr2.IRenderImage image;
		private int avg;

		public Vsp24Tile(Vsp24 parent)
			: this(parent, new pr2.BufferImage(Global.TILE_SIZE, Global.TILE_SIZE))
		{
		}

		public Vsp24Tile(Vsp24 parent, pr2.IRenderImage image)
		{
			this.parent = parent;
			this.image = image;
			this.avg = GetAverageColor();
		}

		public Vsp24Tile Clone()
		{
			Vsp24Tile vt = new Vsp24Tile(parent, image.Clone());
			return vt;
		}

		int GetAverageColor()
		{
			int cr = 0, cg = 0, cb = 0;
			int ca = 0;
			for (int y = 0; y < Global.TILE_SIZE; y++)
			{
				for (int x = 0; x < Global.TILE_SIZE; x++)
				{
					int px = image.GetPixel(y, x);
					if ((px & 0x00FFFFFF) == 0x00FF00FF) continue;
					ca += 256;
					cr += (px & 0x00FF0000) >> 16;
					cg += (px & 0x0000FF00) >> 8;
					cb += (px & 0x000000FF);
				}
			}
			ca >>= 8;
			//ca = ca * ca;
			//ca = ca >> 8;
			//ca = ca * ca;
			//ca = ca >> 8;
			if (ca >= 255) ca = 255;
			return unchecked(((ca) << 24) | ((cr >> 8) << 16) | ((cg >> 8) << 8) | ((cb >> 8)));
		}

		public int ColorAverage
		{
			get
			{
				return avg;
			}
		}

		public pr2.IRenderImage Image
		{
			get
			{
				return image;
			}
		}
	}



}
