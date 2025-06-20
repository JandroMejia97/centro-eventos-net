using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaUseCases;

// Caso de uso para eliminar una Reserva
public class ReservaEliminarUseCase(IRepositorioReserva repositorioReserva, IServicioAutorizacion servicioAutorizacion)
{
    private readonly IRepositorioReserva _repositorioReserva = repositorioReserva;
    private readonly IServicioAutorizacion _servicioAutorizacion = servicioAutorizacion;

    public void Ejecutar(int usuarioId, int personaId, int eventoDeportivoId) {
        _servicioAutorizacion.Autorizar(usuarioId, Permiso.EliminarReserva);
        if (_repositorioReserva.Obtener(personaId, eventoDeportivoId) is null)
            throw new EntidadNotFoundException("Reserva no encontrada.");
        _repositorioReserva.Eliminar(personaId, eventoDeportivoId);
    }
}
