﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpaceTravelVoucher.DataSpaceTravel.Models
{
    public partial class TESTSPACEContext : DbContext
    {
        public TESTSPACEContext()
        {
        }

        public TESTSPACEContext(DbContextOptions<TESTSPACEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airport> Airport { get; set; }
        public virtual DbSet<AviaCarrier> AviaCarrier { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<IdentityDocumentType> IdentityDocumentType { get; set; }
        public virtual DbSet<RailCarrier> RailCarrier { get; set; }
        public virtual DbSet<RailwayStation> RailwayStation { get; set; }
        public virtual DbSet<ReceivingParty> ReceivingParty { get; set; }
        public virtual DbSet<RegionRf> RegionRf { get; set; }
        public virtual DbSet<StatusAviaCarrier> StatusAviaCarrier { get; set; }
        public virtual DbSet<StatusRailCarrier> StatusRailCarrier { get; set; }
        public virtual DbSet<StatusVoucher> StatusVoucher { get; set; }
        public virtual DbSet<TourAgency> TourAgency { get; set; }
        public virtual DbSet<TourismServiceType> TourismServiceType { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<VoucherType> VoucherType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Airport__A25C5AA64C9BC3B7");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeCity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CodeCountry)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CodeIata)
                    .HasMaxLength(5)
                    .HasColumnName("CodeIATA");

                entity.Property(e => e.CodeIcao)
                    .HasMaxLength(5)
                    .HasColumnName("CodeICAO");

                entity.Property(e => e.CodeRf)
                    .HasMaxLength(50)
                    .HasColumnName("CodeRF");

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Timezone).HasMaxLength(50);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<AviaCarrier>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__AviaCarr__A25C5AA63A314268");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeCalculate).HasMaxLength(5);

                entity.Property(e => e.CodeCountry)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CodeIata)
                    .HasMaxLength(5)
                    .HasColumnName("CodeIATA");

                entity.Property(e => e.CodeIcao)
                    .HasMaxLength(5)
                    .HasColumnName("CodeICAO");

                entity.Property(e => e.CodeRf)
                    .HasMaxLength(50)
                    .HasColumnName("CodeRF");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.WebSite).HasMaxLength(200);

                entity.HasOne(d => d.CodeCountryNavigation)
                    .WithMany(p => p.AviaCarrier)
                    .HasForeignKey(d => d.CodeCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AviaCarrier_Country");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.AviaCarrier)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AviaCarrier_StatusAviaCarrier");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__City__A25C5AA6B81C259B");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeCountry)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CodeRegionRf).HasMaxLength(50);

                entity.Property(e => e.Latitude).HasMaxLength(10);

                entity.Property(e => e.Locode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Longitude).HasMaxLength(10);

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Timezone).HasMaxLength(50);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.CodeCountryNavigation)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CodeCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country");

                /*entity.HasOne(d => d.CodeRegionRfNavigation)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CodeRegionRf)
                    .HasConstraintName("FK_City_RegionRf");*/
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Client__A25C5AA61A7FF8CA");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Country__A25C5AA6ABEDB619");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeLet2)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.CodeLet3)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.CodeLet3Ru).HasMaxLength(5);

                entity.Property(e => e.CodeOksm)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("CodeOKSM");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.ShortNameEn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortNameRu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<IdentityDocumentType>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Identity__A25C5AA64C0B7A71");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeEae)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("CodeEAE");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<RailCarrier>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__RailCarr__A25C5AA6DBE31BB3");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeCountry)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.ShortNameEn).HasMaxLength(50);

                entity.Property(e => e.ShortNameRu).HasMaxLength(50);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.WebSite).HasMaxLength(200);

                entity.HasOne(d => d.CodeCountryNavigation)
                    .WithMany(p => p.RailCarrier)
                    .HasForeignKey(d => d.CodeCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RailCarrier_Country");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.RailCarrier)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RailCarrier_StatusRailCarrier");
            });

            modelBuilder.Entity<RailwayStation>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__RailwayS__A25C5AA6D8F88519");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeCity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CodeCountry)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CodeUic)
                    .HasMaxLength(10)
                    .HasColumnName("CodeUIC");

                entity.Property(e => e.EsrCode).HasMaxLength(10);

                entity.Property(e => e.ExpressCode).HasMaxLength(10);

                entity.Property(e => e.Locode).HasMaxLength(5);

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Oktmo).HasMaxLength(15);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.RoadName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoadNumber).HasMaxLength(20);

                entity.Property(e => e.ShortNameEn).HasMaxLength(50);

                entity.Property(e => e.ShortNameRu).HasMaxLength(50);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.CodeCityNavigation)
                    .WithMany(p => p.RailwayStation)
                    .HasForeignKey(d => d.CodeCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RailwayStation_City");

                entity.HasOne(d => d.CodeCountryNavigation)
                    .WithMany(p => p.RailwayStation)
                    .HasForeignKey(d => d.CodeCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RailwayStation_Country");
            });

            modelBuilder.Entity<ReceivingParty>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Receivin__A25C5AA68E7D500F");

                entity.Property(e => e.CodeGis)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("CodeGIS");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NumberPhone)
                    .IsRequired()
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<RegionRf>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__RegionRf__A25C5AA61F880BE8");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeDigital).HasMaxLength(20);

                entity.Property(e => e.CodeLat2).HasMaxLength(5);

                entity.Property(e => e.CodeLat23).HasMaxLength(10);

                entity.Property(e => e.CodeLat3).HasMaxLength(5);

                entity.Property(e => e.CodeRus3).HasMaxLength(5);

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Oktmo)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.RegNumber)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<StatusAviaCarrier>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__StatusAv__A25C5AA63A7EF36E");

                entity.Property(e => e.Code).HasMaxLength(3);

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<StatusRailCarrier>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__StatusRa__A25C5AA694964FE9");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<StatusVoucher>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__StatusVo__A25C5AA69D7ECDED");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TourAgency>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__TourAgen__A25C5AA64DAB125C");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Inn)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("INN");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NumberPhone)
                    .IsRequired()
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<TourismServiceType>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__TourismS__A25C5AA6799ED693");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeIata)
                    .HasMaxLength(5)
                    .HasColumnName("CodeIATA");

                entity.Property(e => e.CodeOta)
                    .HasMaxLength(5)
                    .HasColumnName("CodeOTA");

                entity.Property(e => e.CodeRf)
                    .HasMaxLength(5)
                    .HasColumnName("CodeRF");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Voucher__A25C5AA693F6B063");

                entity.Property(e => e.DestinationCity).HasMaxLength(50);

                entity.Property(e => e.DestinationCountry).HasMaxLength(50);

                entity.Property(e => e.LeavingCity).HasMaxLength(50);

                entity.Property(e => e.LeavingCountry).HasMaxLength(50);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Order).IsRequired();

                entity.Property(e => e.Travelers).IsRequired();

                entity.Property(e => e.TripEndDate).HasColumnType("date");

                entity.Property(e => e.TripStartDate).HasColumnType("date");

                entity.Property(e => e.VoucherType).HasMaxLength(50);

                entity.HasOne(d => d.DestinationCityNavigation)
                    .WithMany(p => p.VoucherDestinationCityNavigation)
                    .HasForeignKey(d => d.DestinationCity)
                    .HasConstraintName("FK_Voucher_City");

                entity.HasOne(d => d.DestinationCountryNavigation)
                    .WithMany(p => p.VoucherDestinationCountryNavigation)
                    .HasForeignKey(d => d.DestinationCountry)
                    .HasConstraintName("FK_Voucher_Country");

                entity.HasOne(d => d.LeavingCityNavigation)
                    .WithMany(p => p.VoucherLeavingCityNavigation)
                    .HasForeignKey(d => d.LeavingCity)
                    .HasConstraintName("FK_Voucher_City1");

                entity.HasOne(d => d.LeavingCountryNavigation)
                    .WithMany(p => p.VoucherLeavingCountryNavigation)
                    .HasForeignKey(d => d.LeavingCountry)
                    .HasConstraintName("FK_Voucher_Country1");

                entity.HasOne(d => d.PartnerNavigation)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.Partner)
                    .HasConstraintName("FK_Voucher_ReceivingParty");

                entity.HasOne(d => d.StatusVoucherNavigation)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.StatusVoucher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_StatusVoucher");

                entity.HasOne(d => d.TourAgentNavigation)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.TourAgent)
                    .HasConstraintName("FK_Voucher_TourAgency");
            });

            modelBuilder.Entity<VoucherType>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__VoucherT__A25C5AA64B876BA5");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeIata)
                    .HasMaxLength(5)
                    .HasColumnName("CodeIATA");

                entity.Property(e => e.CodeOta)
                    .HasMaxLength(5)
                    .HasColumnName("CodeOTA");

                entity.Property(e => e.CodeRf)
                    .HasMaxLength(5)
                    .HasColumnName("CodeRF");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NameRu)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Version).HasColumnType("decimal(8, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=KHAME\\SQLEXPRESS;Initial Catalog=TESTSPACE;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            }
        }
    }
}