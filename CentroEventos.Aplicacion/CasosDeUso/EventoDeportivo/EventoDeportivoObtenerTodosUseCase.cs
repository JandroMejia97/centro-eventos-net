using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoUseCases;

// Caso de uso para obtener todos los Eventos Deportivos
public class EventoDeportivoObtenerTodosUseCase(IRepositorioEventoDeportivo repositorioEvento)
{
    public IEnumerable<EventoDeportivo> Ejecutar() {
        return repositorioEvento.ObtenerTodos();
    }
}
