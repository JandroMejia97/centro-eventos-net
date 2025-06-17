using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaUseCases;

// Caso de uso para obtener una Reserva por ID
public class ReservaObtenerPorIdUseCase(IRepositorioReserva repositorioReserva)
{
    public Reserva Ejecutar(int id) {
        return repositorioReserva.ObtenerPorId(id) ?? throw new EntidadNotFoundException("Reserva no encontrada.");
    }
}
