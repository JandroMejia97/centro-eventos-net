using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;

public class UsuarioActualizarUseCase(IRepositorioUsuario repositorioUsuario, IValidadorUsuario validadorUsuario)
{
    public void Ejecutar(Usuario usuario) {
        validadorUsuario.Validar(usuario);
        var existente = repositorioUsuario.ObtenerPorId(usuario.Id) ?? throw new EntidadNotFoundException("Usuario no encontrado.");
        if (repositorioUsuario.ObtenerTodos().Any(u => u.Email == usuario.Email && u.Id != usuario.Id))
            throw new DuplicadoException("Ya existe un usuario con ese email.");
        repositorioUsuario.Modificar(usuario);
    }
}
