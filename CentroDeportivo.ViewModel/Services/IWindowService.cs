using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroDeportivo.ViewModel.Services
{
    //Interfaz para el servicio de ventanas, encargado de mostrar informes
    public interface IWindowService
    {
        void MostrarInformeSocios(); 
        void MostrarInformeReservasActividad(int idActividad); 
        void MostrarInformeHistorial();
    }
}
