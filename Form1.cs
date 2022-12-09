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
using System.Reflection;
using SharpKml;


namespace Asterix_Decoder
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public Label Lab1;
        GMarkerGoogle Marker_MLAT;
        GMapOverlay GMapOverlay_MLAT;
        GMarkerGoogle Marker_SMR;
        GMapOverlay GMapOverlay_SMR;
        GMarkerGoogle Marker_ADSB;
        GMapOverlay GMapOverlay_ADSB;

        DataTable DataTable_AsterixFile;

        public int FileCount = 0;
        public List<AsterixFile> AsterixFiles = new List<AsterixFile>();
        public List<CAT10> Messages_List_CAT10;
        public List<CAT21> Messages_List_CAT21;
        public string Start_Simulation_time;
        public double Start_Time;
        public DecodeLibrary Library = new DecodeLibrary();
        public double Actual_Time;
        CoordinatesWGS84 PositionWGS84_SMR = new CoordinatesWGS84();
        CoordinatesWGS84 PositionWGS84_MLAT = new CoordinatesWGS84();
        List<MapPoints> points = new List<MapPoints>();
        List<MapPoints> List_points_SMR = new List<MapPoints>();
        List<MapPoints> List_points_MLAT = new List<MapPoints>();
        List<MapPoints> List_points_ADSB = new List<MapPoints>();
        List<PointLatLng> List_MapPoints;
        List<PointLatLng> List_MapPoints_SMR;
        List<PointLatLng> List_MapPoints_MLAT;
        List<PointLatLng> List_MapPoints_ADSB;
        bool SMR_WantToLoad = false;
        bool MLAT_WantToLoad = false;
        bool ADSB_WantToLoad = false;
        bool ALL_WantToLoad = false;
        string Loaded_Asterix_file_type;


        AsterixFile Asterix_used;

        int Previous_Index;

        public Form1()
        {
            InitializeComponent();
            instance = this;
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialogAsterix = new System.Windows.Forms.OpenFileDialog();
            this.Selector_Archivo = new System.Windows.Forms.ComboBox();
            this.Data_table_panel = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Exporting_to_ccsv_panel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Categories_Show_Variable = new System.Windows.Forms.Label();
            this.Showed_categories_lbl = new System.Windows.Forms.Label();
            this.Search_textBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.WantToLoad_Panel = new System.Windows.Forms.Panel();
            this.WantToLoad_CheckList = new System.Windows.Forms.CheckedListBox();
            this.Done_Want_To_Load_Button = new System.Windows.Forms.Button();
            this.Read_Asterix_File_Button = new System.Windows.Forms.PictureBox();
            this.Load_File_Panel = new System.Windows.Forms.Panel();
            this.Loading_Asterix_File_Panel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Loading_gif_Asterix_File = new System.Windows.Forms.PictureBox();
            this.Load_Asterix_File_Button = new System.Windows.Forms.PictureBox();
            this.Simulation_Panel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.AirportMap = new System.Windows.Forms.Button();
            this.BarcelonaMap = new System.Windows.Forms.Button();
            this.CataloniaMap = new System.Windows.Forms.Button();
            this.ActualTime_Label = new System.Windows.Forms.Label();
            this.Label_Velocities = new System.Windows.Forms.Label();
            this.ADSB_filter = new System.Windows.Forms.CheckBox();
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
            this.Data_Table_Button = new System.Windows.Forms.Button();
            this.Load_File_Button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Simulation_Button = new System.Windows.Forms.Button();
            this.Home_Button = new System.Windows.Forms.Button();
            this.Panel_Home = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Data_table_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.Exporting_to_ccsv_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.WantToLoad_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Read_Asterix_File_Button)).BeginInit();
            this.Load_File_Panel.SuspendLayout();
            this.Loading_Asterix_File_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Loading_gif_Asterix_File)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Load_Asterix_File_Button)).BeginInit();
            this.Simulation_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.Panel_Home.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialogAsterix
            // 
            this.openFileDialogAsterix.FileName = "openFileDialogAsterix";
            // 
            // Selector_Archivo
            // 
            this.Selector_Archivo.BackColor = System.Drawing.Color.RoyalBlue;
            this.Selector_Archivo.Font = new System.Drawing.Font("Calibri", 20.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Selector_Archivo.FormattingEnabled = true;
            this.Selector_Archivo.Location = new System.Drawing.Point(203, 321);
            this.Selector_Archivo.Name = "Selector_Archivo";
            this.Selector_Archivo.Size = new System.Drawing.Size(531, 49);
            this.Selector_Archivo.TabIndex = 3;
            this.Selector_Archivo.Text = "Select one file to read";
            this.Selector_Archivo.SelectedIndexChanged += new System.EventHandler(this.Selector_Archivo_SelectedIndexChanged);
            // 
            // Data_table_panel
            // 
            this.Data_table_panel.BackColor = System.Drawing.SystemColors.InfoText;
            this.Data_table_panel.Controls.Add(this.pictureBox2);
            this.Data_table_panel.Controls.Add(this.Exporting_to_ccsv_panel);
            this.Data_table_panel.Controls.Add(this.Categories_Show_Variable);
            this.Data_table_panel.Controls.Add(this.Showed_categories_lbl);
            this.Data_table_panel.Controls.Add(this.Search_textBox);
            this.Data_table_panel.Controls.Add(this.button1);
            this.Data_table_panel.Controls.Add(this.DGV);
            this.Data_table_panel.Location = new System.Drawing.Point(261, 12);
            this.Data_table_panel.Name = "Data_table_panel";
            this.Data_table_panel.Size = new System.Drawing.Size(990, 652);
            this.Data_table_panel.TabIndex = 5;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Asterix_Decoder.Properties.Resources.magnifying_glass1;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Image = global::Asterix_Decoder.Properties.Resources.magnifying_glass;
            this.pictureBox2.Location = new System.Drawing.Point(915, 15);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(62, 60);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // Exporting_to_ccsv_panel
            // 
            this.Exporting_to_ccsv_panel.Controls.Add(this.label3);
            this.Exporting_to_ccsv_panel.Controls.Add(this.pictureBox1);
            this.Exporting_to_ccsv_panel.Location = new System.Drawing.Point(368, 214);
            this.Exporting_to_ccsv_panel.Name = "Exporting_to_ccsv_panel";
            this.Exporting_to_ccsv_panel.Size = new System.Drawing.Size(285, 286);
            this.Exporting_to_ccsv_panel.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(23, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 41);
            this.label3.TabIndex = 1;
            this.label3.Text = "Exporting to CSV";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::Asterix_Decoder.Properties.Resources.travel;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(44, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Categories_Show_Variable
            // 
            this.Categories_Show_Variable.AutoSize = true;
            this.Categories_Show_Variable.BackColor = System.Drawing.Color.Transparent;
            this.Categories_Show_Variable.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Categories_Show_Variable.ForeColor = System.Drawing.Color.ForestGreen;
            this.Categories_Show_Variable.Location = new System.Drawing.Point(261, 28);
            this.Categories_Show_Variable.Name = "Categories_Show_Variable";
            this.Categories_Show_Variable.Size = new System.Drawing.Size(0, 18);
            this.Categories_Show_Variable.TabIndex = 9;
            // 
            // Showed_categories_lbl
            // 
            this.Showed_categories_lbl.AutoSize = true;
            this.Showed_categories_lbl.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Showed_categories_lbl.ForeColor = System.Drawing.Color.Ivory;
            this.Showed_categories_lbl.Location = new System.Drawing.Point(252, -3);
            this.Showed_categories_lbl.Name = "Showed_categories_lbl";
            this.Showed_categories_lbl.Size = new System.Drawing.Size(205, 28);
            this.Showed_categories_lbl.TabIndex = 8;
            this.Showed_categories_lbl.Text = "Showed Categories: ";
            // 
            // Search_textBox
            // 
            this.Search_textBox.BackColor = System.Drawing.Color.ForestGreen;
            this.Search_textBox.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.Search_textBox.Location = new System.Drawing.Point(508, 21);
            this.Search_textBox.Name = "Search_textBox";
            this.Search_textBox.Size = new System.Drawing.Size(401, 52);
            this.Search_textBox.TabIndex = 6;
            this.Search_textBox.Text = "Ex: RYR85RT";
            this.Search_textBox.TextChanged += new System.EventHandler(this.Search_textBox_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(17, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 60);
            this.button1.TabIndex = 5;
            this.button1.Text = "Export CSV";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.DGV.GridColor = System.Drawing.Color.ForestGreen;
            this.DGV.Location = new System.Drawing.Point(17, 92);
            this.DGV.Name = "DGV";
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.DGV.RowTemplate.Height = 29;
            this.DGV.RowTemplate.ReadOnly = true;
            this.DGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGV.Size = new System.Drawing.Size(960, 560);
            this.DGV.TabIndex = 4;
            this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellClick);
            // 
            // WantToLoad_Panel
            // 
            this.WantToLoad_Panel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.WantToLoad_Panel.Controls.Add(this.WantToLoad_CheckList);
            this.WantToLoad_Panel.Controls.Add(this.Done_Want_To_Load_Button);
            this.WantToLoad_Panel.Location = new System.Drawing.Point(491, 15);
            this.WantToLoad_Panel.Name = "WantToLoad_Panel";
            this.WantToLoad_Panel.Size = new System.Drawing.Size(276, 302);
            this.WantToLoad_Panel.TabIndex = 7;
            // 
            // WantToLoad_CheckList
            // 
            this.WantToLoad_CheckList.BackColor = System.Drawing.Color.RoyalBlue;
            this.WantToLoad_CheckList.Font = new System.Drawing.Font("Dubai", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.WantToLoad_CheckList.ForeColor = System.Drawing.Color.Black;
            this.WantToLoad_CheckList.FormattingEnabled = true;
            this.WantToLoad_CheckList.Items.AddRange(new object[] {
            "CAT10_SMR",
            "CAT10_MLAT",
            "CAT21_ADSB",
            "ALL CAT"});
            this.WantToLoad_CheckList.Location = new System.Drawing.Point(3, 2);
            this.WantToLoad_CheckList.Name = "WantToLoad_CheckList";
            this.WantToLoad_CheckList.Size = new System.Drawing.Size(269, 240);
            this.WantToLoad_CheckList.TabIndex = 0;
            this.WantToLoad_CheckList.ThreeDCheckBoxes = true;
            this.WantToLoad_CheckList.SelectedIndexChanged += new System.EventHandler(this.WantToLoad_CheckList_SelectedIndexChanged);
            // 
            // Done_Want_To_Load_Button
            // 
            this.Done_Want_To_Load_Button.Location = new System.Drawing.Point(70, 244);
            this.Done_Want_To_Load_Button.Name = "Done_Want_To_Load_Button";
            this.Done_Want_To_Load_Button.Size = new System.Drawing.Size(135, 51);
            this.Done_Want_To_Load_Button.TabIndex = 1;
            this.Done_Want_To_Load_Button.Text = "DONE";
            this.Done_Want_To_Load_Button.UseVisualStyleBackColor = true;
            this.Done_Want_To_Load_Button.Click += new System.EventHandler(this.Done_Button_Click);
            // 
            // Read_Asterix_File_Button
            // 
            this.Read_Asterix_File_Button.BackgroundImage = global::Asterix_Decoder.Properties.Resources.download;
            this.Read_Asterix_File_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Read_Asterix_File_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Read_Asterix_File_Button.Location = new System.Drawing.Point(505, 75);
            this.Read_Asterix_File_Button.Name = "Read_Asterix_File_Button";
            this.Read_Asterix_File_Button.Size = new System.Drawing.Size(262, 220);
            this.Read_Asterix_File_Button.TabIndex = 13;
            this.Read_Asterix_File_Button.TabStop = false;
            this.Read_Asterix_File_Button.Click += new System.EventHandler(this.Read_Asterix_File_Button_Click);
            // 
            // Load_File_Panel
            // 
            this.Load_File_Panel.BackColor = System.Drawing.Color.DimGray;
            this.Load_File_Panel.Controls.Add(this.Loading_Asterix_File_Panel);
            this.Load_File_Panel.Controls.Add(this.WantToLoad_Panel);
            this.Load_File_Panel.Controls.Add(this.Read_Asterix_File_Button);
            this.Load_File_Panel.Controls.Add(this.Load_Asterix_File_Button);
            this.Load_File_Panel.Controls.Add(this.Selector_Archivo);
            this.Load_File_Panel.Location = new System.Drawing.Point(264, 12);
            this.Load_File_Panel.Name = "Load_File_Panel";
            this.Load_File_Panel.Size = new System.Drawing.Size(990, 650);
            this.Load_File_Panel.TabIndex = 8;
            this.Load_File_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.Load_File_Panel_Paint);
            // 
            // Loading_Asterix_File_Panel
            // 
            this.Loading_Asterix_File_Panel.Controls.Add(this.label2);
            this.Loading_Asterix_File_Panel.Controls.Add(this.Loading_gif_Asterix_File);
            this.Loading_Asterix_File_Panel.Location = new System.Drawing.Point(362, 399);
            this.Loading_Asterix_File_Panel.Name = "Loading_Asterix_File_Panel";
            this.Loading_Asterix_File_Panel.Size = new System.Drawing.Size(285, 236);
            this.Loading_Asterix_File_Panel.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(-2, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 41);
            this.label2.TabIndex = 1;
            this.label2.Text = "Loading Asterix File";
            // 
            // Loading_gif_Asterix_File
            // 
            this.Loading_gif_Asterix_File.BackgroundImage = global::Asterix_Decoder.Properties.Resources.travel;
            this.Loading_gif_Asterix_File.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Loading_gif_Asterix_File.InitialImage = global::Asterix_Decoder.Properties.Resources.travel;
            this.Loading_gif_Asterix_File.Location = new System.Drawing.Point(44, 0);
            this.Loading_gif_Asterix_File.Name = "Loading_gif_Asterix_File";
            this.Loading_gif_Asterix_File.Size = new System.Drawing.Size(200, 200);
            this.Loading_gif_Asterix_File.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Loading_gif_Asterix_File.TabIndex = 0;
            this.Loading_gif_Asterix_File.TabStop = false;
            // 
            // Load_Asterix_File_Button
            // 
            this.Load_Asterix_File_Button.BackgroundImage = global::Asterix_Decoder.Properties.Resources.add;
            this.Load_Asterix_File_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Load_Asterix_File_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Load_Asterix_File_Button.InitialImage = global::Asterix_Decoder.Properties.Resources.icons8_upload_64;
            this.Load_Asterix_File_Button.Location = new System.Drawing.Point(176, 78);
            this.Load_Asterix_File_Button.Name = "Load_Asterix_File_Button";
            this.Load_Asterix_File_Button.Size = new System.Drawing.Size(266, 221);
            this.Load_Asterix_File_Button.TabIndex = 12;
            this.Load_Asterix_File_Button.TabStop = false;
            this.Load_Asterix_File_Button.Click += new System.EventHandler(this.Load_Asterix_File_Button_Click);
            // 
            // Simulation_Panel
            // 
            this.Simulation_Panel.BackColor = System.Drawing.SystemColors.InfoText;
            this.Simulation_Panel.Controls.Add(this.label6);
            this.Simulation_Panel.Controls.Add(this.label5);
            this.Simulation_Panel.Controls.Add(this.pictureBox4);
            this.Simulation_Panel.Controls.Add(this.label4);
            this.Simulation_Panel.Controls.Add(this.pictureBox3);
            this.Simulation_Panel.Controls.Add(this.AirportMap);
            this.Simulation_Panel.Controls.Add(this.BarcelonaMap);
            this.Simulation_Panel.Controls.Add(this.CataloniaMap);
            this.Simulation_Panel.Controls.Add(this.ActualTime_Label);
            this.Simulation_Panel.Controls.Add(this.Label_Velocities);
            this.Simulation_Panel.Controls.Add(this.ADSB_filter);
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
            this.Simulation_Panel.Location = new System.Drawing.Point(258, 12);
            this.Simulation_Panel.Name = "Simulation_Panel";
            this.Simulation_Panel.Size = new System.Drawing.Size(1007, 666);
            this.Simulation_Panel.TabIndex = 9;
            this.Simulation_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.Simulation_Panel_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 20.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.ForestGreen;
            this.label6.Location = new System.Drawing.Point(426, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 42);
            this.label6.TabIndex = 25;
            this.label6.Text = "Current Time";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 20.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.ForestGreen;
            this.label5.Location = new System.Drawing.Point(454, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 42);
            this.label5.TabIndex = 24;
            this.label5.Text = "Initial Time";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::Asterix_Decoder.Properties.Resources.satellite;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox4.Location = new System.Drawing.Point(948, 114);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(56, 71);
            this.pictureBox4.TabIndex = 23;
            this.pictureBox4.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 18.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.ForestGreen;
            this.label4.Location = new System.Drawing.Point(763, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 39);
            this.label4.TabIndex = 22;
            this.label4.Text = "Traffic Filters:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::Asterix_Decoder.Properties.Resources.dashboard1;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Location = new System.Drawing.Point(936, 367);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(60, 57);
            this.pictureBox3.TabIndex = 21;
            this.pictureBox3.TabStop = false;
            // 
            // AirportMap
            // 
            this.AirportMap.Image = global::Asterix_Decoder.Properties.Resources.location;
            this.AirportMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AirportMap.Location = new System.Drawing.Point(316, 615);
            this.AirportMap.Name = "AirportMap";
            this.AirportMap.Size = new System.Drawing.Size(145, 43);
            this.AirportMap.TabIndex = 20;
            this.AirportMap.Text = "LEBL";
            this.AirportMap.UseVisualStyleBackColor = true;
            this.AirportMap.Click += new System.EventHandler(this.AirportMap_Click);
            // 
            // BarcelonaMap
            // 
            this.BarcelonaMap.Image = global::Asterix_Decoder.Properties.Resources.location;
            this.BarcelonaMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BarcelonaMap.Location = new System.Drawing.Point(467, 615);
            this.BarcelonaMap.Name = "BarcelonaMap";
            this.BarcelonaMap.Size = new System.Drawing.Size(145, 43);
            this.BarcelonaMap.TabIndex = 19;
            this.BarcelonaMap.Text = "Barcelona";
            this.BarcelonaMap.UseVisualStyleBackColor = true;
            this.BarcelonaMap.Click += new System.EventHandler(this.BarcelonaMap_Click);
            // 
            // CataloniaMap
            // 
            this.CataloniaMap.Image = global::Asterix_Decoder.Properties.Resources.location;
            this.CataloniaMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CataloniaMap.Location = new System.Drawing.Point(618, 615);
            this.CataloniaMap.Name = "CataloniaMap";
            this.CataloniaMap.Size = new System.Drawing.Size(145, 43);
            this.CataloniaMap.TabIndex = 18;
            this.CataloniaMap.Text = "Catalonia";
            this.CataloniaMap.UseVisualStyleBackColor = true;
            this.CataloniaMap.Click += new System.EventHandler(this.CataloniaMap_Click);
            // 
            // ActualTime_Label
            // 
            this.ActualTime_Label.AutoSize = true;
            this.ActualTime_Label.Font = new System.Drawing.Font("Calibri", 19.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.ActualTime_Label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ActualTime_Label.Location = new System.Drawing.Point(644, 88);
            this.ActualTime_Label.Name = "ActualTime_Label";
            this.ActualTime_Label.Size = new System.Drawing.Size(0, 40);
            this.ActualTime_Label.TabIndex = 16;
            // 
            // Label_Velocities
            // 
            this.Label_Velocities.AutoSize = true;
            this.Label_Velocities.BackColor = System.Drawing.Color.Transparent;
            this.Label_Velocities.Font = new System.Drawing.Font("Calibri", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.Label_Velocities.ForeColor = System.Drawing.Color.ForestGreen;
            this.Label_Velocities.Location = new System.Drawing.Point(779, 350);
            this.Label_Velocities.Name = "Label_Velocities";
            this.Label_Velocities.Size = new System.Drawing.Size(157, 74);
            this.Label_Velocities.TabIndex = 15;
            this.Label_Velocities.Text = "Simulation \r\nVelocities:";
            // 
            // ADSB_filter
            // 
            this.ADSB_filter.AutoSize = true;
            this.ADSB_filter.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ADSB_filter.ForeColor = System.Drawing.Color.Lime;
            this.ADSB_filter.Location = new System.Drawing.Point(806, 294);
            this.ADSB_filter.Name = "ADSB_filter";
            this.ADSB_filter.Size = new System.Drawing.Size(134, 53);
            this.ADSB_filter.TabIndex = 14;
            this.ADSB_filter.Text = "ADSB";
            this.ADSB_filter.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.Black;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.Font = new System.Drawing.Font("Calibri", 24.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkedListBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "x1",
            "x2",
            "x10",
            "x0.5"});
            this.checkedListBox1.Location = new System.Drawing.Point(790, 435);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListBox1.Size = new System.Drawing.Size(122, 212);
            this.checkedListBox1.TabIndex = 13;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged_1);
            // 
            // SMR_filter
            // 
            this.SMR_filter.AutoSize = true;
            this.SMR_filter.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SMR_filter.ForeColor = System.Drawing.Color.Blue;
            this.SMR_filter.Location = new System.Drawing.Point(806, 176);
            this.SMR_filter.Name = "SMR_filter";
            this.SMR_filter.Size = new System.Drawing.Size(121, 53);
            this.SMR_filter.TabIndex = 11;
            this.SMR_filter.Text = "SMR";
            this.SMR_filter.UseVisualStyleBackColor = true;
            // 
            // MLAT_filter
            // 
            this.MLAT_filter.AutoSize = true;
            this.MLAT_filter.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MLAT_filter.ForeColor = System.Drawing.Color.Red;
            this.MLAT_filter.Location = new System.Drawing.Point(806, 235);
            this.MLAT_filter.Name = "MLAT_filter";
            this.MLAT_filter.Size = new System.Drawing.Size(137, 53);
            this.MLAT_filter.TabIndex = 10;
            this.MLAT_filter.Text = "MLAT";
            this.MLAT_filter.UseVisualStyleBackColor = true;
            // 
            // Actual_Time_Label
            // 
            this.Actual_Time_Label.AutoSize = true;
            this.Actual_Time_Label.BackColor = System.Drawing.SystemColors.InfoText;
            this.Actual_Time_Label.Font = new System.Drawing.Font("Calibri", 20.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.Actual_Time_Label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Actual_Time_Label.Location = new System.Drawing.Point(644, 33);
            this.Actual_Time_Label.Name = "Actual_Time_Label";
            this.Actual_Time_Label.Size = new System.Drawing.Size(0, 41);
            this.Actual_Time_Label.TabIndex = 6;
            // 
            // Start_Time_Label
            // 
            this.Start_Time_Label.AutoSize = true;
            this.Start_Time_Label.Font = new System.Drawing.Font("Calibri", 20.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.Start_Time_Label.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Start_Time_Label.Location = new System.Drawing.Point(644, 33);
            this.Start_Time_Label.Name = "Start_Time_Label";
            this.Start_Time_Label.Size = new System.Drawing.Size(0, 41);
            this.Start_Time_Label.TabIndex = 5;
            // 
            // Reset_Simulation_Button
            // 
            this.Reset_Simulation_Button.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Reset_Simulation_Button.Image = global::Asterix_Decoder.Properties.Resources.reset__1_;
            this.Reset_Simulation_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Reset_Simulation_Button.Location = new System.Drawing.Point(209, 39);
            this.Reset_Simulation_Button.Name = "Reset_Simulation_Button";
            this.Reset_Simulation_Button.Size = new System.Drawing.Size(171, 56);
            this.Reset_Simulation_Button.TabIndex = 4;
            this.Reset_Simulation_Button.Text = "Reset";
            this.Reset_Simulation_Button.UseVisualStyleBackColor = true;
            this.Reset_Simulation_Button.Click += new System.EventHandler(this.Reset_Simulation_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(644, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 41);
            this.label1.TabIndex = 2;
            // 
            // Start_Simulation_Button
            // 
            this.Start_Simulation_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Start_Simulation_Button.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Start_Simulation_Button.Image = global::Asterix_Decoder.Properties.Resources.play_button;
            this.Start_Simulation_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Start_Simulation_Button.Location = new System.Drawing.Point(33, 39);
            this.Start_Simulation_Button.Name = "Start_Simulation_Button";
            this.Start_Simulation_Button.Size = new System.Drawing.Size(170, 57);
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
            this.gMapControl1.Location = new System.Drawing.Point(13, 132);
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
            this.gMapControl1.Size = new System.Drawing.Size(750, 467);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            // 
            // Stop_Simulation_Button
            // 
            this.Stop_Simulation_Button.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Stop_Simulation_Button.Image = ((System.Drawing.Image)(resources.GetObject("Stop_Simulation_Button.Image")));
            this.Stop_Simulation_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Stop_Simulation_Button.Location = new System.Drawing.Point(33, 39);
            this.Stop_Simulation_Button.Name = "Stop_Simulation_Button";
            this.Stop_Simulation_Button.Size = new System.Drawing.Size(170, 57);
            this.Stop_Simulation_Button.TabIndex = 3;
            this.Stop_Simulation_Button.Text = "Stop";
            this.Stop_Simulation_Button.UseVisualStyleBackColor = true;
            this.Stop_Simulation_Button.Click += new System.EventHandler(this.Stop_Simulation_Button_Click);
            // 
            // Data_Table_Button
            // 
            this.Data_Table_Button.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Data_Table_Button.Image = global::Asterix_Decoder.Properties.Resources.cells__1_;
            this.Data_Table_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Data_Table_Button.Location = new System.Drawing.Point(21, 464);
            this.Data_Table_Button.Name = "Data_Table_Button";
            this.Data_Table_Button.Size = new System.Drawing.Size(213, 85);
            this.Data_Table_Button.TabIndex = 7;
            this.Data_Table_Button.Text = "Show \r\nData Table";
            this.Data_Table_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Data_Table_Button.UseVisualStyleBackColor = true;
            this.Data_Table_Button.Click += new System.EventHandler(this.Data_Table_Button_Click);
            // 
            // Load_File_Button
            // 
            this.Load_File_Button.Font = new System.Drawing.Font("Calibri", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Load_File_Button.Image = global::Asterix_Decoder.Properties.Resources.find;
            this.Load_File_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Load_File_Button.Location = new System.Drawing.Point(21, 351);
            this.Load_File_Button.Name = "Load_File_Button";
            this.Load_File_Button.Size = new System.Drawing.Size(213, 85);
            this.Load_File_Button.TabIndex = 8;
            this.Load_File_Button.Text = "Load File\r\n";
            this.Load_File_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Load_File_Button.UseVisualStyleBackColor = true;
            this.Load_File_Button.Click += new System.EventHandler(this.Load_File_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Simulation_Button
            // 
            this.Simulation_Button.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Simulation_Button.Image = ((System.Drawing.Image)(resources.GetObject("Simulation_Button.Image")));
            this.Simulation_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Simulation_Button.Location = new System.Drawing.Point(21, 573);
            this.Simulation_Button.Name = "Simulation_Button";
            this.Simulation_Button.Size = new System.Drawing.Size(213, 76);
            this.Simulation_Button.TabIndex = 9;
            this.Simulation_Button.Text = "Simulation";
            this.Simulation_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Simulation_Button.UseVisualStyleBackColor = true;
            this.Simulation_Button.Click += new System.EventHandler(this.Simulation_Button_Click);
            // 
            // Home_Button
            // 
            this.Home_Button.Font = new System.Drawing.Font("Calibri", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Home_Button.Image = global::Asterix_Decoder.Properties.Resources.home;
            this.Home_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Home_Button.Location = new System.Drawing.Point(21, 245);
            this.Home_Button.Name = "Home_Button";
            this.Home_Button.Size = new System.Drawing.Size(213, 77);
            this.Home_Button.TabIndex = 10;
            this.Home_Button.Text = "Home";
            this.Home_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Home_Button.UseVisualStyleBackColor = true;
            this.Home_Button.Click += new System.EventHandler(this.Home_Button_Click);
            // 
            // Panel_Home
            // 
            this.Panel_Home.Controls.Add(this.label8);
            this.Panel_Home.Location = new System.Drawing.Point(257, 9);
            this.Panel_Home.Name = "Panel_Home";
            this.Panel_Home.Size = new System.Drawing.Size(997, 661);
            this.Panel_Home.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Showcard Gothic", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.ForestGreen;
            this.label8.Location = new System.Drawing.Point(134, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(745, 59);
            this.label8.TabIndex = 0;
            this.label8.Text = "Wellcome to Obelix Decoder";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::Asterix_Decoder.Properties.Resources.binary_code;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox5.Location = new System.Drawing.Point(21, 87);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(213, 124);
            this.pictureBox5.TabIndex = 12;
            this.pictureBox5.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.ForestGreen;
            this.label7.Location = new System.Drawing.Point(2, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(253, 35);
            this.label7.TabIndex = 13;
            this.label7.Text = "Obelix Decoder";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1265, 682);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.Home_Button);
            this.Controls.Add(this.Simulation_Button);
            this.Controls.Add(this.Load_File_Button);
            this.Controls.Add(this.Data_Table_Button);
            this.Controls.Add(this.Panel_Home);
            this.Controls.Add(this.Data_table_panel);
            this.Controls.Add(this.Simulation_Panel);
            this.Controls.Add(this.Load_File_Panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Tag = "";
            this.Text = "Obelix Ddecoder";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.Data_table_panel.ResumeLayout(false);
            this.Data_table_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.Exporting_to_ccsv_panel.ResumeLayout(false);
            this.Exporting_to_ccsv_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.WantToLoad_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Read_Asterix_File_Button)).EndInit();
            this.Load_File_Panel.ResumeLayout(false);
            this.Loading_Asterix_File_Panel.ResumeLayout(false);
            this.Loading_Asterix_File_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Loading_gif_Asterix_File)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Load_Asterix_File_Button)).EndInit();
            this.Simulation_Panel.ResumeLayout(false);
            this.Simulation_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.Panel_Home.ResumeLayout(false);
            this.Panel_Home.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Selector_Archivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Data_table_panel.Visible = false;
            Load_File_Panel.Visible = false;
            Simulation_Panel.Visible = false;
            WantToLoad_Panel.Visible = false;
            Read_Asterix_File_Button.Visible = false;   
            Loading_Asterix_File_Panel.Visible =false;
            Exporting_to_ccsv_panel.Visible = false;
            Panel_Home.Visible = true;
            
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
                //DGV.Rows[Previous_Index] = DataGridViewTriState.False;
            }
            DGV.CurrentRow.Cells[e.ColumnIndex].Style.WrapMode = DataGridViewTriState.True;
            Previous_Index = e.RowIndex;

            //DGV.CurrentRow.Cells[e.ColumnIndex].Style.Format = DGV.CurrentRow.Cells[e.ColumnIndex].PreferredSize.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Exporting_to_ccsv_panel.Visible = true;
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
                Exporting_to_ccsv_panel.Visible = false;
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
            Panel_Home.Visible = false;
            Simulation_Panel.Visible = true;
            checkedListBox1.SelectedIndex = 0;

                   
            if (Loaded_Asterix_file_type == "CAT10")
            {
                Start_Simulation_time = Messages_List_CAT10[0].ToD;
                Actual_Time = Messages_List_CAT10[0].ToD_seconds;
                Start_Time = Messages_List_CAT10[0].ToD_seconds;
            }
            if (Loaded_Asterix_file_type == "CAT21")
            {
                Start_Simulation_time = Messages_List_CAT21[0].ToART;
                Actual_Time = Messages_List_CAT21[0].ToArt_Seconds;
                Start_Time = Messages_List_CAT21[0].ToArt_Seconds;
            }
            if (Loaded_Asterix_file_type == "CATALL")
            {
                if(Messages_List_CAT10[0].ToD_seconds >= Messages_List_CAT21[0].ToArt_Seconds)
                {
                    Start_Simulation_time = Library.ToD_Calc(Messages_List_CAT21[0].ToArt_Seconds);
                    Actual_Time = Messages_List_CAT21[0].ToArt_Seconds;
                    Start_Time = Messages_List_CAT21[0].ToArt_Seconds;
                }
                else
                {
                    Start_Simulation_time = Library.ToD_Calc(Messages_List_CAT10[0].ToD_seconds);
                    Actual_Time = Messages_List_CAT10[0].ToD_seconds;
                    Start_Time = Messages_List_CAT10[0].ToD_seconds;
                }
            }
            

            //Start_Simulation_time = DataTable_AsterixFile.Rows[]

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(41.29918003817614, 2.0941180771435848);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 14; //ZOOM inicial
            Stop_Simulation_Button.Visible = false;
            Start_Time_Label.Text = Start_Simulation_time;
            GMapOverlay_MLAT = new GMapOverlay("Markers");
            GMapOverlay_SMR = new GMapOverlay("Markers");
            GMapOverlay_ADSB = new GMapOverlay("Markers");
            PositionWGS84_SMR.Lat = 0.72074450650363;
            PositionWGS84_SMR.Lon = 0.036566640419238002901;
            PositionWGS84_MLAT.Lat = 0.72076971691201;
            PositionWGS84_MLAT.Lon = 0.03627574735274;
            GMapOverlay_MLAT.Clear();
            GMapOverlay_SMR.Clear();
            int i = 0;
            if (Messages_List_CAT10 != null)
            {
                i = 0;
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
                    }
                    i++;
                }
            }
            if (Messages_List_CAT21 != null)
            {
                i = 0;
                while (Messages_List_CAT21.Count > i)
                {
                    //ADSB
                    if (Messages_List_CAT21[i].Lat_WGS84 != null && Messages_List_CAT21[i].Lon_WGS84 != null)
                    {
                        double Lat = Convert.ToDouble(Messages_List_CAT21[i].Lat_WGS84);
                        double Lon = Convert.ToDouble(Messages_List_CAT21[i].Lon_WGS84);
                        double Time = Messages_List_CAT21[i].ToArt_Seconds;
                        string SIC = Messages_List_CAT21[i].SIC;
                        string SAC = Messages_List_CAT21[i].SAC;
                        string Target_ID = Messages_List_CAT21[i].Target_Identification;
                        PointLatLng Google_Map_Point = new PointLatLng(Lat, Lon);
                        string TrackNumber = Messages_List_CAT21[i].TN;

                        MapPoints MapPoint = new MapPoints(Lat, Lon, Time, SIC, SAC, Target_ID, Google_Map_Point, TrackNumber);
                        points.Add(MapPoint);
                        List_points_ADSB.Add(MapPoint);
                    }
                    i++;
                }
            }

            points.Sort((x, y) => x.Time.CompareTo(y.Time));
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

        int i;
        int Marker_count=0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBox1.SelectedItem.ToString() == "x1") { timer1.Interval = 1000; }
                if (checkedListBox1.SelectedItem.ToString() == "x2") { timer1.Interval = 500; }
                if (checkedListBox1.SelectedItem.ToString() == "x10") { timer1.Interval = 10; }
                if (checkedListBox1.SelectedItem.ToString() == "x0.5") { timer1.Interval = 2000; }

                //label1.Text = Convert.ToString(Timer_Count);
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
                            Marker_count++;
                            
                            //Marker_MLAT.ToolTipText = String.Format(points[i].get_TargetID());

                        }
                    }
                    if (points[i].get_SIC() == "7" && SMR_filter.Checked == true)
                    {
                        if (Math.Abs(points[i].get_time() - Actual_Time) <= 2)
                        {
                            int h = 0;
                            while (GMapOverlay_SMR.Markers.Count() > h)
                            {

                                if (Convert.ToString(GMapOverlay_SMR.Markers[h].ToolTipText) == points[i].get_Track_Number())
                                {
                                    GMapOverlay_SMR.Markers.RemoveAt(h);
                                }
                                h++;
                            }
                            Marker_SMR = new GMarkerGoogle(points[i].get_point(), GMarkerGoogleType.blue);
                            GMapOverlay_SMR.Markers.Add(Marker_SMR);

                            Marker_SMR.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            gMapControl1.Overlays.Add(GMapOverlay_SMR);
                            Marker_SMR.ToolTipText = points[i].get_Track_Number();
                            Marker_count++;

                            //Marker_MLAT.ToolTipText = String.Format(points[i].get_TargetID());

                        }
                    }
                    if (points[i].get_SIC() == "219" && ADSB_filter.Checked == true)
                    {
                        if (Math.Abs(points[i].get_time() - Actual_Time) <= 2)
                        {
                            int h = 0;
                            while (GMapOverlay_ADSB.Markers.Count() > h)
                            {

                                if (Convert.ToString(GMapOverlay_ADSB.Markers[h].ToolTipText) == points[i].get_Track_Number())
                                {
                                    GMapOverlay_ADSB.Markers.RemoveAt(h);
                                }
                                h++;
                            }
                            Marker_ADSB = new GMarkerGoogle(points[i].get_point(), GMarkerGoogleType.green);
                            GMapOverlay_ADSB.Markers.Add(Marker_ADSB);

                            Marker_ADSB.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            gMapControl1.Overlays.Add(GMapOverlay_ADSB);
                            Marker_ADSB.ToolTipText = points[i].get_Track_Number();
                            Marker_count++;
                            //Marker_MLAT.ToolTipText = String.Format(points[i].get_TargetID());
                        }
                    }

                    if (SMR_filter.Checked == false) { GMapOverlay_SMR.Clear(); }
                    if (MLAT_filter.Checked == false) { GMapOverlay_MLAT.Clear(); }
                    if (ADSB_filter.Checked == false) { GMapOverlay_ADSB.Clear(); }

                    ActualTime_Label.Text = Library.ToD_Calc(Actual_Time);
                    if (Math.Abs(points[i].get_time() - Actual_Time) > 2)
                    {
                        break;
                    }
                    i++;
                }               
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
            GMapOverlay_SMR.Clear();
            GMapOverlay_ADSB.Clear();
            timer1.Stop();
            Stop_Simulation_Button.Visible = false;
            Start_Simulation_Button.Visible = true;
            i = 0;
            Actual_Time = Start_Time;
            ActualTime_Label.Text = Start_Simulation_time;
            label1.Text = Library.ToD_Calc(Actual_Time);
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
            Stop_Simulation_Button.Visible = true;
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
            string String_To_Show = "";
            if (WantToLoad_CheckList.SelectedItem.ToString() == "CAT10_SMR") 
            { 
                SMR_WantToLoad = true;
                String_To_Show = String_To_Show + "CAT10 SMR\n";
            }
            if (WantToLoad_CheckList.SelectedItem.ToString() == "CAT10_MLAT") 
            {
                MLAT_WantToLoad = true;
                String_To_Show = String_To_Show + "CAT10 MLAT\n";
            }
            if (WantToLoad_CheckList.SelectedItem.ToString() == "CAT21_ADSB")
            {
                ADSB_WantToLoad = true;
                String_To_Show = String_To_Show + "CAT21 ADSB\n";
            }
            if (WantToLoad_CheckList.SelectedItem.ToString() == "ALL CAT")
            { 
                ALL_WantToLoad = true;
                String_To_Show = String_To_Show + "CAT10 SMR\nCAT10 MLAT\nCAT 21 ADSB";
            }
            Categories_Show_Variable.Text = String_To_Show;
            Read_Asterix_File_Button.Visible = true;
        }

        private void Load_Asterix_File_Button_Click(object sender, EventArgs e)
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
                    }
                    else
                    {
                        MessageBox.Show("File already Added");
                    }
                }

                WantToLoad_Panel.Visible = true;
            }

            catch
            {
                MessageBox.Show("The file selected could not be loaded");
            }
        }

        private void Read_Asterix_File_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Read_Asterix_File_Button.UseWaitCursor = true;
                //String Root = Directory.GetCurrentDirectory();
                //string path = Path.Combine(Root, @"Images\", "ben-redblock-loading.gif");
                //Loading_gif_Asterix_File.Image = Image.FromFile(path);
                //Loading_gif_Asterix_File.Size = PictureBoxSizeMode.AutoSize;
                Loading_Asterix_File_Panel.Visible = true;

                int index = Selector_Archivo.SelectedIndex;
                Asterix_used = AsterixFiles[index];
                List<DataTable> Return = AsterixFiles[index].ReadFile(AsterixFiles[index].path, AsterixFiles[index].name);
                if (SMR_WantToLoad == true && ALL_WantToLoad == false)
                {
                    DataTable_AsterixFile = Return[0];
                    DGV.DataSource = Return[0];
                    Messages_List_CAT10 = Asterix_used.get_CAT10_List();
                    Loaded_Asterix_file_type = "CAT10";
                    SMR_filter.Checked = true;
                }
                if (MLAT_WantToLoad == true && ALL_WantToLoad == false)
                {
                    DataTable_AsterixFile = Return[0];
                    DGV.DataSource = Return[0];
                    Messages_List_CAT10 = Asterix_used.get_CAT10_List();
                    Loaded_Asterix_file_type = "CAT10";
                    MLAT_filter.Checked = true;
                }
                if (ADSB_WantToLoad == true && ALL_WantToLoad == false)
                {
                    DataTable_AsterixFile = Return[1];
                    DGV.DataSource = Return[1];
                    Messages_List_CAT21 = Asterix_used.get_CAT21_List();
                    Loaded_Asterix_file_type = "CAT21";
                    ADSB_filter.Checked = true;
                }
                if (ALL_WantToLoad == true)
                {
                    DataTable_AsterixFile = Return[2];
                    DGV.DataSource = Return[2];
                    Messages_List_CAT10 = Asterix_used.get_CAT10_List();
                    Messages_List_CAT21 = Asterix_used.get_CAT21_List();
                    Loaded_Asterix_file_type = "CATALL";
                    SMR_filter.Checked = true;
                    MLAT_filter.Checked = true;
                    ADSB_filter.Checked = true;
                }
                Loading_Asterix_File_Panel.Visible = false;
                //MessageBox.Show(Return);
                Read_Asterix_File_Button.UseWaitCursor = false;
            }
            catch
            {
                MessageBox.Show("First select one file to read");
                Read_Asterix_File_Button.UseWaitCursor = false;
            }
        }

        private void Load_File_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void WantToLoad_CheckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(WantToLoad_CheckList.SelectedIndex == 3)
            {
                for (int i = 0; i < WantToLoad_CheckList.Items.Count; i++)
                {
                    WantToLoad_CheckList.SetItemChecked(i, false);
                }
                WantToLoad_CheckList.SetItemChecked(3, true);
            }
            if (WantToLoad_CheckList.SelectedIndex == 2)
            {          
                WantToLoad_CheckList.SetItemChecked(3, false);
            }
            if (WantToLoad_CheckList.SelectedIndex == 1)
            {
                WantToLoad_CheckList.SetItemChecked(3, false);
            }
            if (WantToLoad_CheckList.SelectedIndex == 0)
            {
                WantToLoad_CheckList.SetItemChecked(3, false);
            }
            if (WantToLoad_CheckList.SelectedIndex == 0)
            {
                if(WantToLoad_CheckList.SelectedIndex == 1)
                {
                    if(WantToLoad_CheckList.SelectedIndex == 2)
                    {
                        WantToLoad_CheckList.SetItemChecked(0, false);
                        WantToLoad_CheckList.SetItemChecked(1, false);
                        WantToLoad_CheckList.SetItemChecked(2, false);
                        WantToLoad_CheckList.SetItemChecked(3, true);
                    }
                }
            }
        }

        private void Data_table_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Simulation_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Export_KML_button_Click(object sender, EventArgs e)
        {                  
            
            
        }

        private void Home_Button_Click(object sender, EventArgs e)
        {
            Panel_Home.Visible = true;
            Panel_Home.BringToFront();
            Data_table_panel.Visible = false;
            Load_File_Panel.Visible = false;
            Simulation_Panel.Visible = false;
            WantToLoad_Panel.Visible = false;
            Read_Asterix_File_Button.Visible = false;
            Loading_Asterix_File_Panel.Visible = false;
            Exporting_to_ccsv_panel.Visible = false;
        }

        private void AirportMap_Click(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(41.29918003817614, 2.0941180771435848);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 14; //ZOOM inicial
        }

        private void BarcelonaMap_Click(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(41.29918003817614, 2.0941180771435848);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 12; //ZOOM inicial
        }

        private void CataloniaMap_Click(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(41.29918003817614, 2.0941180771435848);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 9; //ZOOM inicial
        }
    }
    
}
