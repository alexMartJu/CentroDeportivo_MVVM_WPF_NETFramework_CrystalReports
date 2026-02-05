using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Services
{
    /// <summary>
    /// Interfaz para las operaciones de reservas
    /// </summary>
    public interface IReservaService
    {
        /// <summary>
        /// Obtiene todas las reservas de la base de datos
        /// </summary>
        /// <returns>Colección de reservas</returns>
        IEnumerable<Reservas> GetAll();
        
        /// <summary>
        /// Añade una nueva reserva a la base de datos
        /// </summary>
        /// <param name="reserva">Reserva a añadir</param>
        void Add(Reservas reserva);
        
        /// <summary>
        /// Actualiza una reserva existente en la base de datos
        /// </summary>
        /// <param name="reserva">Reserva con los datos actualizados</param>
        void Update(Reservas reserva);
        
        /// <summary>
        /// Elimina una reserva de la base de datos
        /// </summary>
        /// <param name="reserva">Reserva a eliminar</param>
        void Delete(Reservas reserva);

        /// <summary>
        /// Comprueba si hay aforo disponible para una actividad en una fecha específica
        /// </summary>
        /// <param name="idActividad">ID de la actividad</param>
        /// <param name="fecha">Fecha de la reserva</param>
        /// <returns>True si hay aforo disponible, false en caso contrario</returns>
        bool HayAforoDisponible(int idActividad, DateTime fecha);
    }
}
