using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaUseCases;

// Caso de uso para actualizar una Persona
public class PersonaActualizarUseCase(IRepositorioPersona repositorioPersona, IValidadorPersona validadorPersona, IServicioAutorizacion servicioAutorizacion)
{
    private readonly IRepositorioPersona _repositorioPersona = repositorioPersona;
    private readonly IValidadorPersona _validadorPersona = validadorPersona;
    private readonly IServicioAutorizacion _servicioAutorizacion = servicioAutorizacion;

    public void Ejecutar(Persona persona, int usuarioId) {
        _servicioAutorizacion.Autorizar(usuarioId, Permiso.EditarUsuario);
        _validadorPersona.Validar(persona);
        if (_repositorioPersona.ObtenerPorId(persona.Id) is null)
            throw new EntidadNotFoundException("Persona no encontrada.");
        if (_repositorioPersona.ObtenerTodos().Any(p => p.DNI == persona.DNI && p.Id != persona.Id))
            throw new DuplicadoException("Ya existe una persona con ese DNI.");
        _repositorioPersona.Modificar(persona);
    }
}
