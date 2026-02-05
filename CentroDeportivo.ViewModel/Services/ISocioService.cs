using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Services
{
    /// <summary>
    /// Interfaz para las operaciones de socios
    /// </summary>
    public interface ISocioService
    {
        /// <summary>
        /// Obtiene todos los socios de la base de datos
        /// </summary>
        /// <returns>Colección de socios</returns>
        IEnumerable<Socios> GetAll();
        
        /// <summary>
        /// Añade un nuevo socio a la base de datos
        /// </summary>
        /// <param name="socio">Socio a añadir</param>
        void Add(Socios socio);
        
        /// <summary>
        /// Actualiza un socio existente en la base de datos
        /// </summary>
        /// <param name="socio">Socio con los datos actualizados</param>
        void Update(Socios socio);
        
        /// <summary>
        /// Elimina un socio de la base de datos
        /// </summary>
        /// <param name="socio">Socio a eliminar</param>
        void Delete(Socios socio);
    }
}
