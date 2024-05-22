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
                    var categories = GeneratorDataCategory(10);
                    var shipments = GeneratorDataShipment(20);
                    var products = GeneratorDataProduct(100);
                    var carts = GeneratorDataCart(20, 10);
                    var payments = GenerateDataPayment(10);
                    var wishList = GeneratorDataWishList(10, 5);

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

        /*        public static List<Product> GeneratorDataProduct(int count, List<Category> categories)
                {
                    var id = 1;
                    var productFaker = new Faker<Product>()
                        .RuleFor(p => p.Id, _ => id++)
                        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                        .RuleFor(p => p.Price, f => Convert.ToDouble(f.Commerce.Price(1.0m, 1000.0m)))
                        //.RuleFor(p => p.Carts, (f, p) => GeneratorDataCart((f.Random.Int(1, 10)), p.Id))
                        .RuleFor(p => p.Category_Id, f => f.PickRandom(categories).Id)
                        .RuleFor(p => p.Category, f => f.PickRandom(categories));

                    return productFaker.Generate(count);
                }*/

        public static List<Product> GeneratorDataProduct(int numberOfProducts)
        {
            var categoryIds = new[] { 1, 2, 3 }; // Exemple d'IDs de catégories existantes

            // Créer un faker pour les produits
            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
                .RuleFor(p => p.Price, f => Convert.ToDouble(f.Commerce.Price(1.0m, 1000.0m)))
                .RuleFor(p => p.Category_Id, f => f.PickRandom(categoryIds))
                .RuleFor(p => p.Carts, (f, p) => GeneratorDataCart(f.Random.Int(1, 5), p.Id)) // Génère entre 1 et 5 carts pour chaque produit
                .RuleFor(p => p.WishLists, (f, p) => GeneratorDataWishList(f.Random.Int(1, 5), p.Id)); // Génère entre 1 et 5 wishlists pour chaque produit

            return productFaker.Generate(numberOfProducts);
        }


        public static List<Category> GeneratorDataCategory(int count)
        {
            var id = 1;

            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Id, _ => id++)
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);
            //.RuleFor(p => p.Products, f => f.PickRandom(products));

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

        /*public static List<Cart> GeneratorDataCart(int count, List<Product> products)
        {
            var id = 1;

            var cartFaker = new Faker<Cart>()
                .RuleFor(c => c.Id, _ => id++)
                .RuleFor(c => c.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(p => p.Product, f => f.PickRandom(products))
                .RuleFor(c => c.IsDeleted, f => f.Random.Bool(0.4f)) // 40% chance to be true
                .RuleFor(c => c.Product_Id, f => f.PickRandom(products).Id);

            return cartFaker.Generate(count);
        }*/

        public static List<Cart> GeneratorDataCart(int numberOfCarts, int productId)
        {
            var cartFaker = new Faker<Cart>()
                .RuleFor(c => c.Id, f => f.IndexFaker + 1)
                .RuleFor(c => c.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(c => c.IsDeleted, f => f.Random.Bool(0.1f))
                .RuleFor(c => c.Product_Id, f => productId);

            return cartFaker.Generate(numberOfCarts);
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

        /*public static List<WishList> GeneratorDataWishList(int count, List<Product> products)
        {
            var id = 1;

            var wishList = new Faker<WishList>()
                .RuleFor(w => w.Id, _ => id++)
                //.RuleFor(c => c.IsDeleted, f => f.Random.Bool(0.1f)); // 10% chance to be true
                .RuleFor(w => w.Product, f => f.PickRandom(products));

            return wishList.Generate(count);
        }*/

        public static List<WishList> GeneratorDataWishList(int numberOfWishLists, int productId)
        {
            var wishListFaker = new Faker<WishList>()
                .RuleFor(w => w.Id, f => f.IndexFaker + 1)
                .RuleFor(w => w.IsDeleted, f => f.Random.Bool(0.4f))
                .RuleFor(w => w.Product_Id, f => productId);

            return wishListFaker.Generate(numberOfWishLists);
        }

    }
}
