using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso;

public class ReservaAltaUseCase(
    IRepositorioReserva repositorioReserva,
    IRepositorioEventoDeportivo repositorioEvento,
    IRepositorioPersona repositorioPersona)
{
    public void Ejecutar(int personaId, int eventoId)
    {
        var persona = repositorioPersona.ObtenerPorId(personaId)
            ?? throw new EntidadNotFoundException("Persona no encontrada.");
        var evento = repositorioEvento.ObtenerPorId(eventoId)
            ?? throw new EntidadNotFoundException("Evento no encontrado.");

        if (evento.FechaHoraInicio < DateTime.Now)
            throw new OperacionInvalidaException("No se puede reservar un evento pasado.");

        if (repositorioReserva.ObtenerTodos().Any(r => r.PersonaId == personaId && r.EventoDeportivoId == eventoId))
            throw new DuplicadoException("La persona ya tiene una reserva para este evento.");

        if (repositorioReserva.ObtenerTodos().Count(r => r.EventoDeportivoId == eventoId) >= evento.CupoMaximo)
            throw new CupoExcedidoException("Cupo máximo alcanzado.");

        var reserva = new Reserva(personaId, eventoId);

        repositorioReserva.Agregar(reserva);
    }
}