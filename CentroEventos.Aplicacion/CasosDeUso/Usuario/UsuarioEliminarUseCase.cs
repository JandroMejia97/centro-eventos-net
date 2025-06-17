using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;

public class UsuarioEliminarUseCase(IRepositorioUsuario repositorioUsuario)
{
    public void Ejecutar(int usuarioId) {
        if (repositorioUsuario.ObtenerPorId(usuarioId) is null)
            throw new EntidadNotFoundException("Usuario no encontrado.");
        repositorioUsuario.Eliminar(usuarioId);
    }
}
