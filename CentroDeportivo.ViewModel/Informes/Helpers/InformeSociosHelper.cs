using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Informes.Helpers
{
    //Helper que carga los datos de los socios para el informe.
    public class InformeSociosHelper
    {
        //CargarSocios() --> Método que carga los datos de los socios desde la base de datos.
        public dsInformes CargarSocios()
        {
            var ds = new dsInformes();

            using (var ctx = new CentroDeportivoEntities())
            {
                var socios = ctx.Socios.ToList();

                foreach (var s in socios)
                {
                    ds.SociosMaestro.AddSociosMaestroRow(
                        s.IdSocio,
                        s.Nombre,
                        s.Email,
                        s.Activo
                    );
                }
            }

            return ds;
        }
    }
}
