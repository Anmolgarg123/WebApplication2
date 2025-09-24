using WebApplication2.Controllers;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace WebApplication2.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public void CreateProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var controller = new ProductsController();
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Price = 99.99m
            };

            // Act
            ActionResult<Product> actionResult = controller.Create(product);
            Assert.NotNull(actionResult.Result);

            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.NotNull(createdResult);

            // Assert
            Assert.Equal(201, createdResult!.StatusCode);
            Assert.Equal(product, createdResult.Value);
            Assert.Equal("GetById", createdResult.ActionName);
        }
    }
}
