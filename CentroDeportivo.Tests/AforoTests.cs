using CentroDeportivo.Model;
using CentroDeportivo.ViewModel.Services;

namespace CentroDeportivo.Tests;

/// <summary>
/// Clase de pruebas para validar el control de aforo en reservas
/// </summary>
[TestClass]
public class AforoTests
{
    /// <summary>
    /// AforoMaximo_NoPermiteSegundaReserva() --> Verifica que no se puedan hacer reservas si el aforo máximo de la actividad ya está completo.
    /// </summary>
    [TestMethod]
    public void AforoMaximo_NoPermiteSegundaReserva()
    {
        //Servicios necesarios para la prueba
        var reservaService = new ReservaService(); 
        var actividadService = new ActividadService(); 
        var socioService = new SocioService();

        //Declaramos las variables fuera del bloque try para poder acceder a ellas en el bloque finally
        Socios socio = null; 
        Actividades actividad = null; 
        Reservas r1 = null;

        try
        {
            //Creamos 1 socio de prueba
            socio = new Socios { Nombre = "Test", Email = "test@test.com", Activo = true }; 
            socioService.Add(socio);

            //Creamos una actividad con aforo máximo 1
            actividad = new Actividades { Nombre = "TestAct", AforoMaximo = 1 }; 
            actividadService.Add(actividad);

            //Creamos la primera reserva, que debe ser exitosa
            r1 = new Reservas 
            { 
                IdSocio = socio.IdSocio, 
                IdActividad = actividad.IdActividad, 
                Fecha = DateTime.Today 
            }; 
            reservaService.Add(r1);

            //Intentamos crear una segunda reserva para la misma actividad y fecha
            //En vez de crearla directamente, comprobamos que no hay aforo disponible para una segunda reserva
            bool hayAforo = reservaService.HayAforoDisponible(actividad.IdActividad, DateTime.Today);

            //Como el aforo máximo es 1 y ya hay una reserva, no debe haber aforo disponible y debe ser false
            Assert.IsFalse(hayAforo);
        }
        finally
        {
            //Limpiamos los datos de prueba creados
            if (r1 != null) 
                reservaService.Delete(r1); 
            
            if (actividad != null) 
                actividadService.Delete(actividad); 
            
            if (socio != null) 
                socioService.Delete(socio);

            //Mensaje de limpieza
            Console.WriteLine("Limpieza ejecutada");
        }
    }
}
