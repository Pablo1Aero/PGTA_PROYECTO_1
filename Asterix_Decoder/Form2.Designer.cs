namespace Asterix_Decoder
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Message_DataView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Message_DataView)).BeginInit();
            this.SuspendLayout();
            // 
            // Message_DataView
            // 
            this.Message_DataView.AllowUserToOrderColumns = true;
            this.Message_DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Message_DataView.Location = new System.Drawing.Point(12, 12);
            this.Message_DataView.Name = "Message_DataView";
            this.Message_DataView.RowHeadersWidth = 51;
            this.Message_DataView.RowTemplate.Height = 29;
            this.Message_DataView.Size = new System.Drawing.Size(776, 365);
            this.Message_DataView.TabIndex = 0;
            this.Message_DataView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Message_DataView_CellContentClick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Message_DataView);
            this.Name = "Form2";
            this.Text = "Data Grid View";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Message_DataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Message_DataView;
    }
}