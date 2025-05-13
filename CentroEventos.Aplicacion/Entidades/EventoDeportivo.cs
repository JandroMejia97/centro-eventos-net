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