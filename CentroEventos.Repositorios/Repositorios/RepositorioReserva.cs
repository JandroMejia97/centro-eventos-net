using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Repositorios.Repositorios;

public class RepositorioReserva : IRepositorioReserva
{
    private readonly IFuenteDeDatosReserva _fuenteDeDatos;

    public RepositorioReserva(IFuenteDeDatosReserva fuenteDeDatos)
    {
        _fuenteDeDatos = fuenteDeDatos;
    }

    public void Agregar(Reserva reserva)
    {
        _fuenteDeDatos.Agregar(reserva);
    }

    public void Modificar(Reserva reserva)
    {
        try
        {
            _fuenteDeDatos.Modificar(reserva);
        }
        catch (Exception)
        {
            throw new EntidadNotFoundException("Reserva no encontrada para modificar.");
        }
    }

    public void Eliminar(int personaId, int eventoDeportivoId)
    {
        try
        {
            _fuenteDeDatos.Eliminar(personaId, eventoDeportivoId);
        }
        catch (Exception)
        {
            throw new EntidadNotFoundException("Reserva no encontrada para eliminar.");
        }
    }

    public Reserva? Obtener(int personaId, int eventoDeportivoId)
    {
        return _fuenteDeDatos.Obtener(personaId, eventoDeportivoId);
    }

    public IEnumerable<Reserva> ObtenerPorPersonaId(int personaId)
    {
        return _fuenteDeDatos.ObtenerPorPersonaId(personaId);
    }

    public IEnumerable<Reserva> ObtenerPorEventoId(int eventoDeportivoId)
    {
        return _fuenteDeDatos.ObtenerPorEventoId(eventoDeportivoId);
    }

    public IEnumerable<Reserva> ObtenerTodos()
    {
        return _fuenteDeDatos.ObtenerTodos();
    }
}