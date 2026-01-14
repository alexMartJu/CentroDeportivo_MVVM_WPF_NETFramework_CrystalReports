using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.ViewModel.Services
{
    //Servicio que se encarga de trabajar con los socios en la base de datos. 
    //Aquí hacemos todas las operaciones: obtener, añadir, actualizar y eliminar socios.
    public class SocioService : ISocioService
    {
        //GetAll() --> Método para obtener todos los socios de la base de datos
        public IEnumerable<Socios> GetAll()
        {
            try
            {
                //Abrimos el contexto de la base de datos
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Devolvemos la lista de socios
                    return ctx.Socios.ToList();
                }
            }
            catch (Exception ex)
            {
                //Si hay un error, lanzamos una excepción con un mensaje descriptivo
                throw new Exception("Error al obtener la lista de socios.", ex);
            }
        }

        //Add() --> Método para añadir un nuevo socio a la base de datos
        public void Add(Socios socio)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Añadimos el socio al contexto y guardamos los cambios
                    ctx.Socios.Add(socio);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //Obtenemos el mensaje de error interno
                var mensaje = ex.InnerException?.InnerException?.Message;

                //Detectar email duplicado
                if (mensaje != null &&
                    (mensaje.Contains("duplicate") ||
                     mensaje.Contains("UNIQUE") ||
                     mensaje.Contains("IX_Email")))
                {
                    throw new Exception("Ya existe un socio con ese email.");
                }

                throw new Exception("No se pudo agregar el socio. Revisa los datos.");
            }
        }

        //Update() --> Método para actualizar un socio existente en la base de datos
        public void Update(Socios socio)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Buscamos el socio original en la base de datos
                    var original = ctx.Socios.Find(socio.IdSocio);

                    //Si no lo encontramos, lanzamos una excepción
                    if (original == null)
                    {
                        throw new Exception("No se encontró el socio en la base de datos.");
                    }
                        
                    //Actualizamos solo los campos editables
                    original.Nombre = socio.Nombre;
                    original.Email = socio.Email;
                    original.Activo = socio.Activo;

                    //Guardamos los cambios
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException?.InnerException?.Message;

                if (mensaje != null &&
                    (mensaje.Contains("duplicate") ||
                     mensaje.Contains("UNIQUE") ||
                     mensaje.Contains("IX_Email")))
                {
                    throw new Exception("Ya existe un socio con ese email.");
                }

                throw new Exception("No se pudo actualizar el socio. Revisa los datos.");
            }
        }

        //Delete() --> Método para eliminar un socio de la base de datos
        public void Delete(Socios socio)
        {
            try
            {
                using (var ctx = new CentroDeportivoEntities())
                {
                    //Cargamos el socio original desde el contexto
                    var original = ctx.Socios.Find(socio.IdSocio);

                    //Si no lo encontramos, lanzamos una excepción
                    if (original == null)
                    {
                        throw new Exception("No se encontró el socio en la base de datos.");
                    }
                        
                    //Eliminamos el socio y guardamos los cambios
                    ctx.Socios.Remove(original);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //Obtenemos el mensaje de error interno
                var mensaje = ex.InnerException?.InnerException?.Message;

                //Detectamos error por reservas asociadas
                if (mensaje != null &&
                    (mensaje.Contains("REFERENCE") || mensaje.Contains("FK_Reservas_Socios")))
                {
                    throw new Exception("No se puede eliminar el socio porque tiene reservas asociadas.");
                }

                throw new Exception("Error al eliminar el socio.");
            }
        }
    }
}