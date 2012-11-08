using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace winmaped2
{
	/// <summary>
	/// Summary description for EditZoneWnd.
	/// </summary>
	public class EditZoneWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		public ListView lv_zones;
		private System.Windows.Forms.ColumnHeader ch_zoneID;
		private System.Windows.Forms.ColumnHeader ch_zoneName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tx_zoneName;
		private System.Windows.Forms.NumericUpDown num_zoneRate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chk_zoneAA;
		private System.Windows.Forms.TextBox tx_zoneScript;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button bn_OK;
		private System.Windows.Forms.Button bn_Cancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.GroupBox g_edit;
		private System.Windows.Forms.Button b_delzone;
		private System.Windows.Forms.MenuItem m_newzone;
		private System.Windows.Forms.MenuItem m_delzone;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.ContextMenu cm_ents;

		ArrayList zoneCopy = new ArrayList();

		public ArrayList AlteredZones { get { return zoneCopy; } }
		public MapLayer AlteredZoneLayer { get { return zoneLayerCopy; } }
		Map map;
		private Button b_dupzone;
		private GroupBox groupBox1;
		private RadioButton rbRight;
		private RadioButton rbLeft;
		private RadioButton rbDown;
		private RadioButton rbUp;
		private Label label6;
		private Button b_inszone;
		MapLayer zoneLayerCopy;

		public EditZoneWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}
		public void init(Map m, ArrayList zones)
		{
			map = m;
			zoneLayerCopy = m.ZoneLayer.copy();
			foreach (MapZone mz in zones)
			{
				zoneCopy.Add(mz.Clone());
			}
			foreach (MapZone mz in zoneCopy)
			{
				ListViewItem lvi = new ListViewItem(new string[] { mz.ID.ToString(), mz.Name });
				lvi.Tag = mz;
				lv_zones.Items.Add(lvi);
			}
		}
		public void seek(MapZone mz)
		{
			foreach (ListViewItem lvi in lv_zones.Items)
			{
				MapZone mz2 = (MapZone)lvi.Tag;
				if (mz2.ID == mz.ID)
				{
					lvi.Selected = true;
					lvi.EnsureVisible();
				}
			}
		}

		MapZone pendingSeek;
		public DialogResult ShowDialog(MapZone mz)
		{
			pendingSeek = mz;
			return ShowDialog();
		}

		protected override void OnShown(EventArgs e)
		{
			if (pendingSeek != null)
			{
				seek(pendingSeek);
				tx_zoneName.Focus();
			}
			pendingSeek = null;
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.g_edit = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rbRight = new System.Windows.Forms.RadioButton();
			this.rbLeft = new System.Windows.Forms.RadioButton();
			this.rbDown = new System.Windows.Forms.RadioButton();
			this.rbUp = new System.Windows.Forms.RadioButton();
			this.label6 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.tx_zoneScript = new System.Windows.Forms.TextBox();
			this.chk_zoneAA = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.num_zoneRate = new System.Windows.Forms.NumericUpDown();
			this.tx_zoneName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lv_zones = new System.Windows.Forms.ListView();
			this.ch_zoneID = new System.Windows.Forms.ColumnHeader();
			this.ch_zoneName = new System.Windows.Forms.ColumnHeader();
			this.cm_ents = new System.Windows.Forms.ContextMenu();
			this.m_newzone = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.m_delzone = new System.Windows.Forms.MenuItem();
			this.panel4 = new System.Windows.Forms.Panel();
			this.b_inszone = new System.Windows.Forms.Button();
			this.b_dupzone = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.b_delzone = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.bn_Cancel = new System.Windows.Forms.Button();
			this.bn_OK = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.g_edit.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_zoneRate)).BeginInit();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.g_edit);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(392, 420);
			this.panel1.TabIndex = 0;
			// 
			// g_edit
			// 
			this.g_edit.Controls.Add(this.groupBox1);
			this.g_edit.Controls.Add(this.label6);
			this.g_edit.Controls.Add(this.linkLabel1);
			this.g_edit.Controls.Add(this.tx_zoneScript);
			this.g_edit.Controls.Add(this.chk_zoneAA);
			this.g_edit.Controls.Add(this.label5);
			this.g_edit.Controls.Add(this.num_zoneRate);
			this.g_edit.Controls.Add(this.tx_zoneName);
			this.g_edit.Controls.Add(this.label4);
			this.g_edit.Controls.Add(this.label3);
			this.g_edit.Controls.Add(this.label2);
			this.g_edit.Controls.Add(this.label1);
			this.g_edit.Enabled = false;
			this.g_edit.Location = new System.Drawing.Point(16, 16);
			this.g_edit.Name = "g_edit";
			this.g_edit.Size = new System.Drawing.Size(360, 315);
			this.g_edit.TabIndex = 0;
			this.g_edit.TabStop = false;
			this.g_edit.Text = "Zone";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rbRight);
			this.groupBox1.Controls.Add(this.rbLeft);
			this.groupBox1.Controls.Add(this.rbDown);
			this.groupBox1.Controls.Add(this.rbUp);
			this.groupBox1.Location = new System.Drawing.Point(136, 126);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 68);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "(for spawnpoints)";
			// 
			// rbRight
			// 
			this.rbRight.AutoSize = true;
			this.rbRight.Location = new System.Drawing.Point(51, 42);
			this.rbRight.Name = "rbRight";
			this.rbRight.Size = new System.Drawing.Size(50, 17);
			this.rbRight.TabIndex = 13;
			this.rbRight.TabStop = true;
			this.rbRight.Text = "Right";
			this.rbRight.UseVisualStyleBackColor = true;
			// 
			// rbLeft
			// 
			this.rbLeft.AutoSize = true;
			this.rbLeft.Location = new System.Drawing.Point(6, 42);
			this.rbLeft.Name = "rbLeft";
			this.rbLeft.Size = new System.Drawing.Size(43, 17);
			this.rbLeft.TabIndex = 12;
			this.rbLeft.TabStop = true;
			this.rbLeft.Text = "Left";
			this.rbLeft.UseVisualStyleBackColor = true;
			// 
			// rbDown
			// 
			this.rbDown.AutoSize = true;
			this.rbDown.Location = new System.Drawing.Point(51, 19);
			this.rbDown.Name = "rbDown";
			this.rbDown.Size = new System.Drawing.Size(53, 17);
			this.rbDown.TabIndex = 11;
			this.rbDown.TabStop = true;
			this.rbDown.Text = "Down";
			this.rbDown.UseVisualStyleBackColor = true;
			// 
			// rbUp
			// 
			this.rbUp.AutoSize = true;
			this.rbUp.Location = new System.Drawing.Point(6, 19);
			this.rbUp.Name = "rbUp";
			this.rbUp.Size = new System.Drawing.Size(39, 17);
			this.rbUp.TabIndex = 10;
			this.rbUp.TabStop = true;
			this.rbUp.Text = "Up";
			this.rbUp.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(20, 121);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 23);
			this.label6.TabIndex = 11;
			this.label6.Text = "Facing:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// linkLabel1
			// 
			this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Blue;
			this.linkLabel1.Location = new System.Drawing.Point(320, 240);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(32, 16);
			this.linkLabel1.TabIndex = 9;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Help";
			this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// tx_zoneScript
			// 
			this.tx_zoneScript.Location = new System.Drawing.Point(136, 200);
			this.tx_zoneScript.Name = "tx_zoneScript";
			this.tx_zoneScript.Size = new System.Drawing.Size(200, 20);
			this.tx_zoneScript.TabIndex = 8;
			this.tx_zoneScript.TextChanged += new System.EventHandler(this.tx_zoneScript_TextChanged);
			// 
			// chk_zoneAA
			// 
			this.chk_zoneAA.Location = new System.Drawing.Point(136, 96);
			this.chk_zoneAA.Name = "chk_zoneAA";
			this.chk_zoneAA.Size = new System.Drawing.Size(80, 24);
			this.chk_zoneAA.TabIndex = 7;
			this.chk_zoneAA.Text = "Allow";
			this.chk_zoneAA.CheckedChanged += new System.EventHandler(this.chk_zoneAA_CheckedChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(200, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "/ 255";
			// 
			// num_zoneRate
			// 
			this.num_zoneRate.Location = new System.Drawing.Point(136, 64);
			this.num_zoneRate.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.num_zoneRate.Name = "num_zoneRate";
			this.num_zoneRate.Size = new System.Drawing.Size(64, 20);
			this.num_zoneRate.TabIndex = 5;
			this.num_zoneRate.ValueChanged += new System.EventHandler(this.num_zoneRate_ValueChanged);
			this.num_zoneRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.num_zoneRate_KeyUp);
			// 
			// tx_zoneName
			// 
			this.tx_zoneName.Location = new System.Drawing.Point(136, 32);
			this.tx_zoneName.Name = "tx_zoneName";
			this.tx_zoneName.Size = new System.Drawing.Size(200, 20);
			this.tx_zoneName.TabIndex = 4;
			this.tx_zoneName.TextChanged += new System.EventHandler(this.tx_zoneName_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 23);
			this.label4.TabIndex = 3;
			this.label4.Text = "Adjacent Activation:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 200);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Script Name:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Activation Rate:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lv_zones);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(392, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(232, 420);
			this.panel2.TabIndex = 1;
			// 
			// lv_zones
			// 
			this.lv_zones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_zoneID,
            this.ch_zoneName});
			this.lv_zones.ContextMenu = this.cm_ents;
			this.lv_zones.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv_zones.FullRowSelect = true;
			this.lv_zones.GridLines = true;
			this.lv_zones.HideSelection = false;
			this.lv_zones.Location = new System.Drawing.Point(0, 0);
			this.lv_zones.MultiSelect = false;
			this.lv_zones.Name = "lv_zones";
			this.lv_zones.Size = new System.Drawing.Size(232, 332);
			this.lv_zones.TabIndex = 0;
			this.lv_zones.UseCompatibleStateImageBehavior = false;
			this.lv_zones.View = System.Windows.Forms.View.Details;
			this.lv_zones.SelectedIndexChanged += new System.EventHandler(this.lv_zones_SelectedIndexChanged);
			// 
			// ch_zoneID
			// 
			this.ch_zoneID.Text = "ID";
			this.ch_zoneID.Width = 36;
			// 
			// ch_zoneName
			// 
			this.ch_zoneName.Text = "Name";
			this.ch_zoneName.Width = 174;
			// 
			// cm_ents
			// 
			this.cm_ents.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_newzone,
            this.menuItem3,
            this.m_delzone});
			this.cm_ents.Popup += new System.EventHandler(this.cm_ents_Popup);
			// 
			// m_newzone
			// 
			this.m_newzone.Index = 0;
			this.m_newzone.Text = "Create New Zone";
			this.m_newzone.Click += new System.EventHandler(this.button1_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// m_delzone
			// 
			this.m_delzone.Index = 2;
			this.m_delzone.Text = "Delete Selected Zone";
			this.m_delzone.Click += new System.EventHandler(this.b_delzone_Click);
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.Add(this.b_inszone);
			this.panel4.Controls.Add(this.b_dupzone);
			this.panel4.Controls.Add(this.button1);
			this.panel4.Controls.Add(this.b_delzone);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.Location = new System.Drawing.Point(0, 332);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(232, 88);
			this.panel4.TabIndex = 1;
			// 
			// b_inszone
			// 
			this.b_inszone.Enabled = false;
			this.b_inszone.Location = new System.Drawing.Point(120, 46);
			this.b_inszone.Name = "b_inszone";
			this.b_inszone.Size = new System.Drawing.Size(104, 32);
			this.b_inszone.TabIndex = 2;
			this.b_inszone.Text = "Insert Zone";
			this.b_inszone.Click += new System.EventHandler(this.b_inszone_Click);
			// 
			// b_dupzone
			// 
			this.b_dupzone.Enabled = false;
			this.b_dupzone.Location = new System.Drawing.Point(8, 46);
			this.b_dupzone.Name = "b_dupzone";
			this.b_dupzone.Size = new System.Drawing.Size(104, 32);
			this.b_dupzone.TabIndex = 1;
			this.b_dupzone.Text = "Dup Selected";
			this.b_dupzone.Click += new System.EventHandler(this.b_dupzone_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(104, 32);
			this.button1.TabIndex = 0;
			this.button1.Text = "Add New Zone";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// b_delzone
			// 
			this.b_delzone.Enabled = false;
			this.b_delzone.Location = new System.Drawing.Point(120, 8);
			this.b_delzone.Name = "b_delzone";
			this.b_delzone.Size = new System.Drawing.Size(104, 32);
			this.b_delzone.TabIndex = 0;
			this.b_delzone.Text = "Delete Selected";
			this.b_delzone.Click += new System.EventHandler(this.b_delzone_Click);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.bn_Cancel);
			this.panel3.Controls.Add(this.bn_OK);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 420);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(624, 40);
			this.panel3.TabIndex = 1;
			// 
			// bn_Cancel
			// 
			this.bn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bn_Cancel.Location = new System.Drawing.Point(544, 8);
			this.bn_Cancel.Name = "bn_Cancel";
			this.bn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.bn_Cancel.TabIndex = 1;
			this.bn_Cancel.Text = "Cancel";
			// 
			// bn_OK
			// 
			this.bn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bn_OK.Location = new System.Drawing.Point(456, 8);
			this.bn_OK.Name = "bn_OK";
			this.bn_OK.Size = new System.Drawing.Size(75, 23);
			this.bn_OK.TabIndex = 0;
			this.bn_OK.Text = "OK";
			// 
			// EditZoneWnd
			// 
			this.AcceptButton = this.bn_OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.bn_Cancel;
			this.ClientSize = new System.Drawing.Size(624, 460);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(632, 10000);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(632, 296);
			this.Name = "EditZoneWnd";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Zones";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.EditZoneWnd_Closing);
			this.panel1.ResumeLayout(false);
			this.g_edit.ResumeLayout(false);
			this.g_edit.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_zoneRate)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		bool bInvokeItems = true;
		void DisableEditor()
		{
			g_edit.Enabled = false;
		}
		void EnableEditor()
		{
			g_edit.Enabled = true;
		}
		void EmptyFields()
		{
			tx_zoneName.Text = "";
			num_zoneRate.Value = 0;
			chk_zoneAA.Checked = false;
			tx_zoneScript.Text = "";
		}
		void UpdateListView()
		{
			if (lv_zones.SelectedItems.Count == 0) return;
			ListViewItem lvi = (ListViewItem)lv_zones.SelectedItems[0];
			lvi.SubItems[1].Text = tx_zoneName.Text;
		}
		void ResetListView()
		{
			lv_zones.Items.Clear();
			foreach (MapZone mz in zoneCopy)
			{
				ListViewItem lvi = new ListViewItem(new string[] { mz.ID.ToString(), mz.Name });
				lvi.Tag = mz;
				lv_zones.Items.Add(lvi);
			}
		}

		void FieldsToItem()
		{
			if (!bInvokeItems) return;
			if (lv_zones.SelectedItems.Count == 0) return;
			MapZone mz = (MapZone)((ListViewItem)lv_zones.SelectedItems[0]).Tag;
			mz.Name = tx_zoneName.Text;
			mz.Rate = (int)num_zoneRate.Value;
			mz.Flags = (chk_zoneAA.Checked ? 1 : 0);
			if (rbUp.Checked) mz.Facing = 0;
			if (rbDown.Checked) mz.Facing = 1;
			if (rbLeft.Checked) mz.Facing = 2;
			if (rbRight.Checked) mz.Facing = 3;
			mz.PlayerScript = tx_zoneScript.Text;
		}
		void ItemsToField(MapZone mz)
		{
			bInvokeItems = false;
			tx_zoneName.Text = mz.Name;
			num_zoneRate.Value = mz.Rate;
			chk_zoneAA.Checked = (mz.Flags&1) == 1;
			if (mz.Facing == 0) rbUp.Checked = true;
			if (mz.Facing == 1) rbDown.Checked = true;
			if (mz.Facing == 2) rbLeft.Checked = true;
			if (mz.Facing == 3) rbRight.Checked = true;
			tx_zoneScript.Text = mz.PlayerScript;
			bInvokeItems = true;
		}
		private void lv_zones_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lv_zones.SelectedIndices.Count == 0)
			{
				EmptyFields();
				DisableEditor();
				b_delzone.Enabled = false;
				b_dupzone.Enabled = false;
				b_inszone.Enabled = false;
				return;
			}
			b_delzone.Enabled = true;
			b_dupzone.Enabled = true;
			b_inszone.Enabled = true;
			ListViewItem lvi = (ListViewItem)lv_zones.SelectedItems[0];
			MapZone mz = (MapZone)lvi.Tag;
			ItemsToField(mz);
			if (mz.ID == 0)
			{
				b_delzone.Enabled = false;
				b_dupzone.Enabled = false;
				DisableEditor();
			}
			else
			{
				b_delzone.Enabled = true;
				EnableEditor();
			}

		}


		private void b_dupzone_Click(object sender, EventArgs e)
		{
			ListViewItem lvi = (ListViewItem)lv_zones.SelectedItems[0];
			MapZone mzSel = (MapZone)lvi.Tag;
			MapZone mzNew = mzSel.Clone();
			zoneCopy.Add(mzNew);
			update_zoneIDs();
		}

		private void tx_zoneName_TextChanged(object sender, System.EventArgs e)
		{
			UpdateListView();
			FieldsToItem();
		}

		private void num_zoneRate_ValueChanged(object sender, System.EventArgs e)
		{
			FieldsToItem();
		}

		private void chk_zoneAA_CheckedChanged(object sender, System.EventArgs e)
		{
			FieldsToItem();
		}

		private void tx_zoneScript_TextChanged(object sender, System.EventArgs e)
		{
			FieldsToItem();
		}

		private void EditZoneWnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FieldsToItem();
		}
		private void StopNonIntKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8)
				e.Handled = true;
		}
		private void num_zoneRate_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			FieldsToItem();

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			MapZone mz = new MapZone();
			mz.ID = zoneCopy.Count;
			mz.Rate = 255;
			zoneCopy.Add(mz);
			ListViewItem lvi = new ListViewItem(new string[] { mz.ID.ToString(), mz.Name });
			lvi.Tag = mz;
			lv_zones.Items.Add(lvi);

			lvi.Selected = true;
			lvi.EnsureVisible();
			tx_zoneName.Focus();
		}
		private void update_zoneIDs()
		{
			for (int i = 0; i < zoneCopy.Count; i++)
				((MapZone)zoneCopy[i]).ID = i;
			ResetListView();
		}


		private void b_inszone_Click(object sender, EventArgs e)
		{
			if (lv_zones.SelectedItems.Count == 0) return;
			int id = lv_zones.SelectedIndices[0];
			zoneCopy.Insert(id, new MapZone());
			bool reindex = false;
			if (MessageBox.Show("The zone has been inserted. Do you wish for MapEd to reindex existing zones on your map?\r\nOn larger maps, this may take a moment.\r\nIf you have any existing VC code that relies on the event numbers, do not forget to update that as well!", "Reorder zones?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				== DialogResult.Yes)
				reindex = true;
			int t = 0;
			for (int y = 0; y < zoneLayerCopy.Height; y++)
				for (int x = 0; x < zoneLayerCopy.Width; x++)
				{
					if ((t = zoneLayerCopy.getTile(x, y)) >= id && reindex)
						zoneLayerCopy.setTile(x, y, t + 1);
				}
			update_zoneIDs();
		}

		private void b_delzone_Click(object sender, System.EventArgs e)
		{
			if (lv_zones.SelectedItems.Count == 0) return;
			MapZone mz = (MapZone)lv_zones.SelectedItems[0].Tag;
			if (MessageBox.Show("Are you sure you want to delete Zone #" + mz.ID + "? This action can not be undone. \r\nWARNING: If there are zones in the list with a higher ID than this zone, they will be shifted down and will henceforth have a different ID. \r\nYou will be given the option to reindex existing map zones, so that your indices do not become invalid.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
				== DialogResult.Cancel) return;
			zoneCopy.Remove(mz);
			bool reindex = false;
			if (MessageBox.Show("The zone has been deleted. Do you wish for MapEd to reindex existing zones on your map?\r\nOn larger maps, this may take a moment.\r\nIf you have any existing VC code that relies on the event numbers, do not forget to update that as well!", "Reorder zones?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				== DialogResult.Yes)
				reindex = true;
			int t = 0;
			for (int y = 0; y < zoneLayerCopy.Height; y++)
				for (int x = 0; x < zoneLayerCopy.Width; x++)
				{
					if (zoneLayerCopy.getTile(x, y) == mz.ID)
						zoneLayerCopy.setTile(x, y, 0);
					else if ((t = zoneLayerCopy.getTile(x, y)) > mz.ID && reindex)
						zoneLayerCopy.setTile(x, y, t - 1);
				}
			update_zoneIDs();
		}

		private void cm_ents_Popup(object sender, System.EventArgs e)
		{
			if (lv_zones.SelectedItems.Count == 0) m_delzone.Enabled = false;
			else
			{
				if (lv_zones.SelectedIndices[0] == 0)
					m_delzone.Enabled = false;
				else m_delzone.Enabled = true;
			}
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Global.OpenHelp("index.html");
		}


	}
}
