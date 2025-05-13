namespace CentroEventos.Aplicacion.Excepciones;

public class FalloAutorizacionException : Exception
{
    public FalloAutorizacionException(string mensaje) : base(mensaje) { }
}