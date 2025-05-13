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
}