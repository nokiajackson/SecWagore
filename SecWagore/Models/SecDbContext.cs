using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SecWagore.Models
{
    public partial class SecDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public SecDbContext()
        {
        }

        public SecDbContext(DbContextOptions<SecDbContext> options,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;

        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Campus> Campuses { get; set; } = null!;
        public virtual DbSet<EntryLog> EntryLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                
                optionsBuilder.UseSqlServer("server=192.168.0.8;database=SecDb;User ID=secadmin;Password=wagor,2024;trusted_connection=true;Integrated Security=False;");

                // Get the connection string from appsettings.json
                //string connectionString = _configuration.GetConnectionString("SecWagoreContext");

                // Configure the DbContext to use the SQL Server provider with the connection string
                //optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("ID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasComment("關聯校區");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("創建時間");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("密碼");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("更新時間");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("更新人員");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("帳號");

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CampusId)
                    .HasConstraintName("FK__Account__CampusI__2D27B809");
            });

            modelBuilder.Entity<Campus>(entity =>
            {
                entity.ToTable("Campus");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("住址");

                entity.Property(e => e.CampusName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("校區名");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("縣市");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("創建時間");

                entity.Property(e => e.District)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("區域");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("更新時間");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("更新人員");
            });

            modelBuilder.Entity<EntryLog>(entity =>
            {
                entity.ToTable("EntryLog");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("ID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasComment("關聯校區");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("創建時間");

                entity.Property(e => e.EntryTime)
                    .HasColumnType("datetime")
                    .HasComment("入校時間");

                entity.Property(e => e.ExitTime)
                    .HasColumnType("datetime")
                    .HasComment("離校時間");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Interviewee)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("受訪人");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasComment("備註");

                entity.Property(e => e.NumberOfPeople).HasComment("人數");

                entity.Property(e => e.OtherDescription)
                    .HasColumnType("text")
                    .HasComment("其他說明");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("電話");

                entity.Property(e => e.Purpose)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事由(ENUM)");

                entity.Property(e => e.ReplacementNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("換證號碼");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("更新時間");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("更新人員");

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.EntryLogs)
                    .HasForeignKey(d => d.CampusId)
                    .HasConstraintName("FK__EntryLog__Campus__31EC6D26");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
