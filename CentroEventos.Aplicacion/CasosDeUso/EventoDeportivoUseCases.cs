using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.CasosDeUso.PersonaUseCases;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivos
{
    public abstract class EventoDeportivoUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IServicioAutorizacion servicioAutorizacion
    ) : CasoDeUsoBase(servicioAutorizacion)
    {
        protected readonly IRepositorioEventoDeportivo _repositorioEvento = repositorioEvento;
    }
    public class EventoDeportivoCrearUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IValidadorEventoDeportivo validadorEvento,
        IServicioAutorizacion servicioAutorizacion,
        PersonaObtenerPorIdUseCase personaObtenerPorId
    ) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        protected readonly IValidadorEventoDeportivo _validadorEvento = validadorEvento;

        public void Ejecutar(int usuarioId, EventoDeportivo evento)
        {
            ValidarPermiso(usuarioId, Permiso.CrearEvento);
            _validadorEvento.Validar(evento);
            if (personaObtenerPorId.Ejecutar(usuarioId, evento.ResponsableId) is null)
                throw new EntidadNotFoundException("Responsable del evento no encontrado.");
            _repositorioEvento.Agregar(evento);
        }
    }

    public class EventoDeportivoEliminarUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IServicioAutorizacion servicioAutorizacion
    ) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        public void Ejecutar(int eventoId, int usuarioId)
        {
            ValidarPermiso(usuarioId, Permiso.EliminarEvento);
            if (_repositorioEvento.ObtenerPorId(eventoId) is null)
                throw new EntidadNotFoundException("Evento no encontrado.");
            _repositorioEvento.Eliminar(eventoId);
        }
    }

    public class EventoDeportivoObtenerPorIdUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IServicioAutorizacion servicioAutorizacion
    ) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        public EventoDeportivo Ejecutar(int usuarioId, int id)
        {
            ValidarPermiso(usuarioId, Permiso.VerEventos);
            return _repositorioEvento.ObtenerPorId(id) ?? throw new EntidadNotFoundException("Evento no encontrado.");
        }
    }

    public class EventoDeportivoObtenerTodosUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IServicioAutorizacion servicioAutorizacion
    ) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        public IEnumerable<EventoDeportivo> Ejecutar(int usuarioId)
        {
            ValidarPermiso(usuarioId, Permiso.VerEventos);
            return _repositorioEvento.ObtenerTodos();
        }
    }

    public class EventoDeportivoActualizarUseCase(
        IRepositorioEventoDeportivo repositorioEvento,
        IServicioAutorizacion servicioAutorizacion,
        IValidadorEventoDeportivo validadorEvento,
        PersonaObtenerPorIdUseCase personaObtenerPorId
    ) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        public void Ejecutar(int usuarioId, EventoDeportivo evento)
        {
            ValidarPermiso(usuarioId, Permiso.EditarEvento);
            validadorEvento.Validar(evento);
            EventoDeportivo existente = _repositorioEvento.ObtenerPorId(evento.Id) ?? throw new EntidadNotFoundException("Evento no encontrado.");
            if (existente.FechaHoraInicio != evento.FechaHoraInicio && existente.FechaHoraInicio <= DateTime.Now)
                throw new OperacionInvalidaException("No se puede modificar la fecha de un evento pasado.");
            if (existente.ResponsableId != evento.ResponsableId && personaObtenerPorId.Ejecutar(usuarioId, evento.ResponsableId) is null)
                throw new EntidadNotFoundException("Responsable del evento no encontrado.");
            if (_repositorioEvento.ObtenerTodos().Any(e => e.Nombre == evento.Nombre && e.FechaHoraInicio == evento.FechaHoraInicio && e.Id != evento.Id))
                throw new DuplicadoException("Ya existe un evento deportivo con ese nombre y fecha.");
            _repositorioEvento.Modificar(evento);
        }
    }
}
