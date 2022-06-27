using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RetailApp.Models;

#nullable disable

namespace RetailApp.Data
{
    public partial class RetailAppContext : DbContext
    {
        public RetailAppContext()
        {
        }

        public RetailAppContext(DbContextOptions<RetailAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.OrderStatus, "OrderStatusKey_idx");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(45)
                    .HasColumnName("orderId");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(45)
                    .HasColumnName("addressLine1");

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(45)
                    .HasColumnName("addressLine2");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .HasColumnName("country");

                entity.Property(e => e.County)
                    .HasMaxLength(20)
                    .HasColumnName("county");

                entity.Property(e => e.EirCode)
                    .HasMaxLength(45)
                    .HasColumnName("eirCode");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.OrderStatus).HasColumnName("orderStatus");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.OrderStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderStatusKey");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_item");

                entity.HasIndex(e => e.ProductId, "ProductIdKey_idx");

                entity.HasIndex(e => e.OrderId, "orderIdKey_idx");

                entity.Property(e => e.OrderItemId).HasColumnName("orderItemId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(45)
                    .HasColumnName("orderId");

                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("OrderIdForeignKey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("ProductIdFoerignKey");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("order_status");

                entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusId");

                entity.Property(e => e.OrderStatusName)
                    .HasMaxLength(45)
                    .HasColumnName("orderStatusName");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(45)
                    .HasColumnName("productName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
