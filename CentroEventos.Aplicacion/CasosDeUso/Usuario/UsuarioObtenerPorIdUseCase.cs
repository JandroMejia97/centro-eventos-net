using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Excepciones;

namespace CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;

public class UsuarioObtenerPorIdUseCase(IRepositorioUsuario repositorioUsuario)
{
    public Usuario Ejecutar(int id) {
        return repositorioUsuario.ObtenerPorId(id) ?? throw new EntidadNotFoundException("Usuario no encontrado.");
    }
}
