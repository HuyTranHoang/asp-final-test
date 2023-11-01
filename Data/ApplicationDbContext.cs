using asp_final_test.Models;
using Microsoft.EntityFrameworkCore;
using Type = asp_final_test.Models.Type;

namespace asp_final_test.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Type> Types { get; set; }

    public DbSet<Vaccine> Vaccines { get; set; }

    public DbSet<VaccinationSchedule> VaccinationSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Type>().HasData(
            new Type
                { Id = 1, Name = "Inactivated", CreatedAt = new DateTime(2023, 10, 31, 2, 40, 50) },
            new Type
                { Id = 2, Name = "Live-attenuated", CreatedAt = new DateTime(2023, 10, 31, 3, 44, 12) },
            new Type
                { Id = 3, Name = "Messenger RNA (mRNA)",  CreatedAt = new DateTime(2023, 10, 31, 4, 55, 23) },
            new Type
                { Id = 4, Name = "Subunit, recombinant, polysaccharide, and conjugate",  CreatedAt = new DateTime(2023, 10, 31, 5, 22, 34) });
    }
}