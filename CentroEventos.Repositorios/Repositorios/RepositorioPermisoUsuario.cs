using System.Collections.Generic;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using System.Linq;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Repositorios.Repositorios
{
    public class RepositorioPermisoUsuario : IRepositorioPermisoUsuario
    {
        private readonly IFuenteDeDatosPermisoUsuario _fuente;
        public RepositorioPermisoUsuario(IFuenteDeDatosPermisoUsuario fuente)
        {
            _fuente = fuente;
        }
        public void Agregar(PermisoUsuario permiso) => _fuente.Agregar(permiso);
        public void Eliminar(int usuarioId, Permiso permiso) => _fuente.Eliminar(usuarioId, permiso);
        public PermisoUsuario? Obtener(int usuarioId, Permiso permiso) => 
            _fuente.Obtener(usuarioId, permiso);
        public IEnumerable<PermisoUsuario> ObtenerPorUsuario(int usuarioId) => _fuente.ObtenerPorUsuarioId(usuarioId);
        public IEnumerable<PermisoUsuario> ObtenerTodos() => _fuente.ObtenerTodos();
    }
}
