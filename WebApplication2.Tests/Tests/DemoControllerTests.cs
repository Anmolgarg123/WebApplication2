using WebApplication2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace WebApplication2.Tests
{
	public class DemoControllerTests
	{
		[Fact]
		public void Get_ReturnsHelloMessage()
		{
			// Arrange
			var controller = new DemoController();

			// Act
			var result = controller.Get() as OkObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(200, result!.StatusCode);
			Assert.Equal(new { message = "Hello Jenkins DevOps Pipeline!" }.ToString(), result.Value!.ToString());
		}
	}
}
