using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Entities;

public class RealEstateDbContext : DbContext
{
    public RealEstateDbContext(DbContextOptions options) : base(options)
    {
    }

    public RealEstateDbContext()
    {
    }

    //public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionStrings());
    }

    private static string GetConnectionStrings()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();

        return config.GetConnectionString("DefaultConnection");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(e => e.UserId);
            builder.Property(e => e.UserId).ValueGeneratedOnAdd();
            //builder.Property(e => e.RoleId).IsRequired();
            builder.Property(e => e.Gender).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.CitizenId).IsRequired().HasMaxLength(12).IsFixedLength();
            builder.Property(e => e.Email).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Password).IsRequired().HasMaxLength(32);

            // builder.HasOne(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId).IsRequired();
        });


        // modelBuilder.Entity<Role>(builder =>
        //     {
        //         builder.HasKey(e => e.RoleId);
        //         builder.Property(e => e.RoleId).ValueGeneratedOnAdd();
        //         builder.Property(e => e.Name).IsRequired();
        //     }
        // );
    }
}