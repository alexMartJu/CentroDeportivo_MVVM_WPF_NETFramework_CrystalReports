using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.View.Informes;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.View.Services
{
    //Implementación del servicio de ventanas para mostrar informes
    public class WindowService : IWindowService
    {
        //Método para mostrar el informe de socios
        public void MostrarInformeSocios() 
        { 
            new InformeSociosWindow().ShowDialog(); 
        }

        //Método para mostrar el informe de reservas por actividad
        public void MostrarInformeReservasActividad(int idActividad) 
        {
            new InformeReservasActividadWindow(idActividad).ShowDialog();
        }

        //Método para mostrar el informe del historial de reservas
        public void MostrarInformeHistorial() 
        { 
            new InformeHistorialReservasWindow().ShowDialog(); 
        }
    }
}
