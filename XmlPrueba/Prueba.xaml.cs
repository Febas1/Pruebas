using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace XmlPrueba
{
    /// <summary>
    /// Lógica de interacción para Prueba.xaml
    /// </summary>
    public partial class Prueba : Window
    {
        public string ruta;
        public string second;
        DataTable dt = new DataTable();
        List<object> priList = new List<object>();
        List<object> totaList = new List<object>();
        List<object> produList = new List<object>();
        List<object> legaList = new List<object>();
        List<object> subList = new List<object>();
        List<object> uniqueList = new List<object>();
        XNamespace fe = "http://www.dian.gov.co/contratos/facturaelectronica/v1";
        XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
        XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        public Prueba()
        {
            InitializeComponent();
            BuscarArchivo();
        }

        private void Llenar()
        {
            int pri = 0;//11
            int prod = 0;//5
            int tot = 0;//6
            int cont = 0, con1 = 0, con2 = 0, con3 = 0, con4 = 0, conFina = 0;
            XElement xelement = XElement.Load(ruta);

            var unique = from el in xelement.Elements(fe + "InvoiceLine") select el;
            var sub = from el in xelement.Elements(fe + "TaxTotal").Elements().Elements() select el;
            var codigo = from el in xelement.Elements(fe + "InvoiceLine").Descendants(cbc + "ID") select el;
            var cantidad = from el in xelement.Elements(fe + "InvoiceLine").Elements(cbc + "InvoicedQuantity") select el;
            var description = from el in xelement.Elements(fe + "InvoiceLine").Elements(fe + "Item").Elements(cbc + "Description") select el;
            var valUnit = from el in xelement.Elements(fe + "InvoiceLine").Elements(fe + "Price").Elements(cbc + "PriceAmount") select el;
            dt.Columns.Add("#");
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Valor unitario");
            dt.Columns.Add("Tipo impuesto");
            dt.Columns.Add("% Impuesto");
            dt.Columns.Add("Impuesto");
            dt.Columns.Add("Descuento");
            dt.Columns.Add("Valor Total");
            foreach (var el in unique)
            {
                cont += 1;
            }
            object[] codigoArray = new object[cont];
            object[] cantidadArrray = new object[cont];
            object[] descripcionArray = new object[cont];
            object[] valunitArray = new object[cont];
            foreach (var item in codigo)
            {
                codigoArray[con1] = item.Value;
                con1 += 1;
            }
            foreach (var item in cantidad)
            {
                cantidadArrray[con2] = item.Value;
                con2 += 1;
            }
            foreach (var item in description)
            {
                descripcionArray[con3] = item.Value;
                con3 += 1;
            }
            foreach (var item in valUnit)
            {
                valunitArray[con4] = item.Value;
                con4 += 1;
            }
            foreach (var item in unique)
            {
                dt.Rows.Add("sos", codigoArray[conFina], cantidadArrray[conFina], descripcionArray[conFina], valunitArray[conFina]);
                conFina += 1;
            }
            foreach (var item in sub)
            {
                subList.Add(item.Value);
            }
            //dt.Rows.Add(priList[(0 + pri)], produList[(12 + val)], priList[(2 + pri)], produList[(10 + val)], produList[(13 + val)], "IVA", totaList[(3 + tot)], totaList[(2 + tot)], produList[(6 + val)], priList[(3 + pri)]);
            DataProducto.AutoGenerateColumns = true;
            DataProducto.ItemsSource = dt.DefaultView;
            TBImpo.Text = Convert.ToString(subList[0]);
            TIVA.Text = Convert.ToString(subList[1]);
            txtIva.Text += " " + Convert.ToString(subList[2]) + "%";
        }
        private void LeerXML()
        {
            if (ruta != null)
            {
                int rNameCont = 0;
                XElement xelement = XElement.Load(ruta);
                var id = xelement.Descendants(cbc + "ID");
                var city = xelement.Descendants(cbc + "CityName");
                var dire = xelement.Descendants(cbc + "Line");
                var tele = xelement.Descendants(cbc + "Telephone");
                var rName = xelement.Descendants(cbc + "RegistrationName");
                var fiName = xelement.Descendants(cbc + "FirstName");
                var faName = xelement.Descendants(cbc + "FamilyName");
                var proID = xelement.Elements(cbc + "ID");
                var unique = from el in xelement.Elements(fe + "InvoiceLine") select el;
                var priNod = from el in xelement.Elements(fe + "InvoiceLine").Elements() select el;
                var producto = from el in xelement.Elements(fe + "InvoiceLine").Elements().Elements() select el;
                var total = from el in xelement.Elements(fe + "InvoiceLine").Elements().Elements().Elements() select el;
                var legal = from el in xelement.Elements(fe + "LegalMonetaryTotal").Elements() select el;
                List<object> idList = new List<object>();
                List<object> cityList = new List<object>();
                List<object> direList = new List<object>();
                List<object> teleList = new List<object>();
                List<object> rNameList = new List<object>();
                List<object> fiNameList = new List<object>();
                List<object> faNameList = new List<object>();
                foreach (var item in legal)
                {
                    Console.WriteLine("legal :" + item.Value);
                    legaList.Add(item.Value);
                }
                foreach (var item in priNod)
                {
                    Console.WriteLine("Primario :" + item.Value);
                    priList.Add(item.Value);
                }
                foreach (XElement el in producto)
                {
                    Console.WriteLine("Valor :" + el.Value);
                    produList.Add(el.Value);
                }
                foreach (var item in total)
                {
                    Console.WriteLine("total :" + item.Value);
                    totaList.Add(item.Value);
                }
                foreach (var item in id)
                {
                    idList.Add(item.Value);
                }
                foreach (var item in city)
                {
                    cityList.Add(item.Value);
                }
                foreach (var item in dire)
                {
                    direList.Add(item.Value);
                }
                foreach (var item in tele)
                {
                    teleList.Add(item.Value);
                }
                foreach (var item in rName)
                {
                    rNameList.Add(item.Value);
                    rNameCont += 1;
                }
                foreach (var item in fiName)
                {
                    fiNameList.Add(item.Value);
                }
                foreach (var item in faName)
                {
                    faNameList.Add(item.Value);
                }
                NITTXT.Text = Convert.ToString(idList[2]);
                NITTXT2.Text = Convert.ToString(idList[3]);
                if (rNameCont == 0)
                {
                    if (Convert.ToString(faNameList[0]) != "")
                    {
                        NombreTXT.Text = Convert.ToString(fiNameList[0] + " " + faNameList[0]);
                    }
                    else
                    {
                        NombreTXT.Text = Convert.ToString(fiNameList[0]);
                    }
                }
                else
                {
                    NombreTXT.Text = Convert.ToString(rNameList[0]);
                }
                //if (rNameCont == 1)
                //{
                //    if (Convert.ToString(faNameList[0]) != "")
                //    {
                //        NombreTXT2.Text = Convert.ToString(fiNameList[0] + " " + faNameList[0]);
                //    }
                //    else
                //    {
                //        NombreTXT2.Text = Convert.ToString(fiNameList[0]);
                //    }
                //}
                //else
                //{
                //    NombreTXT2.Text = Convert.ToString(rNameList[1]);
                //}
                DirTXT.Text = (Convert.ToString(direList[0] + " - " + cityList[0]));
                DirTXT2.Text = (Convert.ToString(direList[1] + " - " + cityList[1]));
                //TelTXT.Text = (Convert.ToString(teleList[0]));
                //TelTXT2.Text = (Convert.ToString(teleList[1]));
                Llenar();

            }
            else
            {
                App.Current.MainWindow.Close();
            }
        }

        private void BuscarArchivo()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "XML Files (*.xml)|*.xml"
            };
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                ruta = filename;
            }
            LeerXML();
        }
    }
}
