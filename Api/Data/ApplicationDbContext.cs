using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Anime> Animes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Anime>().HasKey(e => e.Id);
    }
}