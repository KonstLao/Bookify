﻿// <auto-generated />
using System;
using Bookify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookify.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bookify.Domain.Apartments.Apartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Amenities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime?>("LastBookedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Apartments", (string)null);
                });

            modelBuilder.Entity("Bookify.Domain.Bookings.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CancelledOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CompletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ConfirmedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RejectedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings", (string)null);
                });

            modelBuilder.Entity("Bookify.Domain.Reviews.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("BookingId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews", (string)null);
                });

            modelBuilder.Entity("Bookify.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Bookify.Domain.Apartments.Apartment", b =>
                {
                    b.OwnsOne("Bookify.Domain.Apartments.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("ApartmentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ApartmentId");

                            b1.ToTable("Apartments");

                            b1.WithOwner()
                                .HasForeignKey("ApartmentId");
                        });

                    b.OwnsOne("Bookify.Domain.Shared.Money", "CleaningFee", b1 =>
                        {
                            b1.Property<Guid>("ApartmentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ApartmentId");

                            b1.ToTable("Apartments");

                            b1.WithOwner()
                                .HasForeignKey("ApartmentId");
                        });

                    b.OwnsOne("Bookify.Domain.Shared.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ApartmentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ApartmentId");

                            b1.ToTable("Apartments");

                            b1.WithOwner()
                                .HasForeignKey("ApartmentId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("CleaningFee")
                        .IsRequired();

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("Bookify.Domain.Bookings.Booking", b =>
                {
                    b.HasOne("Bookify.Domain.Apartments.Apartment", null)
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Bookify.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Bookify.Domain.Shared.Money", "AmenitiesUpCharge", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BookingId");

                            b1.ToTable("Bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId");
                        });

                    b.OwnsOne("Bookify.Domain.Shared.Money", "CleaningFee", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BookingId");

                            b1.ToTable("Bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId");
                        });

                    b.OwnsOne("Bookify.Domain.Shared.Money", "PriceForPeriod", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BookingId");

                            b1.ToTable("Bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId");
                        });

                    b.OwnsOne("Bookify.Domain.Shared.Money", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BookingId");

                            b1.ToTable("Bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId");
                        });

                    b.OwnsOne("Bookify.Domain.Shared.DateRange", "DateRange", b1 =>
                        {
                            b1.Property<Guid>("BookingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateOnly>("End")
                                .HasColumnType("date");

                            b1.Property<int>("LengthInDays")
                                .HasColumnType("int");

                            b1.Property<DateOnly>("Start")
                                .HasColumnType("date");

                            b1.HasKey("BookingId");

                            b1.ToTable("Bookings");

                            b1.WithOwner()
                                .HasForeignKey("BookingId");
                        });

                    b.Navigation("AmenitiesUpCharge")
                        .IsRequired();

                    b.Navigation("CleaningFee")
                        .IsRequired();

                    b.Navigation("DateRange")
                        .IsRequired();

                    b.Navigation("PriceForPeriod")
                        .IsRequired();

                    b.Navigation("TotalPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Bookify.Domain.Reviews.Review", b =>
                {
                    b.HasOne("Bookify.Domain.Apartments.Apartment", null)
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Bookify.Domain.Bookings.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Bookify.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
