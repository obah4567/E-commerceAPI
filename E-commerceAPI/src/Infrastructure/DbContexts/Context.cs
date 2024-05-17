﻿using E_commerceAPI.src.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.src.Infrastructure.DbContexts
{
    public class Context : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<WishList> WishList { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product1", Price = 10.0, Description = "Description1" },
                new Product { Id = 2, Name = "Product2", Price = 20.0, Description = "Description2" }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}