using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public class FuenteDeDatosUsuarioEF : IFuenteDeDatos<Usuario>
{
    private readonly CentroEventosDbContext _context;

    public FuenteDeDatosUsuarioEF(CentroEventosDbContext context)
    {
        _context = context;
    }

    public void Agregar(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public void Modificar(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario is not null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }

    public Usuario? ObtenerPorId(int id)
    {
        return _context.Usuarios.Include(u => u.Persona).Include(u => u.PermisosUsuarios).FirstOrDefault(u => u.PersonaId == id);
    }

    public IEnumerable<Usuario> ObtenerTodos()
    {
        return _context.Usuarios.Include(u => u.Persona).Include(u => u.PermisosUsuarios).AsNoTracking();
    }
}
