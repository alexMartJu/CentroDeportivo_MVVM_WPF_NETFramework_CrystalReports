using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Informes;
using CentroDeportivo.ViewModel.Informes.Helpers;
using CrystalDecisions.CrystalReports.Engine;

namespace CentroDeportivo.ViewModel.Informes
{
    /// <summary>
    /// ViewModel que controla el informe de socios.
    /// </summary>
    public class InformeSociosViewModel : BaseViewModel
    {
        /// <summary>
        /// Propiedad que contiene el informe de Crystal Reports.
        /// </summary>
        public ReportDocument Informe { get; }

        /// <summary>
        /// Constructor que carga los datos y prepara el informe.
        /// </summary>
        public InformeSociosViewModel()
        {
            //Cargar los datos del informe utilizando el helper.
            var helper = new InformeSociosHelper();
            var ds = helper.CargarSocios();

            //Crear el informe y asignarle la fuente de datos.
            Informe = new crSociosMaestro();
            Informe.SetDataSource(ds);
        }
    }
}
