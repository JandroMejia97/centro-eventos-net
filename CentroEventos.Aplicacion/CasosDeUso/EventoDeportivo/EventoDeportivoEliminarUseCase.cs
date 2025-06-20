using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Aplicacion.Enums;

namespace CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoUseCases;

// Caso de uso para eliminar un Evento Deportivo
public class EventoDeportivoEliminarUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion)
{
    private readonly IRepositorioEventoDeportivo _repositorioEvento = repositorioEvento;
    private readonly IServicioAutorizacion _servicioAutorizacion = servicioAutorizacion;

    public void Ejecutar(int eventoId, int usuarioId) {
        _servicioAutorizacion.Autorizar(usuarioId, Permiso.EliminarEvento);
        if (_repositorioEvento.ObtenerPorId(eventoId) is null)
            throw new EntidadNotFoundException("Evento no encontrado.");
        _repositorioEvento.Eliminar(eventoId);
    }
}
