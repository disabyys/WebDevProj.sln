using Microsoft.EntityFrameworkCore;
using WebDevProj.Api.Models;

namespace WebDevProj.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Valera> Valeras { get; set; }
}