using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoUseCases;

// Caso de uso para actualizar un Evento Deportivo
public class EventoDeportivoActualizarUseCase(IRepositorioEventoDeportivo repositorioEvento, IValidadorEventoDeportivo validadorEvento)
{
    public void Ejecutar(EventoDeportivo evento) {
        validadorEvento.Validar(evento);
        if (repositorioEvento.ObtenerPorId(evento.Id) is null)
            throw new EntidadNotFoundException("Evento no encontrado.");
        if (repositorioEvento.ObtenerTodos().Any(e => e.Nombre == evento.Nombre && e.FechaHoraInicio == evento.FechaHoraInicio && e.Id != evento.Id))
            throw new DuplicadoException("Ya existe un evento deportivo con ese nombre y fecha.");
        repositorioEvento.Modificar(evento);
    }
}
