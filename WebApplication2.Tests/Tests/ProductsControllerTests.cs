public class ProductsControllerTests
{
    private readonly ITestOutputHelper _output;

    public ProductsControllerTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void CreateProduct_ReturnsCreatedAtAction()
    {
        var controller = new ProductsController();
        var product = new Product { Id = 1, Name = "Test Product", Price = 99.99m };

        _output.WriteLine($"[TEST] Input Product: Id={product.Id}, Name={product.Name}, Price={product.Price}");

        var actionResult = controller.Create(product);
        var createdResult = actionResult.Result as CreatedAtActionResult;

        if (createdResult?.Value is Product createdProduct)
        {
            _output.WriteLine($"[TEST] Created Product: Id={createdProduct.Id}, Name={createdProduct.Name}, Price={createdProduct.Price}");
        }
        else
        {
            _output.WriteLine("[TEST] Created result value is null or not a Product");
        }

        Assert.Equal(201, createdResult!.StatusCode);
        Assert.Equal(product, createdResult.Value);
        Assert.Equal("GetById", createdResult.ActionName);
    }
}
