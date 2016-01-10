using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mInvoice_Reports
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "myinvoice_dbDataSet.Invoice_header". Sie können sie bei Bedarf verschieben oder entfernen.
            this.Invoice_headerTableAdapter.Fill(this.myinvoice_dbDataSet.Invoice_header);
            this.reportViewer1.RefreshReport();
        }
    }
}