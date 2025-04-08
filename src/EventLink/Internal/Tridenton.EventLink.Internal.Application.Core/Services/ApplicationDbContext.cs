using Microsoft.EntityFrameworkCore;

namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// 
    /// </summary>
    public DbSet<ErrorEntity> Errors { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite("", options =>
        {
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            options.CommandTimeout(0);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ErrorEntityConfiguration());
    }
}