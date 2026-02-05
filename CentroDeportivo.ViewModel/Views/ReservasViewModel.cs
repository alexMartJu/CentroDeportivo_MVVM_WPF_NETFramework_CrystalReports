using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.ViewModel.Views
{
    /// <summary>
    /// ViewModel que controla toda la parte de reservas en la aplicación.
    /// </summary>
    public class ReservasViewModel : BaseViewModel
    {
        //Servicios que se encargan de hablar con la base de datos.
        private readonly IReservaService _reservaService;
        private readonly ISocioService _socioService;
        private readonly IActividadService _actividadService;

        /// <summary>
        /// Lista de reservas que se muestra en el DataGrid.
        /// </summary>
        public ObservableCollection<Reservas> Reservas { get; set; }
        
        /// <summary>
        /// Lista de socios para el ComboBox.
        /// </summary>
        public ObservableCollection<Socios> Socios { get; set; }
        
        /// <summary>
        /// Lista de actividades para el ComboBox.
        /// </summary>
        public ObservableCollection<Actividades> Actividades { get; set; }

        //Reserva que el usuario selecciona en el DataGrid.
        private Reservas _reservaSeleccionada;
        
        /// <summary>
        /// Reserva seleccionada actualmente en el DataGrid
        /// </summary>
        public Reservas ReservaSeleccionada
        {
            get => _reservaSeleccionada;
            set
            {
                _reservaSeleccionada = value;
                MensajeError = string.Empty; //Limpiamos errores antiguos al seleccionar otra reserva
                OnPropertyChanged(nameof(ReservaSeleccionada));
                CargarEnFormulario();
            }
        }

        /// <summary>
        /// ID de la reserva actualmente en el formulario
        /// </summary>
        public int IdReserva { get; set; }

        private Socios _socioSeleccionado;
        
        /// <summary>
        /// Socio seleccionado en el ComboBox para la reserva
        /// </summary>
        public Socios SocioSeleccionado
        {
            get => _socioSeleccionado;
            set
            {
                _socioSeleccionado = value;
                OnPropertyChanged(nameof(SocioSeleccionado));
            }
        }

        private Actividades _actividadSeleccionada;
        
        /// <summary>
        /// Actividad seleccionada en el ComboBox para la reserva
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

        private DateTime _fecha = DateTime.Today;
        
        /// <summary>
        /// Fecha de la reserva
        /// </summary>
        public DateTime Fecha
        {
            get => _fecha;
            set
            {
                _fecha = value;
                OnPropertyChanged(nameof(Fecha));
            }
        }

        private string _mensajeError;
        
        /// <summary>
        /// Mensaje de error que se muestra en la interfaz
        /// </summary>
        public string MensajeError
        {
            get => _mensajeError;
            set
            {
                _mensajeError = value;
                OnPropertyChanged(nameof(MensajeError));
            }
        }

        /// <summary>
        /// Comando para crear una nueva reserva
        /// </summary>
        public ICommand CrearCommand { get; }
        
        /// <summary>
        /// Comando para editar la reserva seleccionada
        /// </summary>
        public ICommand EditarCommand { get; }
        
        /// <summary>
        /// Comando para eliminar la reserva seleccionada
        /// </summary>
        public ICommand EliminarCommand { get; }

        /// <summary>
        /// Constructor del ViewModel que inicializa servicios, carga datos y configura comandos
        /// </summary>
        public ReservasViewModel()
        {
            //Inicializamos servicios
            _reservaService = new ReservaService();
            _socioService = new SocioService();
            _actividadService = new ActividadService();

            //Cargamos datos iniciales
            Socios = new ObservableCollection<Socios>(_socioService.GetAll().Where(s => s.Activo));
            Actividades = new ObservableCollection<Actividades>(_actividadService.GetAll());
            Reservas = new ObservableCollection<Reservas>(_reservaService.GetAll());

            CrearCommand = new RelayCommand(_ => Crear(), _ => PuedeCrear());
            EditarCommand = new RelayCommand(_ => Editar(), _ => PuedeEditar());
            EliminarCommand = new RelayCommand(_ => Eliminar(), _ => PuedeEliminar());
        }

        //Métodos que comprueban si se pueden ejecutar las acciones.
        //Si devuelven false, el botón se deshabilita automáticamente.
        
        /// <summary>
        /// EsFechaValida() --> Comprueba si la fecha es hoy o posterior.
        /// </summary>
        /// <param name="fecha">Fecha a validar</param>
        /// <returns>True si la fecha es válida, false en caso contrario</returns>
        public bool EsFechaValida(DateTime fecha)
        {
            return fecha.Date >= DateTime.Today;
        }

        //PuedeCrear() --> Se puede crear si hay socio y actividad seleccionados y la fecha es hoy o posterior.
        private bool PuedeCrear()
        {
            return SocioSeleccionado != null
                   && ActividadSeleccionada != null
                   && EsFechaValida(Fecha);
        }

        //PuedeEditar() --> Solo se puede editar si hay una reserva seleccionada y los datos son válidos.
        private bool PuedeEditar()
        {
            return ReservaSeleccionada != null && PuedeCrear();
        }

        //PuedeEliminar() --> Solo se puede eliminar si hay una reserva seleccionada.
        private bool PuedeEliminar()
        {
            return ReservaSeleccionada != null;
        }

        //Crear() --> Método para crear una reserva nueva.
        private void Crear()
        {
            MensajeError = string.Empty;

            //Validamos fecha
            if (!EsFechaValida(Fecha))
            {
                MensajeError = "La fecha de la reserva no puede ser anterior a hoy.";
            }
            else
            {
                try
                {
                    //Validamos aforo disponible
                    if (!_reservaService.HayAforoDisponible(ActividadSeleccionada.IdActividad, Fecha.Date))
                    {
                        MensajeError = "No se pueden crear más reservas: aforo completo.";
                    }
                    else
                    {
                        var reserva = new Reservas
                        {
                            IdSocio = SocioSeleccionado.IdSocio,
                            IdActividad = ActividadSeleccionada.IdActividad,
                            Fecha = Fecha.Date
                        };

                        _reservaService.Add(reserva);
                        RecargarReservas();
                        LimpiarFormulario();
                    }
                }
                catch (Exception ex)
                {
                    MensajeError = ex.Message;
                }
            }
        }

        //Editar() --> Método para editar una reserva existente.
        private void Editar()
        {
            MensajeError = string.Empty;

            if (ReservaSeleccionada == null)
            {
                MensajeError = "Debe seleccionar una reserva para editarla.";
            }
            else if (!EsFechaValida(Fecha))
            {
                MensajeError = "La fecha de la reserva no puede ser anterior a hoy.";
            }
            else
            {
                //Validamos aforo solo si cambia actividad o fecha
                bool cambiaActividad = ReservaSeleccionada.IdActividad != ActividadSeleccionada.IdActividad;
                bool cambiaFecha = ReservaSeleccionada.Fecha.Date != Fecha.Date;

                if (cambiaActividad || cambiaFecha)
                {
                    if (!_reservaService.HayAforoDisponible(ActividadSeleccionada.IdActividad, Fecha.Date))
                    {
                        MensajeError = "No se puede editar la reserva: aforo completo.";
                    }
                    else
                    {
                        try
                        {
                            //Actualizamos los datos de la reserva seleccionada
                            ReservaSeleccionada.IdSocio = SocioSeleccionado.IdSocio;
                            ReservaSeleccionada.IdActividad = ActividadSeleccionada.IdActividad;
                            ReservaSeleccionada.Fecha = Fecha.Date;

                            _reservaService.Update(ReservaSeleccionada);
                            RecargarReservas();
                            LimpiarFormulario();
                        }
                        catch (Exception ex)
                        {
                            MensajeError = ex.Message;
                        }
                    }
                }
                else
                {
                    try
                    {
                        ReservaSeleccionada.IdSocio = SocioSeleccionado.IdSocio;
                        ReservaSeleccionada.IdActividad = ActividadSeleccionada.IdActividad;
                        ReservaSeleccionada.Fecha = Fecha.Date;

                        _reservaService.Update(ReservaSeleccionada);
                        RecargarReservas();
                        LimpiarFormulario();
                    }
                    catch (Exception ex)
                    {
                        MensajeError = ex.Message;
                    }
                }
            }
        }

        //Eliminar() --> Método para eliminar una reserva existente.
        private void Eliminar()
        {
            //Limpiamos mensaje de error previo
            MensajeError = string.Empty;

            if (ReservaSeleccionada == null)
            {
                MensajeError = "Debe seleccionar una reserva para eliminarla.";
            }
            else
            {
                //Pedimos confirmación al usuario
                var confirmar = MessageBox.Show(
                    "¿Seguro que desea eliminar esta reserva?",
                    "Confirmación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirmar == MessageBoxResult.Yes)
                {
                    try
                    {
                        _reservaService.Delete(ReservaSeleccionada);
                        RecargarReservas();
                        LimpiarFormulario();
                    }
                    catch (Exception ex)
                    {
                        MensajeError = ex.Message;
                    }
                }
            }
        }

        //RecargarReservas() --> Recarga la lista de reservas desde la base de datos.
        private void RecargarReservas()
        {
            //Limpiamos mensaje de error previo
            MensajeError = string.Empty;

            try
            {
                Reservas.Clear();
                foreach (var r in _reservaService.GetAll())
                    Reservas.Add(r);
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
            }
        }

        //LimpiarFormulario() --> Limpia los campos del formulario.
        private void LimpiarFormulario()
        {
            IdReserva = 0;
            SocioSeleccionado = null;
            ActividadSeleccionada = null;
            Fecha = DateTime.Today;
            ReservaSeleccionada = null;
            MensajeError = string.Empty;
            OnPropertyChanged(nameof(IdReserva));
        }

        //CargarEnFormulario() --> Carga los datos de la reserva seleccionada en el formulario.
        private void CargarEnFormulario()
        {
            if (ReservaSeleccionada != null)
            {
                IdReserva = ReservaSeleccionada.IdReserva;
                Fecha = ReservaSeleccionada.Fecha.Date;

                SocioSeleccionado = Socios.FirstOrDefault(s => s.IdSocio == ReservaSeleccionada.IdSocio);
                ActividadSeleccionada = Actividades.FirstOrDefault(a => a.IdActividad == ReservaSeleccionada.IdActividad);

                OnPropertyChanged(nameof(IdReserva));
            }
        }
    }
}