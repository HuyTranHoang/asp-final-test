﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asp_final_test.Data;

#nullable disable

namespace asp_final_test.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231031075736_AddVaccies")]
    partial class AddVaccies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("asp_final_test.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 10, 31, 2, 40, 50, 0, DateTimeKind.Unspecified),
                            Name = "Inactivated"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 10, 31, 3, 44, 12, 0, DateTimeKind.Unspecified),
                            Name = "Live-attenuated"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 10, 31, 4, 55, 23, 0, DateTimeKind.Unspecified),
                            Name = "Messenger RNA (mRNA)"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 10, 31, 5, 22, 34, 0, DateTimeKind.Unspecified),
                            Name = "Subunit, recombinant, polysaccharide, and conjugate"
                        });
                });

            modelBuilder.Entity("asp_final_test.Models.Vaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ManufacturingCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("asp_final_test.Models.Vaccine", b =>
                {
                    b.HasOne("asp_final_test.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
