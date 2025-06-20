using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Interfaces;
public interface IFuenteDeDatosReserva
{
    void Agregar(Reserva reserva);
    void Modificar(Reserva reserva);
    void Eliminar(int personaId, int eventoDeportivoId);
    Reserva? Obtener(int personaId, int eventoDeportivoId);
    IEnumerable<Reserva> ObtenerPorPersonaId(int personaId);
    IEnumerable<Reserva> ObtenerPorEventoId(int eventoDeportivoId);
    IEnumerable<Reserva> ObtenerTodos();
}
