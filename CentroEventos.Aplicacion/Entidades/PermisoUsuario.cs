using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.Entidades
{
    public class PermisoUsuario
    {
        public int UsuarioId { get; set; }
        public Permiso Permiso { get; set; }
        public Usuario Usuario { get; set; }
        public PermisoUsuario(int usuarioId, Permiso permiso)
        {
            UsuarioId = usuarioId;
            Permiso = permiso;
        }
        public PermisoUsuario(int usuarioId, Permiso permiso, Usuario usuario): this(usuarioId, permiso)
        {
            Usuario = usuario;
        }

        public override string ToString()
        {
            return $"PermisoUsuario: UsuarioId={UsuarioId}, Permiso={Permiso}, Usuario={Usuario}";
        }
    }
}
