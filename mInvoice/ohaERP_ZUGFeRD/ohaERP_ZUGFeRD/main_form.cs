using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml.xmp;
using s2industries.ZUGFeRD;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
//using System.Windows.Forms;

namespace ZUGFeRD_Test
{
    public class main_form //: Form
    {
        private string m_input_pdf_file;
        private string m_output_xml_file_name = @"ZUGFeRD-invoice.xml";
        private SqlConnection m_SqlConnection = new SqlConnection(
            "Data Source=v-srv-sql.haas.de;Initial Catalog=ohaERP;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=60");

        public main_form()
        {
            //InitializeComponent();
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
           , int InvoiceHeadersysid
             , string Invoice_no
            , DateTime Order_date
            , string CurrencyShortmark
            , string CurrencyShortmark_client
            , string Customer_name
            , string Customers_zip
            , string Customers_city
            , string Customers_street
            , string Customer_no
            , string Tax_number

            , string Clientname
            , string Clients_zip
            , string Clients_city
            , string Clients_street 
           )
        {
            

            string _output_file_directory = Path.GetDirectoryName(NonZUGFeRD_PDF);
            string _output_xml_file_path = Path.Combine(_output_file_directory, m_output_xml_file_name);
            string _output_pdf_file = null;

           
                // Generate XML                
                //generateXMLFromInvoiceInfo(
                //    _output_xml_file_path
                //    , Clientsysid
                //    , Languagesysid
                //    , InvoiceHeadersysid
                //    ,  Invoice_no
                //    ,  Order_date
                //    ,  CurrencyShortmark
                //    ,  CurrencyShortmark_client
                //    ,  Customer_name
                //    ,  Customers_zip
                //    ,  Customers_city
                //    ,  Customers_street
                //    ,  Customer_no
                //    ,  Tax_number

                //    ,  Clientname
                //    ,  Clients_zip
                //    ,  Clients_city
                //    ,  Clients_street 
                    
                //    );

                //_output_pdf_file = Path.Combine(_output_file_directory, dataSet11.rp_invoice_details[0].invoice_no + "_zugferd.pdf");

                ConvertRegularToConformantPDF_3A(
                      _output_pdf_file
                    , NonZUGFeRD_PDF
                    , _output_xml_file_path);

                return _output_pdf_file;
           }

      
        private void generateXMLFromInvoiceInfo(
            string OutputXML
            , int Clientsysid
            , int Languagesysid
            , int InvoiceHeadersysid
             , string Invoice_no
            , DateTime Order_date
            , string CurrencyShortmark
            , string CurrencyShortmark_client
            , string Customer_name
            , string Customers_zip
            , string Customers_city
            , string Customers_street
            , string Customer_no
            , string Tax_number

            , string Clientname
            , string Clients_zip
            , string Clients_city
            , string Clients_street
            , string Clients_ustd_id
            )
        {
            //InvoiceDescriptor desc = _createInvoice(
            //    Clientsysid
            //    , Languagesysid
            //    , InvoiceHeadersysid
            //     ,  Invoice_no
            //, Order_date
            //,  CurrencyShortmark
            //,  CurrencyShortmark_client
            //,  Customer_name
            //,  Customers_zip
            //,  Customers_city
            //,  Customers_street
            //,  Customer_no
            //,  Tax_number

            //,  Clientname
            //,  Clients_zip
            //,  Clients_city
            //,  Clients_street 
            //,  Clients_ustd_id
                
            //    );
            //desc.Save(OutputXML);
        }

        private InvoiceDescriptor _createInvoice(
            int client_id
            , int invoice_header_id
            , string Invoice_no
            , DateTime Order_date
            , string CurrencyShortmark
            , string CurrencyShortmark_client
            , string Customer_name
            , string Customers_zip
            , string Customers_city
            , string Customers_street
            , string Customer_no
            , string Tax_number
            , string Clientname
            , string Clients_zip
            , string Clients_city
            , string Clients_street 
            , string Clients_ustd_id
            , DateTime delivery_date
            , decimal valueofgoods
            , decimal valueofgoods_without_discount
            , decimal freightcosts
            , decimal subtotal
            , decimal taxtotalAmount
            , decimal total
            , decimal taxPercent
            , string paymentterms_description
            , DateTime dueDate
            , string iban_kreditor
            , string bic_kreditor
            , string account_number_kreditor
            , string bank_code_kreditor
            , string bankName_kreditor
            )
        {
            DataSet1.rp_invoice_detailsDataTable _rp_invoice_detailsDataTable = 
                new DataSet1.rp_invoice_detailsDataTable();
            DataSet1TableAdapters.rp_invoice_detailsTableAdapter _rp_invoice_detailsTableAdapter =
                new DataSet1TableAdapters.rp_invoice_detailsTableAdapter();

            _rp_invoice_detailsTableAdapter.Fill(
                _rp_invoice_detailsDataTable,
                client_id,
                invoice_header_id);
 
            CurrencyCodes _currenciesdescription = 
                (CurrencyCodes)System.Enum.Parse(typeof(CurrencyCodes), CurrencyShortmark);
            CountryCodes _invoiceadress_nationdescriptionshortmark =
                (CountryCodes)System.Enum.Parse(typeof(CountryCodes), CurrencyShortmark);
            CurrencyCodes _currenciesdescription_client =
               (CurrencyCodes)System.Enum.Parse(typeof(CurrencyCodes), CurrencyShortmark_client);                       

            /*
                string invoiceNo, 
                DateTime invoiceDate, 
                CurrencyCodes currency, 
                string invoiceNoAsReference = ""
             */
            InvoiceDescriptor desc = InvoiceDescriptor.CreateInvoice(
                Invoice_no 
                , Order_date
                , _currenciesdescription
                //, "GE2020211-471102"
                );
            desc.Profile = Profile.Comfort;
            desc.ReferenceOrderNo = Invoice_no;
            desc.AddNote(Invoice_no);
            desc.SetBuyer(
                  Customer_name 
                , Customers_zip 
                , Customers_city 
                , Customers_street 
                , ""  // street-no.
                , _invoiceadress_nationdescriptionshortmark
                , Customer_no 
                //, "0088"
                //, "4000001987658"
                );

            if (!string.IsNullOrEmpty(Tax_number))
                desc.AddBuyerTaxRegistration(Tax_number, TaxRegistrationSchemeID.VA);

            //if (!_invoice_details_row.Iscustomer_contacts_descriptionNull())
            //    desc.SetBuyerContact(_invoice_details_row.customer_contacts_description);

            // Kaefers Bestellbeleg
            //if (!_invoice_details_row.IsregardingNull())
            //    desc.SetBuyerOrderReferenceDocument(_invoice_details_row.regarding, _invoice_details_row.orderdate);

            // Verkaeufer 
            desc.SetSeller(
                Clientname
                , Clients_zip
                , Clients_city
                , Clients_street 
                , ""
                , CountryCodes.DE
                , ""
                //, "0088"
                //, "4000001123452"
                );
            desc.AddSellerTaxRegistration(Tax_number, TaxRegistrationSchemeID.FC);
            desc.AddSellerTaxRegistration(Clients_ustd_id , TaxRegistrationSchemeID.VA);

            // Lieferschein
            //desc.SetDeliveryNoteReferenceDocument(_invoice_details_row.bill_of_delivery_number, _invoice_details_row.bill_of_delivery_date);
            desc.ActualDeliveryDate = delivery_date ;

            // value of goods - Warenwert
            decimal _rabatt = valueofgoods - valueofgoods_without_discount;

            // 1. "lineTotalAmount"        > Gesamtbetrag der Positionen (Warenwert)
            // 2. "chargeTotalAmount"      > Gesamtbetrag der Zuschläge (Versandkosten)
            // 3. "allowanceTotalAmount"   > Gesamtbetrag der Abschläge (Rabatt)
            // 4. "taxBasisAmount"         > Basisbetrag der Steuerberechnung (Zwischensumme)
            // 5. "taxTotalAmount"         > Steuergesamtbetrag (MwSt.)
            // 6. "grandTotalAmount"       > Bruttosumme (Endsumme)
            // 7. "totalPrepaidAmount"     > Anzahlungsbetrag (immer 0) 
            // 8. "duePayableAmount"       > Zahlbetrag (Endsumme)
            desc.SetTotals(
                  valueofgoods
                , freightcosts
                , _rabatt
                , subtotal
                , taxtotalAmount
                , total
                , 0
                , total
                );

            // verwendbare Gewerbesteuer
            // desc.AddApplicableTradeTax(129.37m, 7m, TaxTypes.VAT, TaxCategoryCodes.S);

            // Logistikbedingungen
            desc.AddLogisticsServiceCharge(freightcosts, "Versandkosten", TaxTypes.VAT, TaxCategoryCodes.S, taxPercent);

            // Handelsrabatt

            if (_rabatt != 0)
                desc.AddTradeAllowanceCharge(true, _rabatt, 
                    _currenciesdescription, _rabatt, "Sondernachlass", 
                    TaxTypes.VAT, TaxCategoryCodes.S, 
                    taxPercent);

            // Zahlungsbedingungen
            desc.SetTradePaymentTerms(paymentterms_description, dueDate);

            // Zahlungsmittel
            desc.setPaymentMeans("", paymentterms_description);

            // Finanzkonto des Kreditors (HAAS, ohaSys)
            desc.addCreditorFinancialAccount(
                iban_kreditor  // "DE08700901001234567890"
                , bic_kreditor //"GENODEF1M04"
                , account_number_kreditor
                , bank_code_kreditor
                , bankName_kreditor);

            // Positionen
            foreach (var _position in _rp_invoice_detailsDataTable)
            {
                QuantityCodes _QuantityCodes = 
                    (QuantityCodes)System.Enum.Parse(typeof(QuantityCodes)
                    , _position.quantity_units_code );

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
                desc.addTradeLineItem(_position.Articles_article_no 
                                      , _position.Articles_description 
                                      , _QuantityCodes
                                      , _position.Invoice_details_quantity 
                                      , _position.Invoice_details_price_netto
                                      , _position.Invoice_details_price_netto
                                      , _position.Invoice_details_quantity
                                      , TaxTypes.VAT
                                      , TaxCategoryCodes.S
                                      , taxPercent
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

        //private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    Properties.Settings.Default.Save();
        //}

        private void main_form_Load(object sender, EventArgs e)
        {

        }


        

    }
}
