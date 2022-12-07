namespace Asterix_Decoder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Button Load_Asterix_Btn;
        private System.Windows.Forms.Button Read_Asterix_Btn;
        private System.Windows.Forms.OpenFileDialog openFileDialogAsterix;
        private System.Windows.Forms.ComboBox Selector_Archivo;
        public System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Panel Data_table_panel;
        private System.Windows.Forms.Panel Load_File_Panel;
        private System.Windows.Forms.TextBox Search_textBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Data_Table_Button;
        private System.Windows.Forms.Button Load_File_Button;
        private System.Windows.Forms.ProgressBar progressBarLoadFile;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Simulation_Button;
        private System.Windows.Forms.Panel Simulation_Panel;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.Button Start_Simulation_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Stop_Simulation_Button;
        private System.Windows.Forms.Button Reset_Simulation_Button;
        private System.Windows.Forms.Label Start_Time_Label;
        private System.Windows.Forms.Label Actual_Time_Label;
        private System.Windows.Forms.CheckBox SMR_filter;
        private System.Windows.Forms.CheckBox MLAT_filter;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel WantToLoad_Panel;
        private System.Windows.Forms.CheckedListBox WantToLoad_CheckList;
        private System.Windows.Forms.Button Done_Button;
    }
}
