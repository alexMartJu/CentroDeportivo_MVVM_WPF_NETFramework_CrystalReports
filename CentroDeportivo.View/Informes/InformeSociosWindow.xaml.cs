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
using System.Windows.Shapes;
using CentroDeportivo.ViewModel.Informes;

namespace CentroDeportivo.View.Informes
{
    /// <summary>
    /// Lógica de interacción para InformeSociosWindow.xaml
    /// Ventana que muestra el informe maestro de socios
    /// </summary>
    public partial class InformeSociosWindow : Window
    {
        /// <summary>
        /// Constructor que inicializa la ventana del informe de socios y carga el reporte
        /// </summary>
        public InformeSociosWindow()
        {
            InitializeComponent();
            var vm = new InformeSociosViewModel();
            reportViewer.ViewerCore.ReportSource = vm.Informe;

        }
    }
}
