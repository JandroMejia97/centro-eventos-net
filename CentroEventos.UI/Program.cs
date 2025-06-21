using CentroEventos.UI.Components;
using Microsoft.AspNetCore.Components.Authorization;

using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Aplicacion.Servicios;

using CentroEventos.Repositorios.Repositorios;
using CentroEventos.Repositorios.FuentesDeDatos;
using CentroEventos.Repositorios.Contextos;
using CentroEventos.Repositorios.Servicios;
using CentroEventos.UI.Auth;
using CentroEventos.Repositorios;


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
builder.Services.AddTransient<ReservaCrearUseCase>();
builder.Services.AddTransient<ReservaActualizarUseCase>();
builder.Services.AddTransient<ReservaEliminarUseCase>();
builder.Services.AddTransient<ReservaObtenerUseCase>();
builder.Services.AddTransient<ReservaObtenerPorPersonaUseCase>();
builder.Services.AddTransient<ReservaObtenerPorEventoUseCase>();

// Casos de uso - Permiso Usuario
builder.Services.AddTransient<PermisoUsuarioAgregarUseCase>();
builder.Services.AddTransient<PermisoUsuarioEliminarUseCase>();
builder.Services.AddTransient<PermisoUsuarioObtenerUseCase>();
builder.Services.AddTransient<PermisoUsuarioObtenerPorUsuarioUseCase>();
builder.Services.AddTransient<PermisoUsuarioObtenerTodosUseCase>();

// Contexto de BD
builder.Services.AddTransient<CentroEventosDbContext>();

// Autorización
builder.Services.AddSingleton<IServicioAutorizacion, ServicioAutorizacion>();
builder.Services.AddSingleton<IServicioCacheDePermisos, ServicioAutorizacion>();
builder.Services.AddSingleton<IServicioHashContrasena, ServicioHashContrasena>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Fuentes de datos
builder.Services.AddSingleton<IFuenteDeDatos<Usuario>, FuenteDeDatosUsuarioEF>();
builder.Services.AddSingleton<IFuenteDeDatosPersona, FuenteDeDatosPersonaEF>();
builder.Services.AddSingleton<IFuenteDeDatosEventoDeportivo, FuenteDeDatosEventoDeportivoEF>();
builder.Services.AddSingleton<IFuenteDeDatosReserva, FuenteDeDatosReservaEF>();
builder.Services.AddSingleton<IFuenteDeDatosPermisoUsuario, FuenteDeDatosPermisoUsuarioEF>();

// Repositorios
builder.Services.AddSingleton<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddSingleton<IRepositorioPersona, RepositorioPersona>();
builder.Services.AddSingleton<IRepositorioEventoDeportivo, RepositorioEventoDeportivo>();
builder.Services.AddSingleton<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddSingleton<IRepositorioPermisoUsuario, RepositorioPermisoUsuario>();

// Validadores
builder.Services.AddScoped<IValidadorPersona, ValidadorPersona>();
builder.Services.AddScoped<IValidadorEventoDeportivo, ValidadorEventoDeportivo>();
builder.Services.AddScoped<IValidadorReserva, ValidadorReserva>();
builder.Services.AddScoped<IValidadorUsuario, ValidadorUsuario>();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
