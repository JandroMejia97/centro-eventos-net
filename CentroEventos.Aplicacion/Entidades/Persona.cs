using System.Globalization;

namespace CentroEventos.Aplicacion.Entidades;

public class Persona
{
    public int Id { get; set; }
    public string DNI { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Id}: {Nombre} {Apellido} (DNI: {DNI}, Email: {Email}, Tel: {Telefono})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Persona other) return false;
        return Id == other.Id && DNI == other.DNI && Email == other.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, DNI, Email);
    }

    public static Persona DesdeCsv(string line)
    {
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
    }

    public string ACsv()
    {
        return $"{Id},{DNI},{Nombre},{Apellido},{Email},{Telefono}";
    }
}