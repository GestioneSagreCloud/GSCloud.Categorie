using Categorie.DataAccessLayer.Entities;

namespace Categorie.DataAccessLayer;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public virtual DbSet<Categoria> Categorie { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("Categorie");
            entity.HasKey(e => e.Id).HasName("PK_Categorie");
        });
    }
}