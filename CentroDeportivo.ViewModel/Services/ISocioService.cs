using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroDeportivo.Model;

namespace CentroDeportivo.ViewModel.Services
{
    //Interfaz para las operaciones de socios
    public interface ISocioService
    {
        IEnumerable<Socios> GetAll();
        void Add(Socios socio);
        void Update(Socios socio);
        void Delete(Socios socio);
    }
}
