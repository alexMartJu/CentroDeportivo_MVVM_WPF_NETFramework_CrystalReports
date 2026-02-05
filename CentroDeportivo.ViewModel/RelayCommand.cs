using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CentroDeportivo.ViewModel
{
    /// <summary>
    /// Comando genérico reutilizable para MVVM que implementa ICommand
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Constructor: recibe la acción a ejecutar y la condición opcional
        /// </summary>
        /// <param name="execute">Acción a ejecutar cuando se invoca el comando</param>
        /// <param name="canExecute">Función opcional que determina si el comando puede ejecutarse</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Indica si el comando se puede ejecutar
        /// </summary>
        /// <param name="parameter">Parámetro del comando</param>
        /// <returns>True si el comando puede ejecutarse, false en caso contrario</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Acción que se ejecuta al lanzar el comando
        /// </summary>
        /// <param name="parameter">Parámetro del comando</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Eventos que WPF escucha para reevaluar el estado del comando (habilitado/deshabilitado).
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
