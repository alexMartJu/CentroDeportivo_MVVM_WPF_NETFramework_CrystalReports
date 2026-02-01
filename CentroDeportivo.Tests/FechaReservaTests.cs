using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Views;

namespace CentroDeportivo.Tests;

[TestClass]
public class FechaReservaTests
{
    //FechaAyer_DebeSerInvalida() --> Verifica que una reserva no pueda hacerse para una fecha pasada.
    [TestMethod]
    public void FechaAyer_DebeSerInvalida()
    {
        //Creamos una reserva con fecha de ayer
        var reserva = new Reservas { Fecha = DateTime.Today.AddDays(-1) };
        //Instanciamos el ViewModel para usar el método de validación
        var vm = new ReservasViewModel();

        //Validamos la fecha de la reserva
        bool resultado = vm.EsFechaValida(reserva.Fecha);

        //La fecha de ayer no debe ser válida, por lo que el resultado debe ser false
        Assert.IsFalse(resultado);
    }


    //FechaAyer_DebeSerValida() --> Verifica que una reserva pueda hacerse para la fecha actual.
    [TestMethod]
    public void FechaAyer_DebeSerValida()
    {
        //Creamos una reserva con fecha de hoy
        var reserva = new Reservas { Fecha = DateTime.Today }; 
        var vm = new ReservasViewModel(); 
        
        bool resultado = vm.EsFechaValida(reserva.Fecha);

        //La fecha de hoy debe ser válida, por lo que el resultado debe ser true
        Assert.IsTrue(resultado);
    }
}
