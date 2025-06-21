using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso
{
    public abstract class UsuarioUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion) : CasoDeUsoBase(servicioAutorizacion)
    {
        protected readonly IRepositorioUsuario repositorioUsuario = repositorioUsuario;
    }

    public class UsuarioActualizarUseCase(
        IRepositorioUsuario repositorioUsuario,
        IServicioAutorizacion servicioAutorizacion,
        IValidadorUsuario validadorUsuario,
        IServicioHashContrasena servicioHash
    ) : UsuarioUseCase(repositorioUsuario, servicioAutorizacion)
    {
        public void Ejecutar(int usuarioId, Usuario usuario, string? contrasenaPlano = null)
        {
            if (usuarioId != usuario.PersonaId)
                ValidarPermiso(usuarioId, Permiso.EditarUsuario);
            if (contrasenaPlano != null)
            {
                usuario.ContrasenaHash = servicioHash.Hashear(contrasenaPlano);
            }
            validadorUsuario.Validar(usuario);
            var existente = repositorioUsuario.ObtenerPorId(usuario.PersonaId) ?? throw new EntidadNotFoundException("Usuario no encontrado.");
            if (existente.Persona.Email != usuario.Persona.Email && repositorioUsuario.ObtenerPorEmail(usuario.Persona.Email) != null)
                throw new DuplicadoException("Ya existe un usuario con ese email.");
            repositorioUsuario.Modificar(usuario);
        }
    }

    public class UsuarioCrearUseCase(
        IRepositorioUsuario repositorioUsuario,
        IServicioAutorizacion servicioAutorizacion,
        IRepositorioPersona repositorioPersona,
        IValidadorUsuario validadorUsuario,
        IServicioHashContrasena servicioHash
    ) : UsuarioUseCase(repositorioUsuario, servicioAutorizacion)
    {
        public void Ejecutar(Usuario usuario, string contrasenaPlano, int? usuarioId = null)
        {
            if (usuarioId.HasValue)
                ValidarPermiso(usuarioId.Value, Permiso.CrearUsuario);
            if (repositorioPersona.ObtenerPorEmail(usuario.Persona.Email).Any())
                throw new DuplicadoException("Ya existe un usuario con ese email.");
            if (repositorioPersona.ObtenerPorDni(usuario.Persona.DNI).Any())
                throw new DuplicadoException("Ya existe un usuario con ese DNI.");
            validadorUsuario.Validar(usuario);
            usuario.ContrasenaHash = servicioHash.Hashear(contrasenaPlano);
            repositorioUsuario.Agregar(usuario);
        }
    }

    public class UsuarioEliminarUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion) : UsuarioUseCase(repositorioUsuario, servicioAutorizacion)
    {
        public void Ejecutar(int usuarioSolicitanteId, int usuarioId)
        {
            if (usuarioId != usuarioSolicitanteId)
                ValidarPermiso(usuarioSolicitanteId, Permiso.EliminarUsuario);
            if (repositorioUsuario.ObtenerPorId(usuarioId) is null)
                throw new EntidadNotFoundException("Usuario no encontrado.");
            repositorioUsuario.Eliminar(usuarioId);
        }
    }

    public class UsuarioLoginUseCase(IRepositorioUsuario repositorioUsuario, IServicioHashContrasena servicioHash)
    {
        public Usuario Ejecutar(string email, string contrasenaPlano)
        {
            var usuario = repositorioUsuario.ObtenerPorEmail(email) ?? throw new FalloAutorizacionException("Usuario o contraseña incorrectos.");
            var hash = servicioHash.Hashear(contrasenaPlano);
            if (usuario.ContrasenaHash != hash)
                throw new FalloAutorizacionException("Usuario o contraseña incorrectos.");
            return usuario;
        }
    }

    public class UsuarioObtenerPorIdUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion) : UsuarioUseCase(repositorioUsuario, servicioAutorizacion)
    {
        public Usuario Ejecutar(int usuarioSolicitanteId, int usuarioId)
        {
            if (usuarioId != usuarioSolicitanteId)
                ValidarPermiso(usuarioSolicitanteId, Permiso.VerUsuarios);
            return repositorioUsuario.ObtenerPorId(usuarioId) ?? throw new EntidadNotFoundException("Usuario no encontrado.");
        }
    }

    public class UsuarioObtenerTodosUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion) : UsuarioUseCase(repositorioUsuario, servicioAutorizacion)
    {
        public IEnumerable<Usuario> Ejecutar(int usuarioSolicitanteId)
        {
            ValidarPermiso(usuarioSolicitanteId, Permiso.VerUsuarios);
            return repositorioUsuario.ObtenerTodos();
        }
    }

}
