using System;
using System.Collections.Generic;
using CoptisTest.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CoptisTest.API;

public partial class CoptisTestContext : DbContext
{
    public CoptisTestContext(DbContextOptions<CoptisTestContext> options)
        : base(options)
    {
    }
    public DbSet<Access> Accesses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Access access1 = new Access { AccessId = 1, Name = "users" };
        Access access2 = new Access { AccessId = 2, Name = "shop" };
        Access access3 = new Access { AccessId = 3, Name = "orders" };
        List<Access> accesses = new List<Access> { access1, access2, access3 };

        modelBuilder.Entity<Access>().HasData(accesses);
        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Username = "admin", Password = "admin", LastName = "Admin", FirstName = "User", Email = "admin@admin" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, Name = "Gold 1kg", Quantity = 10, Price = 50000 },
            new Product { ProductId = 2, Name = "Silver 1kg", Quantity = 10, Price = 7000 }
        );

        modelBuilder.Entity<User>()
                .HasMany(u => u.Accesses)
                .WithMany(a => a.Users)
                .UsingEntity(j => j.ToTable("UserAccesses").HasData(
                    new { UsersUserId = 1, AccessesAccessId = 1 },
                    new { UsersUserId = 1, AccessesAccessId = 2 },
                    new { UsersUserId = 1, AccessesAccessId = 3 }
                ));
        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Product)
            .HasForeignKey(o => o.ProductId);
    }
}
