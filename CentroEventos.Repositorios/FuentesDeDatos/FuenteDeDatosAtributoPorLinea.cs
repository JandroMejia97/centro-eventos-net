using CentroEventos.Aplicacion.Interfaces;
using System.Text;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public class FuenteDeDatosAtributoPorLinea<T> : IFuenteDeDatos<T> where T : class, new()
{
    private readonly string _filePath;
    private readonly Func<IEnumerable<string>, T> _deserializar;
    private readonly Func<T, IEnumerable<string>> _serializar;

    public FuenteDeDatosAtributoPorLinea(string filePath, Func<IEnumerable<string>, T> deserializar, Func<T, IEnumerable<string>> serializar)
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

        var lines = File.ReadAllLines(_filePath, Encoding.UTF8);
        var entidades = new List<T>();
        for (int i = 0; i < lines.Length; i += typeof(T).GetProperties().Length)
        {
            var atributos = lines.Skip(i).Take(typeof(T).GetProperties().Length);
            entidades.Add(_deserializar(atributos));
        }
        return entidades;
    }

    private void Guardar(IEnumerable<T> entidades)
    {
        var lines = new List<string>();
        foreach (var entidad in entidades)
        {
            lines.AddRange(_serializar(entidad));
        }
        File.WriteAllLines(_filePath, lines, Encoding.UTF8);
    }
}