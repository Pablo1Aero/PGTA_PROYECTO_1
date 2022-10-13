using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClassLibrary;
using Class_Library;
using Asterix_Decoder;

namespace Asterix_Decoder
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Message_DataView.DataSource = null;
            Message_DataView.DataSource = Form1.CAT10_Message_Data_Table;
            
        }

        private void Message_DataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
