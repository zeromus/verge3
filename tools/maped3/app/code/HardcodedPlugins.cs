using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using winmaped2.Plugins;

namespace winmaped2.map_plugins
{

	class NoteTool : IMapDragTool, IMapPlugin, IMapPluginNoTileCursorFlag
	{
		public IMapTool getTool(bool lb, bool mb, bool rb, bool shift, bool ctrl, bool alt)
		{
			return noteTool;
		}

		public Guid guid { get { return new Guid("{CBF5D6F6-801A-4dee-A0E1-EC23435D5586}"); } }
		public LayerType[] layerTypes { get { return new LayerType[] { LayerType.Null, LayerType.Tile, LayerType.Zone, LayerType.Obs, LayerType.Entity, LayerType.Special_Retrace }; } }

		public static NoteTool noteTool = new NoteTool();

		Map.Note draggedNote;
		int dx, dy;

		Map.Note Poll(Map map, int px, int py)
		{
			//check for existing notes
			foreach (var n in map.Notes)
			{
				int xmin = n.x;
				int ymin = n.y;
				int xmax = xmin + Render.measureText(n.note);
				int ymax = ymin + 7;
				if (px >= xmin && px < xmax && py >= ymin && py < ymax)
				{
					return n;
				}
			}
			return null;
		}

		public void MouseDown(MapEventInfo mei)
		{
			Map.Note note = Poll(mei.editedMap, mei.current.px, mei.current.py);
			if (note != null)
			{
				if (mei.lb)
				{
					draggedNote = note;
					dx = draggedNote.x - mei.current.px;
					dy = draggedNote.y - mei.current.py;
					modified(mei);
				}
				else
				{
					mei.editedMap.Notes.Remove(note);
					modified(mei);
				}
			}
		}

		public void MouseMove(MapEventInfo mei)
		{
			if (draggedNote != null)
			{
				draggedNote.x = mei.current.px + dx;
				draggedNote.y = mei.current.py + dy;
			}
		}

		public void MouseUp(MapEventInfo mei)
		{
			//foreach (var n in mei.editedMap.Notes)
			//{

			//  int xmin = n.tx * 16 + 1;
			//  int ymin = n.ty * 16 + 1;
			//  int xmax = (xmin + Render.measureText(n.note) / 16) * 16;
			//  int ymax = ymin + 16;
			//  if (mei.current.px >= xmin && mei.current.px < xmax && mei.current.py >= ymin && mei.current.py < ymax)
			//  {
			//    //edit existing note
			//    var ibr = winmaped2.pr2.InputBox.Show("Enter your note", "Note entry", n.note);
			//    if (ibr.ReturnCode == DialogResult.OK)
			//    {
			//      n.note = ibr.Text;
			//      if (n.note == "")
			//        mei.editedMap.Notes.Remove(n);
			//    }
			//    return;
			//  }
			//}

			if (draggedNote != null && mei.clicks == 2)
			{
				var ibr = winmaped2.pr2.InputBox.Show("Edit your note", "Note entry", draggedNote.note);
				if (ibr.ReturnCode == DialogResult.OK)
				{
					draggedNote.note = ibr.Text;
				}
			}

			if(draggedNote == null && mei.lb)
			{
				var ibr = winmaped2.pr2.InputBox.Show("Enter your note", "Note entry", "default");
				if (ibr.ReturnCode == DialogResult.OK)
				{
					var note = new Map.Note();
					note.x = mei.current.px;
					note.y = mei.current.py;
					note.note = ibr.Text;

					mei.editedMap.Notes.Add(note);
					modified(mei);
				}
			}

			draggedNote = null;
		}

		//todo
		void modified(MapEventInfo mei)
		{
			//mei.opManager.addExec(new Ops.SetTile(0, mli, x, y, val));
			mei.bDirty = true;
		}
	}

	class EyedropperTool : IMapDragTileTool
	{
		public string name { get { return "Eyedropper"; } }
		public LayerType[] layerTypes { get { return new LayerType[] { LayerType.Tile, LayerType.Obs, LayerType.Zone }; } }

		public static EyedropperTool eyedropper = new EyedropperTool();

		int lastTile;

		public void MouseDown(MapEventInfo mei)
		{
			lastTile = mei.getTileDropthrough(mei.current.tx, mei.current.ty);

			if (mei.lb) { mei.ltile = lastTile; NotifyScroll(0); }
			if (mei.rb) { mei.rtile = lastTile; NotifyScroll(1); }
		}
		public void MouseMove(MapEventInfo mei) { }
		public void MouseMoveTile(MapEventInfo mei)
		{
			int tempTile = mei.getTileDropthrough(mei.current.tx, mei.current.ty);
			if (tempTile != lastTile)
			{
				if (mei.lb) { mei.ltile = tempTile; NotifyScroll(0); }
				if (mei.rb) { mei.rtile = tempTile; NotifyScroll(1); }
			}
			lastTile = tempTile;
		}
		public void MouseUp(MapEventInfo mei) { }
		void NotifyScroll(int which)
		{
			Global.VspViewer.NotifyScroll(which);
		}
	}

	class MapPanTool : IMapDragTileTool, IMapPlugin
	{
		public string name { get { return "Pan"; } }
		public LayerType[] layerTypes { get { return new LayerType[] { LayerType.Entity, LayerType.Tile, LayerType.Obs, LayerType.Zone }; } }
		public Guid guid { get { return new Guid("{7A510AD2-6C45-4010-B229-3847823164B5}"); } }

		public IMapTool getTool(bool lb, bool mb, bool rb, bool shift, bool ctrl, bool alt)
		{
			return this;
		}

		int sx, sy;
		public void MouseDown(MapEventInfo mei)
		{
			//mei.opManager.beginGroup("Map: Brush");
			sx = Global.MainMapController.hScrollBar.Value;
			sy = Global.MainMapController.vScrollBar.Value;
		}
		public void MouseMove(MapEventInfo mei) {
			int dx = mei.start.mx - mei.current.mx;
			int dy = mei.start.my - mei.current.my;
			dx /= Global.MainMapController.ZoomLevel;
			dy /= Global.MainMapController.ZoomLevel;
			int target_x = sx + dx;
			int target_y = sy + dy;
			if (target_x >= Global.MainMapController.hScrollBar.Maximum) target_x = Global.MainMapController.hScrollBar.Maximum;
			if (target_x < Global.MainMapController.hScrollBar.Minimum) target_x = Global.MainMapController.hScrollBar.Minimum;
			if (target_y >= Global.MainMapController.vScrollBar.Maximum) target_y = Global.MainMapController.vScrollBar.Maximum;
			if (target_y < Global.MainMapController.vScrollBar.Minimum) target_y = Global.MainMapController.vScrollBar.Minimum;
			Global.MainMapController.hScrollBar.Value = target_x;
			Global.MainMapController.vScrollBar.Value = target_y;
		}
		public void MouseMoveTile(MapEventInfo mei)
		{
		}
		public void MouseUp(MapEventInfo mei)
		{
		}
		protected virtual int getValue(MapEventInfo mei)
		{
			return 0;
		}
	}

	class BrushTool : IMapDragTileTool, IMapPlugin
	{
		public string name { get { return "Brush"; } }
		public LayerType[] layerTypes { get { return new LayerType[] { LayerType.Tile, LayerType.Obs, LayerType.Zone }; } }
		public Guid guid { get { return new Guid("{7A510AD2-6C45-4010-B229-D563EDBF64B5}"); } }

		public IMapTool getTool(bool lb, bool mb, bool rb, bool shift, bool ctrl, bool alt)
		{
			if (ctrl)
			{
				if (lb || rb) return EyedropperTool.eyedropper;
				return null;
			}
			return this;

		}

		public void MouseDown(MapEventInfo mei)
		{
			mei.opManager.beginGroup("Map: Brush");
		}
		public void MouseMove(MapEventInfo mei) { }
		public void MouseMoveTile(MapEventInfo mei)
		{
			winmaped2.PrimitiveDrawer.DrawLine(mei.previous.tx, mei.previous.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cb), mei);
			//mei.invalidate();
		}
		public void MouseUp(MapEventInfo mei)
		{
			mei.opManager.endGroup();
			//mei.invalidate();
		}
		protected virtual int getValue(MapEventInfo mei)
		{
			if (mei.editedLayer.type == LayerType.Tile || mei.editedLayer.type == LayerType.Obs || mei.editedLayer.type == LayerType.Zone)
			{
				if (mei.lb)
					return mei.ltile;
				if (mei.rb)
					return mei.rtile;
				//throw new Exception("wtf");
				return 0;
			}
			return 0;
		}
		protected void cb(int x, int y, object tag)
		{
			int i = getValue((MapEventInfo)tag);
			((MapEventInfo)tag).setTile(x, y, i);
		}
	}

	public abstract class DragToolBase : IMapDragTileTool, IMapPainter, IMapPlugin
	{
		protected MapEventInfo mei;
		protected pr2.IRenderImage img;
		public abstract Guid guid { get; }


		public IMapTool getTool(bool lb, bool mb, bool rb, bool shift, bool ctrl, bool alt)
		{
			if (ctrl)
			{
				if (lb || rb) return EyedropperTool.eyedropper;
				return null;
			}
			return this;

		}

		public abstract string name { get; }
		public LayerType[] layerTypes { get { return new LayerType[] { LayerType.Tile, LayerType.Obs, LayerType.Zone }; } }
		protected abstract void onSet();
		protected abstract void onPaint();
		protected abstract void onTweak();

		public void MouseDown(MapEventInfo mei) { mei.opManager.beginGroup("Map: " + name); }
		public void MouseMove(MapEventInfo mei) { }
		public void MouseMoveTile(MapEventInfo mei) { }
		public void MouseUp(MapEventInfo mei)
		{
			this.mei = mei;
			onSet();
			mei.opManager.endGroup();
		}
		public void paintWindow(MapEventInfo mdi, Graphics g) { }

		public void tweakLayer(MapEventInfo mei)
		{
			this.mei = mei;
			onTweak();
		}

		public void paintMap(MapEventInfo mei, pr2.IRenderImage img)
		{
			this.mei = mei;
			this.img = img;
			onPaint();
		}

	}

	public abstract class BasicDragTool : DragToolBase
	{
		//protected BasicTools bt;
		//protected BasicDragTool(BasicTools bt) { this.bt = bt; }

		protected void cbSet(int x, int y, object tag)
		{
			if (mei.editedLayer.type == LayerType.Tile)
				mei.setTile(x, y, mei.tile);
			else if (mei.editedLayer.type == LayerType.Obs || mei.editedLayer.type == LayerType.Zone)
			{
				if (mei.lb)
					mei.setTile(x, y, mei.ltile);
				else if (mei.rb)
					mei.setTile(x, y, 0);
			}
		}

		protected void cbPaint(int x, int y, object tag)
		{
		}

		protected void cbTweak(int x, int y, object tag)
		{
			//if(bt.overwrite)
			if (mei.editedLayer.type == LayerType.Tile || mei.editedLayer.type == LayerType.Obs || mei.editedLayer.type == LayerType.Zone)
				mei.setTile(x, y, mei.tile);
		}
	}


	class RectFill : BasicDragTool
	{
		//public RectFill(BasicTools bt) : base(bt) {}
		public override Guid guid { get { return new Guid("{8F12618D-332F-40e6-B667-9CA0F9311D6F}"); } }
		public override string name { get { return "RectFill"; } }
		protected override void onSet() { PrimitiveDrawer.DrawFilledRectangle(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbSet), null); }
		protected override void onPaint() { PrimitiveDrawer.DrawFilledRectangle(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbPaint), null); }
		protected override void onTweak() { PrimitiveDrawer.DrawFilledRectangle(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbTweak), null); }
	}

	class Rectangle : BasicDragTool
	{
		//public Rectangle(BasicTools bt) : base(bt) {}
		public override Guid guid { get { return new Guid("{50A6E862-93BF-4edd-9C97-5701D5D6D817}"); } }
		public override string name { get { return "Rectangle"; } }
		protected override void onSet() { winmaped2.PrimitiveDrawer.DrawRectangle(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbSet), null); }
		protected override void onPaint() { winmaped2.PrimitiveDrawer.DrawRectangle(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbPaint), null); }
		protected override void onTweak() { winmaped2.PrimitiveDrawer.DrawRectangle(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbTweak), null); }
	}

	class Line : BasicDragTool
	{
		//public Line(BasicTools bt) : base(bt) {}
		public override Guid guid { get { return new Guid("{E2250EA3-0E7A-4977-ACD4-8913775D26D4}"); } }
		public override string name { get { return "Line"; } }
		protected override void onSet() { winmaped2.PrimitiveDrawer.DrawLine(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbSet), null); }
		protected override void onPaint() { winmaped2.PrimitiveDrawer.DrawLine(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbPaint), null); }
		protected override void onTweak() { winmaped2.PrimitiveDrawer.DrawLine(mei.start.tx, mei.start.ty, mei.current.tx, mei.current.ty, new PrimitiveDrawer.Callback(cbTweak), null); }
	}


	class ClipboardPlugin : IMapPlugin
	{
		public Guid guid { get { return new Guid("{BBC38AB0-5872-4862-A629-6F82AE0E62DC}"); } }

		class ClipboardPaster : IMapDragTileTool, IMapPainter, IMapTweaker
		{
			public ClipboardPaster(bool bMultilayer) { this.bMultilayer = bMultilayer; }
			bool bMultilayer;
			MapEventInfo mei;

			public LayerType[] layerTypes { get { return new LayerType[] { LayerType.Tile, LayerType.Obs, LayerType.Zone }; } }

			public void tweakMap(MapEventInfo mei)
			{
				if (Global.clipboard == null)
					return;

				this.mei = mei;
				work(mei.current);
			}

			public void tweakLayer(MapEventInfo mei) { }
			public void paintMap(MapEventInfo mei, pr2.IRenderImage img) { }
			public void paintWindow(MapEventInfo mei, Graphics g) { }

			void work(MapCursorLocation loc)
			{
				if (bMultilayer)
				{
					foreach (MapLayer ml in Global.clipboard.Layers)
					{
						for (int y = 0; y < ml.Height; y++)
							for (int x = 0; x < ml.Width; x++)
								mei.setTile(ml.name, x + loc.tx, y + loc.ty, ml.getTile(x, y));

					}
				}
				else
				{
					MapLayer ml = (MapLayer)Global.clipboard.Layers[0];
					for (int y = 0; y < ml.Height; y++)
						for (int x = 0; x < ml.Width; x++)
							mei.setTile(x + loc.tx, y + loc.ty, ml.getTile(x, y));
				}
			}

			public void MouseDown(MapEventInfo mei)
			{
				mei.bDirty = true;
			}
			public void MouseMove(MapEventInfo mei) { }
			public void MouseMoveTile(MapEventInfo mei)
			{
				mei.bDirty = true;
			}
			public void MouseUp(MapEventInfo mei)
			{
				if (Global.clipboard == null)
					return;

				Global.opManager.beginGroup("Map: Clipboard Paste");
				this.mei = mei;
				work(mei.end);
				Global.opManager.endGroup();

				mei.bDirty = true;
			}
		}

		class ClipboardCopier : IMapDragTileTool, IMapPainter
		{
			public ClipboardCopier(bool bMultilayer) { this.bMultilayer = bMultilayer; }
			bool bMultilayer;

			public LayerType[] layerTypes { get { return new LayerType[] { LayerType.Tile, LayerType.Zone, LayerType.Obs }; } }

			public void MouseMoveTile(MapEventInfo mei)
			{
				int _x0 = mei.start.tx;
				int _y0 = mei.start.ty;
				int _x1 = mei.current.tx;
				int _y1 = mei.current.ty;
				int x0 = Math.Min(_x0, _x1);
				int x1 = Math.Max(_x0, _x1);
				int y0 = Math.Min(_y0, _y1);
				int y1 = Math.Max(_y0, _y1);

				Map m = new Map();

				if (bMultilayer)
					m = mei.editedMap.tileCopyRange(x0, y0, x1 - x0 + 1, y1 - y0 + 1);
				else
				{
					MapLayer ml = mei.editedLayer.copyRange(x0, y0, x1 - x0 + 1, y1 - y0 + 1);
					ml.parentmap = m;
					m.AddLayer(ml);
				}
				m.IsBrush = true;
				m.vsp = mei.editedMap.vsp;
				Global.clipboard = m;
				mei.bDirty = true;
			}
			public void MouseDown(MapEventInfo mei)
			{
				mei.bDirty = true;
			}
			public void MouseMove(MapEventInfo mei) { }
			public void MouseUp(MapEventInfo mei)
			{
				mei.bDirty = true;
			}

			public void tweakLayer(MapEventInfo mei) { }
			public void paintMap(MapEventInfo mei, pr2.IRenderImage img)
			{

			}
			public void paintWindow(MapEventInfo mei, Graphics g)
			{
				int _x0 = (mei.start.tx * Global.TILE_SIZE - mei.xScroll);
				int _y0 = (mei.start.ty * Global.TILE_SIZE - mei.yScroll);
				int _x1 = (mei.current.tx * Global.TILE_SIZE - mei.xScroll);
				int _y1 = (mei.current.ty * Global.TILE_SIZE - mei.yScroll);
				int x0 = Math.Min(_x0, _x1);
				int x1 = Math.Max(_x0, _x1);
				int y0 = Math.Min(_y0, _y1);
				int y1 = Math.Max(_y0, _y1);
				x0 *= mei.zoom;
				x1 *= mei.zoom;
				y0 *= mei.zoom;
				y1 *= mei.zoom;
				System.Drawing.Rectangle r = new System.Drawing.Rectangle(x0, y0, x1 - x0 + mei.zoom * Global.TILE_SIZE, y1 - y0 + mei.zoom * Global.TILE_SIZE);


				Pen pen = new Pen(Color.White);
				pen.DashStyle = DashStyle.Dash;
				pen.Width = 1;
				g.DrawRectangle(pen, r);
			}
		}


		public IMapTool getTool(bool lb, bool mb, bool rb, bool shift, bool ctrl, bool alt)
		{
			if (shift)
				return new ClipboardCopier(ctrl);
			else
				return new ClipboardPaster(ctrl);
		}

	}

	class FloodFillTool : IMapClickTool, IMapPlugin
	{
		public string name { get { return "FloodFill"; } }
		public LayerType[] layerTypes { get { return new LayerType[] { LayerType.Tile, LayerType.Obs }; } }
		public Guid guid { get { return new Guid("{29B9855E-E0F7-4b30-A9B0-1C2A28EDBB60}"); } }

		public IMapTool getTool(bool lb, bool mb, bool rb, bool shift, bool ctrl, bool alt)
		{
			if (ctrl)
			{
				if (lb || rb) return EyedropperTool.eyedropper;
				return null;
			}
			return this;

		}

		private int oldvalue;
		private int newvalue;
		public void MouseClick(MapEventInfo mei)
		{
			oldvalue = mei.getTile(mei.start.tx, mei.start.ty);
			newvalue = mei.tile;
			if (oldvalue != newvalue)
			{
				mei.opManager.beginGroup("Map: FloodFill");
				PrimitiveDrawer.FloodFill(mei.start.tx, mei.start.ty, new PrimitiveDrawer.CheckCallback(ccb), new PrimitiveDrawer.Callback(cb), mei);
				mei.opManager.endGroup();
			}
		}


		protected bool ccb(int x, int y, object tag) { return (((MapEventInfo)tag).getTile(x, y) == oldvalue); }
		protected void cb(int x, int y, object tag) { ((MapEventInfo)tag).setTile(x, y, newvalue); }
	}



}