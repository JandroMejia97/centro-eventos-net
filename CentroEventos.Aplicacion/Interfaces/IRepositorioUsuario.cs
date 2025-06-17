namespace CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.Entidades;

public interface IRepositorioUsuario
{
    void Agregar(Usuario usuario);
    void Modificar(Usuario usuario);
    void Eliminar(int id);
    Usuario? ObtenerPorId(int id);
    Usuario? ObtenerPorEmail(string email);
    IEnumerable<Usuario> ObtenerTodos();
}
