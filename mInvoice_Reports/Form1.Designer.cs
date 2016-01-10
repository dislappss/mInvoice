namespace mInvoice_Reports
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designer-Variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Alle derzeit verwendeten Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">'true', wenn verwaltete Ressourcen freigegeben werden sollen, andernfalls 'false'.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Von Windows Form Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designer-Unterstützung - Ändern Sie den
        /// Inhalt dieser Methode nicht mit dem Code-Editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.myinvoice_dbDataSet = new mInvoice_Reports.myinvoice_dbDataSet();
            this.Invoice_headerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Invoice_headerTableAdapter = new mInvoice_Reports.myinvoice_dbDataSetTableAdapters.Invoice_headerTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.myinvoice_dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Invoice_headerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.Invoice_headerBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "mInvoice_Reports.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 386);
            this.reportViewer1.TabIndex = 0;
            // 
            // myinvoice_dbDataSet
            // 
            this.myinvoice_dbDataSet.DataSetName = "myinvoice_dbDataSet";
            this.myinvoice_dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Invoice_headerBindingSource
            // 
            this.Invoice_headerBindingSource.DataMember = "Invoice_header";
            this.Invoice_headerBindingSource.DataSource = this.myinvoice_dbDataSet;
            // 
            // Invoice_headerTableAdapter
            // 
            this.Invoice_headerTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 386);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myinvoice_dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Invoice_headerBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Invoice_headerBindingSource;
        private myinvoice_dbDataSet myinvoice_dbDataSet;
        private myinvoice_dbDataSetTableAdapters.Invoice_headerTableAdapter Invoice_headerTableAdapter;
    }
}

