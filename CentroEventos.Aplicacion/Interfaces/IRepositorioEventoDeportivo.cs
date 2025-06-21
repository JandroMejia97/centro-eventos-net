namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IRepositorioEventoDeportivo
{
    void Agregar(EventoDeportivo evento);
    void Modificar(EventoDeportivo evento);
    void Eliminar(int id);
    EventoDeportivo? ObtenerPorId(int id);
    IEnumerable<EventoDeportivo> ObtenerTodos();
    IEnumerable<EventoDeportivo> ObtenerPorFechaYDuracion(DateTime fechaHoraInicio, double duracionHoras);
}