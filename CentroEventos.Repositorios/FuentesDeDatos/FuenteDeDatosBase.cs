using System.Text;

namespace CentroEventos.Repositorios.FuentesDeDatos;

public abstract class FuenteDeDatosBase<T> where T : class, new()
{
    private readonly string _idFilePath;

    protected FuenteDeDatosBase(string idFilePath)
    {
        _idFilePath = idFilePath;
    }

    protected int GenerarNuevoId()
    {
        if (!File.Exists(_idFilePath))
        {
            File.WriteAllText(_idFilePath, "0");
        }

        var lastId = int.Parse(File.ReadAllText(_idFilePath));
        var newId = lastId + 1;
        File.WriteAllText(_idFilePath, newId.ToString());
        return newId;
    }

    protected void AsignarId(T entidad)
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty != null && idProperty.CanWrite)
        {
            idProperty.SetValue(entidad, GenerarNuevoId());
        }
    }
}