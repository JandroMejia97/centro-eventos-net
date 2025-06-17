using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaUseCases;

// Caso de uso para obtener todas las Reservas
public class ReservaObtenerTodosUseCase(IRepositorioReserva repositorioReserva)
{
    public IEnumerable<Reserva> Ejecutar() {
        return repositorioReserva.ObtenerTodos();
    }
}
