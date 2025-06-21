using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.Servicios;

public class ServicioAutorizacion : IServicioAutorizacion, IServicioCacheDePermisos
{
    private readonly IRepositorioPermisoUsuario _repoPermiso;
    private readonly IRepositorioUsuario _repoUsuario;
    private readonly Dictionary<int, HashSet<Permiso>> _permisosPorUsuario = new();
    public ServicioAutorizacion(IRepositorioPermisoUsuario repoPermiso, IRepositorioUsuario repoUsuario)
    {
        _repoPermiso = repoPermiso;
        _repoUsuario = repoUsuario;
    }
    public bool Autorizar(int usuarioId, Permiso accion)
    {
        Console.WriteLine($"Verificando permiso '{accion}' para usuario ID {usuarioId}.");
        if (usuarioId <= 0)
            throw new ValidacionException("ID de usuario inválido.");
        var permisosPorUsuario = _permisosPorUsuario.FirstOrDefault(p => p.Key == usuarioId).Value;
        if (permisosPorUsuario is null)
        {
            if (_repoUsuario.ObtenerPorId(usuarioId) is null)
                throw new EntidadNotFoundException("Usuario no encontrado.");
            permisosPorUsuario = [.. _repoPermiso.ObtenerPorUsuario(usuarioId).Select(p => p.Permiso)];
            _permisosPorUsuario[usuarioId] = permisosPorUsuario;
        }
        return permisosPorUsuario.Contains(accion);
    }

    public void Agregar(int usuarioId, Permiso permiso)
    {
        var permisosPorUsuario = new HashSet<Permiso> { permiso };
        Agregar(usuarioId, permisosPorUsuario);
    }

    public void Agregar(int usuarioId, IEnumerable<Permiso> permisos)
    {

        if (usuarioId <= 0)
            throw new ValidacionException("ID de usuario inválido.");
        if (_repoUsuario.ObtenerPorId(usuarioId) is null)
            throw new EntidadNotFoundException("Usuario no encontrado.");
        var permisosPorUsuario = _permisosPorUsuario.FirstOrDefault(p => p.Key == usuarioId).Value;
        if (permisosPorUsuario is not null)
        {
            foreach (var permiso in permisos)
            {
                permisosPorUsuario.Add(permiso);
            }
        }
        else
        {
            permisosPorUsuario = [.. permisos];
            _permisosPorUsuario[usuarioId] = permisosPorUsuario;
        }
    }

    public void Eliminar(int usuarioId)
    {
        if (usuarioId <= 0)
            throw new ValidacionException("ID de usuario inválido.");
        _permisosPorUsuario.Remove(usuarioId);
    }

    public void Eliminar(int usuarioId, Permiso permiso)
    {
        var permisosPorUsuario = new HashSet<Permiso> { permiso };
        Eliminar(usuarioId, permisosPorUsuario);
    }

    public void Eliminar(int usuarioId, IEnumerable<Permiso> permisos)
    {
        if (usuarioId <= 0)
            throw new ValidacionException("ID de usuario inválido.");
        var permisosPorUsuario = _permisosPorUsuario.FirstOrDefault(p => p.Key == usuarioId).Value;
        permisosPorUsuario?.RemoveWhere(p => permisos.Contains(p));
    }
}
