using Microsoft.EntityFrameworkCore;
using CentroEventos.Aplicacion.Entidades;

namespace CentroEventos.Repositorios.Contextos;

public class CentroEventosDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<EventoDeportivo> EventosDeportivos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<PermisoUsuario> PermisosUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=CentroEventos.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PermisoUsuario>()
            .HasKey(p => new { p.UsuarioId, p.Permiso });

        modelBuilder.Entity<Reserva>()
            .HasKey(r => new { r.PersonaId, r.EventoDeportivoId });

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Persona)
            .WithMany()
            .HasForeignKey(r => r.PersonaId);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.EventoDeportivo)
            .WithMany()
            .HasForeignKey(r => r.EventoDeportivoId);

        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.PersonaId);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Persona)
            .WithOne()
            .HasForeignKey<Usuario>(u => u.PersonaId);

        modelBuilder.Entity<EventoDeportivo>()
            .HasOne(e => e.Responsable)
            .WithMany()
            .HasForeignKey(e => e.ResponsableId);
    }
}
