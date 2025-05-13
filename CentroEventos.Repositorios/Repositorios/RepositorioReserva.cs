using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Repositorios.Repositorios;

public class RepositorioReserva : IRepositorioReserva
{
    private readonly IFuenteDeDatos<Reserva> _fuenteDeDatos;
    private readonly string _idFilePath = "ids_reserva.txt";

    public RepositorioReserva(IFuenteDeDatos<Reserva> fuenteDeDatos)
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

    public void Agregar(Reserva reserva)
    {
        reserva.Id = GenerarNuevoId();
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