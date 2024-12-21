namespace CleanArchitecture.Application.Logout.Services
{
    public interface ITokenBlackListService
    {
        public void RevokeToken(string token, DateTime expiry);
        public bool IsTokenRevoked(string token);
    }
}
