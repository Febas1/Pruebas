using Syncfusion.Windows.Controls.Grid;
using System;
using System.Collections.Generic;
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

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ScrollViewer defined here
            ScrollViewer ScrollViewer = new ScrollViewer();

            //GridControl defined here
            GridControl gridControl = new GridControl();

            //GridControl set as the content of the ScrollViewer
            ScrollViewer.Content = gridControl;

            //To bring the Grid control to the view, ScrollViewer should be set as a child of LayoutRoot      
            this.layoutRoot.Children.Add(ScrollViewer);
            //Specifying row and column count
            gridControl.Model.RowCount = 100;
            gridControl.Model.ColumnCount = 20;

            //Looping through the cells and assigning the values based on row and column index
            for (int i = 0; i < 100; i++)
            {

                for (int j = 0; j < 20; j++)
                {
                    gridControl.Model[i, j].CellValue = string.Format("{0}/{1}", i, j);
                }
            }


            
        }
    }
}
