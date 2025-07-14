using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public class FuenteDeDatosPersonaEF : IFuenteDeDatosPersona
{
    private readonly CentroEventosDbContext _context;

    public FuenteDeDatosPersonaEF(CentroEventosDbContext context)
    {
        _context = context;
    }

    public void Agregar(Persona persona)
    {
        _context.Personas.Add(persona);
        _context.SaveChanges();
    }

    public void Modificar(Persona persona)
    {
        var personaExistente = _context.Personas.Find(persona.Id);
        if (personaExistente is not null)
        {
            _context.Entry(personaExistente).CurrentValues.SetValues(persona);
            _context.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("No se encontró la persona a modificar.");
        }
    }

    public void Eliminar(int id)
    {
        var persona = _context.Personas.Find(id);
        if (persona is not null)
        {
            _context.Personas.Remove(persona);
            _context.SaveChanges();
        }
    }

    public Persona? ObtenerPorId(int id)
    {
        return _context.Personas.AsNoTracking().FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Persona> ObtenerTodos()
    {
        return _context.Personas.AsNoTracking().ToList();
    }

    public IEnumerable<Persona> ObtenerPorEmail(string email)
    {
        return [.. _context.Personas.AsNoTracking().Where(p => p.Email == email)];
    }

    public IEnumerable<Persona> ObtenerPorDni(string dni)
    {
        return [.. _context.Personas.AsNoTracking().Where(p => p.DNI == dni)];
    }
}
