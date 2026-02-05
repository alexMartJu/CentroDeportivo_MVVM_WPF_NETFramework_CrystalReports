using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.ViewModel.Services
{
    /// <summary>
    /// Servicio que se encarga de trabajar con las reservas en la base de datos.
    /// Aquí hacemos todas las operaciones: obtener, añadir, actualizar y eliminar reservas.
    /// </summary>
    public class ReservaService : IReservaService
    {
        /// <summary>
        /// GetAll() --> Método para obtener todas las reservas de la base de datos
        /// </summary>
        /// <returns>Colección de reservas con socios y actividades incluidas</returns>
        public IEnumerable<Reservas> GetAll()
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Incluimos socio y actividad para que el DataGrid pueda mostrar sus nombres
                    return ctx.Reservas
                        .Include("Socios")
                        .Include("Actividades")
                        .OrderBy(r => r.Fecha)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las reservas.", ex);
            }
        }

        /// <summary>
        /// Add() --> Método para añadir una nueva reserva a la base de datos
        /// </summary>
        /// <param name="reserva">Reserva a añadir</param>
        public void Add(Reservas reserva)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Añadimos la reserva al contexto y guardamos los cambios
                    ctx.Reservas.Add(reserva);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la reserva.", ex);
            }
        }

        /// <summary>
        /// Update() --> Método para actualizar una reserva existente en la base de datos
        /// </summary>
        /// <param name="reserva">Reserva con los datos actualizados</param>
        public void Update(Reservas reserva)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Cargamos la reserva original desde el contexto
                    var original = ctx.Reservas.Find(reserva.IdReserva);

                    //Si no la encontramos, lanzamos una excepción
                    if (original == null)
                    {
                        throw new Exception("No se encontró la reserva en la base de datos.");
                    }
                        
                    //Actualizamos solo los campos editables
                    original.IdSocio = reserva.IdSocio;
                    original.IdActividad = reserva.IdActividad;
                    original.Fecha = reserva.Fecha;

                    //Guardamos los cambios
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la reserva.", ex);
            }
        }

        /// <summary>
        /// Delete() --> Método para eliminar una reserva de la base de datos
        /// </summary>
        /// <param name="reserva">Reserva a eliminar</param>
        public void Delete(Reservas reserva)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Buscamos la reserva original en la base de datos
                    var original = ctx.Reservas.Find(reserva.IdReserva);

                    //Si no la encontramos, lanzamos una excepción
                    if (original == null)
                    {
                        throw new Exception("No se encontró la reserva en la base de datos.");
                    }
                        
                    //Eliminamos la reserva
                    ctx.Reservas.Remove(original);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reserva.", ex);
            }
        }

        /// <summary>
        /// HayAforoDisponible() --> Método para comprobar si hay aforo disponible para una actividad en una fecha dada
        /// </summary>
        /// <param name="idActividad">ID de la actividad</param>
        /// <param name="fecha">Fecha de la reserva</param>
        /// <returns>True si hay plazas disponibles, false si el aforo está completo</returns>
        public bool HayAforoDisponible(int idActividad, DateTime fecha)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Contamos cuántas reservas hay para esa actividad en esa fecha
                    int reservas = ctx.Reservas
                        .Count(r => r.IdActividad == idActividad 
                            && DbFunctions.TruncateTime(r.Fecha) == fecha.Date);

                    //Obtenemos el aforo máximo de la actividad
                    int aforo = ctx.Actividades
                        .Where(a => a.IdActividad == idActividad)
                        .Select(a => a.AforoMaximo)
                        .FirstOrDefault();

                    return reservas < aforo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al comprobar el aforo disponible.", ex);
            }
        }
    }
}
