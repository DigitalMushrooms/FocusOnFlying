﻿// <auto-generated />
using System;
using FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(FocusOnFlyingContext))]
    partial class FocusOnFlyingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("KodPocztowy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Miejscowosc")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
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

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Misja", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataRozpoczecia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataZakonczenia")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DlugoscGeograficzna")
                        .HasColumnType("decimal(9,6)");

                    b.Property<string>("IdPracownika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdStatusuMisji")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdTypuMisji")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUslugi")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaksymalnaWysokoscLotu")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Promien")
                        .HasColumnType("int");

                    b.Property<decimal>("SzerokoscGeograficzna")
                        .HasColumnType("decimal(8,6)");

                    b.HasKey("Id");

                    b.HasIndex("IdStatusuMisji");

                    b.HasIndex("IdTypuMisji");

                    b.HasIndex("IdUslugi");

                    b.ToTable("Misje");
                });

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.StatusMisji", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusyMisji");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b59173ac-0606-47c4-9ddf-0af363d564de"),
                            Nazwa = "Utworzona"
                        },
                        new
                        {
                            Id = new Guid("beb37d80-9eeb-483c-ab5b-a95c30f6f1ce"),
                            Nazwa = "Zaplanowana"
                        },
                        new
                        {
                            Id = new Guid("c3505f48-abd3-43f8-98cf-439129cc4194"),
                            Nazwa = "Anulowana"
                        },
                        new
                        {
                            Id = new Guid("34e560c8-d677-4292-aaaf-af1fd9d4e8e0"),
                            Nazwa = "Wykonana"
                        });
                });

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.StatusUslugi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusyUslugi");

                    b.HasData(
                        new
                        {
                            Id = new Guid("89407a86-a6d6-415a-b3bf-d3ee0b70ac85"),
                            Nazwa = "Utworzona"
                        },
                        new
                        {
                            Id = new Guid("ee545a45-a7ed-4aa9-9ac6-def05c93204f"),
                            Nazwa = "W realizacji"
                        },
                        new
                        {
                            Id = new Guid("eef8529f-9182-434b-957c-2df7462e2fbf"),
                            Nazwa = "Zakończona"
                        },
                        new
                        {
                            Id = new Guid("bdb1da1b-3713-46a9-8414-1c9a2e91f931"),
                            Nazwa = "Anulowana"
                        });
                });

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.TypDrona", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypyDrona");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7f4ceb1e-2be1-418d-9c28-2fa11bae2429"),
                            Nazwa = "Aircraft"
                        },
                        new
                        {
                            Id = new Guid("686bf52e-798b-42bf-bdfd-3bbd4936d6e8"),
                            Nazwa = "Airship, Balloon"
                        },
                        new
                        {
                            Id = new Guid("39e46048-d0f5-479c-aebc-318d06c44d5e"),
                            Nazwa = "Helicopter"
                        },
                        new
                        {
                            Id = new Guid("3170d6c9-59c4-486e-b392-61c07cc3a0da"),
                            Nazwa = "Multi Rotor"
                        });
                });

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.TypMisji", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypyMisji");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ed9994f8-c9c8-4781-8423-67bcf005064f"),
                            Nazwa = "BVLOS"
                        },
                        new
                        {
                            Id = new Guid("f9a094e4-c4ae-492d-9af4-966022b156d9"),
                            Nazwa = "VLOS"
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

                    b.Property<Guid>("IdStatusuUslugi")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdKlienta");

                    b.HasIndex("IdStatusuUslugi");

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

            modelBuilder.Entity("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Misja", b =>
                {
                    b.HasOne("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.StatusMisji", "StatusMisji")
                        .WithMany("Misje")
                        .HasForeignKey("IdStatusuMisji")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.TypMisji", "TypMisji")
                        .WithMany("Misje")
                        .HasForeignKey("IdTypuMisji")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.Usluga", "Usluga")
                        .WithMany("Misje")
                        .HasForeignKey("IdUslugi")
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

                    b.HasOne("FocusOnFlying.Domain.Entities.FocusOnFlyingDb.StatusUslugi", "StatusUslugi")
                        .WithMany("Uslugi")
                        .HasForeignKey("IdStatusuUslugi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
