using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;

namespace NZWalksAPI.Extensions;

public static class HostExtensions
{
    public static async Task RunWithMigrationsAsync(this IHost host, string migrationControlEnvVar)
    {
        var migrationControl = GetMigrationControlFromEnv(migrationControlEnvVar);
        NotifyConsoleForMigrationStatus(migrationControl);
        await HandleMigrationsAsync(host, migrationControl);

        await host.RunAsync();
    }

    private static bool GetMigrationControlFromEnv(string key)
    {
        var migrationControlValue = Environment.GetEnvironmentVariable(key);

        if (string.IsNullOrWhiteSpace(migrationControlValue))
        {
            return false;
        }

        return ConvertBoolean(migrationControlValue);
    }

    private static bool ConvertBoolean(string value)
    {
        return value == "1" || value.Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    private static void NotifyConsoleForMigrationStatus(bool migrationControl)
    {
        if (migrationControl)
        {
            Console.WriteLine("Migrations will be executed");
        }
        else
        {
            Console.WriteLine("Migrations are ignored");
        }
    }

    private static async Task HandleMigrationsAsync(IHost host, bool migrationControl)
    {
        try
        {
            if (migrationControl)
            {
                Console.WriteLine("Starting to execute migrations...");

                using var scope = host.Services.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<NZWalksDbContext>();
                using var contextAuth = scope.ServiceProvider.GetRequiredService<NZWalksAuthDbContext>();

                await context.Database.MigrateAsync();
                await contextAuth.Database.MigrateAsync();

                Console.WriteLine("Migration execution finished");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Migrations failed");
            Console.WriteLine($"Exception: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");

            throw;
        }
    }
}