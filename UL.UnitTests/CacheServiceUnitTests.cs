using Microsoft.Extensions.Caching.Memory;
using System;
using UL.Infrastructure.Cache;
using Xunit;

namespace UL.UnitTests
{

    public class MemoryCacheServiceTests
    {
        [Fact]
        public void Get_ExistingKey_ReturnsCachedValue()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cacheService = new MemoryCacheService(memoryCache);

            // Define a sample key and value that you expect to be in the cache
            string sampleKey = "existingKey";
            string expectedValue = "cachedValue";

            // Set the value in the cache
            cacheService.Set(sampleKey, expectedValue, TimeSpan.FromMinutes(30));

            // Act
            string? result = cacheService.Get<string>(sampleKey);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Get_NonExistingKey_ReturnsDefault()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cacheService = new MemoryCacheService(memoryCache);

            // Define a sample key that does not exist in the cache
            string nonExistingKey = "nonExistingKey";

            // Act
            string? result = cacheService.Get<string>(nonExistingKey);

            // Assert
            // Since the key does not exist, the result should be the default value for string, which is null.
            Assert.Null(result);
        }

        [Fact]
        public void Set_CacheKeyAndValue_CachesValue()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cacheService = new MemoryCacheService(memoryCache);

            // Define a sample key and value to be cached
            string sampleKey = "cacheKey";
            string sampleValue = "cachedValue";

            // Act
            cacheService.Set(sampleKey, sampleValue, TimeSpan.FromMinutes(30));

            // Assert
            // Attempt to retrieve the value from the cache
            string? cachedValue = cacheService.Get<string>(sampleKey);

            // Verify that the retrieved value matches the value that was cached
            Assert.Equal(sampleValue, cachedValue);
        }
    }

}
