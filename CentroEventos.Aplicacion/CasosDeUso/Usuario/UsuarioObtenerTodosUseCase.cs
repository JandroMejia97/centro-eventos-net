using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;

public class UsuarioObtenerTodosUseCase(IRepositorioUsuario repositorioUsuario)
{
    public IEnumerable<Usuario> Ejecutar() {
        return repositorioUsuario.ObtenerTodos();
    }
}
