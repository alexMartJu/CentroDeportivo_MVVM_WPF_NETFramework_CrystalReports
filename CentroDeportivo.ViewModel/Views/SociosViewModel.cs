using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.ViewModel.Views
{
    //ViewModel que controla toda la parte de socios en la aplicación. 
    //Aquí gestionamos lo que se muestra en pantalla y lo que hace el usuario.
    public class SociosViewModel : BaseViewModel
    {
        //Servicio que se encarga de hablar con la base de datos.
        private readonly ISocioService _socioService;

        //Lista de socios que se muestra en el DataGrid. 
        //ObservableCollection se usa para que la pantalla se actualice sola cuando cambia la lista.
        public ObservableCollection<Socios> Socios { get; set; }

        //Socio que el usuario selecciona en el DataGrid.
        private Socios _socioSeleccionado;
        public Socios SocioSeleccionado
        {
            get => _socioSeleccionado;
            set
            {
                _socioSeleccionado = value;
                MensajeError = string.Empty; //Limpiamos errores antiguos al seleccionar otro socio
                OnPropertyChanged(nameof(SocioSeleccionado)); //Avisamos a la vista de que ha cambiado.
                CargarSocioEnFormulario(); //Cargamos los datos del socio en el formulario.
            }
        }

        //Propiedades del formulario donde el usuario escribe los datos.
        public int IdSocio { get; set; }

        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                OnPropertyChanged(nameof(Nombre)); //Avisamos a la vista de que ha cambiado.
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private bool _activo = true;
        public bool Activo
        {
            get => _activo;
            set
            {
                _activo = value;
                OnPropertyChanged(nameof(Activo));
            }
        }

        //Aquí guardamos mensajes de error para mostrarlos en pantalla.
        private string _mensajeError;
        public string MensajeError
        {
            get => _mensajeError;
            set
            {
                _mensajeError = value;
                OnPropertyChanged(nameof(MensajeError));
            }
        }

        //Commands que se ejecutan cuando el usuario pulsa los botones.
        public ICommand CrearCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }

        //Constructor del ViewModel
        public SociosViewModel()
        {
            _socioService = new SocioService(); //Inicializamos el servicio.
            Socios = new ObservableCollection<Socios>(_socioService.GetAll()); //Cargamos la lista de socios desde la base de datos.

            //Inicializamos los comandos.
            CrearCommand = new RelayCommand(_ => Crear(), _ => PuedeCrear());
            EditarCommand = new RelayCommand(_ => Editar(), _ => PuedeEditar());
            EliminarCommand = new RelayCommand(_ => Eliminar(), _ => PuedeEliminar());
        }

        //Métodos que comprueban si se pueden ejecutar las acciones.
        //Si devuelven false, el botón se deshabilita automáticamente.
        private bool PuedeCrear()
        {
            return !string.IsNullOrWhiteSpace(Nombre)
                && !string.IsNullOrWhiteSpace(Email)
                && EsEmailValido(Email);
        }

        //PuedeEditar() --> Solo se puede editar si hay un socio seleccionado y los datos son válidos.
        private bool PuedeEditar()
        {
            return SocioSeleccionado != null && PuedeCrear();
        }

        //PuedeEliminar() --> Solo se puede eliminar si hay un socio seleccionado.
        private bool PuedeEliminar()
        {
            return SocioSeleccionado != null;
        }

        //Crear() --> Método para crear un socio nuevo.
        private void Crear()
        {
            //Limpiamos mensajes de error previos.
            MensajeError = string.Empty;

            //Validamos el email
            if (!EsEmailValido(Email))
            {
                MensajeError = "El email no tiene un formato válido.";
            }
            else
            {
                try
                {
                    //Creamos el socio con los datos del formulario.
                    var socio = new Socios
                    {
                        Nombre = Nombre,
                        Email = Email,
                        Activo = Activo
                    };

                    _socioService.Add(socio); //Lo guardamos en la base de datos.
                    RecargarSocios(); //Recargamos la lista de socios para que aparezca el nuevo.
                    LimpiarFormulario(); //Limpiamos el formulario.
                }
                catch (Exception ex)
                {
                    MensajeError = ex.Message;
                }
            }
        }

        //Editar() --> Método para editar un socio existente.
        private void Editar()
        {
            MensajeError = string.Empty;

            if (SocioSeleccionado == null)
            {
                MensajeError = "Debe seleccionar un socio para editarlo.";
            }
            else if (!EsEmailValido(Email))
            {
                MensajeError = "El email no tiene un formato válido.";
            }
            else
            {
                try
                {
                    //Actualizamos los datos del socio seleccionado con los del formulario.
                    SocioSeleccionado.Nombre = Nombre;
                    SocioSeleccionado.Email = Email;
                    SocioSeleccionado.Activo = Activo;

                    _socioService.Update(SocioSeleccionado); //Guardamos los cambios en la base de datos.
                    RecargarSocios();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MensajeError = ex.Message;
                }
            }
        }

        //Eliminar() --> Método para eliminar un socio.
        private void Eliminar()
        {
            //Limpiamos mensajes de error previos.
            MensajeError = string.Empty;

            if (SocioSeleccionado == null)
            {
                MensajeError = "Debe seleccionar un socio para eliminarlo.";
            }
            else
            {
                //Pedimos confirmación al usuario antes de eliminar.
                var confirmar = MessageBox.Show(
                    "¿Seguro que desea eliminar este socio?",
                    "Confirmación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                //Si confirma, procedemos a eliminar.
                if (confirmar == MessageBoxResult.Yes)
                {
                    try
                    {
                        //Eliminamos el socio seleccionado.
                        _socioService.Delete(SocioSeleccionado);
                        RecargarSocios();
                        LimpiarFormulario();
                    }
                    catch (Exception ex)
                    {
                        MensajeError = ex.Message;
                    }
                }
            }
        }

        //RecargarSocios() --> Vuelve a cargar la lista de socios desde la base de datos.
        private void RecargarSocios()
        {
            //Limpiamos mensajes de error previos.
            MensajeError = string.Empty;

            try
            {
                //Limpiamos y recargamos la lista.
                Socios.Clear();
                foreach (var socio in _socioService.GetAll())
                    Socios.Add(socio);
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
            }
        }

        //LimpiarFormulario() --> Limpia el formulario después de crear, editar o eliminar.
        private void LimpiarFormulario()
        {
            IdSocio = 0;
            Nombre = string.Empty;
            Email = string.Empty;
            Activo = true;
            SocioSeleccionado = null;
            MensajeError = string.Empty;
        }

        //CargarSocioEnFormulario() --> Cuando seleccionamos un socio del DataGrid, cargo sus datos en el formulario.
        private void CargarSocioEnFormulario()
        {
            if (SocioSeleccionado != null)
            {
                IdSocio = SocioSeleccionado.IdSocio;
                Nombre = SocioSeleccionado.Nombre;
                Email = SocioSeleccionado.Email;
                Activo = SocioSeleccionado.Activo;

                OnPropertyChanged(nameof(IdSocio));
            }
        }

        //EsEmailValido() --> Comprueba si un email tiene un formato básico válido.
        public bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            //Patrón estándar para validar emails reales
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron, RegexOptions.IgnoreCase);
        }
    }
}