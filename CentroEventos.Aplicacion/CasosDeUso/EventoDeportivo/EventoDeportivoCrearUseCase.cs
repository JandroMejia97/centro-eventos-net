using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoUseCases;

// Caso de uso para crear un Evento Deportivo
public class EventoDeportivoCrearUseCase(IRepositorioEventoDeportivo repositorioEvento, IValidadorEventoDeportivo validadorEvento)
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento = repositorioEvento;
    private readonly IValidadorEventoDeportivo _validadorEvento = validadorEvento;

    public void Ejecutar(EventoDeportivo evento) {
        _validadorEvento.Validar(evento);
        if (_repositorioEvento.ObtenerTodos().Any(e => e.Nombre == evento.Nombre && e.FechaHoraInicio == evento.FechaHoraInicio))
            throw new DuplicadoException("Ya existe un evento deportivo con ese nombre y fecha.");
        _repositorioEvento.Agregar(evento);
    }
}
