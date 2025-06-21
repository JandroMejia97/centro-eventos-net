using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Entidades
{
    public class PermisoUsuario(int usuarioId, Permiso permiso)
    {
        public int UsuarioId { get; set; } = usuarioId;
        public Permiso Permiso { get; set; } = permiso;
        public Usuario Usuario { get; set; } = null!;

        public PermisoUsuario(int usuarioId, Permiso permiso, Usuario usuario): this(usuarioId, permiso)
        {
            Usuario = usuario;
        }

        public PermisoUsuario(Permiso permiso, Usuario usuario)
            : this(usuario.PersonaId, permiso)
        {
            Usuario = usuario;
        }

        public override string ToString()
        {
            return $"PermisoUsuario: UsuarioId={UsuarioId}, Permiso={Permiso}, Usuario={Usuario}";
        }
    }
}
