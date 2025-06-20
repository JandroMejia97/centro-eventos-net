using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Servicios;

public interface IServicioAutorizacion {
    bool Autorizar(int usuarioId, Permiso accion);
}
