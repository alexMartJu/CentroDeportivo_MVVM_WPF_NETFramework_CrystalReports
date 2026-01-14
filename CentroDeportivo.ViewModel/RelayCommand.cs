using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CentroDeportivo.ViewModel
{
    //Comando genérico reutilizable para MVVM
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        //Constructor: recibe la acción a ejecutar y la condición opcional
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        //Indica si el comando se puede ejecutar
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        //Acción que se ejecuta al lanzar el comando
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        //Eventos que WPF escucha para reevaluar el estado del comando (habilitado/deshabilitado).
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
