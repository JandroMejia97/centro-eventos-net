using CentroEventos.UI.Components;

using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;
using CentroEventos.Aplicacion.CasosDeUso.PersonaUseCases;
using CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoUseCases;
using CentroEventos.Aplicacion.CasosDeUso.ReservaUseCases;

using CentroEventos.Repositorios.Repositorios;
using CentroEventos.Repositorios.FuentesDeDatos;
using CentroEventos.Repositorios.Contextos;
using CentroEventos.Aplicacion.Servicios;
using CentroEventos.Repositorios.Servicios;
using Microsoft.AspNetCore.Components.Authorization;
using CentroEventos.UI.Auth;
using CentroEventos.Repositorios;
using CentroEventos.Aplicacion.CasosDeUso.PermisoUsuarioUseCases;

CentroEventosSqlite.Inicializar();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Casos de uso - Usuario
builder.Services.AddTransient<UsuarioCrearUseCase>();
builder.Services.AddTransient<UsuarioActualizarUseCase>();
builder.Services.AddTransient<UsuarioEliminarUseCase>();
builder.Services.AddTransient<UsuarioLoginUseCase>();
builder.Services.AddTransient<UsuarioObtenerPorIdUseCase>();
builder.Services.AddTransient<UsuarioObtenerTodosUseCase>();

// Casos de uso - Persona
builder.Services.AddTransient<PersonaCrearUseCase>();
builder.Services.AddTransient<PersonaActualizarUseCase>();
builder.Services.AddTransient<PersonaEliminarUseCase>();
builder.Services.AddTransient<PersonaObtenerPorIdUseCase>();
builder.Services.AddTransient<PersonaObtenerTodosUseCase>();

// Casos de uso - Evento Deportivo
builder.Services.AddTransient<EventoDeportivoCrearUseCase>();
builder.Services.AddTransient<EventoDeportivoActualizarUseCase>();
builder.Services.AddTransient<EventoDeportivoEliminarUseCase>();
builder.Services.AddTransient<EventoDeportivoObtenerPorIdUseCase>();
builder.Services.AddTransient<EventoDeportivoObtenerTodosUseCase>();

// Casos de uso - Reserva
builder.Services.AddTransient<ReservaAltaUseCase>();
builder.Services.AddTransient<ReservaActualizarUseCase>();
builder.Services.AddTransient<ReservaEliminarUseCase>();
builder.Services.AddTransient<ReservaObtenerPorIdUseCase>();
builder.Services.AddTransient<ReservaObtenerTodosUseCase>();

// Casos de uso - Permiso Usuario
builder.Services.AddTransient<PermisoUsuarioAgregarUseCase>();
builder.Services.AddTransient<PermisoUsuarioEliminarUseCase>();
builder.Services.AddTransient<PermisoUsuarioObtenerUseCase>();
builder.Services.AddTransient<PermisoUsuarioObtenerPorUsuarioUseCase>();
builder.Services.AddTransient<PermisoUsuarioObtenerTodosUseCase>();

// Contexto de BD
builder.Services.AddTransient<CentroEventosDbContext>();

// Autorización
builder.Services.AddTransient<IServicioAutorizacion, ServicioAutorizacion>();
builder.Services.AddSingleton<IServicioHashContrasena, ServicioHashContrasena>();
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Fuentes de datos
builder.Services.AddSingleton<IFuenteDeDatos<Usuario>, FuenteDeDatosUsuarioEF>();
builder.Services.AddSingleton<IFuenteDeDatos<Persona>, FuenteDeDatosPersonaEF>();
builder.Services.AddSingleton<IFuenteDeDatos<EventoDeportivo>, FuenteDeDatosEventoDeportivoEF>();
builder.Services.AddSingleton<IFuenteDeDatos<Reserva>, FuenteDeDatosReservaEF>();
builder.Services.AddSingleton<IFuenteDeDatosPermisoUsuario, FuenteDeDatosPermisoUsuarioEF>();

// Repositorios
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioPersona, RepositorioPersona>();
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivo>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddScoped<IRepositorioPermisoUsuario, RepositorioPermisoUsuario>();

// Validadores
builder.Services.AddScoped<IValidadorPersona, ValidadorPersona>();
builder.Services.AddScoped<IValidadorEventoDeportivo, ValidadorEventoDeportivo>();
builder.Services.AddScoped<IValidadorReserva, ValidadorReserva>();
builder.Services.AddScoped<IValidadorUsuario, ValidadorUsuario>();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
