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

namespace Asterix_Decoder
{
    public partial class Form1 : Form
    {
        public int FileCount = 0;
        public List<AsterixFile> AsterixFiles = new List<AsterixFile>();
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
            this.Read_Asterix_Btn = new System.Windows.Forms.Button();
            this.openFileDialogAsterix = new System.Windows.Forms.OpenFileDialog();
            this.Selector_Archivo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Load_Asterix_Btn
            // 
            this.Load_Asterix_Btn.Location = new System.Drawing.Point(132, 12);
            this.Load_Asterix_Btn.Name = "Load_Asterix_Btn";
            this.Load_Asterix_Btn.Size = new System.Drawing.Size(112, 73);
            this.Load_Asterix_Btn.TabIndex = 0;
            this.Load_Asterix_Btn.Text = "LOAD ASTERIX FILE";
            this.Load_Asterix_Btn.UseVisualStyleBackColor = true;
            this.Load_Asterix_Btn.Click += new System.EventHandler(this.Load_Asterix_Btn_Click);
            // 
            // Read_Asterix_Btn
            // 
            this.Read_Asterix_Btn.Location = new System.Drawing.Point(14, 12);
            this.Read_Asterix_Btn.Name = "Read_Asterix_Btn";
            this.Read_Asterix_Btn.Size = new System.Drawing.Size(112, 73);
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
            this.Selector_Archivo.Location = new System.Drawing.Point(14, 91);
            this.Selector_Archivo.Name = "Selector_Archivo";
            this.Selector_Archivo.Size = new System.Drawing.Size(230, 28);
            this.Selector_Archivo.TabIndex = 3;
            this.Selector_Archivo.SelectedIndexChanged += new System.EventHandler(this.Selector_Archivo_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(782, 389);
            this.Controls.Add(this.Selector_Archivo);
            this.Controls.Add(this.Read_Asterix_Btn);
            this.Controls.Add(this.Load_Asterix_Btn);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
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
                string Return = AsterixFiles[index].ReadFile(AsterixFiles[index].path, AsterixFiles[index].name);
                MessageBox.Show(Return);
            }
            catch (Exception sel)
            { 
            MessageBox.Show("Select one file to read");
            }
        }

        private void Selector_Archivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
