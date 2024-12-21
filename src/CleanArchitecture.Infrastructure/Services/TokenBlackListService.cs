using CleanArchitecture.Application.Logout.Services;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.Infrastructure.Services
{
    public class TokenBlackListService : ITokenBlackListService
    {
        private readonly IMemoryCache _memoryCache;
        public TokenBlackListService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void RevokeToken(string token, DateTime expiry)
        {
            _memoryCache.Set(token, true, expiry - DateTime.UtcNow);
        }

        public bool IsTokenRevoked(string token)
        {
            return _memoryCache.TryGetValue(token, out _);
        }
    }
}
