using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using RegisterFileType;
using System.Threading;
using System.Diagnostics;

namespace winmaped2
{
	/// <summary>
	/// Summary description for mainWindow.
	/// </summary>
	public partial class MainWindow : System.Windows.Forms.Form
	{

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem miExit;
		private System.Windows.Forms.MenuItem miOpenMap;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.Panel toolPanel;
		private System.Windows.Forms.Panel mapPanel;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MenuItem miHelp;
		private System.Windows.Forms.MenuItem miAbout;
		private System.Windows.Forms.MenuItem miView;
		private System.Windows.Forms.ImageList toolbarImages;
		private System.Windows.Forms.MenuItem miEditZones;
		private System.Windows.Forms.MenuItem miMapProperties;
		private System.Windows.Forms.StatusBarPanel sbpCursorInfo;
		private System.Windows.Forms.StatusBarPanel sbpLoadInfo;
		private winmaped2.SizeGrip sizeGrip;
		private System.Windows.Forms.MenuItem Zoom1x;
		private System.Windows.Forms.MenuItem miZoom2x;
		private System.Windows.Forms.MenuItem miZoom4x;
		private System.Windows.Forms.MenuItem miEdit;
		private System.Windows.Forms.MenuItem miUndo;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem miNewMap;
		private System.Windows.Forms.MenuItem miFile;
		private System.Windows.Forms.MenuItem mruSeparator;
		public ArrayList lwLayers = new ArrayList();
		private System.Windows.Forms.MenuItem miSave;
		private System.Windows.Forms.MenuItem miSaveAs;
		private System.Windows.Forms.MenuItem misSave;
		private System.Windows.Forms.MenuItem miClose;
		private System.Windows.Forms.Panel mainpanel;
		private System.Windows.Forms.MenuItem miMap;
		private System.Windows.Forms.MenuItem miEditEntities;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem miVsp;
		private System.Windows.Forms.MenuItem miEditTiles;
		private System.Windows.Forms.MenuItem miEditAnims;
		private System.Windows.Forms.MenuItem miExportTiles;
		private System.Windows.Forms.OpenFileDialog openVspDialog;
		private System.Windows.Forms.OpenFileDialog openImageDialog;
		private System.Windows.Forms.MenuItem mi_rft;
		public MRU mru = new MRU(4, "SOFTWARE\\VERGE\\Maped3\\RecentlyUsed");
		private winmaped2.ToolPalette toolPalette;
		private winmaped2.ToolPalette.ToolButton radioButton5;
		private winmaped2.ToolPalette.ToolButton radioButton3;
		private winmaped2.ToolPalette.ToolButton radioButton2;
		private winmaped2.ToolPalette.ToolButton radioButton1;
		private winmaped2.ToolPalette.ToolButton radioButton4;
		private winmaped2.LayerPanel lPanel;
		private winmaped2.VSPController vspController;
		private System.Windows.Forms.Panel p_ents;
		private System.Windows.Forms.Label l_sent;
		private System.Windows.Forms.Button b_editents;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView lv_ents;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel p_zones;
		private System.Windows.Forms.Label l_szone;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private winmaped2.ListViewIndexed lv_zonelist;
		private System.Windows.Forms.ColumnHeader ch_zoneid;
		private System.Windows.Forms.ColumnHeader ch_zonename;
		private System.Windows.Forms.Panel panel2;
		private winmaped2.MiniMapControl miniMap;
		private winmaped2.VSPController vspc_obs;
		private System.Windows.Forms.MenuItem miPreferences;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.GroupBox g_obs;
		private winmaped2.TileViewer tv_obs;
		private System.Windows.Forms.Button b_gotoent;
		private System.Windows.Forms.MenuItem miExportTilesToImageGrid;
		private System.Windows.Forms.SaveFileDialog saveImageDialog;
		private System.Windows.Forms.MenuItem miChangeVSP;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem miChangeVspExisting;
		private System.Windows.Forms.MenuItem miChangeVspBlank;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button b_layeradd;
		private System.Windows.Forms.Button b_layerdown;
		private System.Windows.Forms.Button b_layerup;
		private System.Windows.Forms.Button b_layerdel;
		private System.Windows.Forms.Button b_layerproperties;
		private System.Windows.Forms.MenuItem miShowHelp;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem miExportTilesToClipboardGrid;
		private System.Windows.Forms.MenuItem miRedo;
		private winmaped2.ToolPalette.ToolButton radioButton6;
		private System.Windows.Forms.SaveFileDialog saveVspDialog;
		private winmaped2.MapController mcClipboard;
		private System.Windows.Forms.MenuItem miImportTiles;
		private System.Windows.Forms.MenuItem miImportAgain;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.StatusBarPanel sbpSelection;
		private MapController mapController;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox cb_transeffects;
		private System.Windows.Forms.Label l_rstring;
		private System.Windows.Forms.StatusBarPanel sbpZoom;
		private System.Windows.Forms.Button b_runmap;
		private System.Windows.Forms.MenuItem miArrangeTiles;
		private System.Windows.Forms.Panel ctTilesP;
		System.Timers.Timer animTimer = new System.Timers.Timer(100);
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel ctMinimapP;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TrackBar tb_zoom;
		private MenuItem miExportTilesToImage;
		private MenuItem miExportTilesToClipboard;
		private FlowLayoutPanel sidebarPanel;
		private CollapsiblePanel toolPalletePanel;
		private CollapsiblePanel clipboardPanel;
		private CollapsiblePanel tilesPanel;
		private CollapsiblePanel minimapPanel;
		private MenuItem miViewToolbars;
		private MenuItem menuItem2;
		private Button btnObsAll;
		public CheckBox checkObsRight;
		public CheckBox checkObsDown;
		public CheckBox checkObsLeft;
		public CheckBox checkObsUp;
		private GroupBox g_tiles;
		private TileViewer TileViewA;
		private TileViewer TileViewB;
		private ToolPalette.ToolButton radioButton7;
		private MenuItem menuItem1;
		public MenuItem miViewNotes;
		private CollapsiblePanel layerPanel;


		public MainWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Global.toolPalette = toolPalette;
			Global.MainMapController = mapController;
			Global.ClipboardMapController = mcClipboard;
			Global.zoomChanged += new winmaped2.Global.SimpleEventHandler(Global_zoomChanged);
			Global.zoomChanged += new winmaped2.Global.SimpleEventHandler(zoomchanged);


			Global.FrameCalc.init();

			Preferences.LoadAsCurrent();

			Plugins.IMapPlugin brush, rectfill, line, rectangle, floodfill, clipboard, noteTool;
			Global.pluginManager.addPlugin(brush = new winmaped2.map_plugins.BrushTool());
			Global.pluginManager.addPlugin(rectfill = new winmaped2.map_plugins.RectFill());
			Global.pluginManager.addPlugin(line = new winmaped2.map_plugins.Line());
			Global.pluginManager.addPlugin(rectangle = new winmaped2.map_plugins.Rectangle());
			Global.pluginManager.addPlugin(floodfill = new winmaped2.map_plugins.FloodFillTool());
			Global.pluginManager.addPlugin(clipboard = new winmaped2.map_plugins.ClipboardPlugin());
			Global.pluginManager.addPlugin(noteTool = winmaped2.map_plugins.NoteTool.noteTool);

			toolPalette.registerButton(radioButton1, brush);
			toolPalette.registerButton(radioButton2, rectfill);
			toolPalette.registerButton(radioButton3, line);
			toolPalette.registerButton(radioButton4, rectangle);
			toolPalette.registerButton(radioButton5, floodfill);
			toolPalette.registerButton(radioButton6, clipboard);
			toolPalette.registerButton(radioButton7, noteTool);

			((Bitmap)radioButton1.Image).MakeTransparent(Color.Magenta);
			((Bitmap)radioButton2.Image).MakeTransparent(Color.Magenta);
			((Bitmap)radioButton3.Image).MakeTransparent(Color.Magenta);
			((Bitmap)radioButton4.Image).MakeTransparent(Color.Magenta);
			((Bitmap)radioButton5.Image).MakeTransparent(Color.Magenta);
			((Bitmap)b_runmap.Image).MakeTransparent(Color.Magenta);
			((Bitmap)b_layeradd.Image).MakeTransparent(Color.Magenta);
			((Bitmap)b_layerdel.Image).MakeTransparent(Color.Magenta);
			((Bitmap)b_layerup.Image).MakeTransparent(Color.Magenta);
			((Bitmap)b_layerdown.Image).MakeTransparent(Color.Magenta);
			((Bitmap)b_layerproperties.Image).MakeTransparent(Color.Magenta);



			Images.ImagesInit();

			vspController.SetControllerMode(VSPController.ControllerMode.SelectorDual);
			vspController.SetControllerType(VSPController.ControllerType.VSP);
			vspController.SetTileViewers(TileViewA, TileViewB);
			vspController.VspView.SelectionChanged += new SEventHandler(VspView_SelectionChanged);

			vspc_obs.SetControllerMode(VSPController.ControllerMode.SelectorSingle);
			vspc_obs.SetControllerType(VSPController.ControllerType.Obstruction);
			vspc_obs.SetTileViewers(tv_obs);
			tv_obs.TileSourceType = TileViewer.SourceType.Obstruction;

			Global.SelectedZoneChanged += new winmaped2.Global.SimpleEventHandler(Global_SelectedZoneChanged);
			Global.vspController = vspController;
			Global.obsVspController = vspc_obs;
			Global.MiniMap = miniMap;
			Global.mainWindow = this;
			Global.TileViewerA = TileViewA;
			Global.TileViewerB = TileViewB;
			Global.layerTool = lPanel;

			if (Global.VspViewer != null)
			{
				Global.VspViewer.CalculateScrollValues();
			}

			Global.WriteDestChanged += new winmaped2.Global.LEventHandler(WriteDestChanged);
			Global.CursorLocationChanged += new winmaped2.Global.xyEventHandler(Global_CursorLocationChanged);
			Global.ActiveMapChanged += new winmaped2.Global.SimpleEventHandler(Global_ActiveMapChanged);
			Global.ClipboardChanged += new winmaped2.Global.SimpleEventHandler(Global_ClipboardChanged);


			miniMap.Controller = mapController;
			mapController.AssociateMinimap(miniMap);
			mcClipboard.ZoomLevel = 1;

			Global.zoom = Preferences.Current.DefaultZoomLevel;
			Global.FrameCalc.OnTick += new winmaped2.Global.SimpleEventHandler(FrameCalc_OnTick);


			ui_update();

			animTimer.Elapsed += new System.Timers.ElapsedEventHandler(animTimer_Elapsed);
			animTimer.Start();
			animTimer.Enabled = false;

			Global_zoomChanged();

			throttleDisplay = new ThrottleBuffer(500, new EventHandler(postRedisplay));


			sidebarPanel.ContextMenu = new ContextMenu();
			foreach (Control ctrl in sidebarPanel.Controls)
			{
				if (ctrl is CollapsiblePanel)
				{
					CollapsiblePanel panel = (ctrl as CollapsiblePanel);
					MenuItem toolbarItem = panel.MenuItem.CloneMenu();

					panel.VisibleChanged += delegate(object sender, EventArgs e)
					{
						toolbarItem.Checked = panel.Visible;
					};
					miViewToolbars.MenuItems.Add(toolbarItem);

					//this is the code that adds the contextmenu that is annoying
					sidebarPanel.ContextMenu.MenuItems.Add(panel.MenuItem);
				}
			}

			// test
			//Preferences.Current.Save();

			radioButton1.Checked = true;
		
			//tilesPanel.CanLargeify = true;
			tilesPanel.OnLargeified = OnLargeifyTiles;
		}

		void OnLargeifyTiles(object o)
		{
			//ctTilesP.Height = ctTilesP.Size.Height * 2;
			vspController.Height *= 2;
		}

		public void lpAddLayer(MapLayer ml, bool IsRendered, bool IsWrite, int layer)
		{
			LPanel lw = new LPanel(lPanel, ml, IsRendered, IsWrite, layer);
			lwLayers.Add(lw);
			lPanel.Controls.Add(lw);
			lPanel.SetControlLayouts();
		}

		public void lpSwapLayer(MapLayer oldLayer, MapLayer newLayer)
		{
			foreach (Control lp in lPanel.Controls)
			{
				if (lp is LPanel)
				{
					if (((LPanel)lp).mLayerRef == oldLayer)
					{
						((LPanel)lp).mLayerRef = newLayer;
					}
				}
			}
		}

		public void lpInit(Map map)
		{
			foreach (LPanel lw in lwLayers)
			{
				lw.Dispose();
			}
			lwLayers.Clear();
			for (int i = 0; i < map.RenderManager.Layers.Count; i++)
			{
				MapLayer ml = (MapLayer)map.RenderManager.Layers[i];
				lpAddLayer(ml, true, Global.IsBaseLayer(ml), i);
			}
			//			if(Global.lpSelection==null)
			//				((LPanel)lwLayers[0]).SelectForWrite();

			l_rstring.Text = Global.ActiveMap.RenderManager.ToRenderString();
			ui_update();
		}
		public void lpDestruct()
		{
			lwLayers.Clear();
			lPanel.Controls.Clear();
			l_rstring.Text = "";
		}
		public void lpUpdate(Map map, MapLayer select)
		{
			foreach (LPanel lw in lwLayers)
			{
				lw.Dispose();
			}
			lwLayers.Clear();
			for (int i = 0; i < map.RenderManager.Layers.Count; i++)
			{
				MapLayer ml = (MapLayer)map.RenderManager.Layers[i];
				lpAddLayer(ml, true, (ml == select), i);
			}
			l_rstring.Text = Global.ActiveMap.RenderManager.ToRenderString();
			ui_update();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.miFile = new System.Windows.Forms.MenuItem();
			this.miNewMap = new System.Windows.Forms.MenuItem();
			this.miOpenMap = new System.Windows.Forms.MenuItem();
			this.miClose = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.miSave = new System.Windows.Forms.MenuItem();
			this.miSaveAs = new System.Windows.Forms.MenuItem();
			this.mruSeparator = new System.Windows.Forms.MenuItem();
			this.misSave = new System.Windows.Forms.MenuItem();
			this.miPreferences = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.miExit = new System.Windows.Forms.MenuItem();
			this.miEdit = new System.Windows.Forms.MenuItem();
			this.miUndo = new System.Windows.Forms.MenuItem();
			this.miRedo = new System.Windows.Forms.MenuItem();
			this.miMap = new System.Windows.Forms.MenuItem();
			this.miEditZones = new System.Windows.Forms.MenuItem();
			this.miEditEntities = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.miMapProperties = new System.Windows.Forms.MenuItem();
			this.miVsp = new System.Windows.Forms.MenuItem();
			this.miEditTiles = new System.Windows.Forms.MenuItem();
			this.miEditAnims = new System.Windows.Forms.MenuItem();
			this.miArrangeTiles = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.miImportTiles = new System.Windows.Forms.MenuItem();
			this.miImportAgain = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.miExportTiles = new System.Windows.Forms.MenuItem();
			this.miExportTilesToImage = new System.Windows.Forms.MenuItem();
			this.miExportTilesToImageGrid = new System.Windows.Forms.MenuItem();
			this.miExportTilesToClipboard = new System.Windows.Forms.MenuItem();
			this.miExportTilesToClipboardGrid = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.miChangeVSP = new System.Windows.Forms.MenuItem();
			this.miChangeVspExisting = new System.Windows.Forms.MenuItem();
			this.miChangeVspBlank = new System.Windows.Forms.MenuItem();
			this.miView = new System.Windows.Forms.MenuItem();
			this.miViewToolbars = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.Zoom1x = new System.Windows.Forms.MenuItem();
			this.miZoom2x = new System.Windows.Forms.MenuItem();
			this.miZoom4x = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miViewNotes = new System.Windows.Forms.MenuItem();
			this.miHelp = new System.Windows.Forms.MenuItem();
			this.miShowHelp = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.mi_rft = new System.Windows.Forms.MenuItem();
			this.miAbout = new System.Windows.Forms.MenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.sbpLoadInfo = new System.Windows.Forms.StatusBarPanel();
			this.sbpCursorInfo = new System.Windows.Forms.StatusBarPanel();
			this.sbpSelection = new System.Windows.Forms.StatusBarPanel();
			this.sbpZoom = new System.Windows.Forms.StatusBarPanel();
			this.toolPanel = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.sidebarPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.toolbarImages = new System.Windows.Forms.ImageList(this.components);
			this.mapPanel = new System.Windows.Forms.Panel();
			this.mainpanel = new System.Windows.Forms.Panel();
			this.openVspDialog = new System.Windows.Forms.OpenFileDialog();
			this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveVspDialog = new System.Windows.Forms.SaveFileDialog();
			this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
			this.mapController = new winmaped2.MapController();
			this.sizeGrip = new winmaped2.SizeGrip();
			this.toolPalletePanel = new winmaped2.CollapsiblePanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.toolPalette = new winmaped2.ToolPalette();
			this.radioButton7 = new winmaped2.ToolPalette.ToolButton();
			this.radioButton6 = new winmaped2.ToolPalette.ToolButton();
			this.b_runmap = new System.Windows.Forms.Button();
			this.radioButton5 = new winmaped2.ToolPalette.ToolButton();
			this.radioButton3 = new winmaped2.ToolPalette.ToolButton();
			this.radioButton2 = new winmaped2.ToolPalette.ToolButton();
			this.radioButton1 = new winmaped2.ToolPalette.ToolButton();
			this.radioButton4 = new winmaped2.ToolPalette.ToolButton();
			this.layerPanel = new winmaped2.CollapsiblePanel();
			this.lPanel = new winmaped2.LayerPanel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.l_rstring = new System.Windows.Forms.Label();
			this.b_layeradd = new System.Windows.Forms.Button();
			this.b_layerdown = new System.Windows.Forms.Button();
			this.b_layerup = new System.Windows.Forms.Button();
			this.b_layerdel = new System.Windows.Forms.Button();
			this.b_layerproperties = new System.Windows.Forms.Button();
			this.tilesPanel = new winmaped2.CollapsiblePanel();
			this.ctTilesP = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.vspc_obs = new winmaped2.VSPController();
			this.g_tiles = new System.Windows.Forms.GroupBox();
			this.TileViewA = new winmaped2.TileViewer();
			this.TileViewB = new winmaped2.TileViewer();
			this.vspController = new winmaped2.VSPController();
			this.p_zones = new System.Windows.Forms.Panel();
			this.l_szone = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lv_zonelist = new winmaped2.ListViewIndexed();
			this.ch_zoneid = new System.Windows.Forms.ColumnHeader();
			this.ch_zonename = new System.Windows.Forms.ColumnHeader();
			this.p_ents = new System.Windows.Forms.Panel();
			this.l_sent = new System.Windows.Forms.Label();
			this.b_editents = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.lv_ents = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.b_gotoent = new System.Windows.Forms.Button();
			this.g_obs = new System.Windows.Forms.GroupBox();
			this.btnObsAll = new System.Windows.Forms.Button();
			this.checkObsRight = new System.Windows.Forms.CheckBox();
			this.checkObsDown = new System.Windows.Forms.CheckBox();
			this.checkObsLeft = new System.Windows.Forms.CheckBox();
			this.checkObsUp = new System.Windows.Forms.CheckBox();
			this.tv_obs = new winmaped2.TileViewer();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.minimapPanel = new winmaped2.CollapsiblePanel();
			this.ctMinimapP = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.tb_zoom = new System.Windows.Forms.TrackBar();
			this.panel2 = new System.Windows.Forms.Panel();
			this.miniMap = new winmaped2.MiniMapControl();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cb_transeffects = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.clipboardPanel = new winmaped2.CollapsiblePanel();
			this.mcClipboard = new winmaped2.MapController();
			((System.ComponentModel.ISupportInitialize)(this.sbpLoadInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpCursorInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpSelection)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpZoom)).BeginInit();
			this.toolPanel.SuspendLayout();
			this.sidebarPanel.SuspendLayout();
			this.mapPanel.SuspendLayout();
			this.mainpanel.SuspendLayout();
			this.toolPalletePanel.SuspendLayout();
			this.toolPalette.SuspendLayout();
			this.layerPanel.SuspendLayout();
			this.panel4.SuspendLayout();
			this.tilesPanel.SuspendLayout();
			this.ctTilesP.SuspendLayout();
			this.g_tiles.SuspendLayout();
			this.p_zones.SuspendLayout();
			this.p_ents.SuspendLayout();
			this.g_obs.SuspendLayout();
			this.minimapPanel.SuspendLayout();
			this.ctMinimapP.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_zoom)).BeginInit();
			this.panel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.clipboardPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile,
            this.miEdit,
            this.miMap,
            this.miVsp,
            this.miView,
            this.miHelp});
			// 
			// miFile
			// 
			this.miFile.Index = 0;
			this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miNewMap,
            this.miOpenMap,
            this.miClose,
            this.menuItem7,
            this.miSave,
            this.miSaveAs,
            this.mruSeparator,
            this.misSave,
            this.miPreferences,
            this.menuItem11,
            this.miExit});
			this.miFile.Text = "&File";
			this.miFile.Popup += new System.EventHandler(this.FileMenu_Popup);
			// 
			// miNewMap
			// 
			this.miNewMap.Index = 0;
			this.miNewMap.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.miNewMap.Text = "&New...";
			this.miNewMap.Click += new System.EventHandler(this.miNewMap_Click);
			// 
			// miOpenMap
			// 
			this.miOpenMap.Index = 1;
			this.miOpenMap.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.miOpenMap.Text = "&Open...";
			this.miOpenMap.Click += new System.EventHandler(this.mOpenMap_Click);
			// 
			// miClose
			// 
			this.miClose.Index = 2;
			this.miClose.Text = "&Close";
			this.miClose.Click += new System.EventHandler(this.miClose_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 3;
			this.menuItem7.Text = "-";
			// 
			// miSave
			// 
			this.miSave.Index = 4;
			this.miSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.miSave.Text = "Save";
			this.miSave.Click += new System.EventHandler(this.miSave_Click);
			// 
			// miSaveAs
			// 
			this.miSaveAs.Index = 5;
			this.miSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
			this.miSaveAs.Text = "Save As...";
			this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
			// 
			// mruSeparator
			// 
			this.mruSeparator.Index = 6;
			this.mruSeparator.Text = "-";
			// 
			// misSave
			// 
			this.misSave.Index = 7;
			this.misSave.Text = "-";
			// 
			// miPreferences
			// 
			this.miPreferences.Index = 8;
			this.miPreferences.Text = "&Preferences...";
			this.miPreferences.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 9;
			this.menuItem11.Text = "-";
			// 
			// miExit
			// 
			this.miExit.Index = 10;
			this.miExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
			this.miExit.Text = "E&xit";
			this.miExit.Click += new System.EventHandler(this.mitemExit_Click);
			// 
			// miEdit
			// 
			this.miEdit.Index = 1;
			this.miEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miUndo,
            this.miRedo});
			this.miEdit.Text = "Edit";
			this.miEdit.Popup += new System.EventHandler(this.miEdit_Popup);
			// 
			// miUndo
			// 
			this.miUndo.Index = 0;
			this.miUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
			this.miUndo.Text = "Undo";
			this.miUndo.Click += new System.EventHandler(this.miUndo_Click);
			// 
			// miRedo
			// 
			this.miRedo.Enabled = false;
			this.miRedo.Index = 1;
			this.miRedo.Text = "Redo";
			this.miRedo.Click += new System.EventHandler(this.miRedo_Click);
			// 
			// miMap
			// 
			this.miMap.Index = 2;
			this.miMap.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miEditZones,
            this.miEditEntities,
            this.menuItem8,
            this.miMapProperties});
			this.miMap.Text = "&Map";
			// 
			// miEditZones
			// 
			this.miEditZones.Index = 0;
			this.miEditZones.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.miEditZones.Text = "&Edit Zones...";
			this.miEditZones.Click += new System.EventHandler(this.miEditZones_Click);
			// 
			// miEditEntities
			// 
			this.miEditEntities.Index = 1;
			this.miEditEntities.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.miEditEntities.Text = "&Edit Entities...";
			this.miEditEntities.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 2;
			this.menuItem8.Text = "-";
			// 
			// miMapProperties
			// 
			this.miMapProperties.Index = 3;
			this.miMapProperties.Text = "&Properties...";
			this.miMapProperties.Click += new System.EventHandler(this.miMapProperties_Click);
			// 
			// miVsp
			// 
			this.miVsp.Index = 3;
			this.miVsp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miEditTiles,
            this.miEditAnims,
            this.miArrangeTiles,
            this.menuItem15,
            this.miImportTiles,
            this.miImportAgain,
            this.menuItem9,
            this.miExportTiles,
            this.menuItem13,
            this.miChangeVSP});
			this.miVsp.Text = "&Tiles";
			// 
			// miEditTiles
			// 
			this.miEditTiles.Index = 0;
			this.miEditTiles.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
			this.miEditTiles.Text = "&Edit Tiles...";
			this.miEditTiles.Click += new System.EventHandler(this.miEditTiles_Click);
			// 
			// miEditAnims
			// 
			this.miEditAnims.Index = 1;
			this.miEditAnims.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.miEditAnims.Text = "Edit &Animations...";
			this.miEditAnims.Click += new System.EventHandler(this.miEditAnims_Click);
			// 
			// miArrangeTiles
			// 
			this.miArrangeTiles.Index = 2;
			this.miArrangeTiles.Text = "Arrange...";
			this.miArrangeTiles.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 3;
			this.menuItem15.Text = "-";
			// 
			// miImportTiles
			// 
			this.miImportTiles.Index = 4;
			this.miImportTiles.Text = "&Import...";
			this.miImportTiles.Click += new System.EventHandler(this.miImport_Click);
			// 
			// miImportAgain
			// 
			this.miImportAgain.Enabled = false;
			this.miImportAgain.Index = 5;
			this.miImportAgain.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.miImportAgain.Text = "Import Again";
			this.miImportAgain.Click += new System.EventHandler(this.miImportAgain_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 6;
			this.menuItem9.Text = "-";
			// 
			// miExportTiles
			// 
			this.miExportTiles.Index = 7;
			this.miExportTiles.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miExportTilesToImage,
            this.miExportTilesToImageGrid,
            this.miExportTilesToClipboard,
            this.miExportTilesToClipboardGrid});
			this.miExportTiles.Text = "Export";
			// 
			// miExportTilesToImage
			// 
			this.miExportTilesToImage.Index = 0;
			this.miExportTilesToImage.Text = "To Image...";
			this.miExportTilesToImage.Click += new System.EventHandler(this.miExportImageGridless_Click);
			// 
			// miExportTilesToImageGrid
			// 
			this.miExportTilesToImageGrid.Index = 1;
			this.miExportTilesToImageGrid.Text = "To Image w/ Grid...";
			this.miExportTilesToImageGrid.Click += new System.EventHandler(this.miExportToImage_Click);
			// 
			// miExportTilesToClipboard
			// 
			this.miExportTilesToClipboard.Index = 2;
			this.miExportTilesToClipboard.Text = "To Clipboard...";
			this.miExportTilesToClipboard.Click += new System.EventHandler(this.miExportClipboardGridless_Click);
			// 
			// miExportTilesToClipboardGrid
			// 
			this.miExportTilesToClipboardGrid.Index = 3;
			this.miExportTilesToClipboardGrid.Text = "To Clipboard w/ Grid...";
			this.miExportTilesToClipboardGrid.Click += new System.EventHandler(this.mi_ExportClipboard_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 8;
			this.menuItem13.Text = "-";
			this.menuItem13.Visible = false;
			// 
			// miChangeVSP
			// 
			this.miChangeVSP.Index = 9;
			this.miChangeVSP.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miChangeVspExisting,
            this.miChangeVspBlank});
			this.miChangeVSP.Text = "Change Currently Used";
			this.miChangeVSP.Visible = false;
			// 
			// miChangeVspExisting
			// 
			this.miChangeVspExisting.Index = 0;
			this.miChangeVspExisting.Text = "To Existing...";
			this.miChangeVspExisting.Click += new System.EventHandler(this.miChangeVspExisting_Click);
			// 
			// miChangeVspBlank
			// 
			this.miChangeVspBlank.Index = 1;
			this.miChangeVspBlank.Text = "To New";
			// 
			// miView
			// 
			this.miView.Index = 4;
			this.miView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miViewToolbars,
            this.menuItem2,
            this.Zoom1x,
            this.miZoom2x,
            this.miZoom4x,
            this.menuItem1,
            this.miViewNotes});
			this.miView.Text = "Vie&w";
			this.miView.Popup += new System.EventHandler(this.miView_Popup);
			// 
			// miViewToolbars
			// 
			this.miViewToolbars.Index = 0;
			this.miViewToolbars.Text = "&Toolbars";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// Zoom1x
			// 
			this.Zoom1x.Index = 2;
			this.Zoom1x.RadioCheck = true;
			this.Zoom1x.Shortcut = System.Windows.Forms.Shortcut.Ctrl1;
			this.Zoom1x.Text = "Zoom: 1x";
			this.Zoom1x.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// miZoom2x
			// 
			this.miZoom2x.Index = 3;
			this.miZoom2x.RadioCheck = true;
			this.miZoom2x.Shortcut = System.Windows.Forms.Shortcut.Ctrl2;
			this.miZoom2x.Text = "Zoom: 2x";
			this.miZoom2x.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// miZoom4x
			// 
			this.miZoom4x.Index = 4;
			this.miZoom4x.RadioCheck = true;
			this.miZoom4x.Shortcut = System.Windows.Forms.Shortcut.Ctrl3;
			this.miZoom4x.Text = "Zoom: 4x";
			this.miZoom4x.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 5;
			this.menuItem1.Text = "-";
			// 
			// miViewNotes
			// 
			this.miViewNotes.Checked = true;
			this.miViewNotes.Index = 6;
			this.miViewNotes.Text = "Notes";
			this.miViewNotes.Click += new System.EventHandler(this.miViewNotes_Click);
			// 
			// miHelp
			// 
			this.miHelp.Index = 5;
			this.miHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miShowHelp,
            this.menuItem16,
            this.mi_rft,
            this.miAbout});
			this.miHelp.Text = "&Help";
			// 
			// miShowHelp
			// 
			this.miShowHelp.Index = 0;
			this.miShowHelp.Text = "&Help...";
			this.miShowHelp.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 1;
			this.menuItem16.Text = "-";
			this.menuItem16.Visible = false;
			// 
			// mi_rft
			// 
			this.mi_rft.Index = 2;
			this.mi_rft.Text = "&Register File Associations...";
			this.mi_rft.Click += new System.EventHandler(this.mi_rft_Click);
			// 
			// miAbout
			// 
			this.miAbout.Index = 3;
			this.miAbout.Text = "A&bout...";
			this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "map";
			this.saveFileDialog.Filter = "MAP Files (*.map)|*.map|All Files (*.*)|*.*";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "MAP Files (*.map)|*.map|All Files (*.*)|*.*";
			// 
			// statusBar
			// 
			this.statusBar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusBar.Location = new System.Drawing.Point(0, 616);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpLoadInfo,
            this.sbpCursorInfo,
            this.sbpSelection,
            this.sbpZoom});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(846, 24);
			this.statusBar.TabIndex = 1;
			// 
			// sbpLoadInfo
			// 
			this.sbpLoadInfo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpLoadInfo.Name = "sbpLoadInfo";
			this.sbpLoadInfo.Width = 380;
			// 
			// sbpCursorInfo
			// 
			this.sbpCursorInfo.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.sbpCursorInfo.MinWidth = 150;
			this.sbpCursorInfo.Name = "sbpCursorInfo";
			this.sbpCursorInfo.Width = 150;
			// 
			// sbpSelection
			// 
			this.sbpSelection.MinWidth = 200;
			this.sbpSelection.Name = "sbpSelection";
			this.sbpSelection.Text = "Selected Tiles: 0, 0";
			this.sbpSelection.Width = 200;
			// 
			// sbpZoom
			// 
			this.sbpZoom.MinWidth = 100;
			this.sbpZoom.Name = "sbpZoom";
			this.sbpZoom.Text = "Zoom:";
			// 
			// toolPanel
			// 
			this.toolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.toolPanel.Controls.Add(this.button1);
			this.toolPanel.Controls.Add(this.sidebarPanel);
			this.toolPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.toolPanel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.toolPanel.Location = new System.Drawing.Point(352, 0);
			this.toolPanel.Name = "toolPanel";
			this.toolPanel.Size = new System.Drawing.Size(494, 616);
			this.toolPanel.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Left;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(16, 614);
			this.button1.TabIndex = 5;
			this.button1.Text = ">";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// sidebarPanel
			// 
			this.sidebarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.sidebarPanel.AutoScroll = true;
			this.sidebarPanel.Controls.Add(this.toolPalletePanel);
			this.sidebarPanel.Controls.Add(this.layerPanel);
			this.sidebarPanel.Controls.Add(this.tilesPanel);
			this.sidebarPanel.Controls.Add(this.minimapPanel);
			this.sidebarPanel.Location = new System.Drawing.Point(18, 4);
			this.sidebarPanel.Name = "sidebarPanel";
			this.sidebarPanel.Size = new System.Drawing.Size(471, 630);
			this.sidebarPanel.TabIndex = 16;
			// 
			// toolbarImages
			// 
			this.toolbarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolbarImages.ImageStream")));
			this.toolbarImages.TransparentColor = System.Drawing.Color.Transparent;
			this.toolbarImages.Images.SetKeyName(0, "");
			this.toolbarImages.Images.SetKeyName(1, "");
			// 
			// mapPanel
			// 
			this.mapPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.mapPanel.Controls.Add(this.mainpanel);
			this.mapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapPanel.Location = new System.Drawing.Point(0, 0);
			this.mapPanel.Name = "mapPanel";
			this.mapPanel.Size = new System.Drawing.Size(352, 616);
			this.mapPanel.TabIndex = 5;
			this.mapPanel.Resize += new System.EventHandler(this.mapPanel_Resize);
			// 
			// mainpanel
			// 
			this.mainpanel.Controls.Add(this.mapController);
			this.mainpanel.Controls.Add(this.sizeGrip);
			this.mainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainpanel.ForeColor = System.Drawing.Color.Coral;
			this.mainpanel.Location = new System.Drawing.Point(0, 0);
			this.mainpanel.Name = "mainpanel";
			this.mainpanel.Size = new System.Drawing.Size(348, 612);
			this.mainpanel.TabIndex = 10;
			this.mainpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainpanel_Paint);
			// 
			// openVspDialog
			// 
			this.openVspDialog.Filter = "VSP Files(*.vsp)|*.vsp";
			this.openVspDialog.RestoreDirectory = true;
			// 
			// openImageDialog
			// 
			this.openImageDialog.Filter = "Image Files (*.png,*.jpg,*.jpeg,*.pcx,*.bmp,*.tga,*.gif)|*.png;*.jpg;*.jpeg;*.pcx" +
					";*.bmp;*.tga;*.gif";
			this.openImageDialog.RestoreDirectory = true;
			// 
			// saveVspDialog
			// 
			this.saveVspDialog.DefaultExt = "vsp";
			this.saveVspDialog.Filter = "VSP Files (*.vsp)|*.vsp";
			this.saveVspDialog.RestoreDirectory = true;
			// 
			// saveImageDialog
			// 
			this.saveImageDialog.Filter = "Portable Network Graphics File (*.png)|*.png";
			this.saveImageDialog.RestoreDirectory = true;
			// 
			// mapController
			// 
			this.mapController.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapController.Location = new System.Drawing.Point(0, 0);
			this.mapController.Name = "mapController";
			this.mapController.ParentMap = null;
			this.mapController.Size = new System.Drawing.Size(348, 612);
			this.mapController.TabIndex = 0;
			this.mapController.ZoomLevel = 2;
			// 
			// sizeGrip
			// 
			this.sizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.sizeGrip.ForeColor = System.Drawing.Color.Coral;
			this.sizeGrip.Location = new System.Drawing.Point(328, 614);
			this.sizeGrip.Name = "sizeGrip";
			this.sizeGrip.Size = new System.Drawing.Size(16, 16);
			this.sizeGrip.TabIndex = 3;
			// 
			// toolPalletePanel
			// 
			this.toolPalletePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.toolPalletePanel.CanLargeify = false;
			this.toolPalletePanel.Controls.Add(this.groupBox2);
			this.toolPalletePanel.Controls.Add(this.toolPalette);
			this.toolPalletePanel.Largeified = false;
			this.toolPalletePanel.Location = new System.Drawing.Point(3, 3);
			this.toolPalletePanel.Name = "toolPalletePanel";
			this.toolPalletePanel.Size = new System.Drawing.Size(341, 118);
			this.toolPalletePanel.TabIndex = 0;
			this.toolPalletePanel.Title = "Tools";
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(172, 17);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(152, 94);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Tool Properties";
			// 
			// toolPalette
			// 
			this.toolPalette.Controls.Add(this.radioButton7);
			this.toolPalette.Controls.Add(this.radioButton6);
			this.toolPalette.Controls.Add(this.b_runmap);
			this.toolPalette.Controls.Add(this.radioButton5);
			this.toolPalette.Controls.Add(this.radioButton3);
			this.toolPalette.Controls.Add(this.radioButton2);
			this.toolPalette.Controls.Add(this.radioButton1);
			this.toolPalette.Controls.Add(this.radioButton4);
			this.toolPalette.Location = new System.Drawing.Point(2, 17);
			this.toolPalette.Name = "toolPalette";
			this.toolPalette.Size = new System.Drawing.Size(168, 94);
			this.toolPalette.TabIndex = 0;
			// 
			// radioButton7
			// 
			this.radioButton7.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton7.Image = ((System.Drawing.Image)(resources.GetObject("radioButton7.Image")));
			this.radioButton7.Location = new System.Drawing.Point(36, 40);
			this.radioButton7.Name = "radioButton7";
			this.radioButton7.Size = new System.Drawing.Size(32, 32);
			this.radioButton7.TabIndex = 7;
			this.radioButton7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// radioButton6
			// 
			this.radioButton6.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton6.Image = ((System.Drawing.Image)(resources.GetObject("radioButton6.Image")));
			this.radioButton6.Location = new System.Drawing.Point(4, 40);
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.Size = new System.Drawing.Size(32, 32);
			this.radioButton6.TabIndex = 6;
			this.radioButton6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// b_runmap
			// 
			this.b_runmap.Image = ((System.Drawing.Image)(resources.GetObject("b_runmap.Image")));
			this.b_runmap.Location = new System.Drawing.Point(132, 40);
			this.b_runmap.Name = "b_runmap";
			this.b_runmap.Size = new System.Drawing.Size(32, 32);
			this.b_runmap.TabIndex = 5;
			this.b_runmap.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.b_runmap.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// radioButton5
			// 
			this.radioButton5.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton5.Image = ((System.Drawing.Image)(resources.GetObject("radioButton5.Image")));
			this.radioButton5.Location = new System.Drawing.Point(132, 8);
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.Size = new System.Drawing.Size(32, 32);
			this.radioButton5.TabIndex = 4;
			// 
			// radioButton3
			// 
			this.radioButton3.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton3.Image = ((System.Drawing.Image)(resources.GetObject("radioButton3.Image")));
			this.radioButton3.Location = new System.Drawing.Point(36, 8);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(32, 32);
			this.radioButton3.TabIndex = 2;
			// 
			// radioButton2
			// 
			this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton2.Image = ((System.Drawing.Image)(resources.GetObject("radioButton2.Image")));
			this.radioButton2.Location = new System.Drawing.Point(100, 8);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(32, 32);
			this.radioButton2.TabIndex = 1;
			// 
			// radioButton1
			// 
			this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton1.Image = ((System.Drawing.Image)(resources.GetObject("radioButton1.Image")));
			this.radioButton1.Location = new System.Drawing.Point(4, 8);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(32, 32);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// radioButton4
			// 
			this.radioButton4.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton4.Image = ((System.Drawing.Image)(resources.GetObject("radioButton4.Image")));
			this.radioButton4.Location = new System.Drawing.Point(68, 8);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(32, 32);
			this.radioButton4.TabIndex = 3;
			// 
			// layerPanel
			// 
			this.layerPanel.CanLargeify = false;
			this.layerPanel.Controls.Add(this.lPanel);
			this.layerPanel.Controls.Add(this.panel4);
			this.layerPanel.Largeified = false;
			this.layerPanel.Location = new System.Drawing.Point(3, 127);
			this.layerPanel.Name = "layerPanel";
			this.layerPanel.Size = new System.Drawing.Size(341, 186);
			this.layerPanel.TabIndex = 1;
			this.layerPanel.Title = "Layers";
			// 
			// lPanel
			// 
			this.lPanel.AutoScroll = true;
			this.lPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lPanel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lPanel.Location = new System.Drawing.Point(0, 48);
			this.lPanel.Name = "lPanel";
			this.lPanel.Size = new System.Drawing.Size(341, 138);
			this.lPanel.TabIndex = 0;
			this.lPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lPanel_Paint);
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.Add(this.l_rstring);
			this.panel4.Controls.Add(this.b_layeradd);
			this.panel4.Controls.Add(this.b_layerdown);
			this.panel4.Controls.Add(this.b_layerup);
			this.panel4.Controls.Add(this.b_layerdel);
			this.panel4.Controls.Add(this.b_layerproperties);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 16);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(341, 32);
			this.panel4.TabIndex = 1;
			// 
			// l_rstring
			// 
			this.l_rstring.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.l_rstring.Location = new System.Drawing.Point(8, 0);
			this.l_rstring.Name = "l_rstring";
			this.l_rstring.Size = new System.Drawing.Size(128, 32);
			this.l_rstring.TabIndex = 1;
			this.l_rstring.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// b_layeradd
			// 
			this.b_layeradd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.b_layeradd.Image = ((System.Drawing.Image)(resources.GetObject("b_layeradd.Image")));
			this.b_layeradd.Location = new System.Drawing.Point(304, 0);
			this.b_layeradd.Name = "b_layeradd";
			this.b_layeradd.Size = new System.Drawing.Size(32, 32);
			this.b_layeradd.TabIndex = 0;
			this.b_layeradd.Click += new System.EventHandler(this.b_layeradd_Click);
			// 
			// b_layerdown
			// 
			this.b_layerdown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.b_layerdown.Image = ((System.Drawing.Image)(resources.GetObject("b_layerdown.Image")));
			this.b_layerdown.Location = new System.Drawing.Point(224, 0);
			this.b_layerdown.Name = "b_layerdown";
			this.b_layerdown.Size = new System.Drawing.Size(32, 32);
			this.b_layerdown.TabIndex = 0;
			this.b_layerdown.Click += new System.EventHandler(this.b_layerdown_Click);
			// 
			// b_layerup
			// 
			this.b_layerup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.b_layerup.Image = ((System.Drawing.Image)(resources.GetObject("b_layerup.Image")));
			this.b_layerup.Location = new System.Drawing.Point(184, 0);
			this.b_layerup.Name = "b_layerup";
			this.b_layerup.Size = new System.Drawing.Size(32, 32);
			this.b_layerup.TabIndex = 0;
			this.b_layerup.Click += new System.EventHandler(this.b_layerup_Click);
			// 
			// b_layerdel
			// 
			this.b_layerdel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.b_layerdel.Image = ((System.Drawing.Image)(resources.GetObject("b_layerdel.Image")));
			this.b_layerdel.Location = new System.Drawing.Point(264, 0);
			this.b_layerdel.Name = "b_layerdel";
			this.b_layerdel.Size = new System.Drawing.Size(32, 32);
			this.b_layerdel.TabIndex = 0;
			this.b_layerdel.Click += new System.EventHandler(this.b_layerdel_Click);
			// 
			// b_layerproperties
			// 
			this.b_layerproperties.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.b_layerproperties.Image = ((System.Drawing.Image)(resources.GetObject("b_layerproperties.Image")));
			this.b_layerproperties.Location = new System.Drawing.Point(144, 0);
			this.b_layerproperties.Name = "b_layerproperties";
			this.b_layerproperties.Size = new System.Drawing.Size(32, 32);
			this.b_layerproperties.TabIndex = 0;
			this.b_layerproperties.Click += new System.EventHandler(this.b_layerproperties_Click);
			// 
			// tilesPanel
			// 
			this.tilesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tilesPanel.CanLargeify = false;
			this.tilesPanel.Controls.Add(this.ctTilesP);
			this.tilesPanel.Largeified = false;
			this.tilesPanel.Location = new System.Drawing.Point(3, 319);
			this.tilesPanel.Name = "tilesPanel";
			this.tilesPanel.Size = new System.Drawing.Size(409, 329);
			this.tilesPanel.TabIndex = 2;
			this.tilesPanel.Title = "Tiles";
			// 
			// ctTilesP
			// 
			this.ctTilesP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ctTilesP.Controls.Add(this.button3);
			this.ctTilesP.Controls.Add(this.vspc_obs);
			this.ctTilesP.Controls.Add(this.g_tiles);
			this.ctTilesP.Controls.Add(this.vspController);
			this.ctTilesP.Controls.Add(this.p_zones);
			this.ctTilesP.Controls.Add(this.p_ents);
			this.ctTilesP.Controls.Add(this.g_obs);
			this.ctTilesP.Controls.Add(this.button4);
			this.ctTilesP.Controls.Add(this.button5);
			this.ctTilesP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctTilesP.Location = new System.Drawing.Point(0, 16);
			this.ctTilesP.Name = "ctTilesP";
			this.ctTilesP.Size = new System.Drawing.Size(409, 313);
			this.ctTilesP.TabIndex = 13;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(192, 208);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(144, 24);
			this.button3.TabIndex = 10;
			this.button3.Text = "Edit Tiles";
			this.button3.Click += new System.EventHandler(this.miEditTiles_Click);
			// 
			// vspc_obs
			// 
			this.vspc_obs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.vspc_obs.Location = new System.Drawing.Point(0, 0);
			this.vspc_obs.Name = "vspc_obs";
			this.vspc_obs.Size = new System.Drawing.Size(404, 196);
			this.vspc_obs.TabIndex = 9;
			// 
			// g_tiles
			// 
			this.g_tiles.Controls.Add(this.TileViewA);
			this.g_tiles.Controls.Add(this.TileViewB);
			this.g_tiles.Location = new System.Drawing.Point(8, 200);
			this.g_tiles.Name = "g_tiles";
			this.g_tiles.Size = new System.Drawing.Size(178, 104);
			this.g_tiles.TabIndex = 3;
			this.g_tiles.TabStop = false;
			this.g_tiles.Visible = false;
			// 
			// TileViewA
			// 
			this.TileViewA.ActiveObsTile = null;
			this.TileViewA.ActiveTile = null;
			this.TileViewA.ActiveTileIndex = 0;
			this.TileViewA.Location = new System.Drawing.Point(16, 24);
			this.TileViewA.Name = "TileViewA";
			this.TileViewA.Size = new System.Drawing.Size(64, 64);
			this.TileViewA.TabIndex = 0;
			// 
			// TileViewB
			// 
			this.TileViewB.ActiveObsTile = null;
			this.TileViewB.ActiveTile = null;
			this.TileViewB.ActiveTileIndex = 0;
			this.TileViewB.Location = new System.Drawing.Point(96, 24);
			this.TileViewB.Name = "TileViewB";
			this.TileViewB.Size = new System.Drawing.Size(64, 64);
			this.TileViewB.TabIndex = 1;
			// 
			// vspController
			// 
			this.vspController.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.vspController.Location = new System.Drawing.Point(0, 0);
			this.vspController.Name = "vspController";
			this.vspController.Size = new System.Drawing.Size(404, 196);
			this.vspController.TabIndex = 0;
			// 
			// p_zones
			// 
			this.p_zones.Controls.Add(this.l_szone);
			this.p_zones.Controls.Add(this.button2);
			this.p_zones.Controls.Add(this.label1);
			this.p_zones.Controls.Add(this.lv_zonelist);
			this.p_zones.Location = new System.Drawing.Point(0, 0);
			this.p_zones.Name = "p_zones";
			this.p_zones.Size = new System.Drawing.Size(340, 164);
			this.p_zones.TabIndex = 9;
			this.p_zones.Visible = false;
			// 
			// l_szone
			// 
			this.l_szone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.l_szone.Location = new System.Drawing.Point(8, 8);
			this.l_szone.Name = "l_szone";
			this.l_szone.Size = new System.Drawing.Size(136, 16);
			this.l_szone.TabIndex = 5;
			this.l_szone.Text = "Selected Zone: 0";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(264, 0);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(72, 24);
			this.button2.TabIndex = 4;
			this.button2.Text = "Edit Zones";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(146, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Zones";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lv_zonelist
			// 
			this.lv_zonelist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_zoneid,
            this.ch_zonename});
			this.lv_zonelist.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lv_zonelist.FullRowSelect = true;
			this.lv_zonelist.GridLines = true;
			this.lv_zonelist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lv_zonelist.HideSelection = false;
			this.lv_zonelist.Location = new System.Drawing.Point(0, 28);
			this.lv_zonelist.MultiSelect = false;
			this.lv_zonelist.Name = "lv_zonelist";
			this.lv_zonelist.Size = new System.Drawing.Size(340, 136);
			this.lv_zonelist.TabIndex = 1;
			this.lv_zonelist.UseCompatibleStateImageBehavior = false;
			this.lv_zonelist.View = System.Windows.Forms.View.Details;
			this.lv_zonelist.SelectedIndexChanged += new System.EventHandler(this.lv_zonelist_SelectedIndexChanged);
			this.lv_zonelist.DoubleClick += new System.EventHandler(this.lv_zonelist_DoubleClick);
			// 
			// ch_zoneid
			// 
			this.ch_zoneid.Text = "ID";
			this.ch_zoneid.Width = 44;
			// 
			// ch_zonename
			// 
			this.ch_zonename.Text = "Name";
			this.ch_zonename.Width = 262;
			// 
			// p_ents
			// 
			this.p_ents.Controls.Add(this.l_sent);
			this.p_ents.Controls.Add(this.b_editents);
			this.p_ents.Controls.Add(this.label3);
			this.p_ents.Controls.Add(this.lv_ents);
			this.p_ents.Controls.Add(this.b_gotoent);
			this.p_ents.Location = new System.Drawing.Point(0, 0);
			this.p_ents.Name = "p_ents";
			this.p_ents.Size = new System.Drawing.Size(404, 164);
			this.p_ents.TabIndex = 9;
			this.p_ents.Visible = false;
			// 
			// l_sent
			// 
			this.l_sent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.l_sent.Location = new System.Drawing.Point(8, 8);
			this.l_sent.Name = "l_sent";
			this.l_sent.Size = new System.Drawing.Size(136, 16);
			this.l_sent.TabIndex = 4;
			this.l_sent.Text = "Selected Entity: 0";
			// 
			// b_editents
			// 
			this.b_editents.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.b_editents.Location = new System.Drawing.Point(264, 0);
			this.b_editents.Name = "b_editents";
			this.b_editents.Size = new System.Drawing.Size(72, 24);
			this.b_editents.TabIndex = 3;
			this.b_editents.Text = "Edit Entities";
			this.b_editents.Click += new System.EventHandler(this.b_editents_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(146, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Entities";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lv_ents
			// 
			this.lv_ents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lv_ents.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lv_ents.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lv_ents.FullRowSelect = true;
			this.lv_ents.GridLines = true;
			this.lv_ents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lv_ents.HideSelection = false;
			this.lv_ents.Location = new System.Drawing.Point(0, 28);
			this.lv_ents.MultiSelect = false;
			this.lv_ents.Name = "lv_ents";
			this.lv_ents.Size = new System.Drawing.Size(404, 136);
			this.lv_ents.TabIndex = 1;
			this.lv_ents.UseCompatibleStateImageBehavior = false;
			this.lv_ents.View = System.Windows.Forms.View.Details;
			this.lv_ents.SelectedIndexChanged += new System.EventHandler(this.lv_ents_SelectedIndexChanged);
			this.lv_ents.DoubleClick += new System.EventHandler(this.lv_ents_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "ID";
			this.columnHeader1.Width = 44;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Name";
			this.columnHeader2.Width = 262;
			// 
			// b_gotoent
			// 
			this.b_gotoent.Enabled = false;
			this.b_gotoent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.b_gotoent.Location = new System.Drawing.Point(208, 0);
			this.b_gotoent.Name = "b_gotoent";
			this.b_gotoent.Size = new System.Drawing.Size(48, 24);
			this.b_gotoent.TabIndex = 3;
			this.b_gotoent.Text = "Goto";
			this.b_gotoent.Click += new System.EventHandler(this.b_gotoent_Click);
			// 
			// g_obs
			// 
			this.g_obs.BackColor = System.Drawing.Color.RoyalBlue;
			this.g_obs.Controls.Add(this.btnObsAll);
			this.g_obs.Controls.Add(this.checkObsRight);
			this.g_obs.Controls.Add(this.checkObsDown);
			this.g_obs.Controls.Add(this.checkObsLeft);
			this.g_obs.Controls.Add(this.checkObsUp);
			this.g_obs.Controls.Add(this.tv_obs);
			this.g_obs.Location = new System.Drawing.Point(8, 200);
			this.g_obs.Name = "g_obs";
			this.g_obs.Size = new System.Drawing.Size(178, 104);
			this.g_obs.TabIndex = 9;
			this.g_obs.TabStop = false;
			this.g_obs.Visible = false;
			// 
			// btnObsAll
			// 
			this.btnObsAll.Location = new System.Drawing.Point(112, 40);
			this.btnObsAll.Name = "btnObsAll";
			this.btnObsAll.Size = new System.Drawing.Size(16, 15);
			this.btnObsAll.TabIndex = 5;
			this.btnObsAll.UseVisualStyleBackColor = true;
			this.btnObsAll.Click += new System.EventHandler(this.btnObsAll_Click);
			// 
			// checkObsRight
			// 
			this.checkObsRight.AutoSize = true;
			this.checkObsRight.Location = new System.Drawing.Point(131, 41);
			this.checkObsRight.Name = "checkObsRight";
			this.checkObsRight.Size = new System.Drawing.Size(15, 14);
			this.checkObsRight.TabIndex = 4;
			this.checkObsRight.UseVisualStyleBackColor = true;
			this.checkObsRight.CheckedChanged += new System.EventHandler(this.checkObsXX_CheckedChanged);
			// 
			// checkObsDown
			// 
			this.checkObsDown.AutoSize = true;
			this.checkObsDown.Location = new System.Drawing.Point(113, 59);
			this.checkObsDown.Name = "checkObsDown";
			this.checkObsDown.Size = new System.Drawing.Size(15, 14);
			this.checkObsDown.TabIndex = 3;
			this.checkObsDown.UseVisualStyleBackColor = true;
			this.checkObsDown.CheckedChanged += new System.EventHandler(this.checkObsXX_CheckedChanged);
			// 
			// checkObsLeft
			// 
			this.checkObsLeft.AutoSize = true;
			this.checkObsLeft.Location = new System.Drawing.Point(96, 41);
			this.checkObsLeft.Name = "checkObsLeft";
			this.checkObsLeft.Size = new System.Drawing.Size(15, 14);
			this.checkObsLeft.TabIndex = 2;
			this.checkObsLeft.UseVisualStyleBackColor = true;
			this.checkObsLeft.CheckedChanged += new System.EventHandler(this.checkObsXX_CheckedChanged);
			// 
			// checkObsUp
			// 
			this.checkObsUp.AutoSize = true;
			this.checkObsUp.Location = new System.Drawing.Point(113, 24);
			this.checkObsUp.Name = "checkObsUp";
			this.checkObsUp.Size = new System.Drawing.Size(15, 14);
			this.checkObsUp.TabIndex = 1;
			this.checkObsUp.UseVisualStyleBackColor = true;
			this.checkObsUp.CheckedChanged += new System.EventHandler(this.checkObsXX_CheckedChanged);
			// 
			// tv_obs
			// 
			this.tv_obs.ActiveObsTile = null;
			this.tv_obs.ActiveTile = null;
			this.tv_obs.ActiveTileIndex = 0;
			this.tv_obs.Location = new System.Drawing.Point(16, 24);
			this.tv_obs.Name = "tv_obs";
			this.tv_obs.Size = new System.Drawing.Size(64, 64);
			this.tv_obs.TabIndex = 0;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(192, 240);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(144, 24);
			this.button4.TabIndex = 10;
			this.button4.Text = "Edit Animations";
			this.button4.Click += new System.EventHandler(this.miEditAnims_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(192, 272);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(144, 24);
			this.button5.TabIndex = 10;
			this.button5.Text = "Arrange Tiles";
			this.button5.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// minimapPanel
			// 
			this.minimapPanel.CanLargeify = false;
			this.minimapPanel.Controls.Add(this.ctMinimapP);
			this.minimapPanel.Largeified = false;
			this.minimapPanel.Location = new System.Drawing.Point(3, 654);
			this.minimapPanel.Name = "minimapPanel";
			this.minimapPanel.Size = new System.Drawing.Size(341, 227);
			this.minimapPanel.TabIndex = 3;
			this.minimapPanel.Title = "Minimap";
			// 
			// ctMinimapP
			// 
			this.ctMinimapP.Controls.Add(this.groupBox3);
			this.ctMinimapP.Controls.Add(this.panel2);
			this.ctMinimapP.Controls.Add(this.groupBox1);
			this.ctMinimapP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctMinimapP.Location = new System.Drawing.Point(0, 16);
			this.ctMinimapP.Name = "ctMinimapP";
			this.ctMinimapP.Size = new System.Drawing.Size(341, 211);
			this.ctMinimapP.TabIndex = 15;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.tb_zoom);
			this.groupBox3.Location = new System.Drawing.Point(216, 88);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(120, 64);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Zoom";
			// 
			// tb_zoom
			// 
			this.tb_zoom.LargeChange = 1;
			this.tb_zoom.Location = new System.Drawing.Point(8, 16);
			this.tb_zoom.Maximum = 4;
			this.tb_zoom.Minimum = 1;
			this.tb_zoom.Name = "tb_zoom";
			this.tb_zoom.Size = new System.Drawing.Size(104, 42);
			this.tb_zoom.TabIndex = 0;
			this.tb_zoom.Value = 1;
			this.tb_zoom.Scroll += new System.EventHandler(this.tb_zoom_Scroll);
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.miniMap);
			this.panel2.Location = new System.Drawing.Point(8, 8);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(204, 200);
			this.panel2.TabIndex = 6;
			// 
			// miniMap
			// 
			this.miniMap.Controller = null;
			this.miniMap.Cursor = System.Windows.Forms.Cursors.Hand;
			this.miniMap.Location = new System.Drawing.Point(0, 0);
			this.miniMap.Name = "miniMap";
			this.miniMap.Size = new System.Drawing.Size(200, 200);
			this.miniMap.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cb_transeffects);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Location = new System.Drawing.Point(216, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(120, 80);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Render Options";
			// 
			// cb_transeffects
			// 
			this.cb_transeffects.Checked = true;
			this.cb_transeffects.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_transeffects.Location = new System.Drawing.Point(8, 40);
			this.cb_transeffects.Name = "cb_transeffects";
			this.cb_transeffects.Size = new System.Drawing.Size(88, 32);
			this.cb_transeffects.TabIndex = 11;
			this.cb_transeffects.Text = "Translucent Effects";
			this.cb_transeffects.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.cb_transeffects.CheckedChanged += new System.EventHandler(this.cb_transeffects_CheckedChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(8, 16);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(96, 24);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "Animate";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// clipboardPanel
			// 
			this.clipboardPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.clipboardPanel.CanLargeify = false;
			this.clipboardPanel.Controls.Add(this.mcClipboard);
			this.clipboardPanel.Enabled = false;
			this.clipboardPanel.Largeified = false;
			this.clipboardPanel.Location = new System.Drawing.Point(3, 629);
			this.clipboardPanel.Name = "clipboardPanel";
			this.clipboardPanel.Size = new System.Drawing.Size(341, 191);
			this.clipboardPanel.TabIndex = 1;
			this.clipboardPanel.Title = "Clipboard";
			this.clipboardPanel.Visible = false;
			// 
			// mcClipboard
			// 
			this.mcClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mcClipboard.Location = new System.Drawing.Point(0, 16);
			this.mcClipboard.Name = "mcClipboard";
			this.mcClipboard.ParentMap = null;
			this.mcClipboard.Size = new System.Drawing.Size(341, 175);
			this.mcClipboard.TabIndex = 0;
			this.mcClipboard.ZoomLevel = 2;
			// 
			// MainWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(846, 640);
			this.Controls.Add(this.mapPanel);
			this.Controls.Add(this.toolPanel);
			this.Controls.Add(this.statusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Menu = this.mainMenu;
			this.Name = "MainWindow";
			this.Text = "VERGE MapEd3.0";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.mainWindow_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.sbpLoadInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpCursorInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpSelection)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpZoom)).EndInit();
			this.toolPanel.ResumeLayout(false);
			this.sidebarPanel.ResumeLayout(false);
			this.mapPanel.ResumeLayout(false);
			this.mainpanel.ResumeLayout(false);
			this.toolPalletePanel.ResumeLayout(false);
			this.toolPalette.ResumeLayout(false);
			this.layerPanel.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.tilesPanel.ResumeLayout(false);
			this.ctTilesP.ResumeLayout(false);
			this.g_tiles.ResumeLayout(false);
			this.p_zones.ResumeLayout(false);
			this.p_ents.ResumeLayout(false);
			this.g_obs.ResumeLayout(false);
			this.g_obs.PerformLayout();
			this.minimapPanel.ResumeLayout(false);
			this.ctMinimapP.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_zoom)).EndInit();
			this.panel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.clipboardPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		// ..... //


		public void ui_update(bool dontcare)
		{
			if (Global.ActiveMap == null)
			{
				miSave.Enabled = false;
				miSaveAs.Enabled = false;
				//				miSaveVSP.Enabled=false;
				//				miSaveVSPAs.Enabled=false;
				miClose.Enabled = false;
				miEdit.Visible = false;
				miView.Visible = false;
				miMap.Visible = false;
				miVsp.Visible = false;
				sidebarPanel.Enabled = false;
			}
			else
			{
				sidebarPanel.Enabled = true;
				//				miSaveVSP.Enabled=true;
				//				miSaveVSPAs.Enabled=true;
				miSave.Enabled = true;
				miSaveAs.Enabled = true;
				miClose.Enabled = true;
				miEdit.Visible = true;
				miView.Visible = true;
				miMap.Visible = true;
				miVsp.Visible = true;
				switch (Global.editedLayer.type)
				{
					case LayerType.Obs:
						vspController.Visible = false;
						p_zones.Visible = false;
						p_ents.Visible = false;
						vspc_obs.Visible = true;
						g_tiles.Visible = false;
						g_obs.Visible = true;
						b_layerdel.Enabled = false;
						b_layerup.Enabled = false;
						b_layerdown.Enabled = false;
						b_layerproperties.Enabled = false;
						break;
					case LayerType.Tile:
						vspController.Visible = true;
						p_zones.Visible = false;
						p_ents.Visible = false;
						vspc_obs.Visible = false;
						g_tiles.Visible = true;
						g_obs.Visible = false;
						if (!Global.IsBaseLayer(Global.editedLayer))
							b_layerdel.Enabled = true;
						else b_layerdel.Enabled = false;
						b_layerup.Enabled = true;
						b_layerdown.Enabled = true;
						b_layerproperties.Enabled = true;
						break;
					case LayerType.Zone:
						vspController.Visible = false;
						p_zones.Visible = true;
						p_ents.Visible = false;
						vspc_obs.Visible = false;
						g_tiles.Visible = false;
						g_obs.Visible = false;
						b_layerdel.Enabled = false;
						b_layerup.Enabled = false;
						b_layerdown.Enabled = false;
						b_layerproperties.Enabled = false;
						break;
					case LayerType.Entity:
						vspController.Visible = false;
						p_zones.Visible = false;
						p_ents.Visible = true;
						vspc_obs.Visible = false;
						g_tiles.Visible = false;
						g_obs.Visible = false;
						b_layerdel.Enabled = false;
						b_layerup.Enabled = true;
						b_layerdown.Enabled = true;
						b_layerproperties.Enabled = false;
						break;
					default:
						if (Global.editedLayer is MapLayerSpecial)
						{
							vspController.Visible = true;
							p_zones.Visible = false;
							p_ents.Visible = false;
							vspc_obs.Visible = false;
							g_tiles.Visible = true;
							g_obs.Visible = false;
							b_layerdel.Enabled = false;
							b_layerup.Enabled = true;
							b_layerdown.Enabled = true;
							b_layerproperties.Enabled = false;
						}
						break;
				}
			}
			Global.VspViewer.CalculateScrollValues();
			vspc_obs.VspView.CalculateScrollValues();
			if (Global.SelectedEntity == null)
				l_sent.Text = "No entity selected";
			else
				l_sent.Text = "Selected Entity: " + Global.SelectedEntity.ID.ToString();
			if (Global.SelectedZone == null)
				l_szone.Text = "No zone selected";
			else
				l_szone.Text = "Selected Zone: " + Global.SelectedZone.ID.ToString();
			Text = Global.VERSIONINFO.VERSIONSTRING_LONG;
			if (Global.ActiveMap == null) return;
			Text += " - ";
			if (Global.ActiveMap.FileOnDisk == null) Text += "untitled.map";
			else Text += Global.ActiveMap.FileOnDisk.Name;
			if (Global.ActiveMap.bAltered) Text += " *";
		}
		public void ui_update()
		{
			ui_update(false);
			Global.ForceRedraws();
		}

		public void update_zone_list()
		{
			int selected_index = 0;
			if (lv_zonelist.SelectedIndices.Count > 0) selected_index = lv_zonelist.SelectedIndices[0];
			lv_zonelist.Items.Clear();
			if (Global.ActiveMap == null) return;
			int ctr = 0;
			foreach (MapZone mz in Global.ActiveMap.Zones)
			{
				ListViewItem lvi = new ListViewItem(new string[] { mz.ID.ToString(), mz.Name });
				lvi.Tag = mz;
				lv_zonelist.Items.Add(lvi);
				if (ctr == selected_index) { lvi.Selected = true; lvi.EnsureVisible(); }
				ctr++;
			}
		}
		public void update_ent_list()
		{
			int selected_index = 0;
			if (lv_ents.SelectedIndices.Count > 0) selected_index = lv_ents.SelectedIndices[0];
			lv_ents.Items.Clear();
			if (Global.ActiveMap == null) return;
			int ctr = 0;
			foreach (MapEntity me in Global.ActiveMap.Entities)
			{
				ListViewItem lvi = new ListViewItem(new string[] { me.ID.ToString(), me.Description + ";" + me.ChrName });
				lvi.Tag = me;
				lv_ents.Items.Add(lvi);
				if (ctr == selected_index) { lvi.Selected = true; lvi.EnsureVisible(); }
				ctr++;
			}
		}
		public void LoadMap(string fname)
		{
			mMap = Map.LoadMap(fname);

			if (mMap == null)
				return;

			mru.touch(fname);

			Global.ActiveMap = mMap;
			Global.ActiveVsp = mMap.vsp;
			vspController.SetActiveVsp(mMap.vsp);
			vspc_obs.SetActiveVsp(mMap.vsp);
			update_zone_list();
			update_ent_list();

			if (Global.VspViewer != null)
			{
				Global.VspViewer.CalculateScrollValues();
			}
			if (Global.mapViewer != null)
			{
				Global.mapViewer.CalculateScrollValues();
			}
			lpInit(mMap);
			Global.FrameCalc.generate(mMap.vsp.Animations);
			Global.ForceRedraws();
			statusBar.Panels[0].Text = "Loaded " + fname;
			if (mMap.vsp.ObstructionTiles.Count == 0)
			{
				if (MessageBox.Show("No obstruction tiles were loaded from the VSP file.  Do you wish MapEd to create the most common basic obstruction tiles for you?", "Help", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
								== DialogResult.Yes)
					mMap.vsp.AddBasicObstructionTiles();
			}



		}

		private void CloseMap()
		{
			lpDestruct();
			Global.ActiveMap = null;
			Global.ActiveVsp = null;
			Global.vspController.SetActiveVsp(null);
			vspc_obs.SetActiveVsp(null);
			ui_update();
		}

		private void mitemExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void mainWindow_Load(object sender, System.EventArgs e)
		{

		}

		Map mMap;
		private void mOpenMap_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = openFileDialog.ShowDialog();

			if (dr == DialogResult.Cancel)
			{
				return;
			}

			LoadMap(openFileDialog.FileName);
			ui_update();


		}

		private void mapPanel_Resize(object sender, System.EventArgs e)
		{
			//mViewer.UpdateSize(mapPanel);
			//mvScrollBar.Location = new Point(mapPanel.ClientSize.Width-14,0);
			//mvScrollBar.Size = new Size(14, mapPanel.ClientSize.Height-14);
			//mhScrollBar.Location = new Point(0,mapPanel.ClientSize.Height-14);
			//mhScrollBar.Size = new Size(mapPanel.ClientSize.Width-14, 14);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (button1.Text == ">")
			{
				button1.Text = "<";
				toolPanel.Size = new Size(16, 0);
			}
			else
			{
				button1.Text = ">";
				toolPanel.Size = new Size(408, 0);
			}
			Global.ForceRedraws();
		}

		private void miAbout_Click(object sender, System.EventArgs e)
		{
			AboutBox ab = new AboutBox();
			ab.ShowDialog();
		}

		private void miEditZones_Click(object sender, System.EventArgs e)
		{
			OpenZoneEditor();
		}


		private void miNewLayer_Click(object sender, System.EventArgs e)
		{
			if (Global.ActiveMap != null)
			{
				NewLayerWindow nlw = new NewLayerWindow();

				DialogResult dr = nlw.ShowDialog();

				if (dr != DialogResult.OK)
				{
					return;
				}

				// add the layer

				MapLayer ml = new MapLayer(Global.ActiveMap);

				int w = nlw.SelectedSize.Width;
				int h = nlw.SelectedSize.Height;

				//ml.Height = h;
				//ml.Width = w;
				ml.size(w, h);

				ml.parallaxInfo = new ParallaxInfo();

				ml.Translucency = 0;

				Global.ActiveMap.Layers.Add(ml);

				lpAddLayer(ml, true, false, (Global.ActiveMap.Layers.Count - 1));

				// create a render state for this layer
				Global.ActiveMap.UIState.AddLayer(ml);

				//				Global.ForceRedraws();
			}
			ui_update();
		}
		// ..... //

		private void WriteDestChanged(winmaped2.Global.LEventArgs e)
		{
			if (Global.ActiveMap == null) return;
			ui_update();
		}
		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			Global.zoom = 1;
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			Global.zoom = 2;
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			Global.zoom = 4;
		}

		private void miUndo_Click(object sender, System.EventArgs e)
		{
			Global.opManager.undo();
		}

		private void miEdit_Popup(object sender, System.EventArgs e)
		{
			if (Global.opManager.isEmpty)
			{
				miUndo.Enabled = false;
				miUndo.Text = "Undo";
			}
			else
			{
				miUndo.Enabled = true;
				miUndo.Text = "Undo " + Global.opManager.currOperationName;
			}

			if (Global.opManager.canRedo)
			{
				miRedo.Enabled = true;
				miRedo.Text = "Redo " + Global.opManager.currRedoName;
			}
			else
			{
				miRedo.Enabled = false;
				miRedo.Text = "Redo";
			}

		}

		private void miView_Popup(object sender, System.EventArgs e)
		{
			Zoom1x.Checked = false;
			miZoom2x.Checked = false;
			miZoom4x.Checked = false;

			switch (Global.zoom)
			{
				case 1:
					Zoom1x.Checked = true;
					break;
				case 2:
					miZoom2x.Checked = true;
					break;
				case 4:
					miZoom4x.Checked = true;
					break;
			}
		}


		private class MRUMenuItem : MenuItem
		{

			public FileInfo file;

			public MRUMenuItem(int index, string filename)
			{
				file = new FileInfo(filename);
				Text = index.ToString() + " " + file.Name;
				MergeOrder = 1;
			}
		}
		private void FileMenu_Popup(object sender, System.EventArgs e)
		{

			ArrayList alRemove = new ArrayList();
			foreach (MenuItem mi in miFile.MenuItems)
				if (mi is MRUMenuItem)
					alRemove.Add(mi);
			foreach (MenuItem mi in alRemove)
				miFile.MenuItems.Remove(mi);
			ArrayList mrui = mru.getMRU();
			if (mrui.Count == 0)
			{
				mruSeparator.Visible = false;
			}
			else mruSeparator.Visible = true;
			for (int i = 0; i < mrui.Count; i++)
			{
				MRUMenuItem mi = new MRUMenuItem(1 + i, (string)mrui[i]);
				mi.Click += new EventHandler(mru_Click);
				miFile.MenuItems.Add(mruSeparator.Index + i + 1, mi);
			}
		}
		private void mru_Click(object sender, EventArgs e)
		{
			string fname = ((MRUMenuItem)sender).file.FullName;
			FileInfo fi = new FileInfo(fname);
			if (!fi.Exists)
			{
				statusBar.Panels[0].Text = "Unable to load " + fname;
				mru.untouch(fname);
			}
			LoadMap(fname);
			ui_update();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			ColorDialog cd = new ColorDialog(Color.FromArgb(0, 0, 0));
			cd.ShowDialog();

		}
		private void GetVspName()
		{
			GetVspNameWnd gvnm = new GetVspNameWnd();
			gvnm.ShowDialog();
			//Errors.Error(gvnm.VspName);
			Global.ActiveMap.vsp.fname = gvnm.VspName;
		}

		private bool CheckVSPLoc(string map, string vsp)
		{
			FileInfo fi_v = new FileInfo(vsp);
			if (map.ToLower() == fi_v.FullName.Substring(0, map.Length).ToLower())
				return true;
			return false;
		}
		private void SaveVsp(Vsp24 v, bool ForcePrompt)
		{
			string vspname;
			if (ForcePrompt || v.FileOnDisk == null)
			{
				saveVspDialog.ShowDialog();

				vspname = saveVspDialog.FileName;
				v.Write(vspname);
			}
			else
				v.Write();
		}
		private void SaveMap(Map m, bool ForcePrompt)
		{
			string mapname;
			string vspname;
			Vsp24 vsp = m.vsp;
			if (ForcePrompt || m.FileOnDisk == null)
			{
				// get location of map
				if (m.FileOnDisk != null)
					saveFileDialog.FileName = m.FileOnDisk.FullName;
				else saveFileDialog.FileName = "untitled.map";
				if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

				mapname = saveFileDialog.FileName;
				FileInfo fimap = new FileInfo(mapname);
				DirectoryInfo dimap = fimap.Directory;
				string dm = dimap.FullName;

				bool done = false;
				while (!done)
				{
					if (vsp.FileOnDisk != null)
						saveVspDialog.FileName = vsp.FileOnDisk.FullName;
					else saveVspDialog.FileName = Path.Combine(dimap.FullName, Path.GetFileNameWithoutExtension(fimap.Name)) + ".vsp";
					saveVspDialog.ShowDialog();

					vspname = saveVspDialog.FileName;
					if (CheckVSPLoc(dm, vspname))
					{
						// got a valid vsp filename
						// write vsp.
						vsp.Write(vspname);
						// write map
						m.WriteMap(mapname);

						mru.touch(mapname);

						statusBar.Panels[0].Text = "Saved " + Global.ActiveMap.FileOnDisk.FullName;

						done = true;
					}
					else
					{
						DialogResult dr = MessageBox.Show("The VSP must be located in the same directory, or a sub-directory of that directory, as the map file. Please choose such a location.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
						if (dr == DialogResult.Cancel)
							done = true;
					}
				}
			}
			else
			{
				// write vsp
				vsp.Write();

				// write map
				m.WriteMap();

				statusBar.Panels[0].Text = "Saved " + Global.ActiveMap.FileOnDisk.FullName;
			}
		}

		private void miSave_Click(object sender, System.EventArgs e)
		{
			SaveMap(Global.ActiveMap, false);
			ui_update();
		}
		private void miSaveAs_Click(object sender, System.EventArgs e)
		{
			SaveMap(Global.ActiveMap, true);
			ui_update();
		}
		private bool CloseCheck()
		{
			if (Global.ActiveMap == null) return false;
			if (!Global.ActiveMap.bAltered) return false;
			DialogResult dr = MessageBox.Show("You have unsaved data. Save changes?", "Closing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
			if (dr == DialogResult.Yes)
			{
				SaveMap(Global.ActiveMap, false);
				return false;
			}
			if (dr == DialogResult.No)
				return false;
			if (dr == DialogResult.Cancel)
				return true;
			return false;
		}
		private bool VspCloseCheck()
		{
			if (Global.ActiveMap == null) return false;
			if (Global.ActiveMap.vsp == null) return false;
			if (!Global.ActiveMap.vsp.bAltered) return false;
			DialogResult dr = MessageBox.Show("You have unsaved VSP data. Save changes?", "Closing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
			if (dr == DialogResult.Yes)
			{
				SaveVsp(Global.ActiveMap.vsp, false);
				return false;
			}
			if (dr == DialogResult.No)
				return false;
			if (dr == DialogResult.Cancel)
				return true;
			return false;
		}

		private void miClose_Click(object sender, System.EventArgs e)
		{
			// close file.
			bool c = CloseCheck();
			if (c == false) // go ahead and close
			{
				CloseMap();
			}
		}



		private void miMapProperties_Click(object sender, System.EventArgs e)
		{
			if (Global.ActiveMap == null) return;
			MapPropertiesWnd mpw = new MapPropertiesWnd();
			Map m = Global.ActiveMap;

			mpw.t_title.Text = m.FormalName;
			mpw.t_music.Text = m.MusicFileName;
			mpw.t_rstring.Text = m.RenderString;
			mpw.n_px.Value = m.PlayerStartX;
			mpw.n_py.Value = m.PlayerStartY;
			mpw.t_aescript.Text = m.AutoExecEvent;

			DialogResult dr = mpw.ShowDialog();
			if (dr == DialogResult.Cancel) return;

			m.FormalName = mpw.t_title.Text;
			m.MusicFileName = mpw.t_music.Text;
			m.RenderString = mpw.t_rstring.Text;
			m.PlayerStartX = (int)mpw.n_px.Value;
			m.PlayerStartY = (int)mpw.n_py.Value;
			m.AutoExecEvent = mpw.t_aescript.Text;
			if (mpw.t_vspfile.Text != "")
				m.vsp = InputOutput.ReadVsp24(new FileInfo(mpw.t_vspfile.Text));

			m.touch();
			ui_update();
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = CloseCheck();
			if (e.Cancel == false)
				Preferences.Current.Save();
		}

		private void mainpanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{

		}

		private void mi_zonelist_Popup(object sender, System.EventArgs e)
		{/*
			foreach(MenuItem mi in mi_zone.MenuItems)
				if(mi is ZoneMenuItem)
					mi_zone.MenuItems.Remove(mi);
			if(Global.ActiveMap==null)return;
			foreach(MapZone mz in Global.ActiveMap.Zones)
			{
				ZoneMenuItem mi = new ZoneMenuItem(mz.ID.ToString() + ": "+mz.Name);
				mi.mapzone = mz;
				mi_zone.MenuItems.Add(mi);
			}*/
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			OpenEntityEditor();
		}

		private void Global_CursorLocationChanged(winmaped2.Global.xyEventArgs e)
		{
			if (e.overmap)
				statusBar.Panels[1].Text = "(" + e.x + ", " + e.y + ")";
		}

		private void MainWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}

		private void miAppendFromVsp_Click(object sender, System.EventArgs e)
		{
		}

		private void miAddBlankTiles_Click(object sender, System.EventArgs e)
		{
		}

		private void miAppendFromImage_Click(object sender, System.EventArgs e)
		{
		}

		public void OpenEntityEditor()
		{
			EntityEditWnd eew = new EntityEditWnd();
			eew.init(Global.ActiveMap);
			if (eew.ShowDialog() == DialogResult.Cancel) return;
			Global.ActiveMap.Entities = eew.EditedEntities;
			update_ent_list();
			Global.ActiveMap.touch();
		}
		public void OpenZoneEditor()
		{
			if (Global.ActiveMap == null)
				return;
			EditZoneWnd ezw = new EditZoneWnd();
			ezw.init(Global.ActiveMap, Global.ActiveMap.Zones);
			MapZone mz = null;
			if (lv_zonelist.SelectedIndices.Count > 0)
			{
				mz = (MapZone) lv_zonelist.SelectedItems[0].Tag;
				ezw.seek(mz);
			}
			if (ezw.ShowDialog(mz) == DialogResult.Cancel) return;
			Global.ActiveMap.Zones = ezw.AlteredZones;
			Global.ActiveMap.ZoneLayer.Data = ezw.AlteredZoneLayer.Data;
			update_zone_list();
			lv_zonelist.SelectedIndices.Clear();
			if (ezw.lv_zones.SelectedIndices.Count > 0)
			{
				lv_zonelist.SelectedIndices.Add(ezw.lv_zones.SelectedIndices[0]);
				lv_zonelist.SelectedItems[0].EnsureVisible();
			}
			Global.ActiveMap.touch();
		}

		private void b_editents_Click(object sender, System.EventArgs e)
		{
			OpenEntityEditor();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			OpenZoneEditor();
		}

		private void miNewMap_Click(object sender, System.EventArgs e)
		{
			bool c = CloseCheck();
			if (c == true)
			{
				return;
			}
			else
			{
				CloseMap();
			}
			NewMapWnd nmw = new NewMapWnd();
			if (nmw.ShowDialog() != DialogResult.OK) return;
			Map m = nmw.CreateMap();
			Global.ActiveMap = m;
			lpInit(m);

			Global.ActiveVsp = m.vsp;

			vspController.SetActiveVsp(m.vsp);
			vspc_obs.SetActiveVsp(m.vsp);

			if (m.vsp.ObstructionTiles.Count == 0)
			{
				if (MessageBox.Show("Your VSP does not have any obstructions tiles. Would you like MapEd to generate the most commonly used basic obstruction tiles?", "Obstruction Tiles", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
								== DialogResult.Yes)
					m.vsp.AddBasicObstructionTiles();
			}

			update_zone_list();
			update_ent_list();

			ui_update();
		}

		private void mi_rft_Click(object sender, System.EventArgs e)
		{
			FileType.Register(".map", "Verge3 Map File");
			MessageBox.Show("Type registered.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void lv_ents_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lv_ents.SelectedIndices.Count == 0)
			{
				Global.SelectedEntity = null;
				b_gotoent.Enabled = false;
			}
			else
			{
				ListViewItem lvi = lv_ents.SelectedItems[0];
				MapEntity me = (MapEntity)lvi.Tag;
				Global.SelectedEntity = me;
				b_gotoent.Enabled = true;
			}
			ui_update();
		}

		private void lv_zonelist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lv_zonelist.SelectedIndices.Count == 0)
			{
				Global.SelectedZone = null;
			}
			else
			{
				ListViewItem lvi = lv_zonelist.SelectedItems[0];
				MapZone mz = (MapZone)lvi.Tag;
				Global._SelectedZone = mz;
			}
			ui_update();
		}

		private void lv_ents_DoubleClick(object sender, System.EventArgs e)
		{
			if (lv_ents.SelectedItems.Count == 0) return;
			MapEntity me = (MapEntity)lv_ents.SelectedItems[0].Tag;
			EntityEditWnd eew = new EntityEditWnd();
			eew.init(Global.ActiveMap);
			eew.seek(me);
			if (eew.ShowDialog() == DialogResult.Cancel) return;
			Global.ActiveMap.Entities = eew.EditedEntities;
			update_ent_list();
			Global.ActiveMap.touch();
			ui_update();
		}

		private void lv_zonelist_DoubleClick(object sender, System.EventArgs e)
		{
			if (lv_zonelist.SelectedItems.Count == 0) return;
			MapZone mz = (MapZone)lv_zonelist.SelectedItems[0].Tag;
			EditZoneWnd ezw = new EditZoneWnd();
			ezw.init(Global.ActiveMap, Global.ActiveMap.Zones);
			ezw.seek(mz);
			if (ezw.ShowDialog() == DialogResult.Cancel) return;
			Global.ActiveMap.Zones = ezw.AlteredZones;
			Global.ActiveMap.ZoneLayer.Data = ezw.AlteredZoneLayer.Data;
			update_zone_list();
			lv_zonelist.SelectedIndices.Clear();
			if (ezw.lv_zones.SelectedIndices.Count > 0)
			{
				lv_zonelist.SelectedIndices.Add(ezw.lv_zones.SelectedIndices[0]);
				lv_zonelist.SelectedItems[0].EnsureVisible();
			}

			Global.ActiveMap.touch();
		}

		private void b_gotoent_Click(object sender, System.EventArgs e)
		{
			if (Global.SelectedEntity == null) return;
			MapEntity me = Global.SelectedEntity;
			Global.mapViewer.SeekTo(me.TileX, me.TileY);
		}

		private void miAppendFromImageGrid_Click(object sender, System.EventArgs e)
		{
		}



		private void miExportToImage_Click(object sender, System.EventArgs e)
		{
			if (Global.ActiveMap == null) return;
			if (saveImageDialog.ShowDialog() != DialogResult.OK) return;
			Bitmap bmp = Global.ActiveMap.vsp.ExportToBitmap();
			bmp.Save(saveImageDialog.FileName);
		}

		private void miChangeVspExisting_Click(object sender, System.EventArgs e)
		{

		}

		private void Global_SelectedZoneChanged()
		{
			if (Global.SelectedZone == null) return;
			foreach (ListViewItem lvi in lv_zonelist.Items)
			{
				if (lvi.Tag == Global.SelectedZone)
				{
					lvi.Selected = true;
					lvi.EnsureVisible();
				}
			}
		}

		private void lPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{

		}

		private void b_layerproperties_Click(object sender, System.EventArgs e)
		{
			if (Global.editedLayer == null || Global.editedLayer.type != LayerType.Tile) return;
			MapLayer mLayerRef = Global.editedLayer;
			LayerPropertiesWnd lpw = new LayerPropertiesWnd();
			lpw.init(mLayerRef);
			if (lpw.ShowDialog() == DialogResult.Cancel) return;
			lpw.setvalues(mLayerRef);
			mLayerRef.parentmap.touch();
		}

		private void b_layeradd_Click(object sender, System.EventArgs e)
		{
			NewLayerWindow nlw = new NewLayerWindow();
			MapLayer bl = null;
			foreach (MapLayer ml in Global.ActiveMap.Layers)
				if (ml.type == LayerType.Tile)
				{
					bl = ml;
					break;
				}
			if (bl == null)
				nlw.init(100, 100);
			else nlw.init(bl.Width, bl.Height);
			if (nlw.ShowDialog() != DialogResult.OK) return;

			MapLayer mlz = new MapLayer(Global.ActiveMap);
			mlz.type = LayerType.Tile;
			mlz.size(nlw.SelectedSize.Width, nlw.SelectedSize.Height);
			mlz.name = "Layer " + (Global.ActiveMap.Layers.Count - 3);
			mlz.parallaxInfo = new ParallaxInfo();

			//			lpAddLayer(mlz, true,false, );
			Global.ActiveMap.AddLayer(mlz);
			lpUpdate(Global.ActiveMap, mlz);

		}

		ThrottleBuffer throttleDisplay;
		private void miEditTiles_Click(object sender, System.EventArgs e)
		{
			TileEd te = new TileEd();
			te.init(Global.ActiveMap.vsp, vspController.VspView.SelectedTileF, vspController.VspView.SelectedTileB);
			te.VspDataChanged += new TileEd.VspDataChangedHandler(te_VspDataChanged);
			te.ShowDialog();
		}

		private void postRedisplay(object sender, EventArgs args)
		{
			Invoke((MethodInvoker)delegate()
			{
				Invalidate(true);
			}
			);
		}

		private void te_VspDataChanged(Vsp24 vsp, int tileIndex)
		{
			throttleDisplay.signal();
		}

		private void button3_Click_1(object sender, System.EventArgs e)
		{
			if (Global.ActiveMap == null) return;
			if (Global.ActiveMap.FileOnDisk == null)
			{
				Errors.Error("Map must be saved to run in the engine.");
				return;
			}
			string mdf = Process.GetCurrentProcess().MainModule.FileName;
			FileInfo fi = new FileInfo(mdf);
			FileInfo vfi = new FileInfo(fi.DirectoryName + "\\verge.exe");
			if (!vfi.Exists)
			{
				Errors.Error("Unable to locate verge.exe");
				return;
			}
			ProcessStartInfo psi = new ProcessStartInfo(vfi.FullName, Global.ActiveMap.FileOnDisk.Name);
			psi.WorkingDirectory = Global.ActiveMap.FileOnDisk.DirectoryName;
			Process.Start(psi);
		}

		private void MainWindow_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
			{
				e.Effect = DragDropEffects.All;
			}
			else
				e.Effect = DragDropEffects.None;
		}

		private void MainWindow_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length > 0)
			{
				Errors.Error("Drag/Drop", "Drop one item only, please.");
				return;
			}
		}

		private void mi_ExportClipboard_Click(object sender, System.EventArgs e)
		{
			if (Global.ActiveMap == null || Global.ActiveMap.vsp == null) return;
			Global.ActiveMap.vsp.ExportToClipboard();
			statusBar.Panels[0].Text = "Exported tiles to clipboard.";
		}

		private void mi_ImportFromClipboard_Click(object sender, System.EventArgs e)
		{

		}

		private void miImportClipPadded_Click(object sender, System.EventArgs e)
		{

		}

		private void miRedo_Click(object sender, System.EventArgs e)
		{
			Global.opManager.redo();
		}

		private void Global_ActiveMapChanged()
		{
			mapController.ParentMap = Global.ActiveMap;
		}

		private void mhScrollBar_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{

		}

		private void mvScrollBar_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{

		}

		private void mViewer_Click(object sender, System.EventArgs e)
		{

		}

		private void tpClipboard_Click(object sender, System.EventArgs e)
		{

		}

		private void Global_ClipboardChanged()
		{
			mcClipboard.ParentMap = Global.clipboard;
		}

		private void mapController1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		}

		private unsafe void ImportTiles(bool repeat) {
            Vsp24 v = Global.ActiveMap.vsp;
            Vsp24 vsp = null;
            Bitmap bmp = null;

            int ic = 0;
            if (id.ISource == ImportSource.VSP) {
                if (!repeat)
                    if (openVspDialog.ShowDialog() != DialogResult.OK) return;
                vsp = InputOutput.ReadVsp(openVspDialog.FileName);
                if (vsp == null) {
                    Errors.Error("unable to load vsp");
                    return;
                }
            } else if (id.ISource == ImportSource.Image) {
                if (!repeat)
                    if (openImageDialog.ShowDialog() != DialogResult.OK) return;
								using (var tempbmp = new Bitmap(openImageDialog.FileName))
									bmp = (Bitmap)tempbmp.Clone();
            } else if (id.ISource == ImportSource.Clipboard) {
                if (!WindowsClipboard.IsImage) {
                    Errors.Error("There is no image in the clipboard.");
                    return;
                }
                bmp = WindowsClipboard.getBitmap();
            }

            ArrayList tiles = new ArrayList();
            if (id.IDest == ImportDest.Tiles) {
                int tstart = 0;
                if (id.ISource == ImportSource.Image || id.ISource == ImportSource.Clipboard) {
                    tiles = v.ImportTiles(bmp, id.bPadding ? 1 : 0);
                } else {
                    tiles = v.GetTiles(vsp);
                }
                if (id.IMethod == ImportMethod.Append) {
                    tstart = v.Tiles.Count;
                    v.Tiles.AddRange(tiles);
                } else if (id.IMethod == ImportMethod.Insert) {
                    tstart = id.InsertAt >= tiles.Count ? tiles.Count - 1 : id.InsertAt;
                    v.Tiles.InsertRange(tstart, tiles);
                } else if (id.IMethod == ImportMethod.Replace) {
                    if (MessageBox.Show("Warning, the current tiles will be wiped and replaced with imported tiles!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                            == DialogResult.Cancel) return;
                    v.Tiles.Clear();
                    v.Tiles.AddRange(tiles);
                    vspController.ResetView();
                    tstart = 0;
                }
                if (id.bAddLayer && (id.ISource == ImportSource.Image || id.ISource == ImportSource.Clipboard)) {
									int tw = bmp.Width / Global.TILE_SIZE;
									int th = bmp.Height / Global.TILE_SIZE;

                    MapLayer mlz = new MapLayer(Global.ActiveMap);
                    mlz.type = LayerType.Tile;
                    mlz.size(tw, th);
                    mlz.name = "Imported Layer "; //"Layer " + (Global.ActiveMap.Layers.Count - 3);
                    mlz.parallaxInfo = new ParallaxInfo();
                    for (int ty = 0; ty < th; ty++) {
                        for (int tx = 0; tx < tw; tx++) {
                            mlz.Data[ty * tw + tx] = (short)(tstart + ((ty * tw) + tx));
                        }
                    }
                    Global.ActiveMap.AddLayer(mlz);
                    lpUpdate(Global.ActiveMap, mlz);
                }
            } else if (id.IDest == ImportDest.Obs) {
                if (id.ISource == ImportSource.Image || id.ISource == ImportSource.Clipboard)
                    tiles = v.ImportObstructionTiles(bmp, id.bPadding ? 1 : 0);
                else
                    tiles = v.ImportObstructionTiles(vsp);
                if (id.IMethod == ImportMethod.Append)
                    v.ObstructionTiles.AddRange(tiles);
                else if (id.IMethod == ImportMethod.Insert) {
                    v.ObstructionTiles.InsertRange(id.InsertAt >= tiles.Count ? tiles.Count - 1 : id.InsertAt, tiles);
                } else if (id.IMethod == ImportMethod.Replace) {
                    if (MessageBox.Show("Warning, the current obstruction tiles will be wiped and replaced with imported tiles!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                            == DialogResult.Cancel) return;
                    v.ObstructionTiles.Clear();
                    v.ObstructionTiles.AddRange(tiles);
                    vspc_obs.ResetView();
                }
            }



            ic = tiles.Count;

            statusBar.Panels[0].Text = ic.ToString() + " tiles imported to VSP";

            miImportAgain.Enabled = true;

            ui_update();

        }
		ImportDialog id = new ImportDialog();
		private void miImport_Click(object sender, System.EventArgs e)
		{
			id.init();
			if (id.ShowDialog() != DialogResult.OK) return;
			ImportTiles(false);
		}

		private void miImportAgain_Click(object sender, System.EventArgs e)
		{
			ImportTiles(true);
		}

		private void miEditAnims_Click(object sender, System.EventArgs e)
		{
			AnimationEditor ae = new AnimationEditor();
			ae.init(Global.ActiveMap);
			if (ae.ShowDialog() != DialogResult.OK)
				return;
			Global.ActiveMap.vsp.Animations = ae.Anims;
			Global.FrameCalc.generate(ae.Anims);
		}

		private void VspView_SelectionChanged()
		{
			int sb = vspController.VspView.SelectedTileB;
			int sf = vspController.VspView.SelectedTileF;



			sbpSelection.Text = "Selected Tiles: " + sf + ", " + sb;
		}

		private void FrameCalc_OnTick()
		{
			//			mapController.Invalidate(true);
		}

		private void animTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			mapController.Invoke((MethodInvoker)delegate()
			{
				mapController.Invalidate(true);
			}
			);
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			Global.RenderOptions.bAnimate = animTimer.Enabled = checkBox1.Checked;
			mapController.Invoke((MethodInvoker)delegate()
			{
				mapController.Invalidate(true);
			}
			);
		}

		private void cb_transeffects_CheckedChanged(object sender, System.EventArgs e)
		{
			Global.RenderOptions.bTranslucentEffects = cb_transeffects.Checked;
			mapController.Invoke((MethodInvoker)delegate()
			{
				mapController.Invalidate(true);
			}
			);
		}

		private void b_layerup_Click(object sender, System.EventArgs e)
		{
			if (Global.ActiveMap == null) return;
			Global.ActiveMap.RenderManager.MoveUp(Global.lpSelection.mLayerRef);
			lpUpdate(Global.ActiveMap, Global.lpSelection.mLayerRef);
		}

		private void b_layerdown_Click(object sender, System.EventArgs e)
		{
			if (Global.ActiveMap == null) return;
			Global.ActiveMap.RenderManager.MoveDown(Global.lpSelection.mLayerRef);
			lpUpdate(Global.ActiveMap, Global.lpSelection.mLayerRef);
		}

		private void b_layerdel_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("WARNING: You are about to delete a layer. Are you sure you want to delete this layer?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
							== DialogResult.Cancel)
				return;
			Global.ActiveMap.DeleteLayer(Global.lpSelection.mLayerRef);
			lpInit(Global.ActiveMap);
		}

		private void Global_zoomChanged()
		{
			sbpZoom.Text = "Zoom: " + Global.zoom + "x";
		}
		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			Global.OpenHelp("index.html");
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			new EditVsp().ShowDialog();
		}

		private void tb_zoom_Scroll(object sender, System.EventArgs e)
		{
			Global.zoom = tb_zoom.Value;
		}
		private void zoomchanged()
		{
			if (tb_zoom.Value != Global.zoom)
				tb_zoom.Value = Global.zoom;
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			MapedPreferences mp = new MapedPreferences();
			mp.ShowDialog();
		}

		private void miExportImageGridless_Click(object sender, EventArgs e)
		{
			if (Global.ActiveMap == null) return;
			if (saveImageDialog.ShowDialog() != DialogResult.OK) return;
			Bitmap bmp = Global.ActiveMap.vsp.ExportToBitmap(0);
			bmp.Save(saveImageDialog.FileName);
		}

		private void miExportClipboardGridless_Click(object sender, EventArgs e)
		{
			if (Global.ActiveMap == null || Global.ActiveMap.vsp == null) return;
			Global.ActiveMap.vsp.ExportToClipboard(0);
			statusBar.Panels[0].Text = "Exported tiles to clipboard.";
		}

		private void checkObsXX_CheckedChanged(object sender, EventArgs e)
		{
			int obsTileID = 0;
			obsTileID |= checkObsLeft.Checked ? 1 : 0;
			obsTileID |= checkObsRight.Checked ? 2 : 0;
			obsTileID |= checkObsUp.Checked ? 4 : 0;
			obsTileID |= checkObsDown.Checked ? 8 : 0;
			Global.ObsVspViewer.SetObstructionViewerA(obsTileID);

		}

		private void btnObsAll_Click(object sender, EventArgs e)
		{
			checkObsLeft.Checked = true;
			checkObsRight.Checked = true;
			checkObsDown.Checked = true;
			checkObsUp.Checked = true;
		}

		private void miViewNotes_Click(object sender, EventArgs e)
		{
			miViewNotes.Checked ^= true;
			Update();
		}



	}


	public class ZoneMenuItem : MenuItem
	{
		public ZoneMenuItem(string name)
			: base(name)
		{
		}
		public MapZone mapzone;
	}
	public class SizeGrip : Control
	{
		protected override void OnPaint(PaintEventArgs e)
		{
			ControlPaint.DrawSizeGrip(e.Graphics, Parent.BackColor, 0, 0, Width, Height);
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
			}
		}


	}
}
