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

namespace Asterix_Decoder
{
    public partial class Form1 : Form
    {
        public AsterixFile Asterix = new AsterixFile(null, null, 0);
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public AsterixFile button1_Click(object sender, EventArgs e)
        {

            openFileDialogAsterix.InitialDirectory = "c:\\";
            openFileDialogAsterix.Filter = "ast files (*.ast)|*.ast|All files (*.*)|*.*";
            openFileDialogAsterix.FilterIndex = 2;
            openFileDialogAsterix.RestoreDirectory = true;

            if (openFileDialogAsterix.ShowDialog() == DialogResult.OK)
            {
                int i = 0;
                i++;
                string Name_File = "AsterixFile" + i;
                
                Asterix.Files_Paths.Add(openFileDialogAsterix.FileName);

                string File_Path = openFileDialogAsterix.FileName.ToString();
                bool IsIn = Asterix.Files_Paths.Find(File_Path);
                if (Asterix.Files_Paths.Exists(Asterix.Files_Paths == openFileDialogAsterix.FileName)  == false) 
                {
                    if (Asterix.Files_Paths[Asterix.Number_files_loaded] != null)
                    {
                        textBox1.Text = Asterix.path;
                        Asterix.Number_files_loaded++;
                        return Asterix;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        private void Read_Asterix_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                AsterixFile.ReadFile(Asterix.path);
            }
            catch
            {
                MessageBox.Show("Error reading Asterix file");
            }
        }

       
    }
}
