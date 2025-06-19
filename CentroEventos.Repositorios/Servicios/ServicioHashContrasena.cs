using System.Security.Cryptography;
using System.Text;
using CentroEventos.Aplicacion.Servicios;

namespace CentroEventos.Repositorios.Servicios;

public class ServicioHashContrasena : IServicioHashContrasena
{
    public string Hashear(string contrasena)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(contrasena);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }
}
