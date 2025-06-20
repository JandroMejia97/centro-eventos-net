using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;

public class UsuarioCrearUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IValidadorUsuario _validadorUsuario;
    private readonly IServicioHashContrasena _servicioHash;

    public UsuarioCrearUseCase(IRepositorioUsuario repositorioUsuario, IValidadorUsuario validadorUsuario, IServicioHashContrasena servicioHash)
    {
        _repositorioUsuario = repositorioUsuario;
        _validadorUsuario = validadorUsuario;
        _servicioHash = servicioHash;
    }

    public void Ejecutar(Usuario usuario, string contrasenaPlano)
    {
        if (_repositorioUsuario.ObtenerPorEmail(usuario.Persona.Email) is not null)
            throw new DuplicadoException("Ya existe un usuario con ese email.");
        usuario.ContrasenaHash = _servicioHash.Hashear(contrasenaPlano);
        _validadorUsuario.Validar(usuario);
        _repositorioUsuario.Agregar(usuario);
    }
}
