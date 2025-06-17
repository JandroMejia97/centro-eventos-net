using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoUseCases;

// Caso de uso para obtener un Evento Deportivo por ID
public class EventoDeportivoObtenerPorIdUseCase(IRepositorioEventoDeportivo repositorioEvento)
{
    public EventoDeportivo Ejecutar(int id) {
        return repositorioEvento.ObtenerPorId(id) ?? throw new EntidadNotFoundException("Evento no encontrado.");
    }
}
