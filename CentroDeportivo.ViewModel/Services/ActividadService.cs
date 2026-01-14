using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.ViewModel.Services
{
    //Servicio que se encarga de trabajar con las actividades en la base de datos.
    //Aquí hacemos todas las operaciones: obtener, añadir, actualizar y eliminar actividades.
    public class ActividadService : IActividadService
    {
        //GetAll() --> Método para obtener todas las actividades de la base de datos
        public IEnumerable<Actividades> GetAll()
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Devolvemos la lista de actividades
                    return ctx.Actividades.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las actividades.", ex);
            }
        }

        //Add() --> Método para añadir una nueva actividad a la base de datos
        public void Add(Actividades actividad)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Añadimos la actividad al contexto y guardamos los cambios
                    ctx.Actividades.Add(actividad);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la actividad.", ex);
            }
        }

        //Update() --> Método para actualizar una actividad existente en la base de datos
        public void Update(Actividades actividad)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Cargamos la actividad original desde el contexto
                    var original = ctx.Actividades.Find(actividad.IdActividad);

                    //Si no la encontramos, lanzamos una excepción
                    if (original == null)
                    {
                        throw new Exception("No se encontró la actividad en la base de datos.");
                    }
                        
                    //Actualizamos solo los campos editables
                    original.Nombre = actividad.Nombre;
                    original.AforoMaximo = actividad.AforoMaximo;

                    //Guardamos los cambios
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la actividad.", ex);
            }
        }

        //Delete() --> Método para eliminar una actividad de la base de datos
        public void Delete(Actividades actividad)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Cargamos la actividad original desde el contexto
                    var original = ctx.Actividades.Find(actividad.IdActividad);

                    //Si no la encontramos, lanzamos una excepción
                    if (original == null)
                    {
                        throw new Exception("No se encontró la actividad en la base de datos.");
                    }
                        
                    //Eliminamos la actividad y guardamos los cambios
                    ctx.Actividades.Remove(original);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //Obtenemos el mensaje de error interno
                var mensaje = ex.InnerException?.InnerException?.Message;

                //Detectamos error por reservas asociadas
                if (mensaje != null &&
                    (mensaje.Contains("REFERENCE") || mensaje.Contains("FK_Reservas_Actividades")))
                {
                    throw new Exception("No se puede eliminar la actividad porque tiene reservas asociadas.");
                }

                throw new Exception("Error al eliminar la actividad.");
            }
        }
    }
}