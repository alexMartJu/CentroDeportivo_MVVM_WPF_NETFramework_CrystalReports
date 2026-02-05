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
using CentroDeportivo.ViewModel;

namespace CentroDeportivo.View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// Ventana principal de la aplicación que contiene el menú de navegación
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor que inicializa la ventana principal y configura el DataContext
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
