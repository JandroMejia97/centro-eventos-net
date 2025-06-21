using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso
{
    public class PermisoUsuarioAgregarUseCase
    {
        private readonly IRepositorioPermisoUsuario _repo;
        public PermisoUsuarioAgregarUseCase(IRepositorioPermisoUsuario repo)
        {
            _repo = repo;
        }
        public void Ejecutar(PermisoUsuario permiso)
        {
            _repo.Agregar(permiso);
        }
        public void Ejecutar(int usuarioId, IEnumerable<Permiso> permisos)
        {
            _repo.Agregar(usuarioId, permisos);
        }
        public void Ejecutar(IEnumerable<PermisoUsuario> permisos)
        {
            _repo.Agregar(permisos);
        }
    }

    public class PermisoUsuarioEliminarUseCase
    {
        private readonly IRepositorioPermisoUsuario _repo;
        public PermisoUsuarioEliminarUseCase(IRepositorioPermisoUsuario repo)
        {
            _repo = repo;
        }
        public void Ejecutar(int usuarioId, Permiso permiso)
        {
            _repo.Eliminar(usuarioId, permiso);
        }
        public void Ejecutar(PermisoUsuario permisoUsuario)
        {
            _repo.Eliminar(permisoUsuario);
        }
        public void Ejecutar(int usuarioId, IEnumerable<Permiso> permisos)
        {
            _repo.Eliminar(usuarioId, permisos);
        }
        public void Ejecutar(IEnumerable<PermisoUsuario> permisos)
        {
            _repo.Eliminar(permisos);
        }
    }

    public class PermisoUsuarioObtenerUseCase
    {
        private readonly IRepositorioPermisoUsuario _repo;
        public PermisoUsuarioObtenerUseCase(IRepositorioPermisoUsuario repo)
        {
            _repo = repo;
        }
        public PermisoUsuario? Ejecutar(int usuarioId, Permiso permiso)
        {
            return _repo.Obtener(usuarioId, permiso);
        }
    }

    public class PermisoUsuarioObtenerPorUsuarioUseCase
    {
        private readonly IRepositorioPermisoUsuario _repo;
        public PermisoUsuarioObtenerPorUsuarioUseCase(IRepositorioPermisoUsuario repo)
        {
            _repo = repo;
        }
        public IEnumerable<PermisoUsuario> Ejecutar(int usuarioId)
        {
            return _repo.ObtenerPorUsuario(usuarioId);
        }
    }

    public class PermisoUsuarioObtenerTodosUseCase
    {
        private readonly IRepositorioPermisoUsuario _repo;
        public PermisoUsuarioObtenerTodosUseCase(IRepositorioPermisoUsuario repo)
        {
            _repo = repo;
        }
        public IEnumerable<PermisoUsuario> Ejecutar()
        {
            return _repo.ObtenerTodos();
        }
    }
}
