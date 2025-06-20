using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.Servicios;

public class ServicioAutorizacion : IServicioAutorizacion {
    private readonly IRepositorioPermisoUsuario _repoPermiso;
    private readonly IRepositorioUsuario _repoUsuario;
    public ServicioAutorizacion(IRepositorioPermisoUsuario repoPermiso, IRepositorioUsuario repoUsuario)
    {
        _repoPermiso = repoPermiso;
        _repoUsuario = repoUsuario;
    }
    public bool Autorizar(int idUsuario, Permiso accion) {
        _ = _repoUsuario.ObtenerPorId(idUsuario) ?? throw new FalloAutorizacionException("Usuario no encontrado.");
        _ = _repoPermiso.Obtener(idUsuario, accion) ?? throw new FalloAutorizacionException($"No tiene el permiso requerido: {accion}.");
        return true;
    }
}
