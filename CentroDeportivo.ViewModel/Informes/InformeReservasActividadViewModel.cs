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
    /// ViewModel que controla el informe de reservas por actividad.
    /// </summary>
    public class InformeReservasActividadViewModel : BaseViewModel
    {
        /// <summary>
        /// Propiedad que contiene el informe de Crystal Reports.
        /// </summary>
        public ReportDocument Informe { get; }

        /// <summary>
        /// Constructor que carga los datos y prepara el informe.
        /// </summary>
        /// <param name="idActividad">ID de la actividad para filtrar las reservas</param>
        public InformeReservasActividadViewModel(int idActividad)
        {
            //Cargar los datos del informe utilizando el helper.
            var helper = new InformeReservasActividadHelper();
            var ds = helper.Cargar(idActividad);

            //Crear el informe y asignarle la fuente de datos.
            Informe = new crReservasActividad();
            Informe.SetDataSource(ds);

            //Asignar el parámetro de la actividad al informe.
            Informe.SetParameterValue("IdActividad", idActividad);
        }
    }
}
