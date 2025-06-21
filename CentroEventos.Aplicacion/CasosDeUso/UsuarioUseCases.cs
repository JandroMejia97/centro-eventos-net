using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases
{
    public abstract class UsuarioUseCase
    {
        protected readonly IRepositorioUsuario repositorioUsuario;
        protected readonly IServicioAutorizacion servicioAutorizacion;

        protected UsuarioUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion)
        {
            this.repositorioUsuario = repositorioUsuario;
            this.servicioAutorizacion = servicioAutorizacion;
        }
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
            if (usuarioId != usuario.PersonaId || !servicioAutorizacion.Autorizar(usuarioId, Permiso.EditarUsuario))
                throw new UnauthorizedAccessException("No se puede actualizar el usuario de otro usuario.");
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
        IValidadorUsuario validadorUsuario,
        IServicioHashContrasena servicioHash
    ) : UsuarioUseCase(repositorioUsuario, servicioAutorizacion)
    {
        public void Ejecutar(Usuario usuario, string contrasenaPlano, int? usuarioId = null)
        {
            if (usuarioId.HasValue && !servicioAutorizacion.Autorizar(usuarioId.Value, Permiso.CrearUsuario))
                throw new UnauthorizedAccessException("El usuario no tiene permisos para crear otros usuarios.");
            if (usuario.PersonaId != 0)
                throw new ArgumentException("El ID de la persona no debe ser especificado al crear un usuario nuevo.");
            if (repositorioUsuario.ObtenerPorEmail(usuario.Persona.Email) is not null)
                throw new DuplicadoException("Ya existe un usuario con ese email.");
            validadorUsuario.Validar(usuario);
            usuario.ContrasenaHash = servicioHash.Hashear(contrasenaPlano);
            repositorioUsuario.Agregar(usuario);
        }
    }

    public class UsuarioEliminarUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion) : UsuarioUseCase(repositorioUsuario, servicioAutorizacion)
    {
        public void Ejecutar(int usuarioSolicitanteId, int usuarioId)
        {
            if (
                usuarioId != usuarioSolicitanteId
                && !servicioAutorizacion.Autorizar(usuarioSolicitanteId, Permiso.EliminarUsuario)
            )
                throw new UnauthorizedAccessException("El usuario no tiene permisos para eliminar otros usuarios.");
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
            if (
                usuarioId != usuarioSolicitanteId
                && !servicioAutorizacion.Autorizar(usuarioSolicitanteId, Permiso.VerUsuarios)
            )
                throw new UnauthorizedAccessException("El usuario no tiene permisos para ver otros usuarios.");
            return repositorioUsuario.ObtenerPorId(usuarioId) ?? throw new EntidadNotFoundException("Usuario no encontrado.");
        }
    }

    public class UsuarioObtenerTodosUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion) : UsuarioUseCase(repositorioUsuario, servicioAutorizacion)
    {
        public IEnumerable<Usuario> Ejecutar(int usuarioSolicitanteId)
        {
            if (!servicioAutorizacion.Autorizar(usuarioSolicitanteId, Permiso.VerUsuarios))
                throw new UnauthorizedAccessException("El usuario no tiene permisos para ver la lista de usuarios.");
            return repositorioUsuario.ObtenerTodos();
        }
    }

}
