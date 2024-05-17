using Bogus;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Infrastructure.DbContexts;

namespace E_commerceAPI.src.Domain.GeneratorData
{
    public static class GeneratorDataByBogus
    {
        //Teste pour inserer mes données et injecter dans services
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<Context>())
            {
                if (!context.Categories.Any() && !context.Products.Any() && 
                    !context.Shipment.Any() && !context.Carts.Any() &&
                    !context.Payment.Any() && !context.WishList.Any())
                {
                    var categories  = GeneratorDataCategory(10);
                    var products    = GeneratorDataProduct(100, categories);
                    var shipments   = GeneratorDataShipment(20);
                    var carts       = GeneratorDataCart(20);
                    var payments    = GenerateDataPayment(10);
                    var wishList = GeneratorDataWishList(10, products);


                    context.Categories.AddRange(categories);
                    context.Products.AddRange(products);
                    context.Shipment.AddRange(shipments);
                    context.Carts.AddRange(carts);
                    context.Payment.AddRange(payments);
                    context.WishList.AddRange(wishList);

                    context.SaveChanges();
                }
            }
        }

        public static List<Product> GeneratorDataProduct(int count, List<Category> categories)
        {
            var id = 1;
            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, _ => id++)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => Convert.ToDouble(f.Commerce.Price(1.0m, 1000.0m)))
                .RuleFor(p => p.Category, f => f.PickRandom(categories));

            return productFaker.Generate(count);
        }

        
        public static List<Category> GeneratorDataCategory(int count) 
        { 
            var id = 1;

            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Id, _ => id++)
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);

            return categoryFaker.Generate(count);
        }

        public static List<Shipment> GeneratorDataShipment(int count)
        {
            var id = 1;

            var shipmentFaker = new Faker<Shipment>()
                .RuleFor(c => c.Id, _ => id++)
                .RuleFor(s => s.Date, f => f.Date.Future())
                .RuleFor(s => s.Address, f => f.Address.StreetAddress())
                .RuleFor(s => s.City, f => f.Address.City())
                .RuleFor(s => s.Region, f => f.Address.State())
                .RuleFor(s => s.Country, f => f.Address.Country())
                .RuleFor(s => s.Code_Postal, f => f.Address.ZipCode())
                .RuleFor(s => s.IsDeleted, f => f.Random.Bool(0.1f)); // 10% chance to be true

            return shipmentFaker.Generate(count);
        }

        public static List<Cart> GeneratorDataCart(int count)
        {
            var id = 1;

            var cartFaker = new Faker<Cart>()
                .RuleFor(c => c.Id, _ => id++)
                .RuleFor(c => c.Quantity, f => f.Random.Int(1, 10));
                //.RuleFor(c => c.IsDeleted, f => f.Random.Bool(0.1f)); // 10% chance to be true

            return cartFaker.Generate(count);
        }

        public static List<Payment> GenerateDataPayment(int count)
        {
            var id = 1;

            var paymentFaker = new Faker<Payment>()
                .RuleFor(p => p.Id, _ => id++)
                .RuleFor(p => p.Date, f => f.Date.Future())
                .RuleFor(p => p.Method, f => f.Finance.TransactionType())
                .RuleFor(p => p.Amount, f => Convert.ToDouble(f.Finance.Amount(1.0m, 1000.0m)));

            return paymentFaker.Generate(count);
        }

        public static List<WishList> GeneratorDataWishList(int count, List<Product> products)
        {
            var id = 1;

            var wishList = new Faker<WishList>()
                .RuleFor(w => w.Id, _ => id++)
                //.RuleFor(c => c.IsDeleted, f => f.Random.Bool(0.1f)); // 10% chance to be true
                .RuleFor(w => w.Product, f => f.PickRandom(products));

            return wishList.Generate(count);
        }

    }
}
