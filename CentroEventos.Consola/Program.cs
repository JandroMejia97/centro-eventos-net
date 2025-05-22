using CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Repositorios.Repositorios;
using CentroEventos.Repositorios.FuentesDeDatos;

var fuenteDeDatosPersona = new FuenteDeDatosCsv<Persona>("data/personas.csv", 
    line => {
        if (string.IsNullOrWhiteSpace(line)) 
            throw new Exception("Línea vacía o nula en personas.csv");

        var parts = line.Split(',');
        if (parts.Length < 6)
            throw new Exception("Formato incorrecto en personas.csv");

        return new Persona
        {
            Id = int.Parse(parts[0]),
            DNI = parts[1] ?? throw new Exception("DNI nulo"),
            Nombre = parts[2] ?? throw new Exception("Nombre nulo"),
            Apellido = parts[3] ?? throw new Exception("Apellido nulo"),
            Email = parts[4] ?? throw new Exception("Email nulo"),
            Telefono = parts[5] ?? throw new Exception("Teléfono nulo")
        };
    },
    persona => $"{persona.Id},{persona.DNI},{persona.Nombre},{persona.Apellido},{persona.Email},{persona.Telefono}",
    "data/ids_persona.txt");

var fuenteDeDatosEvento = new FuenteDeDatosCsv<EventoDeportivo>("data/eventos.csv", 
    line => {
        if (string.IsNullOrWhiteSpace(line)) 
            throw new Exception("Línea vacía o nula en eventos.csv");

        var parts = line.Split(',');
        if (parts.Length < 7)
            throw new Exception("Formato incorrecto en eventos.csv");

        return new EventoDeportivo
        {
            Id = int.Parse(parts[0]),
            Nombre = parts[1] ?? throw new Exception("Nombre nulo"),
            Descripcion = parts[2] ?? throw new Exception("Descripción nula"),
            FechaHoraInicio = DateTime.ParseExact(parts[3] ?? throw new Exception("Fecha nula"), "d/M/yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
            DuracionHoras = double.Parse(parts[4] ?? throw new Exception("Duración nula")),
            CupoMaximo = int.Parse(parts[5] ?? throw new Exception("Cupo nulo")),
            ResponsableId = int.Parse(parts[6] ?? throw new Exception("Responsable nulo"))
        };
    },
    evento => $"{evento.Id},{evento.Nombre},{evento.Descripcion},{evento.FechaHoraInicio.ToString("d/M/yyyy H:mm:ss")},{evento.DuracionHoras},{evento.CupoMaximo},{evento.ResponsableId}",
    "data/ids_evento.txt");


var fuenteDeDatosReserva = new FuenteDeDatosCsv<Reserva>("data/reservas.csv", 
    line => {
        if (string.IsNullOrWhiteSpace(line)) 
            throw new Exception("Línea vacía o nula en reservas.csv");

        var parts = line.Split(',');
        if (parts.Length < 5)
            throw new Exception("Formato incorrecto en reservas.csv");

        return new Reserva
        {
            Id = int.Parse(parts[0]),
            PersonaId = int.Parse(parts[1]),
            EventoDeportivoId = int.Parse(parts[2]),
            FechaAltaReserva = DateTime.ParseExact(parts[3] ?? throw new Exception("Fecha nula"), "d/M/yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
            EstadoAsistencia = Enum.Parse<EstadoAsistencia>(parts[4] ?? throw new Exception("Estado nulo"))
        };
    },
    reserva => $"{reserva.Id},{reserva.PersonaId},{reserva.EventoDeportivoId},{reserva.FechaAltaReserva.ToString("d/M/yyyy H:mm:ss")},{reserva.EstadoAsistencia}",
    "data/ids_reserva.txt");


var repositorioPersona = new RepositorioPersona(fuenteDeDatosPersona);
var repositorioEvento = new RepositorioEventoDeportivo(fuenteDeDatosEvento);
var repositorioReserva = new RepositorioReserva(fuenteDeDatosReserva);
var reservaAltaUseCase = new ReservaAltaUseCase(repositorioReserva, repositorioEvento, repositorioPersona);

// ========== MENÚ INTERACTIVO ==========
bool salir = false;

while (!salir)
{
    Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
    Console.WriteLine("1. Crear nueva persona");
    Console.WriteLine("2. Crear nuevo evento");
    Console.WriteLine("3. Crear nueva reserva");
    Console.WriteLine("4. Ver todas las reservas");
    Console.WriteLine("5. Salir");
    Console.Write("Seleccione una opción: ");
    var opcion = Console.ReadLine() ?? string.Empty;  // <-- Validación para evitar null

    switch (opcion)
    {
        case "1":
            Console.Write("DNI: ");
            var dni = Console.ReadLine() ?? string.Empty;
            Console.Write("Nombre: ");
            var nombre = Console.ReadLine() ?? string.Empty;
            Console.Write("Apellido: ");
            var apellido = Console.ReadLine() ?? string.Empty;
            Console.Write("Email: ");
            var email = Console.ReadLine() ?? string.Empty;
            Console.Write("Teléfono: ");
            var telefono = Console.ReadLine() ?? string.Empty;

            var nuevaPersona = new Persona
            {
                DNI = dni,
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Telefono = telefono
            };
            repositorioPersona.Agregar(nuevaPersona);
            Console.WriteLine("✅ Persona creada correctamente.");
            break;

        case "2":
            Console.Write("Nombre del evento: ");
            var nombreEvento = Console.ReadLine() ?? string.Empty;
            Console.Write("Descripción: ");
            var descripcion = Console.ReadLine() ?? string.Empty;
            Console.Write("Fecha y hora de inicio (d/M/yyyy H:mm:ss): ");
            var fechaStr = Console.ReadLine() ?? string.Empty;
            Console.Write("Duración en horas: ");
            var duracionStr = Console.ReadLine() ?? string.Empty;
            Console.Write("Cupo máximo: ");
            var cupoStr = Console.ReadLine() ?? string.Empty;

            var personas = repositorioPersona.ObtenerTodos();
            if (!personas.Any())
            {
                Console.WriteLine("❌ No hay personas registradas. Cree una persona primero.");
                break;
            }

            Console.WriteLine("Seleccione el ID del responsable:");
            foreach (var p in personas)
                Console.WriteLine($"{p.Id}: {p.Nombre} {p.Apellido}");

            var responsableIdStr = Console.ReadLine() ?? string.Empty;

            try
            {
                var evento = new EventoDeportivo
                {
                    Nombre = nombreEvento,
                    Descripcion = descripcion,
                    FechaHoraInicio = DateTime.ParseExact(fechaStr, "d/M/yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    DuracionHoras = double.Parse(duracionStr),
                    CupoMaximo = int.Parse(cupoStr),
                    ResponsableId = int.Parse(responsableIdStr)
                };
                repositorioEvento.Agregar(evento);
                Console.WriteLine("✅ Evento creado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"❌ Error al crear el evento: {e.Message}");
            }
            break;

        case "3":
            var personasDisponibles = repositorioPersona.ObtenerTodos().ToList();
            var eventosDisponibles = repositorioEvento.ObtenerTodos().ToList();

            if (!personasDisponibles.Any() || !eventosDisponibles.Any())
            {
                Console.WriteLine("❌ Debe haber al menos una persona y un evento registrados.");
                break;
            }

            Console.WriteLine("Seleccione una persona por ID:");
            foreach (var p in personasDisponibles)
                Console.WriteLine($"{p.Id}: {p.Nombre} {p.Apellido}");

            Console.WriteLine("Seleccione un evento por ID:");
            foreach (var e in eventosDisponibles)
                Console.WriteLine($"{e.Id}: {e.Nombre} ({e.FechaHoraInicio.ToString("d/M/yyyy H:mm:ss")})");

            Console.Write("ID Persona: ");
            var personaIdStr = Console.ReadLine() ?? string.Empty;
            Console.Write("ID Evento: ");
            var eventoIdStr = Console.ReadLine() ?? string.Empty;

            try
            {
                reservaAltaUseCase.Ejecutar(int.Parse(personaIdStr), int.Parse(eventoIdStr));
                Console.WriteLine("✅ Reserva creada exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear la reserva: {ex.Message}");
            }
            break;

        case "4":
            var reservas = repositorioReserva.ObtenerTodos();
            if (!reservas.Any())
            {
                Console.WriteLine("No hay reservas registradas.");
                break;
            }

            Console.WriteLine("\n📋 Reservas registradas:");
            foreach (var r in reservas)
            {
                Console.WriteLine($"ID: {r.Id}, Persona: {r.PersonaId}, Evento: {r.EventoDeportivoId}, Fecha: {r.FechaAltaReserva.ToString("d/M/yyyy H:mm:ss")}, Estado: {r.EstadoAsistencia}");
            }
            break;

        case "5":
            salir = true;
            Console.WriteLine("👋 Hasta luego.");
            break;

        default:
            Console.WriteLine("❌ Opción inválida.");
            break;
    }
}
