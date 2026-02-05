using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Informes.Helpers
{
    /// <summary>
    /// Helper que carga los datos para el informe del historial de reservas.
    /// </summary>
    public class InformeHistorialHelper
    {
        /// <summary>
        /// Cargar() --> Método que obtiene los datos de la base de datos y los carga en el dataset.
        /// </summary>
        /// <returns>Dataset con el historial completo de reservas ordenado por fecha</returns>
        public dsInformes Cargar()
        {
            var ds = new dsInformes();

            using (var ctx = new CentroDeportivoEntities())
            {
                var datos = ctx.Reservas
                    .OrderBy(r => r.Fecha)
                    .Select(r => new
                    {
                        NombreSocio = r.Socios.Nombre,
                        NombreActividad = r.Actividades.Nombre,
                        Fecha = r.Fecha
                    })
                    .ToList();

                foreach (var d in datos)
                {
                    ds.HistorialReservas.AddHistorialReservasRow(
                        d.NombreSocio,
                        d.NombreActividad,
                        d.Fecha
                    );
                }
            }

            return ds;
        }
    }
}
