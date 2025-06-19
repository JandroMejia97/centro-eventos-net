using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Repositorios.Repositorios;

public class RepositorioUsuario : IRepositorioUsuario
{
    private readonly IFuenteDeDatos<Usuario> _fuenteDeDatos;

    public RepositorioUsuario(IFuenteDeDatos<Usuario> fuenteDeDatos)
    {
        _fuenteDeDatos = fuenteDeDatos;
    }

    public void Agregar(Usuario usuario)
    {
        _fuenteDeDatos.Agregar(usuario);
    }

    public void Modificar(Usuario usuario)
    {
        try
        {
            _fuenteDeDatos.Modificar(usuario);
        }
        catch (Exception)
        {
            throw new EntidadNotFoundException("Usuario no encontrado para modificar.");
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
            throw new EntidadNotFoundException("Usuario no encontrado para eliminar.");
        }
    }

    public Usuario? ObtenerPorId(int id)
    {
        return _fuenteDeDatos.ObtenerTodos().FirstOrDefault(u => u.Id == id);
    }

    public Usuario? ObtenerPorEmail(string email)
    {
        return _fuenteDeDatos.ObtenerTodos().FirstOrDefault(u => u.Persona.Email == email);
    }

    public IEnumerable<Usuario> ObtenerTodos()
    {
        return _fuenteDeDatos.ObtenerTodos();
    }
}
