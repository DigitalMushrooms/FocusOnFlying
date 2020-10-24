﻿// <auto-generated />
using System;
using FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(FocusOnFlyingContext))]
    [Migration("20201024102942_KlienciUsuniecieWymagalnosciNaKolumnieNumerLokalu")]
    partial class KlienciUsuniecieWymagalnosciNaKolumnieNumerLokalu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Klient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<Guid>("IdKraju")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("KodPocztowy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Miejscowosc")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nip")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NumerDomu")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NumerLokalu")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NumerPaszportu")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NumerTelefonu")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Pesel")
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.Property<string>("Regon")
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.Property<string>("Ulica")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("IdKraju");

                    b.ToTable("Klienci");
                });

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Kraj", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazwaKraju")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Skrot")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.HasIndex("NazwaKraju")
                        .IsUnique();

                    b.HasIndex("Skrot")
                        .IsUnique();

                    b.ToTable("Kraje");

                    b.HasData(
                        new
                        {
                            Id = new Guid("abaccb22-4d71-41ef-b96b-199fb007d336"),
                            NazwaKraju = "Polska",
                            Skrot = "PL"
                        },
                        new
                        {
                            Id = new Guid("ccf2c1c3-56ab-4e12-924c-0e9996ff261f"),
                            NazwaKraju = "Niemcy",
                            Skrot = "DE"
                        },
                        new
                        {
                            Id = new Guid("d356b661-b7d1-4c75-80c7-677062dfdb94"),
                            NazwaKraju = "Czechy",
                            Skrot = "CZ"
                        },
                        new
                        {
                            Id = new Guid("2f314b76-9fd4-4e89-ad94-0a4cc9a92f18"),
                            NazwaKraju = "Słowacja",
                            Skrot = "SK"
                        },
                        new
                        {
                            Id = new Guid("1863424c-f0ee-40e8-b8b7-683a445a8d6e"),
                            NazwaKraju = "Ukraina",
                            Skrot = "UA"
                        },
                        new
                        {
                            Id = new Guid("be2f6802-38bc-4ac3-abc2-d19714e6689d"),
                            NazwaKraju = "Białoruś",
                            Skrot = "BY"
                        },
                        new
                        {
                            Id = new Guid("0847f9ab-bd5a-4714-93cb-5a4ec23afee2"),
                            NazwaKraju = "Litwa",
                            Skrot = "LV"
                        },
                        new
                        {
                            Id = new Guid("b57f5888-24ce-4349-8de8-dcb938678915"),
                            NazwaKraju = "Rosja",
                            Skrot = "RU"
                        });
                });

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Usluga", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataPrzyjeciaZlecenia")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdKlienta")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdKlienta");

                    b.ToTable("Uslugi");
                });

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Klient", b =>
                {
                    b.HasOne("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Kraj", "Kraj")
                        .WithMany("Klienci")
                        .HasForeignKey("IdKraju")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Usluga", b =>
                {
                    b.HasOne("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Klient", "Klient")
                        .WithMany("Uslugi")
                        .HasForeignKey("IdKlienta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
