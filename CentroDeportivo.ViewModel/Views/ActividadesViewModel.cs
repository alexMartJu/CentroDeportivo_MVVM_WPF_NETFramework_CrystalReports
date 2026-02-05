using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CentroDeportivo.Model;
using CentroDeportivo.ViewModel;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.ViewModel.Views
{
    /// <summary>
    /// ViewModel que controla toda la parte de actividades en la aplicación.
    /// </summary>
    public class ActividadesViewModel : BaseViewModel
    {
        //Servicio que se encarga de hablar con la base de datos.
        private readonly IActividadService _actividadService;

        /// <summary>
        /// Lista de actividades que se muestra en el DataGrid.
        /// </summary>
        public ObservableCollection<Actividades> Actividades { get; set; }

        //Actividad que el usuario selecciona en el DataGrid.
        private Actividades _actividadSeleccionada;
        
        /// <summary>
        /// Actividad seleccionada actualmente en el DataGrid
        /// </summary>
        public Actividades ActividadSeleccionada
        {
            get => _actividadSeleccionada;
            set
            {
                _actividadSeleccionada = value;
                MensajeError = string.Empty; //Limpiamos errores antiguos al seleccionar otra actividad
                OnPropertyChanged(nameof(ActividadSeleccionada)); //Avisamos a la vista de que ha cambiado.
                CargarEnFormulario(); //Cargamos los datos de la actividad en el formulario.
            }
        }

        /// <summary>
        /// ID de la actividad actualmente en el formulario
        /// </summary>
        public int IdActividad { get; set; }

        private string _nombre;
        
        /// <summary>
        /// Nombre de la actividad en el formulario
        /// </summary>
        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        private int _aforoMaximo;
        
        /// <summary>
        /// Aforo máximo de la actividad
        /// </summary>
        public int AforoMaximo
        {
            get => _aforoMaximo;
            set
            {
                _aforoMaximo = value;
                OnPropertyChanged(nameof(AforoMaximo));
            }
        }

        //Aquí guardamos mensajes de error para mostrarlos en pantalla.
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
        /// Comando para crear una nueva actividad
        /// </summary>
        public ICommand CrearCommand { get; }
        
        /// <summary>
        /// Comando para editar la actividad seleccionada
        /// </summary>
        public ICommand EditarCommand { get; }
        
        /// <summary>
        /// Comando para eliminar la actividad seleccionada
        /// </summary>
        public ICommand EliminarCommand { get; }

        /// <summary>
        /// Constructor del ViewModel que inicializa servicios, carga datos y configura comandos
        /// </summary>
        public ActividadesViewModel()
        {
            _actividadService = new ActividadService(); //Inicializamos el servicio.
            Actividades = new ObservableCollection<Actividades>(_actividadService.GetAll()); //Cargamos la lista de actividades desde la base de datos.

            CrearCommand = new RelayCommand(_ => Crear(), _ => PuedeCrear());
            EditarCommand = new RelayCommand(_ => Editar(), _ => PuedeEditar());
            EliminarCommand = new RelayCommand(_ => Eliminar(), _ => PuedeEliminar());
        }

        //Métodos para comprobar si se pueden ejecutar los comandos.
        //PuedeCrear() --> nombre no vacío y aforo mayor que 0.
        private bool PuedeCrear()
        {
            return !string.IsNullOrWhiteSpace(Nombre) && AforoMaximo > 0;
        }

        //PuedeEditar() --> Solo se puede editar hay una actividad seleccionada y se cumplen las condiciones de PuedeCrear().
        private bool PuedeEditar()
        {
            return ActividadSeleccionada != null && PuedeCrear();
        }

        //PuedeEliminar() --> Solo se puede eliminar si hay una actividad seleccionada.
        private bool PuedeEliminar()
        {
            return ActividadSeleccionada != null;
        }

        //Crear() --> Crea una nueva actividad en la base de datos.
        private void Crear()
        {
            MensajeError = string.Empty;

            try
            {
                var actividad = new Actividades
                {
                    Nombre = Nombre,
                    AforoMaximo = AforoMaximo
                };

                _actividadService.Add(actividad);
                Recargar();
                Limpiar();
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
            }
        }

        //Editar() --> Edita la actividad seleccionada con los datos del formulario.
        private void Editar()
        {
            MensajeError = string.Empty;

            if (ActividadSeleccionada == null)
            {
                MensajeError = "Debe seleccionar una actividad para editarla.";
            }
            else
            {
                try
                {
                    ActividadSeleccionada.Nombre = Nombre;
                    ActividadSeleccionada.AforoMaximo = AforoMaximo;

                    _actividadService.Update(ActividadSeleccionada);
                    Recargar();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MensajeError = ex.Message;
                }
            }
        }

        //Eliminar() --> Elimina la actividad seleccionada de la base de datos.
        private void Eliminar()
        {
            //Limpiamos el mensaje de error
            MensajeError = string.Empty;

            if (ActividadSeleccionada == null)
            {
                MensajeError = "Debe seleccionar una actividad para eliminarla.";
            }
            else
            {
                var confirmar = MessageBox.Show(
                    "¿Seguro que desea eliminar esta actividad?",
                    "Confirmación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirmar == MessageBoxResult.Yes)
                {
                    try
                    {
                        _actividadService.Delete(ActividadSeleccionada);
                        Recargar();
                        Limpiar();
                    }
                    catch (Exception ex)
                    {
                        MensajeError = ex.Message;
                    }
                }
            }
        }

        //Recargar() --> Recarga la lista de actividades desde la base de datos.
        private void Recargar()
        {
            //Limpiamos mensajes de error previos.
            MensajeError = string.Empty;

            try
            {
                Actividades.Clear();
                foreach (var a in _actividadService.GetAll())
                    Actividades.Add(a);
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
            }
        }

        //Limpiar() --> Limpia el formulario.
        private void Limpiar()
        {
            IdActividad = 0;
            Nombre = string.Empty;
            AforoMaximo = 0;
            ActividadSeleccionada = null;
            MensajeError = string.Empty;
            OnPropertyChanged(nameof(IdActividad));
        }

        //CargarEnFormulario() --> Carga los datos de la actividad seleccionada en el formulario.
        private void CargarEnFormulario()
        {
            if (ActividadSeleccionada != null)
            {
                IdActividad = ActividadSeleccionada.IdActividad;
                Nombre = ActividadSeleccionada.Nombre;
                AforoMaximo = ActividadSeleccionada.AforoMaximo;

                OnPropertyChanged(nameof(IdActividad));
            }
        }
    }
}