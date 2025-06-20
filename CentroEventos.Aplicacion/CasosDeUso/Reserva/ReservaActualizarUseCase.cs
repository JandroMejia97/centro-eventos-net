using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaUseCases;

// Caso de uso para actualizar una Reserva
public class ReservaActualizarUseCase(IRepositorioReserva repositorioReserva, IValidadorReserva validadorReserva)
{
    private readonly IRepositorioReserva _repositorioReserva = repositorioReserva;
    private readonly IValidadorReserva _validadorReserva = validadorReserva;

    public void Ejecutar(int usuarioId, Reserva reserva) {
        _validadorReserva.Validar(reserva);
        if (_repositorioReserva.Obtener(reserva.PersonaId, reserva.EventoDeportivoId) is null)
            throw new EntidadNotFoundException("Reserva no encontrada.");
        _repositorioReserva.Modificar(reserva);
    }
}
