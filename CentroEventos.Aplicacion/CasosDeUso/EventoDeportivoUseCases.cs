using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivos
{
    public abstract class EventoDeportivoUseCase
    {
        protected readonly IRepositorioEventoDeportivo _repositorioEvento;
        protected readonly IServicioAutorizacion _servicioAutorizacion;

        protected EventoDeportivoUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion)
        {
            _repositorioEvento = repositorioEvento;
            _servicioAutorizacion = servicioAutorizacion;
        }
    }
    public class EventoDeportivoCrearUseCase(IRepositorioEventoDeportivo repositorioEvento, IValidadorEventoDeportivo validadorEvento, IServicioAutorizacion servicioAutorizacion) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        protected readonly IValidadorEventoDeportivo _validadorEvento = validadorEvento;

        public void Ejecutar(int usuarioId, EventoDeportivo evento)
        {
            if (!_servicioAutorizacion.Autorizar(usuarioId, Permiso.CrearEvento))
                throw new UnauthorizedAccessException("El usuario no tiene permisos para crear eventos.");
            _validadorEvento.Validar(evento);
            if (_repositorioEvento.ObtenerTodos().Any(e => e.Nombre == evento.Nombre && e.FechaHoraInicio == evento.FechaHoraInicio))
                throw new DuplicadoException("Ya existe un evento deportivo con ese nombre y fecha.");
            _repositorioEvento.Agregar(evento);
        }
    }

    public class EventoDeportivoEliminarUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        public void Ejecutar(int eventoId, int usuarioId)
        {
            _servicioAutorizacion.Autorizar(usuarioId, Permiso.EliminarEvento);
            if (_repositorioEvento.ObtenerPorId(eventoId) is null)
                throw new EntidadNotFoundException("Evento no encontrado.");
            _repositorioEvento.Eliminar(eventoId);
        }
    }

    public class EventoDeportivoObtenerPorIdUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        public EventoDeportivo Ejecutar(int usuarioId, int id)
        {
            _servicioAutorizacion.Autorizar(usuarioId, Permiso.VerEventos);
            return _repositorioEvento.ObtenerPorId(id) ?? throw new EntidadNotFoundException("Evento no encontrado.");
        }
    }

    public class EventoDeportivoObtenerTodosUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        public IEnumerable<EventoDeportivo> Ejecutar(int usuarioId)
        {
            _servicioAutorizacion.Autorizar(usuarioId, Permiso.VerEventos);
            return _repositorioEvento.ObtenerTodos();
        }
    }

    public class EventoDeportivoActualizarUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion, IValidadorEventoDeportivo validadorEvento) : EventoDeportivoUseCase(repositorioEvento, servicioAutorizacion)
    {
        public void Ejecutar(int usuarioId, EventoDeportivo evento)
        {
            _servicioAutorizacion.Autorizar(usuarioId, Permiso.EditarEvento);
            validadorEvento.Validar(evento);
            if (_repositorioEvento.ObtenerPorId(evento.Id) is null)
                throw new EntidadNotFoundException("Evento no encontrado.");
            if (_repositorioEvento.ObtenerTodos().Any(e => e.Nombre == evento.Nombre && e.FechaHoraInicio == evento.FechaHoraInicio && e.Id != evento.Id))
                throw new DuplicadoException("Ya existe un evento deportivo con ese nombre y fecha.");
            _repositorioEvento.Modificar(evento);
        }
    }
}
