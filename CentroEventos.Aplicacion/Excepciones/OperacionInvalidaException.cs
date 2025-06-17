namespace CentroEventos.Aplicacion.Excepciones;

public class OperacionInvalidaException(string mensaje) : Exception(mensaje)
{
}