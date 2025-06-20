using CentroEventos.Repositorios.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CentroEventos.Repositorios;

public static class CentroEventosSqlite
{
    public static void Inicializar()
    {
        using var context = new CentroEventosDbContext();
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Base de datos creada exitosamente.");
        }
        else
        {
            Console.WriteLine("La base de datos ya existe.");
        }
        var connection = context.Database.GetDbConnection();
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA journal_mode=DELETE;";
            command.ExecuteNonQuery();
        }

    }
}