﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using wypozyczalnia.Server.Models;

#nullable disable

namespace wypozyczalnia.Server.Migrations
{
    [DbContext(typeof(VehiclesContext))]
    [Migration("20241101230722_First Migration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("wypozyczalnia.Server.Models.Vehicles", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RegistryNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RentalFrom")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("RentalTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SerialNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VinId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("YearOfProduction")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("CarId");

                    b.ToTable("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
