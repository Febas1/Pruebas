﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;

namespace XmlPrueba
{
    /// <summary>
    /// Lógica de interacción para Prueba.xaml
    /// </summary>
    public partial class Prueba : Window
    {
        public object iva = new object();
        public string ruta;
        public string second;
        DataTable dt = new DataTable();
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
            dt.Columns.Add("#");
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Valor unitario");
            dt.Columns.Add("Tipo impuesto");
            dt.Columns.Add("% Impuesto");
            dt.Columns.Add("Valor Total");
        }

        private void Llenar()
        {
            dt.Clear();
            int cont = 0, con1 = 0, con2 = 0, con3 = 0, con4 = 0, con9 = 0, conFina = 0;
            XElement xelement = XElement.Load(ruta);

            var unique = from el in xelement.Elements(fe + "InvoiceLine") select el;
            var sub = from el in xelement.Elements(fe + "TaxTotal").Elements().Elements() select el;
            var codigo = from el in xelement.Elements(fe + "InvoiceLine").Descendants(cbc + "ID") select el;
            var cantidad = from el in xelement.Elements(fe + "InvoiceLine").Elements(cbc + "InvoicedQuantity") select el;
            var description = from el in xelement.Elements(fe + "InvoiceLine").Elements(fe + "Item").Elements(cbc + "Description") select el;
            var valUnit = from el in xelement.Elements(fe + "InvoiceLine").Elements(fe + "Price").Elements(cbc + "PriceAmount") select el;
            var IVA = from el in xelement.Elements(fe + "TaxTotal").Elements(fe + "TaxSubtotal").Elements(cbc + "Percent") select el;
            var valTot = from el in xelement.Elements(fe + "InvoiceLine").Elements(cbc + "LineExtensionAmount") select el;
            var totalPago = xelement.Descendants(cbc + "PayableAmount");
            var totalDes = xelement.Descendants(cbc + "AllowanceTotalAmount").FirstOrDefault();
            var numFac = xelement.Descendants(cbc + "ID").FirstOrDefault();
            var fechFac = xelement.Descendants(cbc + "IssueDate").FirstOrDefault();

            foreach (var el in unique)
            {
                cont += 1;
            }
            object[] sharpArray = new object[cont];
            object[] codigoArray = new object[cont];
            object[] cantidadArrray = new object[cont];
            object[] descripcionArray = new object[cont];
            object[] valunitArray = new object[cont];
            object[] totArray = new object[cont];
            FacTXT.Text = numFac.Value;
            FechaTXT.Text = fechFac.Value;
            TDes.Text = totalDes.Value;
            foreach (var item in codigo)
            {
                if (Regex.Matches(item.Value, "_").Count == 1)
                {
                    Console.WriteLine("Sirve");
                }
                string[] textos = item.Value.Split('_');
                codigoArray[con1] = textos[1];
                sharpArray[con1] = textos[0];
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
            foreach (var item in IVA)
            {
                iva = item.Value;
            }
            foreach (var item in valTot)
            {
                totArray[con9] = item.Value;
                con9 += 1;
            }
            foreach (var item in unique)
            {
                dt.Rows.Add(sharpArray[conFina], codigoArray[conFina], cantidadArrray[conFina], descripcionArray[conFina], valunitArray[conFina], "IVA", iva, totArray[conFina]);
                conFina += 1;//En el xml no se encuentran datos del descuento por producto, solo el total de el descuento al final de la factura en pdf
            }
            foreach (var item in sub)
            {
                subList.Add(item.Value);
            }
            foreach (var item in totalPago)
            {
                TxtTotal.Text += (" " + item.Value);
            }
            DataProducto.AutoGenerateColumns = true;
            DataProducto.ItemsSource = dt.DefaultView;
            string sTotal = Convert.ToString(subList[0]);
            double STotal = Convert.ToDouble(sTotal);
            Sotal.Text = (sTotal);
            TIVA.Text = Convert.ToString(subList[1]);
            txtIva.Text += " " + iva + "%";

        }
        private void LeerXML()
        {
            if (ruta != null)
            {
                XElement xelement = XElement.Load(ruta);
                var proID = xelement.Elements(fe + "AccountingSupplierParty").Elements(fe + "Party").Elements().Elements(cbc + "Name");
                var proNIT = xelement.Elements(fe + "AccountingSupplierParty").Elements(fe + "Party").Elements().Elements(cbc + "ID");
                var proDir = xelement.Elements(fe + "AccountingSupplierParty").Elements(fe + "Party").Elements().Elements().Elements().Elements(cbc + "Line");
                var cliID = xelement.Elements(fe + "AccountingCustomerParty").Elements(fe + "Party").Elements().Elements(cbc + "Name");
                var cliNIT = xelement.Elements(fe + "AccountingCustomerParty").Elements(fe + "Party").Elements().Elements(cbc + "ID");
                var cliDir = xelement.Elements(fe + "AccountingCustomerParty").Elements(fe + "Party").Elements().Elements().Elements().Elements(cbc + "Line");
                var city = xelement.Descendants(cbc + "CityName");
                var dire = xelement.Descendants(cbc + "Line");
                var tele = xelement.Descendants(cbc + "Telephone");
                var unique = from el in xelement.Elements(fe + "InvoiceLine") select el;
                var priNod = from el in xelement.Elements(fe + "InvoiceLine").Elements() select el;
                var producto = from el in xelement.Elements(fe + "InvoiceLine").Elements().Elements() select el;
                var total = from el in xelement.Elements(fe + "InvoiceLine").Elements().Elements().Elements() select el;
                List<object> idList = new List<object>();
                List<object> cityList = new List<object>();
                foreach (var item in proID)//Nombre proveedor
                {
                    NombreTXT.Text = item.Value;
                }
                foreach (var item in proNIT)
                {
                    NITTXT.Text = item.Value;
                }
                foreach (var item in proDir)
                {
                    DirTXT.Text = item.Value;
                }
                foreach (var item in cliID)
                {
                    NombreTXT2.Text = item.Value;
                }
                foreach (var item in cliNIT)
                {
                    NITTXT2.Text = item.Value;
                }
                foreach (var item in cliDir)
                {
                    DirTXT2.Text = item.Value;
                }
                foreach (XElement el in producto)
                {
                    produList.Add(el.Value);
                }
                foreach (var item in total)
                {
                    totaList.Add(item.Value);
                }
                foreach (var item in city)
                {
                    cityList.Add(item.Value);
                }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BuscarArchivo();
        }



        private void BTNdocumento_Click(object sender, RoutedEventArgs e)
        {
            string validarNumNit = "select * from ";
        }
    }
}


