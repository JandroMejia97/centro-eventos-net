using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Interfaces;
public interface IFuenteDeDatosPermisoUsuario
{
    void Agregar(PermisoUsuario permisoUsuario);
    void Eliminar(int usuarioId, Permiso permiso);
    PermisoUsuario? Obtener(int usuarioId, Permiso permiso);
    IEnumerable<PermisoUsuario> ObtenerPorUsuarioId(int usuarioId);
    IEnumerable<PermisoUsuario> ObtenerTodos();
}
