namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IRepositorioPersona
{
    void Agregar(Persona persona);
    void Modificar(Persona persona);
    void Eliminar(int id);
    Persona? ObtenerPorId(int id);
    IEnumerable<Persona> ObtenerTodos();
}