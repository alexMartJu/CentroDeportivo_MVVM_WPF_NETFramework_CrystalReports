using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.ViewModel.Views
{
    /// <summary>
    /// ViewModel que controla la generación de informes en la aplicación.
    /// </summary>
    public class InformesViewModel : BaseViewModel
    {
        //Servicio que se encarga de hablar con la base de datos.
        private readonly IActividadService _actividadService;
        private readonly IWindowService _windowService;

        /// <summary>
        /// Lista de actividades que se muestra en el ComboBox.
        /// </summary>
        public ObservableCollection<Actividades> Actividades { get; set; }

        //Actividad que el usuario selecciona para generar el informe de reservas.
        private Actividades _actividadSeleccionada;
        
        /// <summary>
        /// Actividad seleccionada para el informe de reservas por actividad
        /// </summary>
        public Actividades ActividadSeleccionada
        {
            get => _actividadSeleccionada;
            set
            {
                _actividadSeleccionada = value;
                OnPropertyChanged(nameof(ActividadSeleccionada));
            }
        }

        /// <summary>
        /// Comando para generar el informe de socios
        /// </summary>
        public ICommand InformeSociosCommand { get; }
        
        /// <summary>
        /// Comando para generar el informe de reservas por actividad
        /// </summary>
        public ICommand InformeReservasActividadCommand { get; }
        
        /// <summary>
        /// Comando para generar el informe del historial de reservas
        /// </summary>
        public ICommand InformeHistorialCommand { get; }

        /// <summary>
        /// Constructor que inicializa los servicios, carga las actividades y configura los comandos de informes.
        /// </summary>
        /// <param name="windowService">Servicio para mostrar ventanas de informes</param>
        public InformesViewModel(IWindowService windowService)
        {
            _windowService = windowService;

            _actividadService = new ActividadService();
            Actividades = new ObservableCollection<Actividades>(_actividadService.GetAll());

            InformeSociosCommand = new RelayCommand(_ => _windowService.MostrarInformeSocios());
            InformeReservasActividadCommand = new RelayCommand(
                _ => _windowService.MostrarInformeReservasActividad(ActividadSeleccionada.IdActividad),
                _ => ActividadSeleccionada != null);
            InformeHistorialCommand = new RelayCommand(_ => _windowService.MostrarInformeHistorial());
        }
    }
}