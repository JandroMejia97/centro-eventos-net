using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.Servicios;

public class ServicioPersona
{
    private readonly IRepositorioPersona _repositorioPersona;

    public ServicioPersona(IRepositorioPersona repositorioPersona)
    {
        _repositorioPersona = repositorioPersona;
    }

    public IEnumerable<Persona> ObtenerTodas()
    {
        return _repositorioPersona.ObtenerTodos();
    }

    public Persona? ObtenerPorId(int id)
    {
        return _repositorioPersona.ObtenerPorId(id);
    }

    public void Agregar(Persona persona)
    {
        _repositorioPersona.Agregar(persona);
    }

    public void Modificar(Persona persona)
    {
        _repositorioPersona.Modificar(persona);
    }

    public void Eliminar(int id)
    {
        _repositorioPersona.Eliminar(id);
    }
}