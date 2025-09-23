using Xunit;
using WebApplication2.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace WebApplication2.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            // Create a mock logger
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();

            // Pass the mock logger to the controller
            _controller = new WeatherForecastController(mockLogger.Object);
        }

        [Fact]
        public void Get_ReturnsWeatherForecastArray()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }
    }
}
