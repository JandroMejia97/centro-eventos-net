using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Repositorios.Repositorios;

public class RepositorioReserva : IRepositorioReserva
{
    private readonly IFuenteDeDatos<Reserva> _fuenteDeDatos;

    public RepositorioReserva(IFuenteDeDatos<Reserva> fuenteDeDatos)
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

    public void Eliminar(int id)
    {
        try
        {
            _fuenteDeDatos.Eliminar(id);
        }
        catch (Exception)
        {
            throw new EntidadNotFoundException("Reserva no encontrada para eliminar.");
        }
    }

    public Reserva? ObtenerPorId(int id)
    {
        return _fuenteDeDatos.ObtenerTodos().FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Reserva> ObtenerTodos()
    {
        return _fuenteDeDatos.ObtenerTodos();
    }
}