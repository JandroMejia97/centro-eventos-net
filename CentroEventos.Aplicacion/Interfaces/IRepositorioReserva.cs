namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IRepositorioReserva
{
    void Agregar(Reserva reserva);
    void Modificar(Reserva reserva);
    void Eliminar(int personaId, int eventoDeportivoId);
    Reserva? Obtener(int personaId, int eventoDeportivoId);
    IEnumerable<Reserva> ObtenerPorPersonaId(int personaId);
    IEnumerable<Reserva> ObtenerPorEventoId(int eventoDeportivoId);
    IEnumerable<Reserva> ObtenerTodos();
}