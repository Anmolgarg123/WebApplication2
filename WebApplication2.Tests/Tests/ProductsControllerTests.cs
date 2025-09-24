[Fact]
public void CreateProduct_ReturnsCreatedAtAction()
{
    Console.WriteLine("Starting CreateProduct_ReturnsCreatedAtAction test...");

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

    Console.WriteLine($"Test finished. StatusCode: {createdResult.StatusCode}, Product Name: {product.Name}");
}
