using System.Text.Json;
using System.Threading.Tasks;
using UL.Api;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net;
using System.Text;

namespace UL.IntegrationTests
{

    public class MathControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MathControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("4+5*2", 14)]
        [InlineData("4+5/2", 6.5)]
        [InlineData("4+5/2-1", 5.5)]
        public async Task TestEvaluateExpression_ValidExpression_ReturnsValidResult(string input, double expectedResult)
        {
            // Arrange
            var client = _factory.CreateClient();
            var expression = new
            {
                Expression = input
            };
            var content = new StringContent(JsonSerializer.Serialize(expression), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/v1/math/evaluate", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<double>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(expectedResult, result);
        }


        [Fact]
        public async Task TestDivideByZero_ExceptionThrown()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expression = new
            {
                Expression = "4/0" // Division by zero
            };
            var content = new StringContent(JsonSerializer.Serialize(expression), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/v1/math/evaluate", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("Division by zero is not allowed.", responseContent);
        }

        [Fact]
        public async Task TestInvalidInput_ExceptionThrown()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expression = new
            {
                Expression = "InvalidString" // Division by zero
            };
            var content = new StringContent(JsonSerializer.Serialize(expression), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/v1/math/evaluate", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("Invalid expression format", responseContent);
        }

        [Fact]
        public async Task TestRateLimitingExceedLimit_Returns429()
        {
            // Arrange
            var client = _factory.CreateClient();
            var expression = new
            {
                Expression = "2+5" 
            };

            var content = new StringContent(JsonSerializer.Serialize(expression), Encoding.UTF8, "application/json");
            // Send multiple requests to exceed the rate limit
            for (int i = 0; i < 11; i++)
            {
                 var responseContent = await client.PostAsync("/api/v1/math/evaluate", content);
            }

            // Send one more request to trigger rate limiting
            var response = await  client.PostAsync("/api/v1/math/evaluate", content);
            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        }
    }
}
