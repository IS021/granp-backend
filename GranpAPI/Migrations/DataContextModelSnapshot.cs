﻿// <auto-generated />
using System;
using Granp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GranpAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Granp.Models.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("ElderBirthDate")
                        .HasColumnType("date");

                    b.Property<string>("ElderDescription")
                        .HasColumnType("text");

                    b.Property<string>("ElderFirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ElderLastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ElderPhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsElder")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<double?>("Rating")
                        .HasColumnType("double precision");

                    b.Property<int>("ReviewsNumber")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Granp.Models.Entities.CustomerReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("FromId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<Guid>("ToId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("CustomerReviews");
                });

            modelBuilder.Entity("Granp.Models.Entities.Professional", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("HourlyRate")
                        .HasColumnType("double precision");

                    b.Property<string>("IdCardNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LongTimeJob")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxDistance")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Profession")
                        .HasColumnType("integer");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("ReviewsNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("ShortTimeJob")
                        .HasColumnType("boolean");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Professionals");
                });

            modelBuilder.Entity("Granp.Models.Entities.ProfessionalReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("FromId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<Guid>("ToId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("ProfessionalReviews");
                });

            modelBuilder.Entity("Granp.Models.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProfessionalId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProfessionalId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Granp.Models.Entities.Customer", b =>
                {
                    b.OwnsOne("Granp.Models.Types.Address", "ElderAddress", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("StreetNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");

                            b1.OwnsOne("Granp.Models.Types.Location", "Location", b2 =>
                                {
                                    b2.Property<Guid>("AddressCustomerId")
                                        .HasColumnType("uuid");

                                    b2.Property<double>("Latitude")
                                        .HasColumnType("double precision");

                                    b2.Property<double>("Longitude")
                                        .HasColumnType("double precision");

                                    b2.HasKey("AddressCustomerId");

                                    b2.ToTable("Customers");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressCustomerId");
                                });

                            b1.Navigation("Location");
                        });

                    b.Navigation("ElderAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("Granp.Models.Entities.CustomerReview", b =>
                {
                    b.HasOne("Granp.Models.Entities.Professional", "From")
                        .WithMany("WrittenReviews")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Granp.Models.Entities.Customer", "To")
                        .WithMany("ReceivedReviews")
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("Granp.Models.Entities.Professional", b =>
                {
                    b.OwnsOne("Granp.Models.Types.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("ProfessionalId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("StreetNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ProfessionalId");

                            b1.ToTable("Professionals");

                            b1.WithOwner()
                                .HasForeignKey("ProfessionalId");

                            b1.OwnsOne("Granp.Models.Types.Location", "Location", b2 =>
                                {
                                    b2.Property<Guid>("AddressProfessionalId")
                                        .HasColumnType("uuid");

                                    b2.Property<double>("Latitude")
                                        .HasColumnType("double precision");

                                    b2.Property<double>("Longitude")
                                        .HasColumnType("double precision");

                                    b2.HasKey("AddressProfessionalId");

                                    b2.ToTable("Professionals");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressProfessionalId");
                                });

                            b1.Navigation("Location");
                        });

                    b.OwnsOne("Granp.Models.Types.TimeTable", "TimeTable", b1 =>
                        {
                            b1.Property<Guid>("ProfessionalId")
                                .HasColumnType("uuid");

                            b1.Property<int>("WeeksInAdvance")
                                .HasColumnType("integer");

                            b1.HasKey("ProfessionalId");

                            b1.ToTable("Professionals");

                            b1.WithOwner()
                                .HasForeignKey("ProfessionalId");

                            b1.OwnsMany("Granp.Models.Entities.TimeSlot", "TimeSlots", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.Property<TimeSpan>("EndTime")
                                        .HasColumnType("interval");

                                    b2.Property<bool>("IsAvailable")
                                        .HasColumnType("boolean");

                                    b2.Property<TimeSpan>("StartTime")
                                        .HasColumnType("interval");

                                    b2.Property<Guid>("TimeTableProfessionalId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("WeekDay")
                                        .HasColumnType("integer");

                                    b2.HasKey("Id");

                                    b2.HasIndex("TimeTableProfessionalId");

                                    b2.ToTable("TimeSlots");

                                    b2.WithOwner()
                                        .HasForeignKey("TimeTableProfessionalId");
                                });

                            b1.Navigation("TimeSlots");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("TimeTable")
                        .IsRequired();
                });

            modelBuilder.Entity("Granp.Models.Entities.ProfessionalReview", b =>
                {
                    b.HasOne("Granp.Models.Entities.Customer", "From")
                        .WithMany("WrittenReviews")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Granp.Models.Entities.Professional", "To")
                        .WithMany("ReceivedReviews")
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("Granp.Models.Entities.Reservation", b =>
                {
                    b.HasOne("Granp.Models.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Granp.Models.Entities.Professional", "Professional")
                        .WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Professional");
                });

            modelBuilder.Entity("Granp.Models.Entities.Customer", b =>
                {
                    b.Navigation("ReceivedReviews");

                    b.Navigation("WrittenReviews");
                });

            modelBuilder.Entity("Granp.Models.Entities.Professional", b =>
                {
                    b.Navigation("ReceivedReviews");

                    b.Navigation("WrittenReviews");
                });
#pragma warning restore 612, 618
        }
    }
}
