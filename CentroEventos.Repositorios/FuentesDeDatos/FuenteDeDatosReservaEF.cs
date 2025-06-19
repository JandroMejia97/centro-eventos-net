using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public class FuenteDeDatosReservaEF : IFuenteDeDatos<Reserva>
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
        _context.Reservas.Update(reserva);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var reserva = _context.Reservas.Find(id);
        if (reserva is not null)
        {
            _context.Reservas.Remove(reserva);
            _context.SaveChanges();
        }
    }

    public Reserva? ObtenerPorId(int id)
    {
        return _context.Reservas.Find(id);
    }

    public IEnumerable<Reserva> ObtenerTodos()
    {
        return _context.Reservas.AsNoTracking().ToList();
    }
}
