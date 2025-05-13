using CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Repositorios.Repositorios;
using CentroEventos.Repositorios.FuentesDeDatos;

var fuenteDeDatosPersona = new FuenteDeDatosCsv<Persona>("data/personas.csv", 
    line => {
        var parts = line.Split(',');
        return new Persona
        {
            Id = int.Parse(parts[0]),
            DNI = parts[1],
            Nombre = parts[2],
            Apellido = parts[3],
            Email = parts[4],
            Telefono = parts[5]
        };
    },
    persona => $"{persona.Id},{persona.DNI},{persona.Nombre},{persona.Apellido},{persona.Email},{persona.Telefono}",
    "data/ids_persona.txt");

var fuenteDeDatosEvento = new FuenteDeDatosCsv<EventoDeportivo>("data/eventos.csv", 
    line => {
        var parts = line.Split(',');
        return new EventoDeportivo
        {
            Id = int.Parse(parts[0]),
            Nombre = parts[1],
            Descripcion = parts[2],
            FechaHoraInicio = DateTime.Parse(parts[3]),
            DuracionHoras = double.Parse(parts[4]),
            CupoMaximo = int.Parse(parts[5]),
            ResponsableId = int.Parse(parts[6])
        };
    },
    evento => $"{evento.Id},{evento.Nombre},{evento.Descripcion},{evento.FechaHoraInicio},{evento.DuracionHoras},{evento.CupoMaximo},{evento.ResponsableId}",
    "data/ids_evento.txt");

var fuenteDeDatosReserva = new FuenteDeDatosCsv<Reserva>("data/reservas.csv", 
    line => {
        var parts = line.Split(',');
        return new Reserva
        {
            Id = int.Parse(parts[0]),
            PersonaId = int.Parse(parts[1]),
            EventoDeportivoId = int.Parse(parts[2]),
            FechaAltaReserva = DateTime.Parse(parts[3]),
            EstadoAsistencia = Enum.Parse<EstadoAsistencia>(parts[4])
        };
    },
    reserva => $"{reserva.Id},{reserva.PersonaId},{reserva.EventoDeportivoId},{reserva.FechaAltaReserva},{reserva.EstadoAsistencia}",
    "data/ids_reserva.txt");

var repositorioPersona = new RepositorioPersona(fuenteDeDatosPersona);
var repositorioEvento = new RepositorioEventoDeportivo(fuenteDeDatosEvento);
var repositorioReserva = new RepositorioReserva(fuenteDeDatosReserva);

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