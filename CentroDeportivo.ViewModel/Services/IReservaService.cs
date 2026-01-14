using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Services
{
    //Interfaz para las operaciones de reservas
    public interface IReservaService
    {
        IEnumerable<Reservas> GetAll();
        void Add(Reservas reserva);
        void Update(Reservas reserva);
        void Delete(Reservas reserva);

        bool HayAforoDisponible(int idActividad, DateTime fecha);
    }
}
