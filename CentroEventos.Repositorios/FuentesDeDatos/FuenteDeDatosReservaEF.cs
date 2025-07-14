using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public class FuenteDeDatosReservaEF : IFuenteDeDatosReserva
{
    private readonly CentroEventosDbContext _context;

    public FuenteDeDatosReservaEF(CentroEventosDbContext context)
    {
        _context = context;
    }

    public void Agregar(Reserva reserva)
    {
        _context.Reservas.Add(reserva);
        _context.SaveChanges();
    }

    public void Modificar(Reserva reserva)
    {
        var reservaExistente = _context.Reservas.Find(reserva.PersonaId, reserva.EventoDeportivoId);
        if (reservaExistente is not null)
        {
            _context.Entry(reservaExistente).CurrentValues.SetValues(reserva);
            _context.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("No se encontró la reserva a modificar.");
        }
    }

    public IEnumerable<Reserva> ObtenerTodos()
    {
        return [.. _context.Reservas.AsNoTracking()];
    }

    public Reserva? Obtener(int personaId, int eventoDeportivoId)
    {
        return _context.Reservas
            .Include(r => r.EventoDeportivo)
            .Include(r => r.Persona)
            .AsNoTracking()
            .FirstOrDefault(r => r.PersonaId == personaId && r.EventoDeportivoId == eventoDeportivoId);
    }

    public void Eliminar(int personaId, int eventoDeportivoId)
    {
        var reserva = _context.Reservas.FirstOrDefault(r => r.PersonaId == personaId && r.EventoDeportivoId == eventoDeportivoId);
        if (reserva is not null)
        {
            _context.Reservas.Remove(reserva);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Reserva> ObtenerPorPersonaId(int personaId)
    {
        return [.. _context.Reservas
            .Include(r => r.EventoDeportivo)
            .Include(r => r.Persona)
            .AsNoTracking()
            .Where(r => r.PersonaId == personaId)];
    }

    public IEnumerable<Reserva> ObtenerPorEventoId(int eventoDeportivoId)
    {
        return [.. _context.Reservas
            .Include(r => r.EventoDeportivo)
            .Include(r => r.Persona)
            .AsNoTracking()
            .Where(r => r.EventoDeportivoId == eventoDeportivoId)];
    }
}
