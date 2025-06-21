using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Interfaces;
public interface IFuenteDeDatosPermisoUsuario
{
    void Agregar(PermisoUsuario permisoUsuario);
    void Agregar(int usuarioId, IEnumerable<Permiso> permisos);
    void Agregar(IEnumerable<PermisoUsuario> permisos);
    void Eliminar(int usuarioId, Permiso permiso);
    void Eliminar(PermisoUsuario permiso);
    void Eliminar(int usuarioId, IEnumerable<Permiso> permisos);
    void Eliminar(IEnumerable<PermisoUsuario> permisos);
    PermisoUsuario? Obtener(int usuarioId, Permiso permiso);
    IEnumerable<PermisoUsuario> ObtenerPorUsuarioId(int usuarioId);
    IEnumerable<PermisoUsuario> ObtenerTodos();
}
