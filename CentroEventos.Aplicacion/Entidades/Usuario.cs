using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Entidades;

public class Usuario
{
    public int PersonaId { get; set; }
    public Persona Persona { get; set; } = null!;
    public string ContrasenaHash { get; set; } = string.Empty;

    public List<PermisoUsuario> PermisosUsuarios
    {
        get;
        set;
    } = new();
    public IEnumerable<Permiso> Permisos { get => PermisosUsuarios.Select(pu => pu.Permiso); }

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

    public Usuario(Persona persona, string contrasenaHash, IEnumerable<PermisoUsuario> permisos)
        : this(persona, contrasenaHash)
    {
        PermisosUsuarios = permisos.ToList() ?? [];
    }

    protected Usuario() { }

    public override string ToString()
    {
        return $"{PersonaId}: {Persona.Nombre} {Persona.Apellido} (DNI: {Persona.DNI}, Email: {Persona.Email})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Usuario other) return false;
        return PersonaId == other.PersonaId && Persona.Equals(other.Persona);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PersonaId, Persona);
    }
}
