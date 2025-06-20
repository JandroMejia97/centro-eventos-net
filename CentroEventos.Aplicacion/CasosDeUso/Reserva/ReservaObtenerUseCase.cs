using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaUseCases;

// Caso de uso para obtener una Reserva por ID
public class ReservaObtenerUseCase(IRepositorioReserva repositorioReserva)
{
    public Reserva Ejecutar(int personaId, int eventoDeportivoId) {
        return repositorioReserva.Obtener(personaId, eventoDeportivoId) ?? throw new EntidadNotFoundException("Reserva no encontrada.");
    }
}
