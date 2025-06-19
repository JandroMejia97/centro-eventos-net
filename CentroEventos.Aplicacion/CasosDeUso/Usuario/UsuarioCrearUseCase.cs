using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;

public class UsuarioCrearUseCase(IRepositorioUsuario repositorioUsuario, IValidadorUsuario validadorUsuario)
{
    public void Ejecutar(Usuario usuario) {
        validadorUsuario.Validar(usuario);
        if (repositorioUsuario.ObtenerTodos().Any(u => u.Persona.Email == usuario.Persona.Email))
            throw new DuplicadoException("Ya existe un usuario con ese email.");
        repositorioUsuario.Agregar(usuario);
    }
}
