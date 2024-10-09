﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViccAdatbazis.Data;

#nullable disable

namespace ViccAdatbazis.Migrations
{
    [DbContext(typeof(ViccDbContext))]
    [Migration("20241009061801_sqlite.local_migration_853")]
    partial class sqlitelocal_migration_853
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("ViccAdatbazis.Models.Vicc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Aktiv")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Feltolto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NemTetszik")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tartalom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Tetszik")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Viccek");
                });
#pragma warning restore 612, 618
        }
    }
}
