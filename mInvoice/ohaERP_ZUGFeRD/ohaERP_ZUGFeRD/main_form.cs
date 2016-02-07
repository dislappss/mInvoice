using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml.xmp;
using s2industries.ZUGFeRD;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ZUGFeRD_Test
{
    public partial class main_form : Form
    {
        private string m_input_pdf_file;
        private string m_output_xml_file_name = @"ZUGFeRD-invoice.xml";
        private SqlConnection m_SqlConnection = new SqlConnection(
            "Data Source=v-srv-sql.haas.de;Initial Catalog=ohaERP;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=60");

        public main_form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ZUGFeRD-File generator
        /// </summary>
        /// <param name="NonZUGFeRD_PDF"></param>
        /// <param name="Connection">SQL-Connection</param>
        /// <param name="Clientsysid">Client ID</param>
        /// <param name="Languagesysid">Language ID</param>
        /// <param name="Invoicenumber">Invoice-no.</param>
        /// <param name="InvoiceHeadersysid">Invoice_header-ID</param>
        /// <returns></returns>
        public string getZugFeRD_PDF(
           string NonZUGFeRD_PDF
           , SqlConnection Connection
           , int Clientsysid
           , int Languagesysid
           , string Invoicenumber
           , int InvoiceHeadersysid
           )
        {
            rp_invoice_headerTableAdapter.Connection = Connection;
            rp_invoice_detailsTableAdapter.Connection = Connection;
            rp_invoice_header_paymentsTableAdapter.Connection = Connection;
            banksTableAdapter.Connection = Connection;
            clientTableAdapter.Connection = Connection;
            rp_headofletter_layoutTableAdapter.Connection = Connection;

            string _output_file_directory = Path.GetDirectoryName(NonZUGFeRD_PDF);
            string _output_xml_file_path = Path.Combine(_output_file_directory, m_output_xml_file_name);
            string _output_pdf_file = null;

            rp_invoice_headerTableAdapter.Fill(
                dataSet11.rp_invoice_header, Clientsysid, Languagesysid, Invoicenumber, InvoiceHeadersysid);
            rp_invoice_detailsTableAdapter.Fill(
                dataSet11.rp_invoice_details, Clientsysid, Languagesysid, InvoiceHeadersysid);
            banksTableAdapter.Fill(
                dataSet11.banks, Clientsysid);
            clientTableAdapter.Fill(
                dataSet11.client, Clientsysid);
            rp_headofletter_layoutTableAdapter.Fill(
                dataSet11.rp_headofletter_layout, Clientsysid, Languagesysid, false, false);

            if (dataSet11.rp_invoice_header.Rows.Count == 1
                && dataSet11.rp_invoice_details.Rows.Count > 0
                && dataSet11.banks.Rows.Count == 1
                && dataSet11.client.Rows.Count == 1
                && dataSet11.rp_headofletter_layout.Count == 1
                )
            {
                // Generate XML                
                generateXMLFromInvoiceInfo(_output_xml_file_path, Clientsysid, Languagesysid, InvoiceHeadersysid);

                _output_pdf_file = Path.Combine(_output_file_directory, dataSet11.rp_invoice_header[0].invoicenumber + "_zugferd.pdf");

                ConvertRegularToConformantPDF_3A(
                      _output_pdf_file
                    , NonZUGFeRD_PDF
                    , _output_xml_file_path);

                return _output_pdf_file;
            }
            else
                throw new Exception("Daten sind nicht korrekt!" + Environment.NewLine +
                                    "rp_invoice_header.Rows.Count=" + dataSet11.rp_invoice_header.Rows.Count + Environment.NewLine +
                                    "rp_invoice_details.Rows.Count=" + dataSet11.rp_invoice_details.Rows.Count + Environment.NewLine +
                                    "banks.Rows.Count=" + dataSet11.banks.Rows.Count + Environment.NewLine +
                                    "client.Rows.Count=" + dataSet11.client.Rows.Count + Environment.NewLine +
                                    "dataSet11.rp_headofletter_layout.Count=" + dataSet11.rp_headofletter_layout.Rows.Count
                                    );
        }

        private void generate_pdfButton_Click(object sender, EventArgs e)
        {
            // TEST
            openFileDialog1.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.* ";
            Exception _exception = new Exception();

            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(openFileDialog1.FileName))
                    {
                        getZugFeRD_PDF(openFileDialog1.FileName, m_SqlConnection, 1, 9, "INV-15-000750", 2957);

                        MessageBox.Show("Done");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;

            DialogResult result = folderBrowserDialog1.ShowDialog();

            // OK button was pressed.
            if (result == DialogResult.OK)
            {
                folderTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

       

        private void generateXMLFromInvoiceInfo(string OutputXML, int Clientsysid, int Languagesysid, int InvoiceHeadersysid)
        {
            InvoiceDescriptor desc = _createInvoice(Clientsysid, Languagesysid, InvoiceHeadersysid);
            desc.Save(OutputXML);
        }

        private InvoiceDescriptor _createInvoice(int Clientsysid, int Languagesysid, int InvoiceHeadersysid)
        {
            var _invoice_header_row = dataSet11.rp_invoice_header[0];
            var _invoice_details_row = dataSet11.rp_invoice_details[0];
            var _banks_row = dataSet11.banks[0];
            var _client_row = dataSet11.client[0];
            var _rp_headofletter_layout_row = dataSet11.rp_headofletter_layout[0];
            DataSet1.rp_invoice_header_paymentsRow _invoice_header_paymentsRow = null;

            CurrencyCodes _currenciesdescription = (CurrencyCodes)System.Enum.Parse(typeof(CurrencyCodes), _invoice_header_row.currenciesdescription);
            CountryCodes _invoiceadress_nationdescriptionshortmark = (CountryCodes)System.Enum.Parse(typeof(CountryCodes), _invoice_header_row.invoiceadress_nationdescriptionshortmark);

            rp_invoice_header_paymentsTableAdapter.FillByrp_invoice_header_payments(
                dataSet11.rp_invoice_header_payments,
                    Clientsysid, Languagesysid, InvoiceHeadersysid);

            if (dataSet11.rp_invoice_header_payments.Rows.Count != 1) 
            { 
                throw new Exception("rp_invoice_header_payments sends NULL!"); 
            }

            _invoice_header_paymentsRow = dataSet11.rp_invoice_header_payments[0];

            /*
                string invoiceNo, 
                DateTime invoiceDate, 
                CurrencyCodes currency, 
                string invoiceNoAsReference = ""
             */
            InvoiceDescriptor desc = InvoiceDescriptor.CreateInvoice(
                _invoice_header_row.invoicenumber
                , _invoice_header_row.invoicedate
                , _currenciesdescription
                //, "GE2020211-471102"
                );
            desc.Profile = Profile.Comfort;
            desc.ReferenceOrderNo = _invoice_header_row.ordernumber;
            desc.AddNote(_invoice_header_row.order_header_regarding);
            desc.SetBuyer(
                  _invoice_header_row.invoiceadress_companyname1
                , _invoice_header_row.invoiceadress_postcode
                , _invoice_header_row.invoiceadress_city
                , _invoice_header_row.invoiceadress_street
                , ""  // street-no.
                , _invoiceadress_nationdescriptionshortmark
                , _invoice_header_row.customer_customerid.ToString()
                //, "0088"
                //, "4000001987658"
                );

            if (!_invoice_header_row.IstaxnumberNull())
                desc.AddBuyerTaxRegistration(_invoice_header_row.taxnumber, TaxRegistrationSchemeID.VA);

            if (!_invoice_header_row.Iscustomer_contacts_descriptionNull())
                desc.SetBuyerContact(_invoice_header_row.customer_contacts_description);

            // Kaefers Bestellbeleg
            if (!_invoice_header_row.IsregardingNull())
                desc.SetBuyerOrderReferenceDocument(_invoice_header_row.regarding, _invoice_header_row.orderdate);

            // Verkaeufer 
            desc.SetSeller(
                _rp_headofletter_layout_row.companyname1
                , _rp_headofletter_layout_row.postcode
                , _rp_headofletter_layout_row.city
                , _rp_headofletter_layout_row.street 
                , ""
                , CountryCodes.DE
                , ""
                //, "0088"
                //, "4000001123452"
                );
            desc.AddSellerTaxRegistration(_rp_headofletter_layout_row.tax_number, TaxRegistrationSchemeID.FC);
            desc.AddSellerTaxRegistration(_rp_headofletter_layout_row.vat_number, TaxRegistrationSchemeID.VA);

            // Lieferschein
            desc.SetDeliveryNoteReferenceDocument(_invoice_details_row.bill_of_delivery_number, _invoice_details_row.bill_of_delivery_date);
            desc.ActualDeliveryDate = _invoice_details_row.bill_of_delivery_date;

            decimal _rabatt = _invoice_header_paymentsRow.valueofgoods - _invoice_header_paymentsRow.valueofgoods_without_discount;

            // 1. "lineTotalAmount"        > Gesamtbetrag der Positionen (Warenwert)
            // 2. "chargeTotalAmount"      > Gesamtbetrag der Zuschläge (Versandkosten)
            // 3. "allowanceTotalAmount"   > Gesamtbetrag der Abschläge (Rabatt)
            // 4. "taxBasisAmount"         > Basisbetrag der Steuerberechnung (Zwischensumme)
            // 5. "taxTotalAmount"         > Steuergesamtbetrag (MwSt.)
            // 6. "grandTotalAmount"       > Bruttosumme (Endsumme)
            // 7. "totalPrepaidAmount"     > Anzahlungsbetrag (immer 0) 
            // 8. "duePayableAmount"       > Zahlbetrag (Endsumme)
            desc.SetTotals(
                  _invoice_header_paymentsRow.valueofgoods
                , _invoice_header_paymentsRow.freightcosts
                , _rabatt
                , _invoice_header_paymentsRow.subtotal
                , _invoice_header_paymentsRow.taxtotal
                , _invoice_header_paymentsRow.total 
                , 0
                , _invoice_header_paymentsRow.total
                );

            // verwendbare Gewerbesteuer
           // desc.AddApplicableTradeTax(129.37m, 7m, TaxTypes.VAT, TaxCategoryCodes.S);

            // Logistikbedingungen
            desc.AddLogisticsServiceCharge(_invoice_header_paymentsRow.freightcosts, "Versandkosten", TaxTypes.VAT, TaxCategoryCodes.S, _invoice_header_paymentsRow.valueaddedtax);

            // Handelsrabatt
           
            if (_rabatt != 0)
                desc.AddTradeAllowanceCharge(true, _rabatt, _currenciesdescription, _rabatt, "Sondernachlass", TaxTypes.VAT, TaxCategoryCodes.S, _invoice_header_paymentsRow.valueaddedtax);

            // Zahlungsbedingungen
            desc.SetTradePaymentTerms(_invoice_header_row.paymentterms_description, _invoice_header_paymentsRow.DaysNetDate);

            // Zahlungsmittel
            desc.setPaymentMeans("", _invoice_header_row.paymentterms_description);

            // Finanzkonto des Kreditors (HAAS, ohaSys)
            desc.addCreditorFinancialAccount(
                _banks_row.iban_code  // "DE08700901001234567890"
                , _banks_row.swift_code //"GENODEF1M04"
                , _banks_row.account_number 
                , _banks_row.bank_code 
                , _banks_row.name);

            // Positionen
            foreach (var _position in dataSet11.rp_invoice_details)
            {
                QuantityCodes _QuantityCodes = (QuantityCodes)System.Enum.Parse(typeof(QuantityCodes), _position.quantity_unit_descriptionshortmark_iso_code);

                desc.addTradeLineCommentItem("Wir erlauben uns Ihnen folgende Positionen aus der Lieferung Nr. 2013-51112 in Rechnung zu stellen:");

                /*
                    string name, 
                    string description,                                     
                    QuantityCodes unitCode, 
                    decimal unitQuantity,                                     
                    decimal grossUnitPrice,     // Brutto einzelpreis                                    
                    decimal netUnitPrice,                                     
                    decimal billedQuantity,   // berechnete Menge                                  
                    TaxTypes taxType, 
                    TaxCategoryCodes categoryCode, 
                    decimal taxPercent,                                     
                    string comment = "",                                     
                    string globalIDSchemeID = "", 
                    string globalID = "",                                     
                    string sellerAssignedID = "", 
                    string buyerAssignedID = ""
                 */
                desc.addTradeLineItem(  _position.articleid_variant,
                                        _position.article_text
                                      , _QuantityCodes
                                      , _position.quantity
                                      , _position.price
                                      , _position.price
                                      , _position.quantity
                                      , TaxTypes.VAT
                                      , TaxCategoryCodes.S
                                      , _invoice_header_paymentsRow.valueaddedtax
                                      //, "0160"
                                      //, "4012345001235"
                                      //, "KR3M"
                                      //, "55T01"
                                      );
            }

          
            return desc;

        }

        public void ConvertRegularToConformantPDF_3A(string PDF_WithXML, string PDF_start, string XMLFile)
        {
            if (System.IO.File.Exists(PDF_WithXML))
                System.IO.File.Delete(PDF_WithXML);

            string _xml_filename = System.IO.Path.GetFileName(XMLFile);

            using (var fs = new FileStream(PDF_WithXML, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var document = new Document())
                {
                    // Create PdfAWriter with PdfAConformanceLevel.PDF_A_3B option if you
                    // want to get a PDF/A-3b compliant document.
                    PdfAWriter _new_pdf_writer = PdfAWriter.GetInstance(document, fs, PdfAConformanceLevel.ZUGFeRD );

                    // Create XMP metadata. It's a PDF/A requirement.
                    _new_pdf_writer.CreateXmpMetadata();                                       

                    document.Open();

                    // Attributes
                    document.AddAuthor("Otto Haas KG");
                    document.AddCreationDate();
                    document.AddCreator("Otto Haas KG");
                    document.AddTitle(Path.GetFileName (PDF_WithXML));
                    document.AddSubject(Path.GetFileName(PDF_WithXML)); 
                   
                    // Set output intent. PDF/A requirement.
                    string _rgb_color_profile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) 
                        + @"\resources\", "sRGB Color Space Profile.icm");
                    ICC_Profile icc = ICC_Profile.GetInstance(_rgb_color_profile); 

                    _new_pdf_writer.SetOutputIntents("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", icc);

                    // All fonts shall be embedded. PDF/A requirement.
                    FontSelector _font_selector = new FontSelector();

                    var fonts = FontFactory.GetFont("Helvetica.ttf", BaseFont.WINANSI, BaseFont.EMBEDDED, 10);
                    iTextSharp.text.Font normal9 = FontFactory.GetFont("FreeSans.ttf", BaseFont.WINANSI, BaseFont.EMBEDDED, 9);
                    iTextSharp.text.Font bold9 = FontFactory.GetFont("FreeSansBold.ttf", BaseFont.WINANSI, BaseFont.EMBEDDED, 9);
                    iTextSharp.text.Font normal8 = FontFactory.GetFont("FreeSans.ttf", BaseFont.WINANSI, BaseFont.EMBEDDED, 8);

                    _font_selector.AddFont(fonts);
                    _font_selector.AddFont(normal9);
                    _font_selector.AddFont(bold9);
                    _font_selector.AddFont(normal8);

                    // Creating PDF/A-3 compliant attachment.
                    PdfDictionary parameters = new PdfDictionary();
                    parameters.Put(PdfName.MODDATE, new PdfDate());
                    
                    PdfFileSpecification fileSpec = PdfFileSpecification.FileEmbedded(
                        _new_pdf_writer, XMLFile, _xml_filename, null, "application/xml", parameters, 0);
                    fileSpec.Put(new PdfName("AFRelationship"), new PdfName("Alternative"));
                    _new_pdf_writer.AddFileAttachment(_xml_filename, fileSpec);
                    PdfArray array = new PdfArray();
                    array.Add(fileSpec.Reference);                                        
                    _new_pdf_writer.ExtraCatalog.Put(new PdfName("AF"), array);
                      
                    // Add old PDF-pages
                    PdfContentByte _content_byte = _new_pdf_writer.DirectContent;

                    var _old_pdf_reader = new PdfReader(PDF_start);

                    for (var i = 1; i <= _old_pdf_reader.NumberOfPages; i++)
                    {
                        PdfImportedPage _page = _new_pdf_writer.GetImportedPage(_old_pdf_reader, i);
                        document.NewPage();
                        _content_byte.AddTemplate(_page, 0, 0);
                    }
                    
                    _new_pdf_writer.XmpWriter.SetProperty(PdfAXmpWriter.zugferdSchemaNS  , PdfAXmpWriter.zugferdDocumentFileName, "ZUGFeRD-invoice.xml");
                    _new_pdf_writer.XmpWriter.SetProperty(PdfAXmpWriter.zugferdSchemaNS, PdfAXmpWriter.zugferdDocumentType, "INVOICE");
                    _new_pdf_writer.XmpWriter.SetProperty(PdfAXmpWriter.zugferdSchemaNS, PdfAXmpWriter.zugferdConformanceLevel, "ZUGFeRD");

                    if (document.IsOpen())
                        document.Close();

                    //_new_pdf_writer.Close();
                    _old_pdf_reader.Close();
                }
            }
        }      

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void main_form_Load(object sender, EventArgs e)
        {

        }


        

    }
}
