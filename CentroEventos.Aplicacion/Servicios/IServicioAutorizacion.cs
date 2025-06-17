namespace CentroEventos.Aplicacion.Servicios;

public interface IServicioAutorizacion {
    bool Autorizar(string usuario, string accion);
}
