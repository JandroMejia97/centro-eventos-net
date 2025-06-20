namespace CentroEventos.Aplicacion.Entidades;

public class Persona
{
    public int Id { get; set; }
    public string DNI { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;

    public string NombreCompleto => $"{Nombre} {Apellido}";

    public Persona(string dni, string nombre, string apellido, string email, string telefono)
    {
        if (string.IsNullOrWhiteSpace(dni))
            throw new ArgumentException("El DNI no puede estar vacío.", nameof(dni));
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
        if (string.IsNullOrWhiteSpace(apellido))
            throw new ArgumentException("El apellido no puede estar vacío.", nameof(apellido));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("El email debe ser válido.", nameof(email));
        if (string.IsNullOrWhiteSpace(telefono))
            throw new ArgumentException("El teléfono no puede estar vacío.", nameof(telefono));

        DNI = dni;
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        Telefono = telefono;
    }

    public Persona() { }

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
}