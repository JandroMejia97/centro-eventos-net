namespace CentroEventos.Aplicacion.Interfaces;

public interface IFuenteDeDatos<T>
{
    void Agregar(T entidad);
    void Modificar(T entidad);
    void Eliminar(int id);
    IEnumerable<T> ObtenerTodos();
}