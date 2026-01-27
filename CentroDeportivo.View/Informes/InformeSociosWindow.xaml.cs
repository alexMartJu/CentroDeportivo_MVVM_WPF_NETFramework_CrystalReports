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
    /// </summary>
    public partial class InformeSociosWindow : Window
    {
        public InformeSociosWindow()
        {
            InitializeComponent();
            var vm = new InformeSociosViewModel();
            reportViewer.ViewerCore.ReportSource = vm.Informe;

        }
    }
}
