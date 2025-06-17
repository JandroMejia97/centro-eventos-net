using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CentroEventos.Blazor;

using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.CasosDeUso;
using CentroEventos.Repositorios.FuentesDeDatos;
using CentroEventos.Repositorios.Repositorios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Fuente de datos CSV por cada entidad
builder.Services.AddScoped<IFuenteDeDatos<Persona>>(sp =>
    new FuenteDeDatosCsv<Persona>(
        "personas.csv",
        line => Persona.DesdeCsv(line),
        persona => persona.ACsv(),
        "persona_id.txt"
    ));

builder.Services.AddScoped<IFuenteDeDatos<EventoDeportivo>>(sp =>
    new FuenteDeDatosCsv<EventoDeportivo>(
        "eventos.csv",
        line => EventoDeportivo.DesdeCsv(line),
        evento => evento.ACsv(),
        "evento_id.txt"
    ));

builder.Services.AddScoped<IFuenteDeDatos<Reserva>>(sp =>
    new FuenteDeDatosCsv<Reserva>(
        "reservas.csv",
        line => Reserva.DesdeCsv(line),
        reserva => reserva.ACsv(),
        "reserva_id.txt"
    ));

// Repositorios
builder.Services.AddScoped<IRepositorioPersona, RepositorioPersona>();
builder.Services.AddScoped<IRepositorioEventoDeportivo, RepositorioEventoDeportivo>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();

// Casos de uso
builder.Services.AddScoped<ReservaAltaUseCase>();

await builder.Build().RunAsync();
