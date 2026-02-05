using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.View.Informes;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.View.Services
{
    /// <summary>
    /// Implementación del servicio de ventanas para mostrar informes
    /// </summary>
    public class WindowService : IWindowService
    {
        /// <summary>
        /// Método para mostrar el informe de socios
        /// </summary>
        public void MostrarInformeSocios() 
        { 
            new InformeSociosWindow().ShowDialog(); 
        }

        /// <summary>
        /// Método para mostrar el informe de reservas por actividad
        /// </summary>
        /// <param name="idActividad">ID de la actividad para filtrar las reservas</param>
        public void MostrarInformeReservasActividad(int idActividad) 
        {
            new InformeReservasActividadWindow(idActividad).ShowDialog();
        }

        /// <summary>
        /// Método para mostrar el informe del historial de reservas
        /// </summary>
        public void MostrarInformeHistorial() 
        { 
            new InformeHistorialReservasWindow().ShowDialog(); 
        }
    }
}
