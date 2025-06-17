using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaUseCases;

// Caso de uso para eliminar una Persona
public class PersonaEliminarUseCase(IRepositorioPersona repositorioPersona, IServicioAutorizacion servicioAutorizacion)
{
    public void Ejecutar(int personaId, string usuario) {
        servicioAutorizacion.Autorizar(usuario, "eliminar");
        if (repositorioPersona.ObtenerPorId(personaId) is null)
            throw new EntidadNotFoundException("Persona no encontrada.");
        repositorioPersona.Eliminar(personaId);
    }
}
