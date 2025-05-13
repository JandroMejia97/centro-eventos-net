using CentroEventos.Aplicacion.Interfaces;
using System.Text;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public class FuenteDeDatosCsv<T> : IFuenteDeDatos<T> where T : class, new()
{
    private readonly string _filePath;
    private readonly Func<string, T> _deserializar;
    private readonly Func<T, string> _serializar;

    public FuenteDeDatosCsv(string filePath, Func<string, T> deserializar, Func<T, string> serializar)
    {
        _filePath = filePath;
        _deserializar = deserializar;
        _serializar = serializar;
    }

    public void Agregar(T entidad)
    {
        var entidades = ObtenerTodos().ToList();
        entidades.Add(entidad);
        Guardar(entidades);
    }

    public void Modificar(T entidad)
    {
        var entidades = ObtenerTodos().ToList();
        var index = entidades.FindIndex(e => e.Equals(entidad));
        if (index == -1) throw new EntidadNotFoundException("Entidad no encontrada para modificar.");
        entidades[index] = entidad;
        Guardar(entidades);
    }

    public void Eliminar(int id)
    {
        var entidades = ObtenerTodos().ToList();
        var entidad = entidades.FirstOrDefault(e => e.GetHashCode() == id);
        if (entidad == null) throw new EntidadNotFoundException("Entidad no encontrada para eliminar.");
        entidades.Remove(entidad);
        Guardar(entidades);
    }

    public IEnumerable<T> ObtenerTodos()
    {
        if (!File.Exists(_filePath)) return new List<T>();

        return File.ReadAllLines(_filePath, Encoding.UTF8)
            .Select(line => _deserializar(line));
    }

    private void Guardar(IEnumerable<T> entidades)
    {
        File.WriteAllLines(_filePath, entidades.Select(e => _serializar(e)), Encoding.UTF8);
    }
}