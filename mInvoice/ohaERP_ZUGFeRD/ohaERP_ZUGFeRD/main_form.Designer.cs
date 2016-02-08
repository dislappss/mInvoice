namespace ZUGFeRD_Test
{
    partial class main_form
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.generate_pdfButton = new System.Windows.Forms.Button();
            this.folderButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderTextBox = new System.Windows.Forms.TextBox();
            //this.rp_invoice_headerTableAdapter = new DataSet1TableAdapters.Invoice_headerTableAdapter ();
            this.rp_invoice_detailsTableAdapter = new  DataSet1TableAdapters.rp_invoice_detailsTableAdapter   ();
            this.dataSet11 = new ZUGFeRD_Test.DataSet1();
            //this.rp_invoice_header_paymentsTableAdapter = new ZUGFeRD_Test.DataSet1TableAdapters.rp_invoice_header_paymentsTableAdapter();
            //this.banksTableAdapter = new ZUGFeRD_Test.DataSet1TableAdapters.banksTableAdapter();
            //this.clientTableAdapter = new ZUGFeRD_Test.DataSet1TableAdapters.clientTableAdapter();
            //this.rp_headofletter_layoutTableAdapter = new ZUGFeRD_Test.DataSet1TableAdapters.rp_headofletter_layoutTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 100);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(552, 336);
            this.textBox1.TabIndex = 12;
            // 
            // generate_pdfButton
            // 
            this.generate_pdfButton.Location = new System.Drawing.Point(12, 50);
            this.generate_pdfButton.Name = "generate_pdfButton";
            this.generate_pdfButton.Size = new System.Drawing.Size(151, 23);
            this.generate_pdfButton.TabIndex = 11;
            this.generate_pdfButton.Text = "&Generate PDF";
            this.generate_pdfButton.UseVisualStyleBackColor = true;
            this.generate_pdfButton.Click += new System.EventHandler(this.generate_pdfButton_Click);
            // 
            // folderButton
            // 
            this.folderButton.Location = new System.Drawing.Point(332, 12);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(122, 23);
            this.folderButton.TabIndex = 9;
            this.folderButton.Text = "Folder";
            this.folderButton.UseVisualStyleBackColor = true;
            this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // folderTextBox
            // 
            this.folderTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ZUGFeRD_Test.Properties.Settings.Default, "folder_txt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.folderTextBox.Location = new System.Drawing.Point(12, 12);
            this.folderTextBox.Name = "folderTextBox";
            this.folderTextBox.Size = new System.Drawing.Size(314, 20);
            this.folderTextBox.TabIndex = 10;
            this.folderTextBox.Text = global::ZUGFeRD_Test.Properties.Settings.Default.folder_txt;
            // 
            // rp_invoice_headerTableAdapter
            // 
            //this.rp_invoice_headerTableAdapter.ClearBeforeFill = true;
            // 
            // rp_invoice_detailsTableAdapter
            // 
            this.rp_invoice_detailsTableAdapter.ClearBeforeFill = true;
            // 
            // dataSet11
            // 
            this.dataSet11.DataSetName = "DataSet1";
            this.dataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rp_invoice_header_paymentsTableAdapter
            // 
            //this.rp_invoice_header_paymentsTableAdapter.ClearBeforeFill = true;
            //// 
            //// banksTableAdapter
            //// 
            //this.banksTableAdapter.ClearBeforeFill = true;
            // 
            // clientTableAdapter
            // 
            // 
            // rp_headofletter_layoutTableAdapter
            // 
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 449);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.generate_pdfButton);
            this.Controls.Add(this.folderTextBox);
            this.Controls.Add(this.folderButton);
            this.Name = "main_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "main_form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.main_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button generate_pdfButton;
        private System.Windows.Forms.TextBox folderTextBox;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        //private DataSet1TableAdapters.Invoice_headerTableAdapter rp_invoice_headerTableAdapter;
        private DataSet1TableAdapters.rp_invoice_detailsTableAdapter  rp_invoice_detailsTableAdapter;
        private DataSet1 dataSet11;
        //private DataSet1TableAdapters.rp_invoice_header_paymentsTableAdapter rp_invoice_header_paymentsTableAdapter;
        //private DataSet1TableAdapters.banksTableAdapter banksTableAdapter;
        //private DataSet1TableAdapters.clientTableAdapter clientTableAdapter;
        //private DataSet1TableAdapters.rp_headofletter_layoutTableAdapter rp_headofletter_layoutTableAdapter;
    }
}