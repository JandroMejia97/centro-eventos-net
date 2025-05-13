using CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Repositorios.Repositorios;

var repositorioPersona = new RepositorioPersona();
var repositorioEvento = new RepositorioEventoDeportivo();
var repositorioReserva = new RepositorioReserva();

var reservaAltaUseCase = new ReservaAltaUseCase(repositorioReserva, repositorioEvento, repositorioPersona);

// Crear datos iniciales
var persona = new Persona { DNI = "12345678", Nombre = "Juan", Apellido = "Pérez", Email = "juan.perez@example.com", Telefono = "123456789" };
repositorioPersona.Agregar(persona);

var evento = new EventoDeportivo
{
    Nombre = "Fútbol 5",
    Descripcion = "Partido amistoso de fútbol 5",
    FechaHoraInicio = DateTime.Now.AddDays(1),
    DuracionHoras = 2,
    CupoMaximo = 10,
    ResponsableId = persona.Id
};
repositorioEvento.Agregar(evento);

// Probar caso de uso
try
{
    reservaAltaUseCase.Ejecutar(persona.Id, evento.Id);
    Console.WriteLine("Reserva creada exitosamente.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error al crear la reserva: {ex.Message}");
}

// Mostrar reservas
var reservas = repositorioReserva.ObtenerTodos();
foreach (var reserva in reservas)
{
    Console.WriteLine(reserva);
}