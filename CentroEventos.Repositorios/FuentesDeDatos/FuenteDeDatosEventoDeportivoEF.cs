using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public class FuenteDeDatosEventoDeportivoEF : IFuenteDeDatos<EventoDeportivo>
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
        return _context.EventosDeportivos.Include(e => e.Responsable).FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<EventoDeportivo> ObtenerTodos()
    {
        return _context.EventosDeportivos.Include(e => e.Responsable).AsNoTracking().ToList();
    }
}
