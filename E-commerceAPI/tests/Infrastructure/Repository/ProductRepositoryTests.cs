using Bogus.DataSets;
using E_commerceAPI.src.Domain.Models;
using E_commerceAPI.src.Infrastructure.DbContexts;
using E_commerceAPI.src.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace E_commerceAPI.tests.Infrastructure.Repository
{
    public class ProductRepositoryTests : IDisposable
    {

        private readonly DbContextOptions<Context> _dbContextOptions;
        private readonly Context _context;

        //private readonly ProductRepository productRepository;

        public ProductRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "SharedInMemoryDb")
                .Options;
            _context = new Context(_dbContextOptions);
        }
        //Guid.NewGuid().ToString()

        public void Dispose()
        {
            // Supprimer la base de données en mémoire après chaque test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProducts()
        {
            //Arrange
            _context.Products.AddRange(
                new Product { Id = 1, Name = "Iphone X", Description = "description", Category = new Category { Id = 1, Name = "Telephone"} },
                new Product { Id = 2, Name = "Samsung S9", Description = "description", Category = new Category { Id = 2, Name = "Telephone" } });
            await _context.SaveChangesAsync();

            var productRepository = new ProductRepository(_context);
            //Act
            var result = await productRepository.GetAllAsync(CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExist()
        {
            //Arrange
            var product = new Product { Id = 3, Name = "Iphone IX", Description = "description", Category = new Category { Id = 3, Name = "Telephone" } };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var productRepository = new ProductRepository(_context);
            //Act
            var result = await productRepository.GetByIdAsync(3, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNothing_WhenProductDoesNotExist()
        {
            //Arrange
            var productRepository = new ProductRepository(_context);

            //Act
            var productId = await productRepository.GetByIdAsync(It.IsAny<int>(), CancellationToken.None);

            //Assert
            Assert.Null(productId);
        }

        [Fact]
        public async Task Create_ShouldCreateProduct()
        {
            //Arrange
            var product = new Product { Id = 3, Name = "Iphone IX", Description = "description", Category = new Category { Id = 3, Name = "Telephone" } };
            var productRepository = new ProductRepository(_context);

            //Act 
            await productRepository.Create(product, CancellationToken.None);

            //Assert
            Assert.Equal(3, product.Id);
            Assert.Equal($"{product.Name}", product.Name);
            Assert.True(_context.Products.Contains(product));
        }

        [Fact]
        public async Task Delete_ShouldRemoveProduct_WhenProductExist()
        {
            //Arrange
            _context.Products.AddRange(
                new Product { Id = 1, Name = "Iphone X", Description = "description", Category = new Category { Id = 1, Name = "Telephone" } },
                new Product { Id = 2, Name = "Samsung S9", Description = "description", Category = new Category { Id = 2, Name = "Telephone" } });
            await _context.SaveChangesAsync();

            var productRepository = new ProductRepository(_context);
            
            //Act
            await productRepository.Delete(1 ,CancellationToken.None);

            //Assert
            var listProducts = await _context.Products.ToListAsync();
            Assert.Null(await _context.Products.FindAsync(1));
            Assert.Single(listProducts);
            Assert.NotNull(await _context.Products.FindAsync(2));
        }

        [Fact (Skip = "Later")]
        public async Task Update_ShouldUpdateProduct_WhenProductExist()
        {
            //Arrange
            _context.Products.AddRange(
                new Product { Id = 1, Name = "Iphone X",   Description = "description", Category = new Category { Id = 1, Name = "Telephone"   }},
                new Product { Id = 2, Name = "Samsung S9", Description = "description", Category = new Category { Id = 2, Name = "Telephone - Samsung" }},
                new Product { Id = 3, Name = "Iphone VI",  Description = "description", Category = new Category { Id = 3, Name = "Telephone" } });
            await _context.SaveChangesAsync();

            var productRepository = new ProductRepository(_context);
            var updateProduct = new Product { Id = 3, Name = "Nokia", Description = "description - Update", Category = new Category { Id = 3, Name = "Telephone" }};

            //Act 
            await productRepository.Update(updateProduct, CancellationToken.None);

            //Assert
            var product = await _context.Products.FindAsync(3);
            Assert.NotNull(product);
            Assert.Equal(3, product.Id);
            Assert.Equal("Nokia", product.Name);
            Assert.Equal("Telephone " , product.Category.Name);

            var unchangesProduct = await _context.Products.FindAsync(2);
            Assert.NotNull(unchangesProduct);
            Assert.Equal(2, unchangesProduct.Id);
            Assert.Equal("Samsung S9", unchangesProduct.Name);
        }

    }
}
