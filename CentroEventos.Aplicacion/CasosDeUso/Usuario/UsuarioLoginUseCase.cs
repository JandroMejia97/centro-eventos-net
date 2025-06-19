using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;

public class UsuarioLoginUseCase(IRepositorioUsuario repositorioUsuario, IServicioHashContrasena servicioHash)
{
    public Usuario Ejecutar(string email, string contrasenaPlano) {
        var usuario = repositorioUsuario.ObtenerPorEmail(email) ?? throw new FalloAutorizacionException("Usuario o contraseña incorrectos.");
        var hash = servicioHash.Hashear(contrasenaPlano);
        if (usuario.ContrasenaHash != hash)
            throw new FalloAutorizacionException("Usuario o contraseña incorrectos.");
        return usuario;
    }
}
