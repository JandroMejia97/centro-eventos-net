namespace CentroEventos.Aplicacion.Entidades;

public enum EstadoAsistencia
{
    Pendiente,
    Presente,
    Ausente
}

public class Reserva
{
    public int PersonaId { get; set; }
    public Persona? Persona { get; set; }
    public int EventoDeportivoId { get; set; }
    public EventoDeportivo? EventoDeportivo { get; set; }
    public DateTime FechaAltaReserva { get; set; }
    public EstadoAsistencia EstadoAsistencia { get; set; }

    public Reserva(int personaId, int eventoDeportivoId)
    {
        if (personaId <= 0) throw new ArgumentException("El ID de la persona debe ser mayor que cero.", nameof(personaId));
        if (eventoDeportivoId <= 0) throw new ArgumentException("El ID del evento deportivo debe ser mayor que cero.", nameof(eventoDeportivoId));
        PersonaId = personaId;
        EventoDeportivoId = eventoDeportivoId;
        FechaAltaReserva = DateTime.Now;
        EstadoAsistencia = EstadoAsistencia.Pendiente;
    }

    public Reserva(int personaId, int eventoDeportivoId, EstadoAsistencia estadoAsistencia)
        : this(personaId, eventoDeportivoId)
    {
        EstadoAsistencia = estadoAsistencia;
    }

    public Reserva(int personaId, int eventoDeportivoId, EstadoAsistencia estadoAsistencia, EventoDeportivo? eventoDeportivo = null, Persona? persona = null)
        : this(personaId, eventoDeportivoId, estadoAsistencia)
    {
        EventoDeportivo = eventoDeportivo;
        Persona = persona;
    }

    public Reserva() { }

    public override string ToString()
    {
        return $"Persona {PersonaId}, Evento {EventoDeportivoId}, Fecha: {FechaAltaReserva}, Estado: {EstadoAsistencia}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Reserva other) return false;
        return PersonaId == other.PersonaId && EventoDeportivoId == other.EventoDeportivoId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PersonaId, EventoDeportivoId);
    }
}