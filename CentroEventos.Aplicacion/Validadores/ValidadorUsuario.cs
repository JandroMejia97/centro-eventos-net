namespace CentroEventos.Aplicacion.Validadores;

using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;

public class ValidadorUsuario : IValidadorUsuario {
    private readonly IValidadorPersona _validadorPersona;
    public ValidadorUsuario(IValidadorPersona validadorPersona) {
        _validadorPersona = validadorPersona;
    }
    public void Validar(Usuario usuario) {
        if (usuario.Persona == null)
            throw new ValidacionException("El usuario debe tener una persona asociada.");
        _validadorPersona.Validar(usuario.Persona);
        if (string.IsNullOrWhiteSpace(usuario.ContrasenaHash))
            throw new ValidacionException("La contraseña es obligatoria.");
        if (usuario.ContrasenaHash.Length < 8)
            throw new ValidacionException("La contraseña debe tener al menos 8 caracteres.");
    }
}
