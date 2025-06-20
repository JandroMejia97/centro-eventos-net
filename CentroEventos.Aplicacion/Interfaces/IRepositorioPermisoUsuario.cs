using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Interfaces
{
    public interface IRepositorioPermisoUsuario
    {
        void Agregar(PermisoUsuario permiso);
        void Eliminar(int usuarioId, Permiso permiso);
        PermisoUsuario? Obtener(int usuarioId, Permiso permiso);
        IEnumerable<PermisoUsuario> ObtenerPorUsuario(int usuarioId);
        IEnumerable<PermisoUsuario> ObtenerTodos();
    }
}
