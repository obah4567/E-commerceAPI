using Bogus;
using E_commerceAPI.src.Domain.GeneratorData;
using E_commerceAPI.src.Domain.Models;
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
/*            var id = 1;

            var shipmentFaker = new Faker<Shipment>();

            var test = GeneratorDataByBogus.GeneratorDataCategory(10);*/

/*            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product1", Price = 10.0, Description = "Description1" },
                new Product { Id = 2, Name = "Product2", Price = 20.0, Description = "Description2" }
            );*/

/*            modelBuilder.Entity<Shipment>().HasData(
                shipmentFaker = new Faker<Shipment>()
                .RuleFor(c => c.Id, _ => id++)
                .RuleFor(s => s.Date, f => f.Date.Future())
                .RuleFor(s => s.Address, f => f.Address.StreetAddress())
                .RuleFor(s => s.City, f => f.Address.City())
                .RuleFor(s => s.Region, f => f.Address.State())
                .RuleFor(s => s.Country, f => f.Address.Country())
                .RuleFor(s => s.Code_Postal, f => f.Address.ZipCode())
                .RuleFor(s => s.IsDeleted, f => f.Random.Bool(0.1f))
                );
            shipmentFaker.Generate(5);


            modelBuilder.Entity<Category>().HasData(
                test);*/


            base.OnModelCreating(modelBuilder);
        }

    }
}
