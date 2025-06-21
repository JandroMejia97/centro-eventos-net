using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Servicios;

public interface IServicioCacheDePermisos
{
    void Agregar(int usuarioId, Permiso permiso);
    void Agregar(int usuarioId, IEnumerable<Permiso> permisos);
    void Eliminar(int usuarioId);
    void Eliminar(int usuarioId, Permiso permiso);
    void Eliminar(int usuarioId, IEnumerable<Permiso> permisos);
}