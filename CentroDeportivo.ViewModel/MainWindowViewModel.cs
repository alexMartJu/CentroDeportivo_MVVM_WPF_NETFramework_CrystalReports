using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CentroDeportivo.ViewModel.Services;
using CentroDeportivo.ViewModel.Views;

namespace CentroDeportivo.ViewModel
{
    /// <summary>
    /// ViewModel de la ventana principal, se encarga de la navegación entre vistas
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        //Propiedad que contiene la vista actual
        private BaseViewModel _vistaActual;
        
        /// <summary>
        /// Vista actual que se muestra en el contenedor principal
        /// </summary>
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
        
        /// <summary>
        /// Título dinámico de la ventana que cambia según la vista actual
        /// </summary>
        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                OnPropertyChanged(nameof(Titulo));
            }
        }

        /// <summary>
        /// Comando para mostrar la vista de gestión de socios
        /// </summary>
        public ICommand MostrarSociosCommand { get; }
        
        /// <summary>
        /// Comando para mostrar la vista de gestión de actividades
        /// </summary>
        public ICommand MostrarActividadesCommand { get; }
        
        /// <summary>
        /// Comando para mostrar la vista de gestión de reservas
        /// </summary>
        public ICommand MostrarReservasCommand { get; }
        
        /// <summary>
        /// Comando para mostrar la vista de informes
        /// </summary>
        public ICommand MostrarInformesCommand { get; }

        /// <summary>
        /// Constructor que inicializa la vista por defecto y los comandos de navegación
        /// </summary>
        public MainWindowViewModel()
        {
            //Vista por defecto: socios y título inicial
            VistaActual = new SociosViewModel();
            Titulo = "Gestión de Socios";

            //Comandos para cambiar de vista
            MostrarSociosCommand = new RelayCommand(_ => VistaActual = new SociosViewModel());
            MostrarActividadesCommand = new RelayCommand(_ => VistaActual = new ActividadesViewModel());
            MostrarReservasCommand = new RelayCommand(_ => VistaActual = new ReservasViewModel());
            MostrarInformesCommand = new RelayCommand(_ => VistaActual = new InformesViewModel(null));
        }

        /// <summary>
        /// ActualizarTitulo() --> Método que actualiza el título según la vista actual
        /// </summary>
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
            else if (VistaActual is InformesViewModel) 
            { 
                Titulo = "Informes del Centro Deportivo"; 
            }
        }
    }
}
