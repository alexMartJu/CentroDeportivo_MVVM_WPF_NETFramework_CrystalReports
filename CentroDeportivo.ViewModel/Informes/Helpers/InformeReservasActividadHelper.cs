using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Informes.Helpers
{
    //Helper que carga los datos para el informe de reservas por actividad.
    public class InformeReservasActividadHelper
    {
        //Cargar() --> Método que obtiene los datos de la base de datos y los carga en el dataset.
        public dsInformes Cargar(int idActividad)
        {
            var ds = new dsInformes();

            using (var ctx = new CentroDeportivoEntities())
            {
                var datos = ctx.Reservas
                    .Where(r => r.IdActividad == idActividad)
                    .Select(r => new
                    {
                        NombreActividad = r.Actividades.Nombre,
                        Fecha = r.Fecha,
                        NombreSocio = r.Socios.Nombre,
                        AforoMaximo = r.Actividades.AforoMaximo
                    })
                    .ToList();

                foreach (var d in datos)
                {
                    ds.ReservasPorActividad.AddReservasPorActividadRow(
                        d.NombreActividad,
                        d.Fecha,
                        d.NombreSocio,
                        d.AforoMaximo
                    );
                }
            }

            return ds;
        }
    }
}
