using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace winmaped2
{

	public class vspView : Panel
	{
		/**************************************************/
		/* sub controls                                   */
		/**************************************************/
		VScrollBar scrollbar;
		VSPController.ControllerMode controller_mode;
		VSPController.ControllerType controller_type;
		TileViewer viewer_a, viewer_b;

		/**************************************************/
		/* variables                                      */
		/**************************************************/
		int TilesWide
		{
			get
			{
				return Width / Global.TILE_SIZE;
			}
		}
		int TilesHigh
		{
			get
			{
				return Height / Global.TILE_SIZE;
			}
		}
		int st0, st1;
		int scrollOffset;
		Vsp24 vsp;

		/**************************************************/
		/* public properties                              */
		/**************************************************/
		public int SelectedTileF
		{
			get
			{
				return st0;
			}
			set
			{
				if (value < 0)
				{
					return;
				}
				st0 = value;
				Invalidate();
				if (viewer_a == null)
				{
					return;
				}
				viewer_a.ActiveTile = (Vsp24Tile)vsp.Tiles[st0];
				if (SelectionChanged != null)
				{
					SelectionChanged();
				}
			}
		}
		public int SelectedTileB
		{
			get
			{
				return st1;
			}
			set
			{
				if (value < 0)
				{
					return;
				}
				st1 = value;
				Invalidate();
				if (viewer_b == null)
				{
					return;
				}
				viewer_b.ActiveTile = (Vsp24Tile)vsp.Tiles[st1];
				if (SelectionChanged != null)
				{
					SelectionChanged();
				}
			}
		}
		public VSPController.ControllerMode ControllerMode
		{
			get
			{
				return controller_mode;
			}
			set
			{
				controller_mode = value;
			}
		}
		public VSPController.ControllerType ControllerType
		{
			get
			{
				return controller_type;
			}
			set
			{
				controller_type = value;
			}
		}
		protected override Size DefaultSize
		{
			get
			{
				return new Size(Global.VSP_SIZE_PIXELS, Global.VSP_SIZE_PIXELS_HEIGHT);
			}
		}
		public TileViewer PrimaryTileViewer
		{
			get
			{
				return viewer_a;
			}
			set
			{
				viewer_a = value;
			}
		}
		public TileViewer SecondaryTileViewer
		{
			get
			{
				return viewer_b;
			}
			set
			{
				viewer_b = value;
			}
		}
		public Vsp24 ActiveVsp
		{
			get
			{
				return vsp;
			}
			set
			{
				vsp = value;
			}
		}

		public event SEventHandler SelectionChanged;

		/**************************************************/
		/* constructors                                   */
		/**************************************************/
		public vspView(VScrollBar scroller, VSPController.ControllerMode controller_mode, VSPController.ControllerType controller_type, TileViewer primaryTileViewer)
			: this(scroller, controller_mode, controller_type)
		{

		}
		public vspView(VScrollBar scroller, VSPController.ControllerMode controller_mode, VSPController.ControllerType controller_type, TileViewer primaryTileViewer, TileViewer secondaryTileViewer)
			: this(scroller, controller_mode, controller_type)
		{
		}
		public vspView(VScrollBar scroller, VSPController.ControllerMode controller_mode, VSPController.ControllerType controller_type)
			: this()
		{
			this.scrollbar = scroller;
			this.controller_mode = controller_mode;
		}
		public vspView()
		{
			Size = new Size(TilesWide * Global.TILE_SIZE, TilesHigh * Global.TILE_SIZE);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.Opaque, true);
			SetStyle(ControlStyles.UserMouse, true);

			//prevent the context menu from showing up from the underlying form when the right mouse is used to select a tile
			ContextMenu = new ContextMenu();
		}

		public void NotifyScroll(int which)
		{
			int num = which == 0 ? SelectedTileF : SelectedTileB;
			if (num == -1) return;
			int top = num / Global.VSP_SIZE_TILES;
			top *= Global.TILE_SIZE;
			//dont scroll if we're already visible
			int myscroll = scrollOffset / Global.TILE_SIZE * Global.TILE_SIZE;
			if (top >= myscroll && top < myscroll + Height)
			{
			}
			else
			{
				top -= Height/2;
				if (top < scrollbar.Minimum) top = scrollbar.Minimum;
				if (top >= scrollbar.Maximum) top = scrollbar.Maximum;
				scrollbar.Value = top;
			}
		}

		public void ResetView()
		{
			st0 = 0;
			st1 = 0;
			scrollOffset = 0;
			Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if (vsp == null)
			{
				return;
			}

			const int WHITE = unchecked((int)0xFFFFFFFF);

			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

			CalculateScrollValues();

			Bitmap bmp = new Bitmap(TilesWide * Global.TILE_SIZE, TilesHigh * Global.TILE_SIZE, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (pr2.IRenderImage qimg = pr2.RenderImage.LockBitmap(bmp))
			{
				if (ControllerType == VSPController.ControllerType.VSP)
				{
					int row = 0, col = 0;
					for (int i = scrollOffset / Global.TILE_SIZE * Global.VSP_SIZE_TILES; i < vsp.Tiles.Count; i++)
					{
						Render.render(qimg, col * Global.TILE_SIZE, row * Global.TILE_SIZE, vsp.GetTile(i).Image, true);
						if (i == st0)
						{
							if (controller_mode != VSPController.ControllerMode.ViewOnly)
							{
								Render.renderBox(qimg, col * Global.TILE_SIZE, row * Global.TILE_SIZE, Global.TILE_SIZE, Global.TILE_SIZE, WHITE, Render.PixelOp.Src);
								Render.renderBox(qimg, col * Global.TILE_SIZE + 1, row * Global.TILE_SIZE + 1, Global.TILE_SIZE - 2, Global.TILE_SIZE - 2, WHITE, Render.PixelOp.Src);
							}
						}
						if (i == st1)
						{
							if (controller_mode == VSPController.ControllerMode.SelectorDual)
							{
								Render.renderBox(qimg, col * Global.TILE_SIZE + 2, row * Global.TILE_SIZE + 2, Global.TILE_SIZE - 4, Global.TILE_SIZE-4, WHITE, Render.PixelOp.Src);
								Render.renderBox(qimg, col * Global.TILE_SIZE + 3, row * Global.TILE_SIZE + 3, Global.TILE_SIZE - 6, Global.TILE_SIZE-6, WHITE, Render.PixelOp.Src);
							}
						}
						col++;
						if (col == TilesWide)
						{
							col = 0;
							row++;
							if (row == TilesHigh)
								break;
						}
					}
				}
				else if (ControllerType == VSPController.ControllerType.Obstruction)
				{
					// render obs tiles
					int row = 0, col = 0;
					for (int i = scrollOffset / Global.TILE_SIZE * Global.VSP_SIZE_TILES; i < vsp.ObstructionTiles.Count; i++)
					{
						VspObstructionTile vot = ((VspObstructionTile)vsp.ObstructionTiles[i]);
						Render.renderObsTile(qimg, col * Global.TILE_SIZE, row * Global.TILE_SIZE, vot.Image, true, Preferences.Current.ObsColor);
						if (i == st0)
						{
							if (controller_mode != VSPController.ControllerMode.ViewOnly)
							{
								Render.renderBox(qimg, col * Global.TILE_SIZE, row * Global.TILE_SIZE, Global.TILE_SIZE, Global.TILE_SIZE, WHITE, Render.PixelOp.Src);
								Render.renderBox(qimg, col * Global.TILE_SIZE + 1, row * Global.TILE_SIZE + 1, Global.TILE_SIZE - 2, Global.TILE_SIZE - 2, WHITE, Render.PixelOp.Src);
							}
						}
						col++;
						if (col == TilesWide)
						{
							col = 0;
							row++;
							if (row == TilesHigh)
								break;
						}
					}
				}
			}

			e.Graphics.DrawImage(bmp, 0, 0, Size.Width, Size.Height);
			bmp.Dispose();

			if (trackingDrag)
			{
				int _x0 = dragStart.X * Global.TILE_SIZE;
				int _y0 = (dragStart.Y * Global.TILE_SIZE);
				int _x1 = (dragEnd.X * Global.TILE_SIZE);
				int _y1 = (dragEnd.Y * Global.TILE_SIZE);
				int x0 = Math.Min(_x0, _x1);
				int x1 = Math.Max(_x0, _x1);
				int y0 = Math.Min(_y0, _y1);
				int y1 = Math.Max(_y0, _y1);
				System.Drawing.Rectangle r = new System.Drawing.Rectangle(x0, y0, x1 - x0 + Global.TILE_SIZE, y1 - y0 + Global.TILE_SIZE);
				Pen pen = new Pen(Color.White);
				pen.DashStyle = DashStyle.Dash;
				pen.Width = 1;
				e.Graphics.DrawRectangle(pen, r);
			}

		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (scrollbar == null) return;
			int v = scrollbar.Value;
			if (e.Delta != 0)
			{
				int delta = e.Delta / 120;
				v -= delta * Global.VSP_SIZE_TILES;
				if (v < scrollbar.Minimum) v = scrollbar.Minimum;
				if (v > scrollbar.Maximum) v = scrollbar.Maximum;
				scrollbar.Value = v;
			}
		}

		public void SetObstructionViewerA(int tileIndex)
		{
			st0 = tileIndex;
			viewer_a.ActiveObsTile = (VspObstructionTile)vsp.ObstructionTiles[tileIndex];
			viewer_a.ActiveObsTileIndex = tileIndex;
			if (SelectionChanged != null) SelectionChanged();
			Invalidate();
		}

		long autoscrollTimestamp;
		bool trackingMouse = false;
		bool trackingDrag = false;
		Point dragStart, dragEnd;
		private void activateMouse(bool move, MouseEventArgs e)
		{
			int mx = e.X, my = e.Y;
			if (mx < 0) mx = 0;
			if (mx >= Width) mx = Width - 1;
			if (my < 0) my = 0;
			if (my >= Height) my = Height - 1;

			if (trackingDrag)
			{
				if (!move)
				{
					dragEnd = dragStart = new Point(mx / Global.TILE_SIZE, my / Global.TILE_SIZE);
					Refresh();
				}
				else
				{
					dragEnd = new Point(mx / Global.TILE_SIZE, my / Global.TILE_SIZE);
					bool eligible = (autoscrollTimestamp + 10000 * 300 < DateTime.Now.Ticks);
					if (my > 158 && eligible)
					{
						int v = scrollbar.Value;
						v += Global.TILE_SIZE;
						if (v < scrollbar.Maximum)
						{
							dragStart.Y -= 1;
							scrollbar.Value = v;
						}
						autoscrollTimestamp = DateTime.Now.Ticks;
					}

					if (my <= 2 && eligible)
					{
						int v = scrollbar.Value;
						v -= Global.TILE_SIZE;
						if (v > scrollbar.Minimum)
						{
							dragStart.Y += 1;
							scrollbar.Value = v;
						}
						autoscrollTimestamp = DateTime.Now.Ticks;
					}

					Refresh();
				}
				return;
			}

			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				if (ControllerType == VSPController.ControllerType.VSP)
				{
					int tile = ((scrollOffset / Global.TILE_SIZE) + (my / Global.TILE_SIZE)) * TilesWide + (mx / Global.TILE_SIZE);
					if (tile >= vsp.tileCount) return;
					Vsp24Tile vtile = (Vsp24Tile)vsp.Tiles[tile];
					if (e.Button == MouseButtons.Left)
					{
						if (viewer_a != null)
						{
							viewer_a.ActiveTile = vtile;
						}
						st0 = tile;
					}
					else if (e.Button == MouseButtons.Right)
					{
						if (viewer_b != null)
						{
							viewer_b.ActiveTile = vtile;
						}
						st1 = tile;
					}
				}
				else if (ControllerType == VSPController.ControllerType.Obstruction)
				{
					int tile = ((scrollOffset / Global.TILE_SIZE) + (my / Global.TILE_SIZE)) * TilesWide + (mx / Global.TILE_SIZE);
					if (tile >= vsp.ObstructionTiles.Count)
					{
						return;
					}
					if (e.Button == MouseButtons.Left)
					{
						st0 = tile;
						if (viewer_a != null)
						{
							SetObstructionViewerA(tile);
						}
					}
					if (e.Button == MouseButtons.Right) st1 = tile;
				}
				if (SelectionChanged != null) SelectionChanged();
				Invalidate();
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Capture = false;
			trackingMouse = false;
			if (trackingDrag)
			{
				trackingDrag = false;

				int _x0 = dragStart.X;
				int _y0 = (dragStart.Y);
				int _x1 = (dragEnd.X);
				int _y1 = (dragEnd.Y);
				int x0 = Math.Min(_x0, _x1);
				int x1 = Math.Max(_x0, _x1);
				int y0 = Math.Min(_y0, _y1);
				int y1 = Math.Max(_y0, _y1);

				Map m = new Map();
				MapLayer ml = new MapLayer(m);
				ml.resize(x1 - x0 + 1, y1 - y0 + 1);
				for (int y = y0; y <= y1; y++)
				{
					for (int x = x0; x <= x1; x++)
					{
						//((scrollOffset / Global.TILE_SIZE) + (my / Global.TILE_SIZE)) * TilesWide + (mx / Global.TILE_SIZE);
						int tile = y * TilesWide + x + (scrollOffset / Global.TILE_SIZE * TilesWide);
						ml.setTile(x - x0, y - y0, tile);
					}
				}
				m.AddLayer(ml);
				m.IsBrush = true;
				m.vsp = vsp;
				Global.clipboard = m;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (vsp == null)
			{
				return;
			}
			if (trackingMouse)
			{
				activateMouse(true, e);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (vsp == null)
			{
				return;
			}
			Focus();
			bool shift = ((Control.ModifierKeys & Keys.Shift) != 0);
			trackingMouse = true;
			if (shift)
				trackingDrag = true;
			activateMouse(false, e);
			Capture = true;
		}

		public void OnScroll(object sender, EventArgs e)
		{
			Invalidate();
			scrollOffset = scrollbar.Value;
		}

		public void CalculateScrollValues()
		{
			if (vsp == null)
			{
				if (scrollbar != null)
				{
					scrollbar.Enabled = false;
					return;
				}
			}

			// set values
			scrollbar.Minimum = 0;
			scrollbar.LargeChange = Height - 4;
			scrollbar.SmallChange = Global.TILE_SIZE;
			if (ControllerType == VSPController.ControllerType.VSP)
			{
				if (vsp.Tiles.Count / TilesWide * Global.TILE_SIZE + Global.TILE_SIZE > Height)
				{
					scrollbar.Maximum = vsp.Tiles.Count / TilesWide * Global.TILE_SIZE + Global.TILE_SIZE;
					scrollbar.Enabled = true;
				}
				else
				{
					scrollbar.Enabled = false;
				}
			}
			else if (ControllerType == VSPController.ControllerType.Obstruction)
			{
				if (vsp.ObstructionTiles.Count / TilesWide * Global.TILE_SIZE + Global.TILE_SIZE > Height)
				{
					scrollbar.Enabled = true;
					scrollbar.Maximum = vsp.ObstructionTiles.Count / TilesWide * Global.TILE_SIZE + Global.TILE_SIZE;
				}
				else
				{
					scrollbar.Enabled = false;
				}
			}
		}
		public void ForceRedraw()
		{
			Invalidate();
		}
	}
}