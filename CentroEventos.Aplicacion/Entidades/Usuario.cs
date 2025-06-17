namespace CentroEventos.Aplicacion.Entidades;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ContrasenaHash { get; set; } = string.Empty;
    public List<string> Permisos { get; set; } = new();

    public override string ToString()
    {
        return $"{Id}: {Nombre} {Apellido} (Email: {Email})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Usuario other) return false;
        return Id == other.Id && Email == other.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Email);
    }
}
