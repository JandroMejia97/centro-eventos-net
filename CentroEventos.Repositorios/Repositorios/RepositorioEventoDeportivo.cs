using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Repositorios.Repositorios;

public class RepositorioEventoDeportivo : IRepositorioEventoDeportivo
{
    private readonly IFuenteDeDatosEventoDeportivo _fuenteDeDatos;

    public RepositorioEventoDeportivo(IFuenteDeDatosEventoDeportivo fuenteDeDatos)
    {
        _fuenteDeDatos = fuenteDeDatos;
    }

    public void Agregar(EventoDeportivo evento)
    {
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

    public IEnumerable<EventoDeportivo> ObtenerPorFechaYDuracion(DateTime fechaHoraInicio, double duracionHoras)
    {
        return _fuenteDeDatos.ObtenerPorFechaYDuracion(fechaHoraInicio, duracionHoras);
    }

    public IEnumerable<EventoDeportivo> ObtenerAPartirDeFecha(DateTime fechaHoraInicio)
    {
        return _fuenteDeDatos.ObtenerAPartirDeFecha(fechaHoraInicio);
    }
}