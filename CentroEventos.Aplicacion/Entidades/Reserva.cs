using System.Globalization;

namespace CentroEventos.Aplicacion.Entidades;

public enum EstadoAsistencia
{
    Pendiente,
    Presente,
    Ausente
}

public class Reserva
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public int EventoDeportivoId { get; set; }
    public DateTime FechaAltaReserva { get; set; }
    public EstadoAsistencia EstadoAsistencia { get; set; }

    public override string ToString()
    {
        return $"{Id}: Persona {PersonaId}, Evento {EventoDeportivoId}, Fecha: {FechaAltaReserva}, Estado: {EstadoAsistencia}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Reserva other) return false;
        return Id == other.Id && PersonaId == other.PersonaId && EventoDeportivoId == other.EventoDeportivoId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, PersonaId, EventoDeportivoId);
    }

    public static Reserva DesdeCsv(string line)
    {
        var parts = line.Split(',');
        return new Reserva
        {
            Id = int.Parse(parts[0]),
            PersonaId = int.Parse(parts[1]),
            EventoDeportivoId = int.Parse(parts[2]),
            FechaAltaReserva = DateTime.ParseExact(parts[3], "d/M/yyyy H:mm:ss", CultureInfo.InvariantCulture),
            EstadoAsistencia = Enum.Parse<EstadoAsistencia>(parts[4])
        };
    }

    public string ACsv()
    {
        return $"{Id},{PersonaId},{EventoDeportivoId},{FechaAltaReserva.ToString("d/M/yyyy H:mm:ss")},{EstadoAsistencia}";
    }
}