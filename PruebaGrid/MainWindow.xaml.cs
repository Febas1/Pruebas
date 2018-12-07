using Syncfusion.Windows.Controls.Grid;
using System;
using System.Collections.Generic;
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

        //GridControl defined here
        public MainWindow()
        {
            InitializeComponent();
            PrepararConexion();
        }

        private void CambioColor()
        {
            for (int i = 0; i < 100; i++)
            {

                for (int j = 0; j < 20; j++)
                {
                    if (j == 0 || i == 0)
                    {
                        rutt.Model[i, j].CellValue = "";
                    }
                    else
                    {
                        if (Convert.ToInt32(rutt.Model[i,j].CellValue) == 0)
                        {
                            rutt.Model[i, j].Background = Brushes.Black;
                            rutt.Model[i, j].Foreground = Brushes.White;
                        }
                        else if(Convert.ToInt32(rutt.Model[i, j].CellValue) <= 0)
                        {
                            rutt.Model[i, j].Foreground = Brushes.Red;
                        }
                        else
                        {
                            rutt.Model[i, j].Foreground = Brushes.Green;
                        }
                    }
                }
            }
        }
        private void PrepararConexion()
        {
            SqlConnection con = new SqlConnection("Data Source=64.250.116.210,8333;Initial Catalog=Lecollezioni_emp010;User ID=wilmer1104@yahoo.com;Password=Q1w2e3r4*/*");
            //SqlDataReader rdr = null;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Ano_acu,cod_ref,cod_bod,Salu_00,Salc_00 from InIn_acum where Ano_acu=2018 and Salc_00 != 0", con);
                //cmd.Parameters.Add("Ano_acu").value;
                //rdr = cmd.ExecuteReader();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("InIn_acum");
                sda.Fill(dt);
                //rutt.ItemsSource = dt.DefaultView;




                //rdr.Close();
                con.Close();
                rutt.ItemsSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en: "+ e);
            }
        }
    }

}
