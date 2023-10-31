using asp_final_test.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_final_test.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasData(
            new Category
                { Id = 1, Name = "Inactivated", CreatedAt = new DateTime(2023, 10, 31, 2, 40, 50) },
            new Category
                { Id = 2, Name = "Live-attenuated", CreatedAt = new DateTime(2023, 10, 31, 3, 44, 12) },
            new Category
                { Id = 3, Name = "Messenger RNA (mRNA)",  CreatedAt = new DateTime(2023, 10, 31, 4, 55, 23) },
            new Category
                { Id = 4, Name = "Subunit, recombinant, polysaccharide, and conjugate",  CreatedAt = new DateTime(2023, 10, 31, 5, 22, 34) });
    }
}