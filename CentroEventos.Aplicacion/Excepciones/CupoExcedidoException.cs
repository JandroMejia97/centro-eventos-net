namespace CentroEventos.Aplicacion.Excepciones;

public class CupoExcedidoException : Exception
{
    public CupoExcedidoException(string mensaje) : base(mensaje) { }
}