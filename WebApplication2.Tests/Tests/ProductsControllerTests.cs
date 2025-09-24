using WebApplication2.Controllers;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System;

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

            Console.WriteLine($"[TEST] Input Product: Id={product.Id}, Name={product.Name}, Price={product.Price}");

            // Act
            ActionResult<Product> actionResult = controller.Create(product);
            Assert.NotNull(actionResult.Result);

            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.NotNull(createdResult);

            // Console output for the result
            if (createdResult?.Value is Product createdProduct)
            {
                Console.WriteLine($"[TEST] Created Product: Id={createdProduct.Id}, Name={createdProduct.Name}, Price={createdProduct.Price}");
            }
            else
            {
                Console.WriteLine("[TEST] Created result value is null or not a Product");
            }

            // Assert
            Assert.Equal(201, createdResult!.StatusCode);
            Assert.Equal(product, createdResult.Value);
            Assert.Equal("GetById", createdResult.ActionName);
        }
    }
}
