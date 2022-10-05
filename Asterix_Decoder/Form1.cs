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

namespace Asterix_Decoder
{
    public partial class Form1 : Form
    {
        public int FileCount = 0;
        public List<AsterixFile> AsterixFiles = new List<AsterixFile>();
        public DataView Message_DataView = new DataView();
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
            this.SuspendLayout();
            // 
            // Load_Asterix_Btn
            // 
            this.Load_Asterix_Btn.Location = new System.Drawing.Point(12, 12);
            this.Load_Asterix_Btn.Name = "Load_Asterix_Btn";
            this.Load_Asterix_Btn.Size = new System.Drawing.Size(293, 29);
            this.Load_Asterix_Btn.TabIndex = 0;
            this.Load_Asterix_Btn.Text = "LOAD ASTERIX FILE";
            this.Load_Asterix_Btn.UseVisualStyleBackColor = true;
            this.Load_Asterix_Btn.Click += new System.EventHandler(this.Load_Asterix_Btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(293, 27);
            this.textBox1.TabIndex = 1;
            // 
            // Read_Asterix_Btn
            // 
            this.Read_Asterix_Btn.Location = new System.Drawing.Point(12, 80);
            this.Read_Asterix_Btn.Name = "Read_Asterix_Btn";
            this.Read_Asterix_Btn.Size = new System.Drawing.Size(293, 29);
            this.Read_Asterix_Btn.TabIndex = 2;
            this.Read_Asterix_Btn.Text = "READ ASTERIX FILE";
            this.Read_Asterix_Btn.UseVisualStyleBackColor = true;
            this.Read_Asterix_Btn.Click += new System.EventHandler(this.Read_Asterix_Btn_Click);
            // 
            // openFileDialogAsterix
            // 
            this.openFileDialogAsterix.FileName = "openFileDialogAsterix";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(782, 389);
            this.Controls.Add(this.Read_Asterix_Btn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Load_Asterix_Btn);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void Load_Asterix_Btn_Click(object sender, EventArgs e)
        {
            openFileDialogAsterix.InitialDirectory = "c:\\";
            openFileDialogAsterix.Filter = "ast files (*.ast)|*.ast|All files (*.*)|*.*";
            openFileDialogAsterix.FilterIndex = 2;
            openFileDialogAsterix.RestoreDirectory = true;

            if (openFileDialogAsterix.ShowDialog() == DialogResult.OK)
            {
                AsterixFile Asterix = new AsterixFile(null, null);

                string File_Path = openFileDialogAsterix.FileName.ToString();

                bool IsIn = AsterixFiles.Exists(Asterix => Asterix.path == File_Path);
                if (IsIn == false)
                {
                    FileCount++;
                    Asterix.path = File_Path;
                    Asterix.name = "Asterix File" + FileCount;
                    AsterixFiles.Add(Asterix);
                    textBox1.Text = "Added: " + Asterix.name;
                }
            }
        }

        private void Read_Asterix_Btn_Click(object sender, EventArgs e)
        {
            
            try
            {
                foreach (AsterixFile Asterix in AsterixFiles)
                {
                    Asterix.ReadFile(Asterix.path);
                    
                }
                Message_DataView.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Error reading Asterix files. Check for loaded Asterix files.");
            }
        }


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
