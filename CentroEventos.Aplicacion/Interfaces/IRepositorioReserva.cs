namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IRepositorioReserva
{
    void Agregar(Reserva reserva);
    void Modificar(Reserva reserva);
    void Eliminar(int id);
    Reserva? ObtenerPorId(int id);
    IEnumerable<Reserva> ObtenerTodos();
}