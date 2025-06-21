using System.Collections.Generic;
using System.Linq;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios.Contextos;

namespace CentroEventos.Repositorios.FuentesDeDatos
{
    public class FuenteDeDatosPermisoUsuarioEF : IFuenteDeDatosPermisoUsuario
    {
        private readonly CentroEventosDbContext _context;

        public FuenteDeDatosPermisoUsuarioEF(CentroEventosDbContext context)
        {
            _context = context;
        }
        public void Agregar(PermisoUsuario permisoUsuario)
        {
            _context.PermisosUsuarios.Add(permisoUsuario);
            _context.SaveChanges();
        }

        public void Agregar(int usuarioId, IEnumerable<Permiso> permisos)
        {
            var permisoUsuarios = permisos.Select(p => new PermisoUsuario(usuarioId, p));
            Agregar(permisoUsuarios);
        }
        public void Agregar(IEnumerable<PermisoUsuario> permisos)
        {
            _context.PermisosUsuarios.AddRange(permisos);
            _context.SaveChanges();
        }

        public void Eliminar(int usuarioId, Permiso permiso)
        {
            var permisoUsuario = _context.PermisosUsuarios
                .FirstOrDefault(p => p.UsuarioId == usuarioId && p.Permiso == permiso);
            if (permisoUsuario != null)
            {
                _context.PermisosUsuarios.Remove(permisoUsuario);
                _context.SaveChanges();
            }
        }

        public void Eliminar(PermisoUsuario permiso)
        {
            _context.PermisosUsuarios.Remove(permiso);
            _context.SaveChanges();
        }

        public void Eliminar(int usuarioId, IEnumerable<Permiso> permisos)
        {
            var permisoUsuarios = permisos.Select(p => new PermisoUsuario(usuarioId, p));
            Eliminar(permisoUsuarios);
        }
        public void Eliminar(IEnumerable<PermisoUsuario> permisos)
        {
            _context.PermisosUsuarios.RemoveRange(permisos);
            _context.SaveChanges();
        }

        public PermisoUsuario? Obtener(int usuarioId, Permiso permiso)
        {
            return _context.PermisosUsuarios
                .FirstOrDefault(p => p.UsuarioId == usuarioId && p.Permiso == permiso);
        }

        public IEnumerable<PermisoUsuario> ObtenerPorUsuarioId(int usuarioId)
        {
            return _context.PermisosUsuarios
                .Where(p => p.UsuarioId == usuarioId);
        }

        public IEnumerable<PermisoUsuario> ObtenerTodos()
        {
            return [.. _context.PermisosUsuarios];
        }
    }
}
