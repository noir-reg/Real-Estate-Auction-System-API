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


    public virtual DbSet<User> Users { get; set; }
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
        optionsBuilder.UseSqlServer(GetConnectionStrings()).EnableSensitiveDataLogging();
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
            builder.Property(e => e.Gender).HasColumnType("nvarchar").HasMaxLength(10).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.Property(e => e.CitizenId).IsRequired().HasMaxLength(12).IsFixedLength().HasColumnType("nvarchar");
            builder.Property(e => e.Email).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Password).IsRequired().HasMaxLength(32);
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(30);
            builder.Property(e => e.Username).IsRequired().HasMaxLength(30);
            builder.HasIndex(e => new
                {
                    e.CitizenId,
                    e.Email,
                    e.Username,
                    e.PhoneNumber
                }).IsUnique()
                ;
            builder.HasDiscriminator(e => e.Role).HasValue<Admin>("Admin").HasValue<Member>("Member")
                .HasValue<Staff>("Staff");
            builder.Property(e => e.Role).HasMaxLength(30).HasColumnType("nvarchar");
        });

        modelBuilder.Entity<Auction>(builder =>
        {
            builder.HasKey(e => e.AuctionId);
            builder.Property(e => e.AuctionId).ValueGeneratedOnAdd();
            builder.Property(e => e.Title).IsRequired().HasMaxLength(50).HasColumnType("nvarchar");
            builder.Property(e => e.Description).IsRequired().HasColumnType("text");
            builder.Property(e => e.Status).IsRequired().HasMaxLength(30).HasColumnType("nvarchar");
            builder.Property(e => e.IncrementalPrice).IsRequired().HasPrecision(18, 0);
            builder.Property(e => e.InitialPrice).IsRequired().HasPrecision(18, 0);
            builder.HasOne(e => e.StartingBid).WithOne().HasForeignKey<Auction>(e => e.StartingBidId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.CurrentBid).WithOne().HasForeignKey<Auction>(e => e.CurrentBidId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.WinningBid).WithOne().HasForeignKey<Auction>(e => e.WinningBidId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.RealEstate).WithOne(e => e.Auction).HasForeignKey<Auction>(e => e.RealEstateId);
            builder.Property(e => e.ListingDate).IsRequired().ValueGeneratedOnAdd();
            builder.HasOne(e => e.Admin).WithOne(e => e.Auction).HasForeignKey<Auction>(e => e.AdminId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.Staff).WithMany(e => e.Auctions).HasForeignKey(e => e.StaffId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<RealEstate>(builder =>
        {
            builder.HasKey(e => e.RealEstateId);
            builder.Property(e => e.RealEstateId).ValueGeneratedOnAdd();
            builder.Property(e => e.Status).HasColumnType("nvarchar").HasMaxLength(30).IsRequired();
            builder.Property(e => e.Address).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(e => e.Description).HasColumnType("text");
            builder.Property(e => e.ImageUrl).HasColumnType("text");
            builder.HasOne(e => e.Owner).WithMany(e => e.RealEstates).HasForeignKey(e => e.OwnerId);
        });

        modelBuilder.Entity<AuctionRegistration>(builder =>
        {
            builder.HasKey(e => e.RegistrationId);
            builder.Property(e => e.RegistrationId).ValueGeneratedOnAdd();
            builder.Property(e => e.RegistrationStatus).HasColumnType("nvarchar").HasMaxLength(30);
            builder.Property(e => e.RegistrationDate).ValueGeneratedOnAdd();
            builder.Property(e => e.DepositAmount).HasPrecision(18, 0);
        });

        modelBuilder.Entity<Transaction>(builder =>
        {
            builder.HasKey(e => e.TransactionId);
            builder.Property(e => e.TransactionId).ValueGeneratedOnAdd();
            builder.Property(e => e.TransactionDate).ValueGeneratedOnAdd();
            builder.Property(e => e.Amount).IsRequired().HasPrecision(18, 0);
            builder.Property(e => e.Status).HasColumnType("nvarchar").HasMaxLength(30);
            builder.Property(e => e.PaymentMethod).HasColumnType("nvarchar").HasMaxLength(30);
            builder.HasOne(e => e.Bid).WithOne(e => e.Transaction).HasForeignKey<Transaction>(e => e.TransactionId);
        });

        modelBuilder.Entity<RealEstateOwner>(builder =>
        {
            builder.HasKey(e => e.RealEstateOwnerId);
            builder.Property(e => e.RealEstateOwnerId).ValueGeneratedOnAdd();
            builder.Property(e => e.CitizenId).HasMaxLength(10).IsFixedLength().IsRequired();
            builder.Property(e => e.ContactInformation).HasColumnType("text");
        });

        modelBuilder.Entity<Bid>(builder =>
        {
            builder.HasKey(e => e.BidId);
            builder.Property(e => e.BidId).ValueGeneratedOnAdd();
            builder.HasOne(e => e.Member).WithMany(e => e.Bids).HasForeignKey(e => e.MemberId);
            builder.HasOne(e => e.Auction).WithMany(e => e.Bids).HasForeignKey(e => e.AuctionId);
            builder.Property(e => e.Amount).IsRequired().HasPrecision(18, 0);
            builder.Property(e => e.IsWinningBid).IsRequired();
        });

        modelBuilder.Entity<LegalDocument>(builder =>
        {
            builder.HasKey(e => e.DocumentId);
            builder.Property(e => e.DocumentId).ValueGeneratedOnAdd();
            builder.Property(e => e.DocumentUrl).HasColumnType("text");
            builder.Property(e => e.DocumentType).HasColumnType("nvarchar").HasMaxLength(30);
            builder.Property(e => e.FileName).HasColumnType("nvarchar").HasMaxLength(100);
            builder.HasOne(e => e.RealEstate).WithMany(e => e.LegalDocuments).HasForeignKey(e => e.RealEstateId);
        });

        modelBuilder.Entity<AuctionRegistration>().ToTable("AuctionRegistrations");
        modelBuilder.Entity<RealEstateOwner>().ToTable("RealEstateOwners");
        modelBuilder.Entity<Transaction>().ToTable("Transactions");
        modelBuilder.Entity<RealEstate>().ToTable("RealEstates");
        modelBuilder.Entity<Auction>().ToTable("Auctions");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Bid>().ToTable("Bids");
        modelBuilder.Entity<LegalDocument>().ToTable("LegalDocuments");
    }
}