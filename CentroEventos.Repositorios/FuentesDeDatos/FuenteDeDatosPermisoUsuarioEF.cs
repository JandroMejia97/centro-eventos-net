using System.Collections.Generic;
using System.Linq;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Repositorios.Contextos;
using Microsoft.EntityFrameworkCore;

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
            var permisoExistente = _context.PermisosUsuarios
                .FirstOrDefault(p => p.UsuarioId == permiso.UsuarioId && p.Permiso == permiso.Permiso);
            if (permisoExistente != null)
            {
                _context.PermisosUsuarios.Remove(permisoExistente);
                _context.SaveChanges();
            }
        }

        public void Eliminar(int usuarioId, IEnumerable<Permiso> permisos)
        {
            var permisoUsuarios = permisos.Select(p => new PermisoUsuario(usuarioId, p));
            Eliminar(permisoUsuarios);
        }
        public void Eliminar(IEnumerable<PermisoUsuario> permisos)
        {
            if (!permisos.Any()) return;
            
            // Extraer los IDs de usuario únicos
            var usuarioIds = permisos.Select(p => p.UsuarioId).Distinct().ToList();
            var permisosEnum = permisos.Select(p => p.Permiso).Distinct().ToList();
            
            // Hacer una consulta más simple que EF pueda traducir
            var permisosExistentes = _context.PermisosUsuarios
                .Where(p => usuarioIds.Contains(p.UsuarioId) && permisosEnum.Contains(p.Permiso))
                .ToList();
            
            // Filtrar en memoria para obtener exactamente los que queremos eliminar
            var permisosAEliminar = permisosExistentes
                .Where(pe => permisos.Any(p => p.UsuarioId == pe.UsuarioId && p.Permiso == pe.Permiso))
                .ToList();
            
            if (permisosAEliminar.Any())
            {
                _context.PermisosUsuarios.RemoveRange(permisosAEliminar);
                _context.SaveChanges();
            }
        }

        public PermisoUsuario? Obtener(int usuarioId, Permiso permiso)
        {
            return _context.PermisosUsuarios
                .AsNoTracking()
                .FirstOrDefault(p => p.UsuarioId == usuarioId && p.Permiso == permiso);
        }

        public IEnumerable<PermisoUsuario> ObtenerPorUsuarioId(int usuarioId)
        {
            return _context.PermisosUsuarios
                .AsNoTracking()
                .Where(p => p.UsuarioId == usuarioId);
        }

        public IEnumerable<PermisoUsuario> ObtenerTodos()
        {
            return [.. _context.PermisosUsuarios.AsNoTracking()];
        }
    }
}
