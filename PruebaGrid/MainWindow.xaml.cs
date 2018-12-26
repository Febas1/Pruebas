using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Controls.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PruebaGrid
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection("Data Source=64.250.116.210,8333;Initial Catalog=Lecollezioni_emp010;User ID=wilmer1104@yahoo.com;Password=Q1w2e3r4*/*");
        SqlDataAdapter adapter = null;

        //GridControl defined here
        public MainWindow()
        {
            InitializeComponent();
            PrepararConexion();
        }

        private void CambioColor()
        {
            //for (int i = 0; i < 100; i++)
            //{

            //    for (int j = 0; j < 20; j++)
            //    {
            //        if (j == 0 || i == 0)
            //        {
            //            rutt.Model[i, j].CellValue = "";
            //        }
            //        else
            //        {
            //            if (Convert.ToInt32(rutt.Model[i,j].CellValue) == 0)
            //            {
            //                rutt.Model[i, j].Background = Brushes.Black;
            //                rutt.Model[i, j].Foreground = Brushes.White;
            //            }
            //            else if(Convert.ToInt32(rutt.Model[i, j].CellValue) <= 0)
            //            {
            //                rutt.Model[i, j].Foreground = Brushes.Red;
            //            }
            //            else
            //            {
            //                rutt.Model[i, j].Foreground = Brushes.Green;
            //            }
            //        }
            //    }
            //}
        }
        private void PrepararConexion()
        {
            ViewModel viewmodel = new ViewModel();
            this.rutt.GridValidationMode = GridValidationMode.InView;
            rutt.ItemsSource = viewmodel.Orders;
            //try
            //{
            //    con.Open();
            //    adapter = new SqlDataAdapter("select Ano_acu,cod_ref,cod_bod,Salu_00,Salc_00 from InIn_acum where Ano_acu=2018 and Salc_00 != 0", con);
            //    adapter.Fill(ds, "InIn_acum");
            //    rutt.ItemsSource = ds.Tables["InIn_acum"];
            //    con.Close();

            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Error en: " + e);
            //}
        }
    }
    public class OrderInfo : IDataErrorInfo
    {
        string customerId;
        string country;
        string customerName;
        string shippingCity;

        private int orderID;
        [Range(1001, 1005, ErrorMessage = "OrderID between 1001 and 1005 alone processed")]

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        [Display(AutoGenerateField = false)]

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {

                if (!columnName.Equals("Country"))
                    return string.Empty;

                if (this.Country.Contains("Germany") || this.Country.Contains("UK"))
                    return "Delivery not available for the country " + this.Country;

                return string.Empty;
            }
        }

        public string ShipCity
        {
            get { return shippingCity; }
            set { shippingCity = value; }
        }

        public OrderInfo(int orderId, string customerName, string country, string customerId, string shipCity)
        {
            this.OrderID = orderId;
            this.CustomerName = customerName;
            this.Country = country;
            this.CustomerID = customerId;
            this.ShipCity = shipCity;
        }
    }
    public class ViewModel
    {
        private ObservableCollection<OrderInfo> _orders;
        public ObservableCollection<OrderInfo> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public ViewModel()
        {
            _orders = new ObservableCollection<OrderInfo>();
            this.GenerateOrders();
        }

        private void GenerateOrders()
        {
            _orders.Add(new OrderInfo(1001, "Maria Anders", "Germany", "ALFKI", "Berlin"));
            _orders.Add(new OrderInfo(1002, "Ana Trujilo", "Mexico", "ANATR", "Mexico D.F."));
            _orders.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", "Mexico D.F."));
            _orders.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", "London"));
            _orders.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", "Lula"));
            _orders.Add(new OrderInfo(1006, "Hanna Moos", "Germany", "BLAUS", "Mannheim"));
            _orders.Add(new OrderInfo(1007, "Frederique Citeaux", "France", "BLONP", "Strasbourg"));
            _orders.Add(new OrderInfo(1008, "Martin Sommer", "Spain", "BOLID", "Madrid"));
            _orders.Add(new OrderInfo(1009, "Laurence Lebihan", "France", "BONAP", "Marseille"));
            _orders.Add(new OrderInfo(1010, "Elizabeth Lincoln", "Canada", "@BOTTM", "Tsawassen"));
        }
    }

}
