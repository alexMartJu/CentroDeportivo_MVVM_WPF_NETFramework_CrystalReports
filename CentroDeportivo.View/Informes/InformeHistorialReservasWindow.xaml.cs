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
    /// Lógica de interacción para InformeHistorialReservasWindow.xaml
    /// Ventana que muestra el informe del historial completo de reservas
    /// </summary>
    public partial class InformeHistorialReservasWindow : Window
    {
        /// <summary>
        /// Constructor que inicializa la ventana del informe del historial de reservas y carga el reporte
        /// </summary>
        public InformeHistorialReservasWindow()
        {
            InitializeComponent();
            var vm = new InformeHistorialViewModel();
            reportViewer.ViewerCore.ReportSource = vm.Informe;
        }
    }
}
