using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Interfaces
{
    public interface IRepositorioPermisoUsuario
    {
        void Agregar(PermisoUsuario permisoUsuario);
        void Agregar(int usuarioId, IEnumerable<Permiso> permisos);
        void Agregar(IEnumerable<PermisoUsuario> permisos);
        void Eliminar(int usuarioId, Permiso permiso);
        void Eliminar(PermisoUsuario permisoUsuario);
        void Eliminar(int usuarioId, IEnumerable<Permiso> permisos);
        void Eliminar(IEnumerable<PermisoUsuario> permisos);
        PermisoUsuario? Obtener(int usuarioId, Permiso permiso);
        IEnumerable<PermisoUsuario> ObtenerPorUsuario(int usuarioId);
        IEnumerable<PermisoUsuario> ObtenerTodos();
    }
}
