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
    //ViewModel que controla el informe de socios.
    public class InformeSociosViewModel : BaseViewModel
    {
        //Propiedad que contiene el informe de Crystal Reports.
        public ReportDocument Informe { get; }

        //Constructor que carga los datos y prepara el informe.
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
