namespace CentroEventos.Aplicacion.Excepciones;

public class EntidadNotFoundException : Exception
{
    public EntidadNotFoundException(string mensaje) : base(mensaje) { }
}