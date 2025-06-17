using System.Globalization;

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

    public static EventoDeportivo DesdeCsv(string line)
    {
        var parts = line.Split(',');
        return new EventoDeportivo
        {
            Id = int.Parse(parts[0]),
            Nombre = parts[1],
            Descripcion = parts[2],
            FechaHoraInicio = DateTime.ParseExact(parts[3], "d/M/yyyy H:mm:ss", CultureInfo.InvariantCulture),
            DuracionHoras = double.Parse(parts[4]),
            CupoMaximo = int.Parse(parts[5]),
            ResponsableId = int.Parse(parts[6])
        };
    }

    public string ACsv()
    {
        return $"{Id},{Nombre},{Descripcion},{FechaHoraInicio.ToString("d/M/yyyy H:mm:ss")},{DuracionHoras},{CupoMaximo},{ResponsableId}";
    }
}