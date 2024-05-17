using E_commerceAPI.src.Infrastructure.DbContexts;
using E_commerceAPI.src.Infrastructure.Repository;
using Moq;

namespace E_commerceAPI.tests.Infrastructure.Repository
{
    public class ProductRepositoryTests
    {

        private readonly ProductRepository _productRepository;
        private readonly Mock<Context> _context = new Mock<Context>();

        private readonly CancellationToken cancellation;



        public ProductRepositoryTests()
        {
            _productRepository = new ProductRepository(_context.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExist()
        {
            //Arrange

            //Act
            var productId = await _productRepository.GetByIdAsync(10, cancellation);

            //Assert
            Assert.Equal(10,  productId.Id);
        }
    }
}
