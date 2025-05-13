using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso;

public class ReservaAltaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IRepositorioEventoDeportivo _repositorioEvento;
    private readonly IRepositorioPersona _repositorioPersona;

    public ReservaAltaUseCase(
        IRepositorioReserva repositorioReserva,
        IRepositorioEventoDeportivo repositorioEvento,
        IRepositorioPersona repositorioPersona)
    {
        _repositorioReserva = repositorioReserva;
        _repositorioEvento = repositorioEvento;
        _repositorioPersona = repositorioPersona;
    }

    public void Ejecutar(int personaId, int eventoId)
    {
        _repositorioPersona.ObtenerPorId(personaId)
            ?? throw new EntidadNotFoundException("Persona no encontrada.");
        var evento = _repositorioEvento.ObtenerPorId(eventoId)
            ?? throw new EntidadNotFoundException("Evento no encontrado.");

        if (evento.FechaHoraInicio < DateTime.Now)
            throw new OperacionInvalidaException("No se puede reservar un evento pasado.");

        if (_repositorioReserva.ObtenerTodos().Any(r => r.PersonaId == personaId && r.EventoDeportivoId == eventoId))
            throw new DuplicadoException("La persona ya tiene una reserva para este evento.");

        if (_repositorioReserva.ObtenerTodos().Count(r => r.EventoDeportivoId == eventoId) >= evento.CupoMaximo)
            throw new CupoExcedidoException("Cupo máximo alcanzado.");

        var reserva = new Reserva
        {
            PersonaId = personaId,
            EventoDeportivoId = eventoId,
            FechaAltaReserva = DateTime.Now,
            EstadoAsistencia = EstadoAsistencia.Pendiente
        };

        _repositorioReserva.Agregar(reserva);
    }
}