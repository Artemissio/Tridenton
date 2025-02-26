using Microsoft.EntityFrameworkCore;
using Tridenton.EventLink.Internal.Sources.PostgreSQL.Models;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Services;

internal sealed class ApplicationDbContext : DbContext
{
    public DbSet<ChangeEntity> Changes { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
}