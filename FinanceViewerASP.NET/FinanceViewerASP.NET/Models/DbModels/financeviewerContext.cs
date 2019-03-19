using System;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using FinanceViewerASP.NET.Models.GetModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FinanceViewerASP.NET.Models.DbModels
{
    public partial class FinanceviewerContext : DbContext
    {
        public FinanceviewerContext() : base() { }

        public FinanceviewerContext(DbContextOptions<FinanceviewerContext> options) : base(options) { }

        public virtual DbSet<FvUsers> FvUsers { get; set; }
        public virtual DbSet<FvViews> FvViews { get; set; }
        public virtual DbSet<FvYears> FvYears { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FvUsers>(entity =>
            {
                entity.HasKey(e => e.UName);

                entity.ToTable("fv_users");

                entity.Property(e => e.UName)
                    .HasColumnName("u_name")
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.UFirstName)
                    .IsRequired()
                    .HasColumnName("u_firstName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UIsLocked).HasColumnName("u_isLocked");

                entity.Property(e => e.ULastName)
                    .IsRequired()
                    .HasColumnName("u_lastName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UPassword)
                    .IsRequired()
                    .HasColumnName("u_password")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FvViews>(entity =>
            {
                entity.HasKey(e => e.VId);

                entity.ToTable("fv_views");

                entity.Property(e => e.VId)
                    .HasColumnName("v_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.VDescription)
                    .IsRequired()
                    .HasColumnName("v_description")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.VHtmlId)
                    .IsRequired()
                    .HasColumnName("v_html_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth01)
                    .IsRequired()
                    .HasColumnName("v_month_01")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth02)
                    .IsRequired()
                    .HasColumnName("v_month_02")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth03)
                    .IsRequired()
                    .HasColumnName("v_month_03")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth04)
                    .IsRequired()
                    .HasColumnName("v_month_04")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth05)
                    .IsRequired()
                    .HasColumnName("v_month_05")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth06)
                    .IsRequired()
                    .HasColumnName("v_month_06")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth07)
                    .IsRequired()
                    .HasColumnName("v_month_07")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth08)
                    .IsRequired()
                    .HasColumnName("v_month_08")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth09)
                    .IsRequired()
                    .HasColumnName("v_month_09")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth10)
                    .IsRequired()
                    .HasColumnName("v_month_10")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth11)
                    .IsRequired()
                    .HasColumnName("v_month_11")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VMonth12)
                    .IsRequired()
                    .HasColumnName("v_month_12")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VName)
                    .IsRequired()
                    .HasColumnName("v_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VNotes)
                    .IsRequired()
                    .HasColumnName("v_notes")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.VProfileUrl)
                    .IsRequired()
                    .HasColumnName("v_profile_url")
                    .HasMaxLength(180)
                    .IsUnicode(false);

                entity.Property(e => e.VUName)
                    .IsRequired()
                    .HasColumnName("v_u_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VYYear)
                    .IsRequired()
                    .HasColumnName("v_y_year")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FvYears>(entity =>
            {
                entity.HasKey(e => new { e.YYear, e.YUName });

                entity.ToTable("fv_years");

                entity.Property(e => e.YYear)
                    .HasColumnName("y_year")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.YUName)
                    .HasColumnName("y_u_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });
        }


        public Boolean CheckCorrectUser(LoginData loginData)
        {
            if (loginData.username == AdminCredentials.Username && loginData.password == AdminCredentials.Password)
            {
                return true;
            }

            //Check if user was found
            FvUsers finalUser = null;
            try
            {
                finalUser = FvUsers.Single(m => m.UName == loginData.username);
            }
            catch (InvalidOperationException) { }
            if (finalUser == null)
            {
                return false;
            }

            //Check if password is right
            if ((finalUser.UName == loginData.username) && (finalUser.UPassword == loginData.password))
            {
                //Check if username is not blocked
                if (finalUser.UIsLocked == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetFirstNameForUsername(string username)
        {
            if (UserIsAdmin(username))
            {
                return "Administrator";
            }

            //Check if user was found
            FvUsers finalUser = null;
            try
            {
                finalUser = FvUsers.Single(m => m.UName == username);
            }
            catch (InvalidOperationException) { }
            if (finalUser == null)
            {
                return "[undefined]";
            }

            return finalUser.UFirstName;
        }

        public string GetUrlForUser(string username)
        {
            return UserIsAdmin(username) ? "admin" : "dashboard";
        }

        public bool UserIsAdmin(string username)
        {
            return username == AdminCredentials.Username;
        }
    }
}
