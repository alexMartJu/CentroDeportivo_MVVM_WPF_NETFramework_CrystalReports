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
    //ViewModel que controla la generación de informes en la aplicación.
    public class InformesViewModel : BaseViewModel
    {
        //Servicio que se encarga de hablar con la base de datos.
        private readonly IActividadService _actividadService;
        private readonly IWindowService _windowService;

        //Lista de actividades que se muestra en el ComboBox.
        public ObservableCollection<Actividades> Actividades { get; set; }

        //Actividad que el usuario selecciona para generar el informe de reservas.
        private Actividades _actividadSeleccionada;
        public Actividades ActividadSeleccionada
        {
            get => _actividadSeleccionada;
            set
            {
                _actividadSeleccionada = value;
                OnPropertyChanged(nameof(ActividadSeleccionada));
            }
        }

        //Commands para generar los informes.
        public ICommand InformeSociosCommand { get; }
        public ICommand InformeReservasActividadCommand { get; }
        public ICommand InformeHistorialCommand { get; }

        //Constructor que inicializa los servicios y carga las actividades.
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