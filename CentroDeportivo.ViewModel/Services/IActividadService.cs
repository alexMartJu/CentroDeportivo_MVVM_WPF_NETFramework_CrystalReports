using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Services
{
    /// <summary>
    /// Interfaz para las operaciones de actividades
    /// </summary>
    public interface IActividadService
    {
        /// <summary>
        /// Obtiene todas las actividades de la base de datos
        /// </summary>
        /// <returns>Colección de actividades</returns>
        IEnumerable<Actividades> GetAll();
        
        /// <summary>
        /// Añade una nueva actividad a la base de datos
        /// </summary>
        /// <param name="actividad">Actividad a añadir</param>
        void Add(Actividades actividad);
        
        /// <summary>
        /// Actualiza una actividad existente en la base de datos
        /// </summary>
        /// <param name="actividad">Actividad con los datos actualizados</param>
        void Update(Actividades actividad);
        
        /// <summary>
        /// Elimina una actividad de la base de datos
        /// </summary>
        /// <param name="actividad">Actividad a eliminar</param>
        void Delete(Actividades actividad);
    }
}
