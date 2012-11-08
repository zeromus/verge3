using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace winmaped2
{
	/// <summary>
	/// Summary description for MapPropertiesWnd.
	/// </summary>
	public class MapPropertiesWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.TextBox t_title;
		public System.Windows.Forms.TextBox t_music;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button b_ok;
		private System.Windows.Forms.Button b_cancel;
		private System.Windows.Forms.Button b_browse;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.NumericUpDown n_px;
		public System.Windows.Forms.NumericUpDown n_py;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.TextBox t_rstring;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox t_aescript;
        private Panel g_vspfile;
        private Label label8;
        private Button button1;
        public TextBox t_vspfile;
        private Label label9;
        private Button button2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MapPropertiesWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(Global.ActiveMap.FileOnDisk==null)
				b_browse.Enabled=false;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.g_vspfile = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.t_vspfile = new System.Windows.Forms.TextBox();
            this.t_aescript = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.t_rstring = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.n_py = new System.Windows.Forms.NumericUpDown();
            this.n_px = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.b_browse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.t_music = new System.Windows.Forms.TextBox();
            this.t_title = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.b_ok = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.g_vspfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_py)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_px)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.g_vspfile);
            this.groupBox1.Controls.Add(this.t_aescript);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.t_rstring);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.n_py);
            this.groupBox1.Controls.Add(this.n_px);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.b_browse);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.t_music);
            this.groupBox1.Controls.Add(this.t_title);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 275);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(93, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(211, 23);
            this.label9.TabIndex = 14;
            this.label9.Text = "leave blank to not change";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // g_vspfile
            // 
            this.g_vspfile.Controls.Add(this.label8);
            this.g_vspfile.Controls.Add(this.button1);
            this.g_vspfile.Controls.Add(this.t_vspfile);
            this.g_vspfile.Location = new System.Drawing.Point(32, 178);
            this.g_vspfile.Name = "g_vspfile";
            this.g_vspfile.Size = new System.Drawing.Size(368, 40);
            this.g_vspfile.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 9;
            this.label8.Text = "VSP File:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(280, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Browse...";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // t_vspfile
            // 
            this.t_vspfile.Location = new System.Drawing.Point(128, 8);
            this.t_vspfile.Name = "t_vspfile";
            this.t_vspfile.Size = new System.Drawing.Size(144, 20);
            this.t_vspfile.TabIndex = 10;
            // 
            // t_aescript
            // 
            this.t_aescript.Location = new System.Drawing.Point(160, 152);
            this.t_aescript.Name = "t_aescript";
            this.t_aescript.Size = new System.Drawing.Size(152, 20);
            this.t_aescript.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(40, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 11;
            this.label7.Text = "Startup Script:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // t_rstring
            // 
            this.t_rstring.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_rstring.Location = new System.Drawing.Point(160, 120);
            this.t_rstring.Name = "t_rstring";
            this.t_rstring.ReadOnly = true;
            this.t_rstring.Size = new System.Drawing.Size(152, 22);
            this.t_rstring.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 23);
            this.label6.TabIndex = 9;
            this.label6.Text = "Render String:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(160, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "X";
            // 
            // n_py
            // 
            this.n_py.Location = new System.Drawing.Point(272, 88);
            this.n_py.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.n_py.Name = "n_py";
            this.n_py.Size = new System.Drawing.Size(72, 20);
            this.n_py.TabIndex = 7;
            // 
            // n_px
            // 
            this.n_px.Location = new System.Drawing.Point(176, 88);
            this.n_px.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.n_px.Name = "n_px";
            this.n_px.Size = new System.Drawing.Size(72, 20);
            this.n_px.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player Start Position:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // b_browse
            // 
            this.b_browse.Location = new System.Drawing.Point(336, 56);
            this.b_browse.Name = "b_browse";
            this.b_browse.Size = new System.Drawing.Size(75, 23);
            this.b_browse.TabIndex = 4;
            this.b_browse.Text = "Browse..";
            this.b_browse.Click += new System.EventHandler(this.b_browse_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Music File:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Map Title:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // t_music
            // 
            this.t_music.Location = new System.Drawing.Point(160, 56);
            this.t_music.Name = "t_music";
            this.t_music.Size = new System.Drawing.Size(168, 20);
            this.t_music.TabIndex = 1;
            // 
            // t_title
            // 
            this.t_title.Location = new System.Drawing.Point(160, 24);
            this.t_title.Name = "t_title";
            this.t_title.Size = new System.Drawing.Size(248, 20);
            this.t_title.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(256, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Y";
            // 
            // b_ok
            // 
            this.b_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.b_ok.Location = new System.Drawing.Point(267, 327);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 23);
            this.b_ok.TabIndex = 1;
            this.b_ok.Text = "OK";
            // 
            // b_cancel
            // 
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Location = new System.Drawing.Point(355, 327);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(75, 23);
            this.b_cancel.TabIndex = 2;
            this.b_cancel.Text = "Cancel";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Music File...";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Reset Obs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MapPropertiesWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(442, 362);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.b_cancel);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapPropertiesWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Properties";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.g_vspfile.ResumeLayout(false);
            this.g_vspfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_py)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_px)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void b_browse_Click(object sender, System.EventArgs e)
		{
			openFileDialog.Filter = "Music Files (*.s3m,*.xm,*.it,*.mod,*.mp3)|*.s3m;*.xm;*.it;*.mod;*.mp3|All Files (*.*)|*.*";
			DialogResult dr = openFileDialog.ShowDialog();
			if(dr==DialogResult.Cancel)return;
			string root = Global.ActiveMap.FileOnDisk.Directory.FullName;
			string path = Helper.GetRelativePath(root,openFileDialog.FileName);
			if(path==null) 
			{
				Errors.Error("Oops", "You selected a music file that wasn't in the same directory tree as your map. " + root + " : " + openFileDialog.FileName);
				return;
			}
			t_music.Text = path;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog.ShowDialog() != DialogResult.OK) return;
			t_vspfile.Text = openFileDialog.FileName;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Vsp24 vsp = Global.ActiveMap.vsp;
			vsp.ObstructionTiles.Clear();
			vsp.AddBasicObstructionTiles();
		}


	}
}
