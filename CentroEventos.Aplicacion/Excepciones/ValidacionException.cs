namespace CentroEventos.Aplicacion.Excepciones;

public class ValidacionException : Exception
{
    public ValidacionException(string mensaje) : base(mensaje) { }
}