using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SmartInventory.Entities
{
    internal class AppDbcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            options.UseLazyLoadingProxies()
                   .UseSqlServer(connectionString);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet <Category> Categories { get; set; }
        public DbSet <Product> Products { get; set; }
        public DbSet <OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                        .HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<Customer>()
                        .HasMany(o => o.Orders)
                        .WithOne(c => c.Customer)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>()
                       .HasMany(o => o.Products)
                       .WithOne(s => s.Supplier)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                       .HasMany(p => p.Products)
                       .WithOne(c => c.Category)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                       .HasMany(i =>i.OrderItems )
                       .WithOne(o => o.Order)
                       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                       .HasMany(o => o.OrderItems)
                       .WithOne(p => p.Product)
                       .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Order>()
                        .Property(d => d.OrderDate)
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<OrderItem>()
                        .Property(st => st.SubTotal)
                        .HasComputedColumnSql("[Quantity]*[UnitPrice]")
                        .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                        .Property(o => o.TotalPrice)
                        .HasPrecision(18, 2);


            modelBuilder.Entity<Order>()
                        .HasIndex(o => new
                        {
                            o.CustomerID,
                            o.OrderDate
                        });


            modelBuilder.Entity<Customer>()
                        .HasIndex(e => e.CustomerEmail)
                        .IsUnique();

            modelBuilder.Entity<Product>()
                        .HasIndex(n => n.ProductName)
                        .IsUnique();


            modelBuilder.HasSequence<int>("Order_Number")
                        .StartsAt(1)
                        .IncrementsBy(1);


            modelBuilder.Entity<Order>()
                        .Property(o => o.OrderNumber)
                        .HasDefaultValueSql("NEXT VALUE FOR Order_Number");

          

        }

    }
}
