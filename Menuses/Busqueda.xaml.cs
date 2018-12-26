using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Menuses
{
    /// <summary>
    /// Lógica de interacción para Busqueda.xaml
    /// </summary>
    public partial class Busqueda : Window
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adapter = null;
        public string conexion = @"Data Source=64.250.116.210,8333;Initial Catalog=GrupoSaavedra_Emp010;User ID=wilmer1104@yahoo.com;Password=Q1w2e3r4*/*";
        public string cadena = "select cod_cta, nom_cta from Comae_cta";
        public Busqueda()
        {
            InitializeComponent();

            con = new SqlConnection(conexion);
            con.Open();
            adapter = new SqlDataAdapter(cadena, con);
            adapter.Fill(ds, "Comae_cta");
            Daticos.ItemsSource = ds.Tables["Comae_cta"];
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Daticos.SearchHelper.AllowFiltering = true;
            this.Daticos.SearchHelper.Search(Bus.Text);
        }

        private void Daticos_CellDoubleTapped(object sender, Syncfusion.UI.Xaml.Grid.GridCellDoubleTappedEventArgs e)
        {
            
        }
    }
    static class Variables
    {
        public static string CodMae;
    }

}
