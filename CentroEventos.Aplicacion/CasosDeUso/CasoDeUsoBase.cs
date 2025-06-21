using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso;

public abstract class CasoDeUsoBase(IServicioAutorizacion servicioAutorizacion)
{
    protected readonly IServicioAutorizacion _servicioAutorizacion = servicioAutorizacion;

    protected void ValidarPermiso(int usuarioId, Permiso permiso)
    {
        if (!_servicioAutorizacion.Autorizar(usuarioId, permiso))
            throw new UnauthorizedAccessException($"El usuario no tiene permisos para realizar la acción: {permiso}");
    }
}