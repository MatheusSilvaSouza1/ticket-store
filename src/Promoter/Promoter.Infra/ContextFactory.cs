using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

#pragma warning disable CS8625

namespace Infra;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Context>();
        optionsBuilder.UseNpgsql("Host=localhost; Database=ticket-store; Username=ticket-store; Password=postgres");

        return new Context(optionsBuilder.Options);
    }
}