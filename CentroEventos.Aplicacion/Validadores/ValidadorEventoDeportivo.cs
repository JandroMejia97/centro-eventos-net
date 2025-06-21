namespace CentroEventos.Aplicacion.Validadores;

using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Excepciones;

public class ValidadorEventoDeportivo : IValidadorEventoDeportivo {
    public void Validar(EventoDeportivo evento) {
        if (string.IsNullOrWhiteSpace(evento.Nombre))
            throw new ValidacionException("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(evento.Descripcion))
            throw new ValidacionException("La descripción es obligatoria.");
        if (evento.FechaHoraInicio < DateTime.Now)
            throw new ValidacionException("La fecha de inicio debe ser futura.");
        if (evento.CupoMaximo <= 0)
            throw new ValidacionException("El cupo máximo debe ser mayor a cero.");
        if (evento.DuracionHoras <= 0)
            throw new ValidacionException("La duración debe ser mayor a cero.");
    }
}
