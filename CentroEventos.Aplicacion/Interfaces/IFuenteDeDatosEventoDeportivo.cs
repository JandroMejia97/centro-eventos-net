using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Interfaces;

public interface IFuenteDeDatosEventoDeportivo : IFuenteDeDatos<EventoDeportivo>
{
    IEnumerable<EventoDeportivo> ObtenerPorFechaYDuracion(DateTime fechaHoraInicio, double duracionHoras);
}