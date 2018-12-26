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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Copiar_Click(object sender, RoutedEventArgs e)
        {
            Contenido.Children.Clear();
            Contenido.Children.Add(new Copiar());
        }
        private void Reclasificar_Click(object sender, RoutedEventArgs e)
        {
            Contenido.Children.Clear();
            Contenido.Children.Add(new Reclasificar());
        }

    }
}
