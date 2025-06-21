using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Aplicacion.Interfaces;

public interface IFuenteDeDatosPersona : IFuenteDeDatos<Persona>
{
    IEnumerable<Persona> ObtenerPorEmail(string email);
    IEnumerable<Persona> ObtenerPorDni(string dni);
}