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
using Class_Library;


namespace Asterix_Decoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialogAsterix.InitialDirectory = "c:\\";
            openFileDialogAsterix.Filter = "ast files (*.ast)|*.ast|All files (*.*)|*.*";
            openFileDialogAsterix.FilterIndex = 2;
            openFileDialogAsterix.RestoreDirectory = true;

            if (openFileDialogAsterix.ShowDialog() == DialogResult.OK)
            {
                Class_Library.Asterix_File Asterix = new Class_Library.Asterix_File(openFileDialogAsterix.FileName);
                if (Asterix.path != null)
                {
                    textBox1.Text = Asterix.path;
                }
            }
        }
    }
}
