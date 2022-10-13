using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ClassLibrary;
using Asterix_Decoder;
using System.IO;

namespace Asterix_Decoder
{
    public partial class Form1 : Form
    {
        public int FileCount = 0;
        public List<AsterixFile> AsterixFiles = new List<AsterixFile>();
        public List<byte[]> Message_Data = new List<byte[]>();
        public static DataTable CAT10_Message_Data_Table;
        public static DataTable CAT21_Message_Data_Table;
        

        //private CurrencyManager currencymanager = null;
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void InitializeComponent()
        {
            this.Load_Asterix_Btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Read_Asterix_Btn = new System.Windows.Forms.Button();
            this.openFileDialogAsterix = new System.Windows.Forms.OpenFileDialog();
            this.Selector_Archivos = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Load_Asterix_Btn
            // 
            this.Load_Asterix_Btn.Location = new System.Drawing.Point(124, 45);
            this.Load_Asterix_Btn.Name = "Load_Asterix_Btn";
            this.Load_Asterix_Btn.Size = new System.Drawing.Size(113, 80);
            this.Load_Asterix_Btn.TabIndex = 0;
            this.Load_Asterix_Btn.Text = "LOAD ASTERIX FILE";
            this.Load_Asterix_Btn.UseVisualStyleBackColor = true;
            this.Load_Asterix_Btn.Click += new System.EventHandler(this.Load_Asterix_Btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(225, 27);
            this.textBox1.TabIndex = 1;
            // 
            // Read_Asterix_Btn
            // 
            this.Read_Asterix_Btn.Location = new System.Drawing.Point(12, 45);
            this.Read_Asterix_Btn.Name = "Read_Asterix_Btn";
            this.Read_Asterix_Btn.Size = new System.Drawing.Size(106, 80);
            this.Read_Asterix_Btn.TabIndex = 2;
            this.Read_Asterix_Btn.Text = "READ ASTERIX FILE";
            this.Read_Asterix_Btn.UseVisualStyleBackColor = true;
            // 
            // openFileDialogAsterix
            // 
            this.openFileDialogAsterix.FileName = "openFileDialogAsterix";
            // 
            // Selector_Archivos
            // 
            this.Selector_Archivos.FormattingEnabled = true;
            this.Selector_Archivos.Location = new System.Drawing.Point(243, 12);
            this.Selector_Archivos.Name = "Selector_Archivos";
            this.Selector_Archivos.Size = new System.Drawing.Size(314, 28);
            this.Selector_Archivos.TabIndex = 3;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(782, 389);
            this.Controls.Add(this.Selector_Archivos);
            this.Controls.Add(this.Read_Asterix_Btn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Load_Asterix_Btn);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

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
                        textBox1.Text = File_Name;
                        Selector_Archivos.Items.Add(File_Name);
                    }
                }
            }
            catch
            {
                MessageBox.Show("The file selected could not be loaded.");
            }
        }

        private void Read_Asterix_Btn_Click(object sender, EventArgs e)
        {

            try
            {
                int index = Selector_Archivos.SelectedIndex;
                string Return = AsterixFiles[index].ReadFile(AsterixFiles[index].path, AsterixFiles[index].name);
                /*
                DataGridView CAT10Message_Data_View = new DataGridView();
                DataTable CAT10_Table = new DataTable();
                DataTable CAT21_Table = new DataTable();
                int i = 0;
                while (i < AsterixFiles[index].CAT10_Messages_List.Count())
                {
                    DataGridViewRow row = (DataGridViewRow)CAT10Message_Data_View.Rows[i].Clone();
                    int j = 0;
                    while (j < AsterixFiles[index].CAT10_Messages_List[i].CAT10_Message.Count())
                    {
                        row.Cells[j].Value = AsterixFiles[index].CAT10_Messages_List[i].CAT10_Message[j];
                        j++;
                    }
                    i++;
                }
                //CAT10Message_Data_View.DataSource = CAT10_Table;
                //CAT10Message_Data_View.Show();
                */
                string[] test = AsterixFiles[index].CAT10_Messages_List[index].CAT10_Message; 
                MessageBox.Show(Return);
            }
            catch (Exception sel)
            {
                MessageBox.Show("Select one file to read");
            }
        }

        /*private void Form1_Load_1(object sender, EventArgs e)
        {
            try
            {
                string Return = AsterixFiles[0].ReadFile(AsterixFiles[0].path);
                MessageBox.Show(Return);
            }
            catch
            {
                MessageBox.Show("Error reading Asterix File");
            }
        }
        */

        /*private void Read_Asterix_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                AsterixFile.ReadFile(AsterixFiles[].path);
            }
            catch
            {
                MessageBox.Show("Error reading Asterix file");
            }
        }
        */

    }
}
