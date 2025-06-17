namespace CentroEventos.Aplicacion.Validadores;

using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;

public class ValidadorPersona : IValidadorPersona {
    public void Validar(Persona persona) {
        if (string.IsNullOrWhiteSpace(persona.DNI))
            throw new ValidacionException("El DNI es obligatorio.");
        if (string.IsNullOrWhiteSpace(persona.Nombre))
            throw new ValidacionException("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(persona.Apellido))
            throw new ValidacionException("El apellido es obligatorio.");
        if (string.IsNullOrWhiteSpace(persona.Email))
            throw new ValidacionException("El email es obligatorio.");
        if (!persona.Email.Contains("@"))
            throw new ValidacionException("El email no es válido.");
        if (persona.DNI.Length < 7)
            throw new ValidacionException("El DNI debe tener al menos 7 dígitos.");
    }
}
