using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public static string BaseDirectory { get; internal set; }
    public DbSet<Anime> Animes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=animes.db");
}
