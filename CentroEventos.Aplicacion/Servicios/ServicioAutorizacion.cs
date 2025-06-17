using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.Servicios;

public class ServicioAutorizacion : IServicioAutorizacion {
    public bool Autorizar(string usuario, string accion) {
        // Ejemplo: solo el usuario 'admin' puede eliminar
        if (accion == "eliminar" && usuario != "admin")
            throw new FalloAutorizacionException("No tiene permisos para eliminar.");
        return true;
    }
}
