﻿// <auto-generated />
using System;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObjects.Migrations
{
    [DbContext(typeof(RealEstateDbContext))]
    [Migration("20240201032618_AddTonsOfEntities")]
    partial class AddTonsOfEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObjects.Entities.Auction", b =>
                {
                    b.Property<Guid>("AuctionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuctionPeriodEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AuctionPeriodStart")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CurrentBid")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("IncrementalPrice")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("InitialPrice")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<DateTime>("ListingDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RealEstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RegistrationPeriodEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RegistrationPeriodStart")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("StartingBid")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("WinningBid")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("AuctionId");

                    b.HasIndex("StaffId");

                    b.ToTable("Auctions", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.AuctionRegistration", b =>
                {
                    b.Property<Guid>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuctionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("DepositAmount")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("RegistrationStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("RegistrationId");

                    b.HasIndex("AuctionId");

                    b.HasIndex("MemberId");

                    b.ToTable("AuctionRegistrations", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.Bid", b =>
                {
                    b.Property<Guid>("BidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<Guid>("AuctionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsWinningBid")
                        .HasColumnType("bit");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BidId");

                    b.HasIndex("AuctionId");

                    b.HasIndex("MemberId");

                    b.ToTable("Bids", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.LegalDocument", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("DocumentUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RealEstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DocumentId");

                    b.HasIndex("RealEstateId");

                    b.ToTable("LegalDocuments", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.RealEstate", b =>
                {
                    b.Property<Guid>("RealEstateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RealEstateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("RealEstateId");

                    b.HasIndex("OwnerId");

                    b.ToTable("RealEstates", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.RealEstateOwner", b =>
                {
                    b.Property<Guid>("RealEstateOwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CitizenId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.Property<string>("ContactInformation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RealEstateOwnerId");

                    b.ToTable("RealEstateOwners", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<Guid>("BidId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("TransactionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CitizenId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nchar(12)")
                        .IsFixedLength();

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserId");

                    b.HasIndex("CitizenId", "Email", "Username")
                        .IsUnique();

                    b.ToTable("Users", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Admin", b =>
                {
                    b.HasBaseType("BusinessObjects.Entities.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Member", b =>
                {
                    b.HasBaseType("BusinessObjects.Entities.User");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Member");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Staff", b =>
                {
                    b.HasBaseType("BusinessObjects.Entities.User");

                    b.HasDiscriminator().HasValue("Staff");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Auction", b =>
                {
                    b.HasOne("BusinessObjects.Entities.RealEstate", "RealEstate")
                        .WithOne("Auction")
                        .HasForeignKey("BusinessObjects.Entities.Auction", "AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Entities.Admin", "Admin")
                        .WithOne("Auction")
                        .HasForeignKey("BusinessObjects.Entities.Auction", "AuctionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Entities.Staff", "Staff")
                        .WithMany("Auctions")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("RealEstate");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("BusinessObjects.Entities.AuctionRegistration", b =>
                {
                    b.HasOne("BusinessObjects.Entities.Auction", "Auction")
                        .WithMany("AuctionRegistrations")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Entities.Member", "Member")
                        .WithMany("AuctionRegistrations")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Bid", b =>
                {
                    b.HasOne("BusinessObjects.Entities.Auction", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Entities.Member", "Member")
                        .WithMany("Bids")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("BusinessObjects.Entities.LegalDocument", b =>
                {
                    b.HasOne("BusinessObjects.Entities.RealEstate", "RealEstate")
                        .WithMany("LegalDocuments")
                        .HasForeignKey("RealEstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealEstate");
                });

            modelBuilder.Entity("BusinessObjects.Entities.RealEstate", b =>
                {
                    b.HasOne("BusinessObjects.Entities.RealEstateOwner", "Owner")
                        .WithMany("RealEstates")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Transaction", b =>
                {
                    b.HasOne("BusinessObjects.Entities.Bid", "Bid")
                        .WithOne("Transaction")
                        .HasForeignKey("BusinessObjects.Entities.Transaction", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bid");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Auction", b =>
                {
                    b.Navigation("AuctionRegistrations");

                    b.Navigation("Bids");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Bid", b =>
                {
                    b.Navigation("Transaction")
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Entities.RealEstate", b =>
                {
                    b.Navigation("Auction")
                        .IsRequired();

                    b.Navigation("LegalDocuments");
                });

            modelBuilder.Entity("BusinessObjects.Entities.RealEstateOwner", b =>
                {
                    b.Navigation("RealEstates");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Admin", b =>
                {
                    b.Navigation("Auction")
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Entities.Member", b =>
                {
                    b.Navigation("AuctionRegistrations");

                    b.Navigation("Bids");
                });

            modelBuilder.Entity("BusinessObjects.Entities.Staff", b =>
                {
                    b.Navigation("Auctions");
                });
#pragma warning restore 612, 618
        }
    }
}
