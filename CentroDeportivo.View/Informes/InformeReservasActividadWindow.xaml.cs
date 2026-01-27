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
    /// Lógica de interacción para InformeReservasActividadWindow.xaml
    /// </summary>
    public partial class InformeReservasActividadWindow : Window
    {
        public InformeReservasActividadWindow(int idActividad)
        {
            InitializeComponent();
            var vm = new InformeReservasActividadViewModel(idActividad); 
            reportViewer.ViewerCore.ReportSource = vm.Informe;
        }
    }
}
