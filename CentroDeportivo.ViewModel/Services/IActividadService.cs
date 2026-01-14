using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Services
{
    //Interfaz para las operaciones de actividades
    public interface IActividadService
    {
        IEnumerable<Actividades> GetAll();
        void Add(Actividades actividad);
        void Update(Actividades actividad);
        void Delete(Actividades actividad);
    }
}
