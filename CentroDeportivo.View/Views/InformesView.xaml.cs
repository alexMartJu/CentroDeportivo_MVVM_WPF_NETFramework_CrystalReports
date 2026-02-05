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
using CentroDeportivo.View.Services;
using CentroDeportivo.ViewModel.Views;

namespace CentroDeportivo.View.Views
{
    /// <summary>
    /// Lógica de interacción para InformesView.xaml
    /// Vista que muestra la selección y generación de informes
    /// </summary>
    public partial class InformesView : UserControl
    {
        /// <summary>
        /// Constructor que inicializa la vista de informes y configura su ViewModel
        /// </summary>
        public InformesView()
        {
            InitializeComponent();
            DataContext = new InformesViewModel(new WindowService());
        }
    }
}
