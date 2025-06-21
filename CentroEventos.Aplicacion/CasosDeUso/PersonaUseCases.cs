using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso
{
    public abstract class PersonaUseCase(IRepositorioPersona repositorioPersona, IServicioAutorizacion servicioAutorizacion) : CasoDeUsoBase(servicioAutorizacion)
    {
        protected readonly IRepositorioPersona repositorioPersona = repositorioPersona;
    }

    public class PersonaActualizarUseCase(
        IRepositorioPersona repositorioPersona,
        IServicioAutorizacion servicioAutorizacion,
        IValidadorPersona validadorPersona
    ) : PersonaUseCase(repositorioPersona, servicioAutorizacion)
    {
        public void Ejecutar(int personaId, Persona persona)
        {
            if (personaId != persona.Id)
                ValidarPermiso(personaId, Permiso.EditarUsuario);
            validadorPersona.Validar(persona);
            var existente = repositorioPersona.ObtenerPorId(persona.Id) ?? throw new EntidadNotFoundException("Persona no encontrado.");
            if (existente.Email != persona.Email && repositorioPersona.ObtenerPorEmail(persona.Email).Any())
                throw new DuplicadoException("Ya existe un persona con ese email.");
            if (existente.DNI != persona.DNI && repositorioPersona.ObtenerPorDni(persona.DNI).Any())
                throw new DuplicadoException("Ya existe un persona con ese DNI.");
            repositorioPersona.Modificar(persona);
        }
    }

    public class PersonaEliminarUseCase(
        IRepositorioPersona repositorioPersona,
        IServicioAutorizacion servicioAutorizacion,
        ReservaObtenerPorPersonaUseCase reservaObtenerPorPersonaUseCase,
        UsuarioEliminarUseCase usuarioEliminarUseCase
    ) : PersonaUseCase(repositorioPersona, servicioAutorizacion)
    {
        public void Ejecutar(int usuarioSolicitanteId, int personaId)
        {
            if (personaId != usuarioSolicitanteId)
                ValidarPermiso(usuarioSolicitanteId, Permiso.EliminarUsuario);
            if (repositorioPersona.ObtenerPorId(personaId) is null)
                throw new EntidadNotFoundException("Persona no encontrado.");
            if (reservaObtenerPorPersonaUseCase.Ejecutar(personaId).Any())
                throw new OperacionInvalidaException("No se puede eliminar una persona con reservas asociadas.");
            repositorioPersona.Eliminar(personaId);
            usuarioEliminarUseCase.Ejecutar(usuarioSolicitanteId, personaId);
        }
    }

    public class PersonaObtenerPorIdUseCase(IRepositorioPersona repositorioPersona, IServicioAutorizacion servicioAutorizacion) : PersonaUseCase(repositorioPersona, servicioAutorizacion)
    {
        public Persona Ejecutar(int personaSolicitanteId, int id)
        {
            if (id != personaSolicitanteId)
                ValidarPermiso(personaSolicitanteId, Permiso.VerUsuarios);
            return repositorioPersona.ObtenerPorId(id) ?? throw new EntidadNotFoundException("Persona no encontrada.");
        }
    }

    public class PersonaObtenerTodosUseCase(IRepositorioPersona repositorioPersona, IServicioAutorizacion servicioAutorizacion) : PersonaUseCase(repositorioPersona, servicioAutorizacion)
    {
        public IEnumerable<Persona> Ejecutar(int personaSolicitanteId)
        {
            ValidarPermiso(personaSolicitanteId, Permiso.VerUsuarios);
            return repositorioPersona.ObtenerTodos();
        }
    }

}
