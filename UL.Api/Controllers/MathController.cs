using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UL.Application;
using UL.Domain;
using UL.Infrastructure.Cache;

namespace UL.Api.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableRateLimiting("fixed")]
    public class MathController : ControllerBase
    {
        private readonly IMathService _mathService;
        private readonly ICacheService _cacheService;
        private readonly ILogger<MathController> _logger;

        public MathController(IMathService mathService, ICacheService cacheService, ILogger<MathController> logger)
        {
            _mathService = mathService;
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpPost("evaluate")]
        public IActionResult EvaluateExpression([FromBody] MathExpression mathExpression)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Model validation failed;
                    _logger.LogError("Input Expression is invalid");
                    // return a bad request with validation errors.
                    return BadRequest(ModelState);
                }
                else
                {
                    _logger.LogInformation("Received request to evaluate expression: {Expression}",
                        mathExpression.Expression);
                    // Try to retrieve the result from the cache
                    string cacheKey = $"MathResult_{mathExpression.Expression}";

                    double? cachedResult = _cacheService.Get<double?>(cacheKey);
                    if (cachedResult.HasValue)
                    {
                        return Ok(cachedResult.Value);
                    }
                    // Evaluate expression
                    var result = _mathService.EvaluateExpression(mathExpression.Expression);
                    //set cache
                    _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(10));
                    return Ok(result);

                }
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "Invalid expression format: {Expression}", mathExpression.Expression);
                return BadRequest("Invalid expression format.");
            }
            catch (DivideByZeroException ex)
            {
                _logger.LogError(ex, "Divide by zero error in expression: {Expression}", mathExpression.Expression);
                return BadRequest("Division by zero is not allowed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while evaluating expression: {Expression}",
                    mathExpression.Expression);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
