using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaUseCases;

// Caso de uso para eliminar una Persona
public class PersonaEliminarUseCase(IRepositorioPersona repositorioPersona, IServicioAutorizacion servicioAutorizacion)
{
    public void Ejecutar(int personaId, int usuarioId) {
        servicioAutorizacion.Autorizar(usuarioId, Permiso.EliminarUsuario);
        if (repositorioPersona.ObtenerPorId(personaId) is null)
            throw new EntidadNotFoundException("Persona no encontrada.");
        repositorioPersona.Eliminar(personaId);
    }
}
