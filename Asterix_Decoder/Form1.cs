using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ClassLibrary;
using Class_Library;
using GMap;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;
using MultiCAT6.Utils;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace Asterix_Decoder
{
    public partial class Form1 : Form
    {
        GMarkerGoogle Marker_MLAT;
        GMapOverlay GMapOverlay_MLAT;
        GMarkerGoogle Marker_SMR;
        GMapOverlay GMapOverlay_SMR;

        DataTable DataTable_AsterixFile;

        public int FileCount = 0;
        public List<AsterixFile> AsterixFiles = new List<AsterixFile>();
        public List<CAT10> Messages_List_CAT10;
        public string Start_Simulation_time;
        public DecodeLibrary Library = new DecodeLibrary();
        public double Actual_Time;
        CoordinatesWGS84 PositionWGS84_SMR = new CoordinatesWGS84();
        CoordinatesWGS84 PositionWGS84_MLAT = new CoordinatesWGS84();
        List<MapPoints> points = new List<MapPoints>();
        List<MapPoints> List_points_SMR = new List<MapPoints>();
        List<MapPoints> List_points_MLAT = new List<MapPoints>();
        List<MapPoints> List_points_ADSB;
        List<PointLatLng> List_MapPoints;
        List<PointLatLng> List_MapPoints_SMR;
        List<PointLatLng> List_MapPoints_MLAT;
        List<PointLatLng> List_MapPoints_ADSB;
        bool SMR_WantToLoad = false;
        bool MLAT_WantToLoad = false;
        bool ADSB_WantToLoad = false;
        bool ALL_WantToLoad = false;

        int Previous_Index;

        public Form1()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Load_Asterix_Btn = new System.Windows.Forms.Button();
            this.Read_Asterix_Btn = new System.Windows.Forms.Button();
            this.openFileDialogAsterix = new System.Windows.Forms.OpenFileDialog();
            this.Selector_Archivo = new System.Windows.Forms.ComboBox();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Data_table_panel = new System.Windows.Forms.Panel();
            this.Search_textBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.WantToLoad_Panel = new System.Windows.Forms.Panel();
            this.Done_Button = new System.Windows.Forms.Button();
            this.WantToLoad_CheckList = new System.Windows.Forms.CheckedListBox();
            this.Load_File_Panel = new System.Windows.Forms.Panel();
            this.progressBarLoadFile = new System.Windows.Forms.ProgressBar();
            this.Data_Table_Button = new System.Windows.Forms.Button();
            this.Load_File_Button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Simulation_Button = new System.Windows.Forms.Button();
            this.Simulation_Panel = new System.Windows.Forms.Panel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.SMR_filter = new System.Windows.Forms.CheckBox();
            this.MLAT_filter = new System.Windows.Forms.CheckBox();
            this.Actual_Time_Label = new System.Windows.Forms.Label();
            this.Start_Time_Label = new System.Windows.Forms.Label();
            this.Reset_Simulation_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Start_Simulation_Button = new System.Windows.Forms.Button();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.Stop_Simulation_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.Data_table_panel.SuspendLayout();
            this.WantToLoad_Panel.SuspendLayout();
            this.Load_File_Panel.SuspendLayout();
            this.Simulation_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Load_Asterix_Btn
            // 
            this.Load_Asterix_Btn.Location = new System.Drawing.Point(51, 129);
            this.Load_Asterix_Btn.Name = "Load_Asterix_Btn";
            this.Load_Asterix_Btn.Size = new System.Drawing.Size(256, 114);
            this.Load_Asterix_Btn.TabIndex = 0;
            this.Load_Asterix_Btn.Text = "LOAD ASTERIX FILE";
            this.Load_Asterix_Btn.UseVisualStyleBackColor = true;
            this.Load_Asterix_Btn.Click += new System.EventHandler(this.Load_Asterix_Btn_Click);
            // 
            // Read_Asterix_Btn
            // 
            this.Read_Asterix_Btn.Location = new System.Drawing.Point(382, 129);
            this.Read_Asterix_Btn.Name = "Read_Asterix_Btn";
            this.Read_Asterix_Btn.Size = new System.Drawing.Size(256, 114);
            this.Read_Asterix_Btn.TabIndex = 2;
            this.Read_Asterix_Btn.Text = "READ ASTERIX FILE";
            this.Read_Asterix_Btn.UseVisualStyleBackColor = true;
            this.Read_Asterix_Btn.Click += new System.EventHandler(this.Read_Asterix_Btn_Click);
            // 
            // openFileDialogAsterix
            // 
            this.openFileDialogAsterix.FileName = "openFileDialogAsterix";
            // 
            // Selector_Archivo
            // 
            this.Selector_Archivo.FormattingEnabled = true;
            this.Selector_Archivo.Location = new System.Drawing.Point(51, 271);
            this.Selector_Archivo.Name = "Selector_Archivo";
            this.Selector_Archivo.Size = new System.Drawing.Size(587, 28);
            this.Selector_Archivo.TabIndex = 3;
            this.Selector_Archivo.SelectedIndexChanged += new System.EventHandler(this.Selector_Archivo_SelectedIndexChanged);
            // 
            // DGV
            // 
            this.DGV.AllowDrop = true;
            this.DGV.AllowUserToOrderColumns = true;
            this.DGV.AllowUserToResizeRows = false;
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.DGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGV.GridColor = System.Drawing.SystemColors.MenuHighlight;
            this.DGV.Location = new System.Drawing.Point(22, 87);
            this.DGV.Name = "DGV";
            this.DGV.RowHeadersWidth = 51;
            this.DGV.RowTemplate.Height = 29;
            this.DGV.RowTemplate.ReadOnly = true;
            this.DGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGV.Size = new System.Drawing.Size(1586, 757);
            this.DGV.TabIndex = 4;
            this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellClick);
            // 
            // Data_table_panel
            // 
            this.Data_table_panel.BackColor = System.Drawing.SystemColors.InfoText;
            this.Data_table_panel.Controls.Add(this.Search_textBox);
            this.Data_table_panel.Controls.Add(this.button1);
            this.Data_table_panel.Controls.Add(this.DGV);
            this.Data_table_panel.Location = new System.Drawing.Point(260, 12);
            this.Data_table_panel.Name = "Data_table_panel";
            this.Data_table_panel.Size = new System.Drawing.Size(1630, 866);
            this.Data_table_panel.TabIndex = 5;
            // 
            // Search_textBox
            // 
            this.Search_textBox.Location = new System.Drawing.Point(1154, 24);
            this.Search_textBox.Name = "Search_textBox";
            this.Search_textBox.Size = new System.Drawing.Size(454, 27);
            this.Search_textBox.TabIndex = 6;
            this.Search_textBox.TextChanged += new System.EventHandler(this.Search_textBox_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 51);
            this.button1.TabIndex = 5;
            this.button1.Text = "Export CSV";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WantToLoad_Panel
            // 
            this.WantToLoad_Panel.Controls.Add(this.Done_Button);
            this.WantToLoad_Panel.Controls.Add(this.WantToLoad_CheckList);
            this.WantToLoad_Panel.Location = new System.Drawing.Point(382, 87);
            this.WantToLoad_Panel.Name = "WantToLoad_Panel";
            this.WantToLoad_Panel.Size = new System.Drawing.Size(256, 171);
            this.WantToLoad_Panel.TabIndex = 7;
            // 
            // Done_Button
            // 
            this.Done_Button.Location = new System.Drawing.Point(80, 131);
            this.Done_Button.Name = "Done_Button";
            this.Done_Button.Size = new System.Drawing.Size(94, 29);
            this.Done_Button.TabIndex = 1;
            this.Done_Button.Text = "DONE";
            this.Done_Button.UseVisualStyleBackColor = true;
            this.Done_Button.Click += new System.EventHandler(this.Done_Button_Click);
            // 
            // WantToLoad_CheckList
            // 
            this.WantToLoad_CheckList.FormattingEnabled = true;
            this.WantToLoad_CheckList.Items.AddRange(new object[] {
            "CAT10_SMR",
            "CAT10_MLAT",
            "CAT21_ADSB",
            "ALL CAT"});
            this.WantToLoad_CheckList.Location = new System.Drawing.Point(55, 33);
            this.WantToLoad_CheckList.Name = "WantToLoad_CheckList";
            this.WantToLoad_CheckList.Size = new System.Drawing.Size(150, 92);
            this.WantToLoad_CheckList.TabIndex = 0;
            // 
            // Load_File_Panel
            // 
            this.Load_File_Panel.BackColor = System.Drawing.SystemColors.InfoText;
            this.Load_File_Panel.Controls.Add(this.WantToLoad_Panel);
            this.Load_File_Panel.Controls.Add(this.progressBarLoadFile);
            this.Load_File_Panel.Controls.Add(this.Load_Asterix_Btn);
            this.Load_File_Panel.Controls.Add(this.Selector_Archivo);
            this.Load_File_Panel.Controls.Add(this.Read_Asterix_Btn);
            this.Load_File_Panel.Location = new System.Drawing.Point(260, 12);
            this.Load_File_Panel.Name = "Load_File_Panel";
            this.Load_File_Panel.Size = new System.Drawing.Size(1630, 866);
            this.Load_File_Panel.TabIndex = 8;
            // 
            // progressBarLoadFile
            // 
            this.progressBarLoadFile.Location = new System.Drawing.Point(51, 376);
            this.progressBarLoadFile.Name = "progressBarLoadFile";
            this.progressBarLoadFile.Size = new System.Drawing.Size(587, 29);
            this.progressBarLoadFile.TabIndex = 9;
            // 
            // Data_Table_Button
            // 
            this.Data_Table_Button.Location = new System.Drawing.Point(22, 226);
            this.Data_Table_Button.Name = "Data_Table_Button";
            this.Data_Table_Button.Size = new System.Drawing.Size(213, 85);
            this.Data_Table_Button.TabIndex = 7;
            this.Data_Table_Button.Text = "Show Data Table";
            this.Data_Table_Button.UseVisualStyleBackColor = true;
            this.Data_Table_Button.Click += new System.EventHandler(this.Data_Table_Button_Click);
            // 
            // Load_File_Button
            // 
            this.Load_File_Button.Location = new System.Drawing.Point(22, 125);
            this.Load_File_Button.Name = "Load_File_Button";
            this.Load_File_Button.Size = new System.Drawing.Size(213, 85);
            this.Load_File_Button.TabIndex = 8;
            this.Load_File_Button.Text = "Load File\r\n";
            this.Load_File_Button.UseVisualStyleBackColor = true;
            this.Load_File_Button.Click += new System.EventHandler(this.Load_File_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Simulation_Button
            // 
            this.Simulation_Button.Location = new System.Drawing.Point(22, 332);
            this.Simulation_Button.Name = "Simulation_Button";
            this.Simulation_Button.Size = new System.Drawing.Size(213, 76);
            this.Simulation_Button.TabIndex = 9;
            this.Simulation_Button.Text = "Simulation";
            this.Simulation_Button.UseVisualStyleBackColor = true;
            this.Simulation_Button.Click += new System.EventHandler(this.Simulation_Button_Click);
            // 
            // Simulation_Panel
            // 
            this.Simulation_Panel.BackColor = System.Drawing.SystemColors.InfoText;
            this.Simulation_Panel.Controls.Add(this.checkedListBox1);
            this.Simulation_Panel.Controls.Add(this.SMR_filter);
            this.Simulation_Panel.Controls.Add(this.MLAT_filter);
            this.Simulation_Panel.Controls.Add(this.Actual_Time_Label);
            this.Simulation_Panel.Controls.Add(this.Start_Time_Label);
            this.Simulation_Panel.Controls.Add(this.Reset_Simulation_Button);
            this.Simulation_Panel.Controls.Add(this.label1);
            this.Simulation_Panel.Controls.Add(this.Start_Simulation_Button);
            this.Simulation_Panel.Controls.Add(this.gMapControl1);
            this.Simulation_Panel.Controls.Add(this.Stop_Simulation_Button);
            this.Simulation_Panel.Location = new System.Drawing.Point(260, 12);
            this.Simulation_Panel.Name = "Simulation_Panel";
            this.Simulation_Panel.Size = new System.Drawing.Size(1630, 866);
            this.Simulation_Panel.TabIndex = 9;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.checkedListBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "x1",
            "x2",
            "x10",
            "x0.5"});
            this.checkedListBox1.Location = new System.Drawing.Point(1192, 288);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(60, 92);
            this.checkedListBox1.TabIndex = 13;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged_1);
            // 
            // SMR_filter
            // 
            this.SMR_filter.AutoSize = true;
            this.SMR_filter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SMR_filter.Location = new System.Drawing.Point(1192, 446);
            this.SMR_filter.Name = "SMR_filter";
            this.SMR_filter.Size = new System.Drawing.Size(61, 24);
            this.SMR_filter.TabIndex = 11;
            this.SMR_filter.Text = "SMR";
            this.SMR_filter.UseVisualStyleBackColor = true;
            // 
            // MLAT_filter
            // 
            this.MLAT_filter.AutoSize = true;
            this.MLAT_filter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MLAT_filter.Location = new System.Drawing.Point(1192, 416);
            this.MLAT_filter.Name = "MLAT_filter";
            this.MLAT_filter.Size = new System.Drawing.Size(68, 24);
            this.MLAT_filter.TabIndex = 10;
            this.MLAT_filter.Text = "MLAT";
            this.MLAT_filter.UseVisualStyleBackColor = true;
            // 
            // Actual_Time_Label
            // 
            this.Actual_Time_Label.AutoSize = true;
            this.Actual_Time_Label.BackColor = System.Drawing.SystemColors.InfoText;
            this.Actual_Time_Label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Actual_Time_Label.Location = new System.Drawing.Point(1325, 37);
            this.Actual_Time_Label.Name = "Actual_Time_Label";
            this.Actual_Time_Label.Size = new System.Drawing.Size(0, 20);
            this.Actual_Time_Label.TabIndex = 6;
            // 
            // Start_Time_Label
            // 
            this.Start_Time_Label.AutoSize = true;
            this.Start_Time_Label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Start_Time_Label.Location = new System.Drawing.Point(1193, 37);
            this.Start_Time_Label.Name = "Start_Time_Label";
            this.Start_Time_Label.Size = new System.Drawing.Size(0, 20);
            this.Start_Time_Label.TabIndex = 5;
            // 
            // Reset_Simulation_Button
            // 
            this.Reset_Simulation_Button.Location = new System.Drawing.Point(1195, 129);
            this.Reset_Simulation_Button.Name = "Reset_Simulation_Button";
            this.Reset_Simulation_Button.Size = new System.Drawing.Size(94, 29);
            this.Reset_Simulation_Button.TabIndex = 4;
            this.Reset_Simulation_Button.Text = "Reset";
            this.Reset_Simulation_Button.UseVisualStyleBackColor = true;
            this.Reset_Simulation_Button.Click += new System.EventHandler(this.Reset_Simulation_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(1195, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 2;
            // 
            // Start_Simulation_Button
            // 
            this.Start_Simulation_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Start_Simulation_Button.Location = new System.Drawing.Point(1195, 75);
            this.Start_Simulation_Button.Name = "Start_Simulation_Button";
            this.Start_Simulation_Button.Size = new System.Drawing.Size(94, 29);
            this.Start_Simulation_Button.TabIndex = 1;
            this.Start_Simulation_Button.Text = "Start";
            this.Start_Simulation_Button.UseVisualStyleBackColor = true;
            this.Start_Simulation_Button.Click += new System.EventHandler(this.Start_Simulation_Button_Click);
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(22, 24);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(1085, 820);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            // 
            // Stop_Simulation_Button
            // 
            this.Stop_Simulation_Button.Location = new System.Drawing.Point(1195, 75);
            this.Stop_Simulation_Button.Name = "Stop_Simulation_Button";
            this.Stop_Simulation_Button.Size = new System.Drawing.Size(94, 29);
            this.Stop_Simulation_Button.TabIndex = 3;
            this.Stop_Simulation_Button.Text = "Stop";
            this.Stop_Simulation_Button.UseVisualStyleBackColor = true;
            this.Stop_Simulation_Button.Click += new System.EventHandler(this.Stop_Simulation_Button_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.Simulation_Button);
            this.Controls.Add(this.Load_File_Button);
            this.Controls.Add(this.Data_Table_Button);
            this.Controls.Add(this.Load_File_Panel);
            this.Controls.Add(this.Simulation_Panel);
            this.Controls.Add(this.Data_table_panel);
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.Data_table_panel.ResumeLayout(false);
            this.Data_table_panel.PerformLayout();
            this.WantToLoad_Panel.ResumeLayout(false);
            this.Load_File_Panel.ResumeLayout(false);
            this.Simulation_Panel.ResumeLayout(false);
            this.Simulation_Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        public void Load_Asterix_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogAsterix.InitialDirectory = "c:\\";
                openFileDialogAsterix.Filter = "ast files (*.ast)|*.ast|All files (*.*)|*.*";
                openFileDialogAsterix.FilterIndex = 2;
                openFileDialogAsterix.RestoreDirectory = true;

                if (openFileDialogAsterix.ShowDialog() == DialogResult.OK)
                {
                    AsterixFile Asterix = new AsterixFile(null, null);

                    string File_Path = openFileDialogAsterix.FileName.ToString();
                    string File_Name = Path.GetFileName(File_Path);

                    bool IsIn = AsterixFiles.Exists(Asterix => Asterix.path == File_Path);
                    if (IsIn == false)
                    {
                        FileCount++;
                        Asterix.path = File_Path;
                        Asterix.name = File_Name;
                        AsterixFiles.Add(Asterix);  
                        Selector_Archivo.Items.Add(File_Name);
                        Selector_Archivo.Text = (File_Name);
                        //Messages_List_CAT10 =  Asterix.get_CAT10List();
                    }
                }
                WantToLoad_Panel.Visible = true;
            }
            
            catch
            {
                MessageBox.Show("The file selected could not be loaded");
            }
            
        }


        private void Read_Asterix_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (WantToLoad_CheckList.SelectedItem.ToString() == "CAT10_SMR") { SMR_WantToLoad = true; }
                if (WantToLoad_CheckList.SelectedItem.ToString() == "CAT10_MLAT") { MLAT_WantToLoad = true; }
                if (WantToLoad_CheckList.SelectedItem.ToString() == "CAT21_ADSB") { ADSB_WantToLoad = true; }
                if (WantToLoad_CheckList.SelectedItem.ToString() == "ALL CAT") { ALL_WantToLoad = true; }

                int index = Selector_Archivo.SelectedIndex;
                progressBarLoadFile.Maximum = AsterixFiles[index].get_FileSize();
                progressBarLoadFile.Minimum = 0;
                progressBarLoadFile.Value = 0;
                progressBarLoadFile.Step = 1;
                progressBarLoadFile.Style = ProgressBarStyle.Continuous;
                List<DataTable> Return = AsterixFiles[index].ReadFile(AsterixFiles[index].path, AsterixFiles[index].name);
                if (SMR_WantToLoad == true && ALL_WantToLoad == false)
                {
                    DataTable_AsterixFile = Return[0];
                    DGV.DataSource = Return[0];
                }
                if (MLAT_WantToLoad == true && ALL_WantToLoad == false)
                {
                    DataTable_AsterixFile = Return[0];
                    DGV.DataSource = Return[0];
                }
                if (ADSB_WantToLoad == true && ALL_WantToLoad == false)
                {
                    DataTable_AsterixFile = Return[1];
                    DGV.DataSource = Return[1];
                }
                if (ALL_WantToLoad == true)
                {
                    DataTable_AsterixFile = Return[2];
                    DGV.DataSource = Return[2];
                }

                //MessageBox.Show(Return);             
            }
            catch (Exception)
            { 
            MessageBox.Show("Select one file to read");
            }
        }

        private void Selector_Archivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Data_table_panel.Visible = false;
            Load_File_Panel.Visible = true;
            Simulation_Panel.Visible = false;
            WantToLoad_Panel.Visible = false;
            Read_Asterix_Btn.Visible = false;   

            
        }
        private void set_Messages_List(List<CAT10> L)
        {
            L = this.Messages_List_CAT10;
        }

        private void Load_File_Click(object sender, EventArgs e)
        {
            Load_File_Panel.Visible = true;
            Load_File_Panel.Enabled = true;
            Data_table_panel.Visible = false;
            Load_File_Panel.BringToFront();
            Load_File_Panel.Visible = true;
        }

        private void Data_Table_Button_Click(object sender, EventArgs e)
        {
            Data_table_panel.Visible = true;
            Load_File_Panel.Visible = false;
            Data_table_panel.BringToFront();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {   
            
            if (Previous_Index != null)
            {
                DGV.Rows[Previous_Index].Height = 29;
            }
            DGV.CurrentRow.Cells[e.ColumnIndex].Style.WrapMode = DataGridViewTriState.True;
            Previous_Index = e.RowIndex;

            //DGV.CurrentRow.Cells[e.ColumnIndex].Style.Format = DGV.CurrentRow.Cells[e.ColumnIndex].PreferredSize.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DGV.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "DataTable_Export.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = DGV.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[DGV.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += DGV.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i) < DGV.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += DGV.Rows[i-1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Export Dones Succesfully", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export", "Info");
            }
        }

        public void Simulation_Button_Click(object sender, EventArgs e)
        {
            Data_table_panel.Visible = false;
            Load_File_Panel.Visible = false;
            Simulation_Panel.Visible = true;
            checkedListBox1.SelectedIndex = 0;


            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(41.29918003817614, 2.0941180771435848);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 14; //ZOOM inicial
            Stop_Simulation_Button.Visible = false;
            Start_Simulation_time = Messages_List_CAT10[0].ToD;
            Start_Time_Label.Text = Start_Simulation_time;
            Actual_Time = Messages_List_CAT10[0].ToD_seconds;
            GMapOverlay_MLAT = new GMapOverlay("Markers");
            GMapOverlay_SMR = new GMapOverlay("Markers");
            PositionWGS84_SMR.Lat = 0.72074450650363;
            PositionWGS84_SMR.Lon = 0.036566640419238002901;
            PositionWGS84_MLAT.Lat = 0.72076971691201;
            PositionWGS84_MLAT.Lon = 0.03627574735274;
            GMapOverlay_MLAT.Clear();
            GMapOverlay_SMR.Clear();

            int i = 0;
            while (Messages_List_CAT10.Count > i)
            {
    
                if ((Messages_List_CAT10[i].Lat_WGS84 != null && Messages_List_CAT10[i].Lon_WGS84 != null) || (Messages_List_CAT10[i].X_Component != null && Messages_List_CAT10[i].Y_Component != null))
                {
                    if (Messages_List_CAT10[i].SIC == "7") //SMR
                    {
                        points.Add(Create_ActualPosition_Marker(Convert.ToInt32(Messages_List_CAT10[i].X_Component), Convert.ToInt32(Messages_List_CAT10[i].Y_Component), PositionWGS84_SMR, Messages_List_CAT10[i].SIC, Messages_List_CAT10[i].SAC, Messages_List_CAT10[i].Target_Identification, Messages_List_CAT10[i].ToD_seconds, Messages_List_CAT10[i].Track_Number));
                        List_points_SMR.Add(Create_ActualPosition_Marker(Convert.ToInt32(Messages_List_CAT10[i].X_Component), Convert.ToInt32(Messages_List_CAT10[i].Y_Component), PositionWGS84_SMR, Messages_List_CAT10[i].SIC, Messages_List_CAT10[i].SAC, Messages_List_CAT10[i].Target_Identification, Messages_List_CAT10[i].ToD_seconds, Messages_List_CAT10[i].Track_Number));
                    }
                    if (Messages_List_CAT10[i].SIC == "107")//MLAT
                    {
                        points.Add(Create_ActualPosition_Marker(Convert.ToInt32(Messages_List_CAT10[i].X_Component), Convert.ToInt32(Messages_List_CAT10[i].Y_Component), PositionWGS84_MLAT, Messages_List_CAT10[i].SIC, Messages_List_CAT10[i].SAC, Messages_List_CAT10[i].Target_Identification, Messages_List_CAT10[i].ToD_seconds, Messages_List_CAT10[i].Track_Number));
                        List_points_MLAT.Add(Create_ActualPosition_Marker(Convert.ToInt32(Messages_List_CAT10[i].X_Component), Convert.ToInt32(Messages_List_CAT10[i].Y_Component), PositionWGS84_SMR, Messages_List_CAT10[i].SIC, Messages_List_CAT10[i].SAC, Messages_List_CAT10[i].Target_Identification, Messages_List_CAT10[i].ToD_seconds, Messages_List_CAT10[i].Track_Number));
                    }
                    if (Messages_List_CAT10[i].SIC == "219")//ADSB
                    {

                    }
                    Messages_List_CAT10.RemoveAt(i);
                }
                i++;
            }
              

        }

        private void Start_Simulation_Button_Click(object sender, EventArgs e)
        {
            timer1.Start();
            Start_Simulation_Button.Visible = false;
            Stop_Simulation_Button.Visible = true;
            if (checkedListBox1.SelectedItem.ToString() == "x1") { timer1.Interval = 1000; }
            if (checkedListBox1.SelectedItem.ToString() == "x2") { timer1.Interval = 500; }
            if (checkedListBox1.SelectedItem.ToString() == "x10") { timer1.Interval = 10; }
            if (checkedListBox1.SelectedItem.ToString() == "x0.5") { timer1.Interval = 2000; }


        }

        int Timer_Count;
        int i;
        int MarkerCount;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try 
            {
                Timer_Count++;
                label1.Text = Convert.ToString(Timer_Count);   
                Actual_Time = Actual_Time + 1;
                while (points.Count > i)
                {
                    if (points[i].get_SIC() == "107" && MLAT_filter.Checked == true)
                    {
                        if (Math.Abs(points[i].get_time() - Actual_Time) <= 2)
                        {
                            int h = 0;
                            while (GMapOverlay_MLAT.Markers.Count() > h)
                            {

                                if (Convert.ToString(GMapOverlay_MLAT.Markers[h].ToolTipText) == points[i].get_Track_Number()) 
                                {
                                    GMapOverlay_MLAT.Markers.RemoveAt(h);
                                }
                                h++;
                            }
                            Marker_MLAT = new GMarkerGoogle(points[i].get_point(), GMarkerGoogleType.red);
                            GMapOverlay_MLAT.Markers.Add(Marker_MLAT);

                            Marker_MLAT.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            gMapControl1.Overlays.Add(GMapOverlay_MLAT);
                            Marker_MLAT.ToolTipText = points[i].get_Track_Number();
                            MarkerCount++;
                            //Marker_MLAT.ToolTipText = String.Format(points[i].get_TargetID());
                          
                        }
                    }                                                       
                    
                    if (points[i].get_SIC() == "7" && SMR_filter.Checked == true)
                    {
                        if (Math.Abs(points[i].get_time() - Actual_Time) < 2)
                        {
                            Marker_SMR = new GMarkerGoogle(points[i].get_point(), GMarkerGoogleType.red);
                            GMapOverlay_MLAT.Markers.Add(Marker_SMR);

                            Marker_SMR.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            gMapControl1.Overlays.Add(GMapOverlay_SMR);
                            MarkerCount++;
                            //Marker_MLAT.ToolTipText = String.Format(points[i].get_TargetID());
                        }
                    }
                    if (SMR_filter.Checked == false) {GMapOverlay_SMR.Clear(); }


                    if (Math.Abs(points[i].get_time() - Actual_Time) > 2)
                    {
                        break;
                    }
                    i++;
                }              
                Actual_Time_Label.Text = Library.ToD_Calc(Actual_Time);
            }
            catch
            {

            }
        }
        private void Stop_Simulation_Button_Click(object sender, EventArgs e)
        {
                       
            timer1.Stop();
            Start_Simulation_Button.Visible = true;
            Stop_Simulation_Button.Visible = false;
            
        }
        static PointLatLng Convert_To_LatLngPoint(int x, int y, CoordinatesWGS84 pos)
        {
            CoordinatesXYZ xy = new CoordinatesXYZ(x, y, 0);
            GeoUtils f = new GeoUtils();
            CoordinatesXYZ Geocentric = f.change_radar_cartesian2geocentric(pos, xy);
            CoordinatesWGS84 LatLng = f.change_geocentric2geodesic(Geocentric);
            PointLatLng position = new PointLatLng(LatLng.Lat * 180 / Math.PI, LatLng.Lon * 180 / Math.PI);
            return position;
        }
        private MapPoints Create_ActualPosition_Marker(int x,int y, CoordinatesWGS84 pos,string SIC, string SAC, string Target_ID, double Time,string TrackNumber)
        {
            //Marker
            PointLatLng Google_Map_Point = Convert_To_LatLngPoint(x, y, pos);
            double Lat = Google_Map_Point.Lat;
            double Lon = Google_Map_Point.Lng;
            MapPoints MapPoint = new MapPoints(Lat,Lon,Time,SIC,SAC,Target_ID,Google_Map_Point,TrackNumber);
            
            //Marker = new GMarkerGoogle(Map_Point, GMarkerGoogleType.red);
            //GMapOverlay.Markers.Add(Marker);

            //Tooltip text markers
            //Marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            //Marker.ToolTipText = String.Format(Convert.ToString(Map_Point.Lat), Convert.ToString(Map_Point.Lng));

            return MapPoint;
            //MapPoints NewPoint = new MapPoints(Map_Point.Lat, Map_Point.Lng, Time, SIC,SAC,Target_ID);
            //NewPoint.ADD_to_Points_List();


            //Add map and marker
            //gMapControl1.Overlays.Add(GMapOverlay);
        }
        private void Reset_Simulation_Button_Click(object sender, EventArgs e)
        {
            GMapOverlay_MLAT.Clear();
            timer1.Stop();
            Stop_Simulation_Button.Visible = false;
            Start_Simulation_Button.Visible = true;
            i = 0;
            Actual_Time = Messages_List_CAT10[0].ToD_seconds;
            Actual_Time_Label.Text = Library.ToD_Calc(Actual_Time);
        }
        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int iSelectedIndex = checkedListBox1.SelectedIndex;
            if (iSelectedIndex == -1)
                return;
            for (int iIndex = 0; iIndex < checkedListBox1.Items.Count; iIndex++)
                checkedListBox1.SetItemCheckState(iIndex, CheckState.Unchecked);
            checkedListBox1.SetItemCheckState(iSelectedIndex, CheckState.Checked);
            timer1.Stop();
        }


        private void Search_textBox_TextChanged(object sender, EventArgs e)
        {
            DataView dv = DataTable_AsterixFile.DefaultView;
            dv.RowFilter = "TargetID LIKE '" + Search_textBox.Text + "%'";
            DGV.DataSource = dv;
        }

        private void Done_Button_Click(object sender, EventArgs e)
        {
            WantToLoad_Panel.Visible = false;
            Read_Asterix_Btn.Visible = true;
        }
    }
    
}
