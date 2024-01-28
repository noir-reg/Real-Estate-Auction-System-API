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

    public virtual DbSet<Staff> Staffs { get; set; }
    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<Auction> Auctions { get; set; }
    public virtual DbSet<AuctionRegistration> AuctionRegistrations { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Bid> Bids { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<RealEstate> RealEstates { get; set; }
    public virtual DbSet<RealEstateOwner> RealEstateOwners { get; set; }
    public virtual DbSet<LegalDocument> LegalDocuments { get; set; }
    


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionStrings());
    }

    private static string GetConnectionStrings()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").AddJsonFile("appsettings.Development.json", true)
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
            builder.Property(e => e.Gender).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.CitizenId).IsRequired().HasMaxLength(12).IsFixedLength();
            builder.Property(e => e.Email).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Password).IsRequired().HasMaxLength(32);
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(30);
            builder.Property(e => e.Username).IsRequired().HasMaxLength(30);
            builder.HasIndex(e => new
            {
                e.CitizenId,
                e.Email,
                e.Username
            }).IsUnique()
            ;
        });

        modelBuilder.Entity<User>().ToTable("Users");


        // modelBuilder.Entity<Role>(builder =>
        //     {
        //         builder.HasKey(e => e.RoleId);
        //         builder.Property(e => e.RoleId).ValueGeneratedOnAdd();
        //         builder.Property(e => e.Name).IsRequired();
        //     }
        // );
    }
}