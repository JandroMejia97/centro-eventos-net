using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Repositorios.Repositorios;

public class RepositorioPersona : IRepositorioPersona
{
    private readonly IFuenteDeDatos<Persona> _fuenteDeDatos;
    private readonly string _idFilePath = "ids_persona.txt";

    public RepositorioPersona(IFuenteDeDatos<Persona> fuenteDeDatos)
    {
        _fuenteDeDatos = fuenteDeDatos;
    }

    private int GenerarNuevoId()
    {
        if (!File.Exists(_idFilePath))
        {
            File.WriteAllText(_idFilePath, "0");
        }

        var lastId = int.Parse(File.ReadAllText(_idFilePath));
        var newId = lastId + 1;
        File.WriteAllText(_idFilePath, newId.ToString());
        return newId;
    }

    public void Agregar(Persona persona)
    {
        persona.Id = GenerarNuevoId();
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
}