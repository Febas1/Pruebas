using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menuses
{
    /// <summary>
    /// Lógica de interacción para Reclasificar.xaml
    /// </summary>
    public partial class Busqueda :Window
    {

    }
    public partial class Reclasificar : UserControl
    {
        public Reclasificar()
        {
            InitializeComponent();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked ?? false)
            {
                GCuenta.IsEnabled = true;
            }
            else
            {
                GCuenta.IsEnabled = false;
            }
        }

        private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked ?? false)
            {
                GCiudad.IsEnabled = true;
            }
            else
            {
                GCiudad.IsEnabled = false;
            }
        }

        private void ToggleButton_Checked_2(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked ?? false)
            {
                GSucursal.IsEnabled = true;
            }
            else
            {
                GSucursal.IsEnabled = false;
            }
        }

        private void ToggleButton_Checked_3(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked ?? false)
            {
                GCosto.IsEnabled = true;
            }
            else
            {
                GCosto.IsEnabled = false;
            }
        }

        private void ToggleButton_Checked_4(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked ?? false)
            {
                GTercero.IsEnabled = true;
            }
            else
            {
                GTercero.IsEnabled = false;
            }
        }

        private void ToggleButton_Checked_5(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked ?? false)
            {
                GDocu.IsEnabled = true;
            }
            else
            {
                GDocu.IsEnabled = false;
            }
        }

        private void TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Busqueda a = new Busqueda();
            //new Busqueda().Show();
           
           

        }
    }
}
