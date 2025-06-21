using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;

namespace CentroEventos.Aplicacion.CasosDeUso.ReservaUseCases
{
    public abstract class ReservaUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion servicioAutorizacion
    ) : CasoDeUsoBase(servicioAutorizacion)
    {
        protected readonly IRepositorioReserva repositorioReserva = repositorioReserva;
    }
    public class ReservaActualizarUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion servicioAutorizacion,
        IValidadorReserva validadorReserva
    ) : ReservaUseCase(repositorioReserva, servicioAutorizacion)
    {
        private readonly IRepositorioReserva _repositorioReserva = repositorioReserva;
        private readonly IValidadorReserva _validadorReserva = validadorReserva;

        public void Ejecutar(int usuarioId, Reserva reserva)
        {
            ValidarPermiso(usuarioId, Permiso.EditarReserva);
            _validadorReserva.Validar(reserva);
            Reserva existente = _repositorioReserva.Obtener(reserva.PersonaId, reserva.EventoDeportivoId) ?? throw new EntidadNotFoundException("Reserva no encontrada.");
            if (existente.PersonaId != reserva.PersonaId)
                throw new OperacionInvalidaException("No puedes modificar la persona de la reserva.");
            if (existente.EventoDeportivoId != reserva.EventoDeportivoId)
                throw new OperacionInvalidaException("No puedes modificar el evento deportivo de la reserva.");
            _repositorioReserva.Modificar(reserva);
        }
    }

    public class ReservaCrearUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion servicioAutorizacion,
        IRepositorioEventoDeportivo repositorioEvento,
        IRepositorioPersona repositorioPersona
    ) : ReservaUseCase(repositorioReserva, servicioAutorizacion)
    {
        public void Ejecutar(int usuarioId, int personaId, int eventoId)
        {
            ValidarPermiso(usuarioId, Permiso.CrearReserva);
            var persona = repositorioPersona.ObtenerPorId(personaId)
                ?? throw new EntidadNotFoundException("Persona no encontrada.");
            var evento = repositorioEvento.ObtenerPorId(eventoId)
                ?? throw new EntidadNotFoundException("Evento no encontrado.");

            if (repositorioReserva.ObtenerPorPersonaId(personaId).Any(r => r.EventoDeportivoId == eventoId))
                throw new DuplicadoException("La persona ya tiene una reserva para este evento.");

            if (repositorioReserva.ObtenerPorEventoId(eventoId).Count() >= evento.CupoMaximo)
                throw new CupoExcedidoException("Cupo máximo alcanzado.");

            var reserva = new Reserva(personaId, eventoId);

            repositorioReserva.Agregar(reserva);
        }
    }

    public class ReservaEliminarUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion servicioAutorizacion
    ) : ReservaUseCase(repositorioReserva, servicioAutorizacion)
    {

        public void Ejecutar(int usuarioId, int personaId, int eventoDeportivoId)
        {
            ValidarPermiso(usuarioId, Permiso.EliminarReserva);
            if (repositorioReserva.Obtener(personaId, eventoDeportivoId) is null)
                throw new EntidadNotFoundException("Reserva no encontrada.");
            repositorioReserva.Eliminar(personaId, eventoDeportivoId);
        }
    }

    public class ReservaObtenerUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion servicioAutorizacion
    ) : ReservaUseCase(repositorioReserva, servicioAutorizacion)
    {
        public Reserva Ejecutar(int usuarioId, int personaId, int eventoDeportivoId)
        {
            ValidarPermiso(usuarioId, Permiso.VerReservas);
            return repositorioReserva.Obtener(personaId, eventoDeportivoId) ?? throw new EntidadNotFoundException("Reserva no encontrada.");
        }
    }

    public class ReservaObtenerPorEventoUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion servicioAutorizacion
    ) : ReservaUseCase(repositorioReserva, servicioAutorizacion)
    {
        public IEnumerable<Reserva> Ejecutar(int usuarioId, int eventoDeportivoId)
        {
            ValidarPermiso(usuarioId, Permiso.VerReservas);
            return repositorioReserva.ObtenerPorEventoId(eventoDeportivoId);
        }
    }

    public class ReservaObtenerPorPersonaUseCase(
        IRepositorioReserva repositorioReserva,
        IServicioAutorizacion servicioAutorizacion
    ) : ReservaUseCase(repositorioReserva, servicioAutorizacion)
    {
        public IEnumerable<Reserva> Ejecutar(int usuarioId, int personaId)
        {
            ValidarPermiso(usuarioId, Permiso.VerReservas);
            return repositorioReserva.ObtenerPorPersonaId(personaId);
        }
    }
}