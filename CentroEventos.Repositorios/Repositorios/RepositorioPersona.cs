using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Repositorios.Repositorios;

public class RepositorioPersona : IRepositorioPersona
{
    private readonly IFuenteDeDatosPersona _fuenteDeDatos;

    public RepositorioPersona(IFuenteDeDatosPersona fuenteDeDatos)
    {
        _fuenteDeDatos = fuenteDeDatos;
    }

    public void Agregar(Persona persona)
    {
        _fuenteDeDatos.Agregar(persona);
    }

    public void Modificar(Persona persona)
    {
        try
        {
            _fuenteDeDatos.Modificar(persona);
        }
        catch (Exception)
        {
            throw new EntidadNotFoundException("Persona no encontrada para modificar.");
        }
    }

    public void Eliminar(int id)
    {
        try
        {
            _fuenteDeDatos.Eliminar(id);
        }
        catch (Exception)
        {
            throw new EntidadNotFoundException("Persona no encontrada para eliminar.");
        }
    }

    public Persona? ObtenerPorId(int id)
    {
        return _fuenteDeDatos.ObtenerTodos().FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Persona> ObtenerTodos()
    {
        return _fuenteDeDatos.ObtenerTodos();
    }

    public IEnumerable<Persona> ObtenerPorEmail(string email)
    {
        return _fuenteDeDatos.ObtenerPorEmail(email);
    }

    public IEnumerable<Persona> ObtenerPorDni(string dni)
    {
        return _fuenteDeDatos.ObtenerPorDni(dni);
    }
}