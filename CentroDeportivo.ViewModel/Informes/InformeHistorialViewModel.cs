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
    /// ViewModel que controla el informe del historial de reservas.
    /// </summary>
    public class InformeHistorialViewModel : BaseViewModel
    {
        /// <summary>
        /// Propiedad que contiene el informe de Crystal Reports.
        /// </summary>
        public ReportDocument Informe { get; }

        /// <summary>
        /// Constructor que carga los datos y prepara el informe.
        /// </summary>
        public InformeHistorialViewModel()
        {
            //Cargar los datos del informe utilizando el helper.
            var helper = new InformeHistorialHelper();
            var ds = helper.Cargar();

            //Crear el informe y asignarle la fuente de datos.
            Informe = new crHistorialReservas();
            Informe.SetDataSource(ds);
        }
    }
}
