using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso.PermisoUsuarioUseCases
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
