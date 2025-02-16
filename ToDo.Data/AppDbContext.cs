using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ToDo.Data;

internal class AppDbContext : IdentityDbContext<AppUser>
{
    public DbSet<ToDoListEntity> ToDoLists { get; set; }
    public DbSet<ToDoItemEntity> ToDoItems { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}

internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        IConfigurationBuilder configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        var config = configBuilder.Build();

        var connString = config.GetConnectionString("Default") ?? throw new ArgumentNullException("Unable to get connection string.");

        var oBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connString);

        return new AppDbContext(oBuilder.Options);
    }
}
