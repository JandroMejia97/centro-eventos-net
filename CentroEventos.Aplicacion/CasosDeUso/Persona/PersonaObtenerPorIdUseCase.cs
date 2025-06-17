using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaUseCases;

// Caso de uso para obtener una Persona por ID
public class PersonaObtenerPorIdUseCase(IRepositorioPersona repositorioPersona)
{
    public Persona Ejecutar(int id) {
        return repositorioPersona.ObtenerPorId(id) ?? throw new EntidadNotFoundException("Persona no encontrada.");
    }
}
