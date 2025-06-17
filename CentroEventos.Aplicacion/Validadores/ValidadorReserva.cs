namespace CentroEventos.Aplicacion.Validadores;

using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;

public class ValidadorReserva : IValidadorReserva {
    public void Validar(Reserva reserva) {
        if (reserva.PersonaId <= 0)
            throw new ValidacionException("El ID de persona es inválido.");
        if (reserva.EventoDeportivoId <= 0)
            throw new ValidacionException("El ID de evento deportivo es inválido.");
        if (reserva.FechaAltaReserva > DateTime.Now)
            throw new ValidacionException("La fecha de alta de la reserva no puede ser futura.");
        if (!Enum.IsDefined(typeof(EstadoAsistencia), reserva.EstadoAsistencia))
            throw new ValidacionException("El estado de asistencia es inválido.");
    }
}
