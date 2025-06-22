using System.ComponentModel.DataAnnotations.Schema;

namespace CentroEventos.Aplicacion.Entidades;

public class EventoDeportivo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaHoraInicio { get; set; }
    public double DuracionHoras { get; set; }
    public int CupoMaximo { get; set; }
    public int ResponsableId { get; set; }
    public Persona? Responsable { get; set; }
    [NotMapped]
    public int NumeroReservas { get; set; }

    public EventoDeportivo(string nombre, string descripcion, DateTime fechaHoraInicio, double duracionHoras, int cupoMaximo, int responsableId, Persona? responsable = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del evento no puede estar vacío.", nameof(nombre));
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new ArgumentException("La descripción del evento no puede estar vacía.", nameof(descripcion));
        if (duracionHoras <= 0)
            throw new ArgumentOutOfRangeException(nameof(duracionHoras), "La duración del evento debe ser mayor a cero.");
        if (cupoMaximo <= 0)
            throw new ArgumentOutOfRangeException(nameof(cupoMaximo), "El cupo máximo debe ser mayor a cero.");
        if (responsableId <= 0)
            throw new ArgumentOutOfRangeException(nameof(responsableId), "El ID del responsable debe ser mayor a cero.");
        if (fechaHoraInicio < DateTime.Now)
            throw new ArgumentOutOfRangeException(nameof(fechaHoraInicio), "La fecha y hora de inicio del evento no puede ser en el pasado.");
        
        Nombre = nombre;
        Descripcion = descripcion;
        FechaHoraInicio = fechaHoraInicio;
        DuracionHoras = duracionHoras;
        CupoMaximo = cupoMaximo;
        ResponsableId = responsableId;
    }

    public EventoDeportivo() { }

    public override string ToString()
    {
        return $"{Id}: {Nombre} ({Descripcion}) - Inicio: {FechaHoraInicio}, Duración: {DuracionHoras} horas, Cupo: {CupoMaximo}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EventoDeportivo other) return false;
        return Id == other.Id && Nombre == other.Nombre && FechaHoraInicio == other.FechaHoraInicio;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nombre, FechaHoraInicio);
    }
}