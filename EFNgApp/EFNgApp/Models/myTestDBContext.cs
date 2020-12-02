﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFNgApp.Models
{
    public partial class myTestDBContext: DbContext
    {
        public myTestDBContext()
        {
        }

        public myTestDBContext(DbContextOptions<myTestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCities> TblCities { get; set; }
        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<Account> account { get; set; }
        public virtual DbSet<User> user { get; set; }

        public virtual DbSet<Ledger> ledger { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.user_id)
                    .HasName("PK__user__F2D21A967F3B9C20");

                entity.ToTable("user");

                entity.Property(e => e.user_id).HasColumnName("user_id");

                entity.Property(e => e.mobile_number)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Ledger>(entity =>
            {
                entity.HasKey(e => e.ledger_id)
                    .HasName("PK__ledger__F2D21A967F3B9C20");

                entity.ToTable("ledger");

                entity.Property(e => e.ledger_id).HasColumnName("ledger_id");

                entity.Property(e => e.ledger_id)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.account_id)
                    .HasName("PK__account__F2D21A967F3B9C20");

                entity.ToTable("account");

                entity.Property(e => e.account_id).HasColumnName("account_id");

                entity.Property(e => e.user_id)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<TblCities>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__tblCitie__F2D21A967F3B9C20");

                entity.ToTable("tblCities");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__tblEmplo__7AD04FF1F8EC0A37");

                entity.ToTable("tblEmployee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
