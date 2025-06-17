namespace CentroEventos.Aplicacion.Validadores;

using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;

public class ValidadorUsuario : IValidadorUsuario {
    public void Validar(Usuario usuario) {
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
            throw new ValidacionException("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(usuario.Apellido))
            throw new ValidacionException("El apellido es obligatorio.");
        if (string.IsNullOrWhiteSpace(usuario.ContrasenaHash))
            throw new ValidacionException("La contraseña es obligatoria.");
    }
}
