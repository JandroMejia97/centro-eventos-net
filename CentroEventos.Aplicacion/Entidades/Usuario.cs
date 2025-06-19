namespace CentroEventos.Aplicacion.Entidades;

public class Usuario
{
    public int Id { get; set; }
    public Persona Persona { get; set; } = null!;
    public string ContrasenaHash { get; set; } = string.Empty;
    public List<string> Permisos { get; set; } = new();

    public Usuario(Persona persona)
    {
        Persona = persona ?? throw new ArgumentNullException(nameof(persona));
    }

    public Usuario(Persona persona, string contrasenaHash): this(persona)
    {
        if (string.IsNullOrWhiteSpace(contrasenaHash))
            throw new ArgumentException("La contraseña no puede estar vacía.", nameof(contrasenaHash));
        if (contrasenaHash.Length < 8)
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.", nameof(contrasenaHash));
        ContrasenaHash = contrasenaHash;
    }

    protected Usuario() { }

    public override string ToString()
    {
        return $"{Id}: {Persona.Nombre} {Persona.Apellido} (DNI: {Persona.DNI}, Email: {Persona.Email})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Usuario other) return false;
        return Id == other.Id && Persona.Equals(other.Persona);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Persona);
    }
}
