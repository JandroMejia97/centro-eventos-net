using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaUseCases;

// Caso de uso para obtener todas las Personas
public class PersonaObtenerTodosUseCase(IRepositorioPersona repositorioPersona)
{
    public IEnumerable<Persona> Ejecutar() {
        return repositorioPersona.ObtenerTodos();
    }
}
