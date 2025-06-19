using CentroEventos.UI.Components;

using CentroEventos.Repositorios;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Validadores;
using CentroEventos.Aplicacion.Interfaces;

using CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Aplicacion.CasosDeUso.UsuarioUseCases;
using CentroEventos.Aplicacion.CasosDeUso.PersonaUseCases;
using CentroEventos.Aplicacion.CasosDeUso.EventoDeportivoUseCases;
using CentroEventos.Aplicacion.CasosDeUso.ReservaUseCases;

using CentroEventos.Repositorios.Repositorios;

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

// Repositorios
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioPersona, RepositorioPersona>();
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivo>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();

// Validadores
builder.Services.AddScoped<ValidadorPersona>();
builder.Services.AddScoped<ValidadorEventoDeportivo>();
builder.Services.AddScoped<ValidadorReserva>();
builder.Services.AddScoped<ValidadorUsuario>();

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
