using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Views;

namespace CentroDeportivo.Tests;

[TestClass]
public class EmailTests
{
    //EmailValido_DebeSerTrue() --> Verifica que un email con formato correcto sea considerado válido.
    [TestMethod]
    public void EmailValido_DebeSerTrue()
    {
        //Creamos un socio con un email válido
        var socio = new Socios { Email = "usuario@dominio.com" };
        //Instanciamos el ViewModel para usar el método de validación
        var vm = new SociosViewModel();

        //Validamos el email del socio
        bool resultado = vm.EsEmailValido(socio.Email);

        //El email es válido, por lo que el resultado debe ser true
        Assert.IsTrue(resultado);

    }

    //EmailInvalido_DebeSerFalse() --> Verifica que un email sin formato correcto sea considerado inválido.
    [TestMethod]
    public void EmailValido_DebeSerFalse()
    {
        //Creamos un socio con un email inválido
        var socio = new Socios { Email = "usuario.com" }; 
        var vm = new SociosViewModel(); 

        bool resultado = vm.EsEmailValido(socio.Email);

        //El email es inválido, por lo que el resultado debe ser false
        Assert.IsFalse(resultado);

    }
}
