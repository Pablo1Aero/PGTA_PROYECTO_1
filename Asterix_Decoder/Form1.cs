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

namespace Asterix_Decoder
{
    public partial class Form1 : Form
    {
        GMarkerGoogle Marker;
        GMapOverlay GMapOverlay;

        public int FileCount = 0;
        public List<AsterixFile> AsterixFiles = new List<AsterixFile>();
        public List<CAT10> Messages_List_CAT10;
        public string Start_Simulation_time;
        public DecodeLibrary Library = new DecodeLibrary();
        public int Actual_Time;

        public Form1()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Load_Asterix_Btn = new System.Windows.Forms.Button();
            this.Read_Asterix_Btn = new System.Windows.Forms.Button();
            this.openFileDialogAsterix = new System.Windows.Forms.OpenFileDialog();
            this.Selector_Archivo = new System.Windows.Forms.ComboBox();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Data_table_panel = new System.Windows.Forms.Panel();
            this.Cell_Display = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Load_File_Panel = new System.Windows.Forms.Panel();
            this.progressBarLoadFile = new System.Windows.Forms.ProgressBar();
            this.Data_Table_Button = new System.Windows.Forms.Button();
            this.Load_File_Button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Simulation_Button = new System.Windows.Forms.Button();
            this.Simulation_Panel = new System.Windows.Forms.Panel();
            this.Actual_Time_Label = new System.Windows.Forms.Label();
            this.Start_Time_Label = new System.Windows.Forms.Label();
            this.Reset_Simulation_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Start_Simulation_Button = new System.Windows.Forms.Button();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.Stop_Simulation_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.Data_table_panel.SuspendLayout();
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
            this.DGV.AllowUserToOrderColumns = true;
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.DGV.ColumnHeadersHeight = 29;
            this.DGV.GridColor = System.Drawing.SystemColors.MenuHighlight;
            this.DGV.Location = new System.Drawing.Point(22, 87);
            this.DGV.Name = "DGV";
            this.DGV.RowHeadersWidth = 51;
            this.DGV.RowTemplate.Height = 29;
            this.DGV.RowTemplate.ReadOnly = true;
            this.DGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.Size = new System.Drawing.Size(1586, 669);
            this.DGV.TabIndex = 4;
            this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellClick);
            // 
            // Data_table_panel
            // 
            this.Data_table_panel.BackColor = System.Drawing.SystemColors.InfoText;
            this.Data_table_panel.Controls.Add(this.Cell_Display);
            this.Data_table_panel.Controls.Add(this.button2);
            this.Data_table_panel.Controls.Add(this.textBox1);
            this.Data_table_panel.Controls.Add(this.button1);
            this.Data_table_panel.Controls.Add(this.DGV);
            this.Data_table_panel.Location = new System.Drawing.Point(260, 12);
            this.Data_table_panel.Name = "Data_table_panel";
            this.Data_table_panel.Size = new System.Drawing.Size(1630, 866);
            this.Data_table_panel.TabIndex = 5;
            // 
            // Cell_Display
            // 
            this.Cell_Display.Location = new System.Drawing.Point(22, 803);
            this.Cell_Display.Name = "Cell_Display";
            this.Cell_Display.Size = new System.Drawing.Size(1586, 27);
            this.Cell_Display.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1514, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 7;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(849, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(646, 27);
            this.textBox1.TabIndex = 6;
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
            // Load_File_Panel
            // 
            this.Load_File_Panel.BackColor = System.Drawing.SystemColors.InfoText;
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
            this.progressBarLoadFile.Location = new System.Drawing.Point(192, 401);
            this.progressBarLoadFile.Name = "progressBarLoadFile";
            this.progressBarLoadFile.Size = new System.Drawing.Size(125, 29);
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(1195, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Start_Simulation_Button
            // 
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
            this.Controls.Add(this.Simulation_Panel);
            this.Controls.Add(this.Simulation_Button);
            this.Controls.Add(this.Load_File_Button);
            this.Controls.Add(this.Data_Table_Button);
            this.Controls.Add(this.Data_table_panel);
            this.Controls.Add(this.Load_File_Panel);
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.Data_table_panel.ResumeLayout(false);
            this.Data_table_panel.PerformLayout();
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
                        Messages_List_CAT10 =  Asterix.get_CAT10List();
                    }
                }
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
                int index = Selector_Archivo.SelectedIndex;
                progressBarLoadFile.Maximum = AsterixFiles[index].get_FileSize();
                progressBarLoadFile.Minimum = 0;
                progressBarLoadFile.Value = 0;
                progressBarLoadFile.Step = 1;
                progressBarLoadFile.Style = ProgressBarStyle.Continuous;
                DataTable Return = AsterixFiles[index].ReadFile(AsterixFiles[index].path, AsterixFiles[index].name);
                //MessageBox.Show(Return);             

                DGV.DataSource = Return;


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
            DataGridView DataGrid = new DataGridView();
            DataGrid = DGV;         
            Cell_Display.Text = DGV.CurrentRow.Cells[e.ColumnIndex].Value.ToString();
            DGV.CurrentRow.Cells[e.ColumnIndex].Style.WrapMode = DataGridViewTriState.True;
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
            GMapOverlay = new GMapOverlay("Marker");
        }

        private void Start_Simulation_Button_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 1000;
            Start_Simulation_Button.Visible = false;
            Stop_Simulation_Button.Visible = true;

        }

        int Timer_Count;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {

                Timer_Count++;
                label1.Text = Convert.ToString(Timer_Count);
                if (timer1.Interval == 1000)
                {
                    
                    int i = 0;
                    while (Messages_List_CAT10.Count > i)
                    {

                        if (Messages_List_CAT10[i].ToD_seconds == Actual_Time)
                        {
                            if ((Messages_List_CAT10[i].Lat_WGS84 != null && Messages_List_CAT10[i].Lon_WGS84 != null) || (Messages_List_CAT10[i].X_Component != null && Messages_List_CAT10[i].Y_Component != null))
                            {
                                if (Messages_List_CAT10[i].SIC == "7") //SMR
                                {
                                    CoordinatesWGS84 PositionWGS84 = new CoordinatesWGS84();
                                    PositionWGS84.Lat = 0.72074450650363;
                                    PositionWGS84.Lon = 0.036566640419238002901;
                                    Create_ActualPosition_Marker(Convert.ToInt32(Messages_List_CAT10[i].X_Component), Convert.ToInt32(Messages_List_CAT10[i].Y_Component), PositionWGS84);
                                }
                                if (Messages_List_CAT10[i].SIC == "107")//MLAT
                                {
                                    CoordinatesWGS84 PositionWGS84 = new CoordinatesWGS84();
                                    PositionWGS84.Lat = 0.72076971691201;
                                    PositionWGS84.Lon = 0.03627574735274;
                                    Create_ActualPosition_Marker(Convert.ToInt32(Messages_List_CAT10[i].X_Component), Convert.ToInt32(Messages_List_CAT10[i].Y_Component), PositionWGS84);
                                }
                                if (Messages_List_CAT10[i].SIC == "219")//ADSB
                                {
                                    
                                }
                                Messages_List_CAT10.RemoveAt(i);
                                //GMapOverlay.Markers.Clear();
                            }
                        }                                            
                        i++;
                    }
                    Actual_Time = Actual_Time + 1;
                }
                Actual_Time_Label.Text = Library.ToD_Calc(Actual_Time);
                //Añadir lo mismo para cada velocidad de simulacion
                //Messages_List_CAT10[Timer_Count].

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

        private void Create_ActualPosition_Marker(int x,int y, CoordinatesWGS84 pos)
        {
            //Marker
            PointLatLng Map_Point = Convert_To_LatLngPoint(x, y, pos);
            Marker = new GMarkerGoogle(Map_Point, GMarkerGoogleType.red);
            GMapOverlay.Markers.Add(Marker);

            //Tooltip text markers
            Marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            Marker.ToolTipText = String.Format(Convert.ToString(Map_Point.Lat), Convert.ToString(Map_Point.Lng));

            //Add map and marker
            gMapControl1.Overlays.Add(GMapOverlay);
        }
    }
    
}
