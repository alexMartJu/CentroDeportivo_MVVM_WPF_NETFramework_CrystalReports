using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CentroDeportivo.ViewModel.Views;

namespace CentroDeportivo.ViewModel
{
    //ViewModel de la ventana principal, se encarga de la navegación entre vistas
    public class MainWindowViewModel : BaseViewModel
    {
        //Propiedad que contiene la vista actual
        private BaseViewModel _vistaActual;
        public BaseViewModel VistaActual
        {
            get => _vistaActual;
            set
            {
                _vistaActual = value;
                OnPropertyChanged(nameof(VistaActual));
                ActualizarTitulo();
            }
        }

        //Propiedad del título de la ventana
        private string _titulo;
        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                OnPropertyChanged(nameof(Titulo));
            }
        }

        //Commands para cambiar de vista
        public ICommand MostrarSociosCommand { get; }
        public ICommand MostrarActividadesCommand { get; }
        public ICommand MostrarReservasCommand { get; }

        public MainWindowViewModel()
        {
            //Vista por defecto: socios y título inicial
            VistaActual = new SociosViewModel();
            Titulo = "Gestión de Socios";

            //Comandos para cambiar de vista
            MostrarSociosCommand = new RelayCommand(_ => VistaActual = new SociosViewModel());
            MostrarActividadesCommand = new RelayCommand(_ => VistaActual = new ActividadesViewModel());
            MostrarReservasCommand = new RelayCommand(_ => VistaActual = new ReservasViewModel());
        }

        //ActualizarTitulo() --> Método que actualiza el título según la vista actual
        private void ActualizarTitulo()
        {
            if (VistaActual is SociosViewModel)
            {
                Titulo = "Gestión de Socios";
            }
            else if (VistaActual is ActividadesViewModel)
            {
                Titulo = "Gestión de Actividades";
            }
            else if (VistaActual is ReservasViewModel)
            {
                Titulo = "Gestión de Reservas";
            }
        }
    }
}
