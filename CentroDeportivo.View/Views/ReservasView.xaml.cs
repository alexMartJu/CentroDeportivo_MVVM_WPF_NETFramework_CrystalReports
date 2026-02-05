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
using CentroDeportivo.ViewModel.Views;

namespace CentroDeportivo.View.Views
{
    /// <summary>
    /// Lógica de interacción para ReservasView.xaml
    /// Vista que muestra la gestión de reservas
    /// </summary>
    public partial class ReservasView : UserControl
    {
        /// <summary>
        /// Constructor que inicializa la vista de reservas y configura su ViewModel
        /// </summary>
        public ReservasView()
        {
            InitializeComponent();
            DataContext = new ReservasViewModel();
        }
    }
}
