using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace WheyMenDAL.Library.Model
{
    public partial class WheyMenContext : DbContext
    {
        public WheyMenContext()
        {
        }

        public WheyMenContext(DbContextOptions<WheyMenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Loc> Loc { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                  .Build();
            string conn = config.GetConnectionString("Default");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__customer__AB6E616451187AD9")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__customer__F3DBC57207D5D1BA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("pwd")
                    .HasMaxLength(20);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("inventory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.StoreId).HasColumnName("STORE_ID");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__inventory__pid__74AE54BC");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__inventory__STORE__5535A963");
            });

            modelBuilder.Entity<Loc>(entity =>
            {
                entity.ToTable("loc");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.LocId).HasColumnName("loc_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("money");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order__cust_id__534D60F1");

                entity.HasOne(d => d.Loc)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.LocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order__loc_id__5441852A");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Oid).HasColumnName("oid");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.O)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.Oid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_item__oid__5070F446");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK__order_item__pid__6C190EBB");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => new { e.Name, e.Price })
                    .HasName("UQ__products__A9210D39D42E209C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
