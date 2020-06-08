using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pd.Core.Areas.Pd.Models.BaseModel;
using Pd.Core.Helper;

namespace Pd.Core.Areas.Pd.Models.Base
{
    public partial class PDBaseContext : DbContext
    {
        public PDBaseContext()
        {
        }

        public PDBaseContext(DbContextOptions<PDBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<TableField> TableField { get; set; }
        public virtual DbSet<TableLogs> TableLogs { get; set; }
        public virtual DbSet<TableTable> TableTable { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<CommonType> CommonType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Setting.APPSett.Sqlconn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseModel.Project>(entity =>
            {
                entity.Property(e => e.ProjectId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateYear)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Creater)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remake)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableField>(entity =>
            {
                entity.HasKey(e => e.FieldId)
                    .HasName("PK_TABLEFIELD");

                entity.Property(e => e.FieldId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Creater)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldContentId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.FieldDefultValue)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FieldExplain)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldLength)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remake)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableId)
                    .HasMaxLength(36)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableLogs>(entity =>
            {
                entity.HasKey(e => e.LogsId)
                    .HasName("PK_TABLELOGS");

                entity.Property(e => e.LogsId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Creater)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Param)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.LogsImport)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Remake)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OperateId)
                    .HasMaxLength(36)
                    .IsUnicode(false);
                entity.Property(e => e.MendName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.IngOrEd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableTable>(entity =>
            {
                entity.HasKey(e => e.TableId)
                    .HasName("PK_TABLETABLE");

                entity.Property(e => e.TableId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Creater)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Remake)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableExplain)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TableRemake)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableSortOut)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_USERINFO");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Creater)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remake)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.UserPwd)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CommonType>(entity =>
            {
                entity.HasKey(e => e.CommonId)
                    .HasName("PK_USERINFO");

                entity.Property(e => e.CommonId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remake)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CommonName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CommonBrlong)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CommonValue)
                    .HasMaxLength(200)
                    .IsUnicode(false);
               
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
