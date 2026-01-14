using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroDeportivo.ViewModel
{
    //Clase base para todos los ViewModels 
    //Implementa INotifyPropertyChanged para avisar a la vista cuando cambia una propiedad
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Método que lanza el evento de cambio de propiedad
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
