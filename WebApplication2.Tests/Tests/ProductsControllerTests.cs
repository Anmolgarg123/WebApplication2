using WebApplication2.Controllers;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace WebApplication2.Tests
{
    public class ProductsControllerTests
    {
        private readonly ITestOutputHelper _output;

        public ProductsControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CreateProduct_ShouldReturn_CreatedAtActionResult_WithExpectedProduct()
        {
            // Arrange
            var controller = new ProductsController();
            var inputProduct = new Product
            {
                Id = 1,
                Name = "Test Product",
                Price = 99.99m
            };

            _output.WriteLine($"[Arrange] Input Product => Id={inputProduct.Id}, Name={inputProduct.Name}, Price={inputProduct.Price}");

            // Act
            var actionResult = controller.Create(inputProduct);

            // Assert - Result type
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Result);

            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);

            // Assert - Created result details
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("GetById", createdResult.ActionName);

            var returnedProduct = Assert.IsType<Product>(createdResult.Value);

            _output.WriteLine($"[Assert] Returned Product => Id={returnedProduct.Id}, Name={returnedProduct.Name}, Price={returnedProduct.Price}");

            // Assert - Product equality
            Assert.Equal(inputProduct.Id, returnedProduct.Id);
            Assert.Equal(inputProduct.Name, returnedProduct.Name);
            Assert.Equal(inputProduct.Price, returnedProduct.Price);
        }
    }
}
