using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public class FuenteDeDatosEventoDeportivoEF : IFuenteDeDatosEventoDeportivo
{
    private readonly CentroEventosDbContext _context;

    public FuenteDeDatosEventoDeportivoEF(CentroEventosDbContext context)
    {
        _context = context;
    }

    public void Agregar(EventoDeportivo evento)
    {
        _context.EventosDeportivos.Add(evento);
        _context.SaveChanges();
    }

    public void Modificar(EventoDeportivo evento)
    {
        _context.EventosDeportivos.Update(evento);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var evento = _context.EventosDeportivos.Find(id);
        if (evento is not null)
        {
            _context.EventosDeportivos.Remove(evento);
            _context.SaveChanges();
        }
    }

    public EventoDeportivo? ObtenerPorId(int id)
    {
        var evento = _context.EventosDeportivos.Include(e => e.Responsable).FirstOrDefault(e => e.Id == id);
        if (evento != null)
        {
            evento.NumeroReservas = _context.Reservas.Count(r => r.EventoDeportivoId == evento.Id);
        }
        return evento;
    }

    public IEnumerable<EventoDeportivo> ObtenerTodos()
    {
        var eventos = _context.EventosDeportivos.Include(e => e.Responsable).AsNoTracking().ToList();
        var reservasPorEvento = _context.Reservas.GroupBy(r => r.EventoDeportivoId)
            .ToDictionary(g => g.Key, g => g.Count());
        foreach (var evento in eventos)
        {
            evento.NumeroReservas = reservasPorEvento.TryGetValue(evento.Id, out var count) ? count : 0;
        }
        return eventos;
    }

    public IEnumerable<EventoDeportivo> ObtenerPorFechaYDuracion(DateTime fechaHoraInicio, double duracionHoras)
    {
        DateTime FechaFinal = fechaHoraInicio.AddHours(duracionHoras);
        return [.. _context.EventosDeportivos
            .Include(e => e.Responsable)
            .Where(e => e.FechaHoraInicio >= fechaHoraInicio && e.FechaHoraInicio <= FechaFinal)
            .AsNoTracking()];
    }

    public IEnumerable<EventoDeportivo> ObtenerAPartirDeFecha(DateTime fechaHoraInicio)
    {
        return [.. _context.EventosDeportivos
            .Include(e => e.Responsable)
            .Where(e => e.FechaHoraInicio >= fechaHoraInicio)
            .OrderBy(e => e.FechaHoraInicio)
            .AsNoTracking()];
    }
}
