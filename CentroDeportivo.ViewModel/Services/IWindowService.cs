using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Services
{
    /// <summary>
    /// Interfaz para el servicio de ventanas, encargado de mostrar informes
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Muestra el informe maestro de socios
        /// </summary>
        void MostrarInformeSocios();
        
        /// <summary>
        /// Muestra el informe de reservas filtradas por actividad
        /// </summary>
        /// <param name="idActividad">ID de la actividad para filtrar las reservas</param>
        void MostrarInformeReservasActividad(int idActividad);
        
        /// <summary>
        /// Muestra el informe del historial completo de reservas
        /// </summary>
        void MostrarInformeHistorial();
    }
}
