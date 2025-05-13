using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Repositorios.Repositorios;

public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
{
    private readonly IFuenteDeDatos<EventoDeportivo> _fuenteDeDatos;
    private readonly string _idFilePath = "ids_evento.txt";

    public RepositorioEventoDeportivo(IFuenteDeDatos<EventoDeportivo> fuenteDeDatos)
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

    public void Agregar(EventoDeportivo evento)
    {
        evento.Id = GenerarNuevoId();
        _fuenteDeDatos.Agregar(evento);
    }

    public void Modificar(EventoDeportivo evento)
    {
        try
        {
            _fuenteDeDatos.Modificar(evento);
        }
        catch (Exception)
        {
            throw new EntidadNotFoundException("Evento deportivo no encontrado para modificar.");
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
            throw new EntidadNotFoundException("Evento deportivo no encontrado para eliminar.");
        }
    }

    public EventoDeportivo? ObtenerPorId(int id)
    {
        return _fuenteDeDatos.ObtenerTodos().FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<EventoDeportivo> ObtenerTodos()
    {
        return _fuenteDeDatos.ObtenerTodos();
    }
}