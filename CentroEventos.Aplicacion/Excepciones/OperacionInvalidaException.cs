namespace CentroEventos.Aplicacion.Excepciones;

public class OperacionInvalidaException : Exception
{
    public OperacionInvalidaException(string mensaje) : base(mensaje) { }
}