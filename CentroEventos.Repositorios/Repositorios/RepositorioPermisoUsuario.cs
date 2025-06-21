using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using System.Linq;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioPermisoUsuario : IRepositorioPermisoUsuario
    {
        private readonly IFuenteDeDatosPermisoUsuario _fuente;
        public RepositorioPermisoUsuario(IFuenteDeDatosPermisoUsuario fuente)
        {
            _fuente = fuente;
        }
        public void Agregar(PermisoUsuario permisoUsuario) => _fuente.Agregar(permisoUsuario);
        public void Agregar(int usuarioId, IEnumerable<Permiso> permisos) => 
            _fuente.Agregar(usuarioId, permisos);
        public void Agregar(IEnumerable<PermisoUsuario> permisos) =>
            _fuente.Agregar(permisos);

        public void Eliminar(int usuarioId, Permiso permiso) => _fuente.Eliminar(usuarioId, permiso);
        public void Eliminar(PermisoUsuario permisoUsuario) => _fuente.Eliminar(permisoUsuario);
        public void Eliminar(int usuarioId, IEnumerable<Permiso> permisos) =>
            _fuente.Eliminar(usuarioId, permisos);
        public void Eliminar(IEnumerable<PermisoUsuario> permisos) =>
            _fuente.Eliminar(permisos);
        public PermisoUsuario? Obtener(int usuarioId, Permiso permiso) =>
            _fuente.Obtener(usuarioId, permiso);
        public IEnumerable<PermisoUsuario> ObtenerPorUsuario(int usuarioId) => _fuente.ObtenerPorUsuarioId(usuarioId);
        public IEnumerable<PermisoUsuario> ObtenerTodos() => _fuente.ObtenerTodos();
    }
}
