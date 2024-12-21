using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Logout.Services;
using CleanArchitecture.Core.Abstractions.Exceptions;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace CleanArchitecture.Application.Logout.Commands
{
    public sealed record LogoutCommand(string AccessToken) : CommandBase<string>;
    public sealed class LogoutCommandHandler : CommandHandler<LogoutCommand, string>
    {
        private readonly ITokenBlackListService _tokenBlackListService;
        public LogoutCommandHandler(ITokenBlackListService tokenBlackListService, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork)
        {
            _tokenBlackListService = tokenBlackListService;
        }
        protected override async Task<string> HandleAsync(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken))
                throw new BadRequestException("Token is required");
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(request.AccessToken);
                _tokenBlackListService.RevokeToken(request.AccessToken, token.ValidTo);

                return await Task.FromResult("Token revoked successfully");
            }
            catch (ArgumentException ex)
            {
                // Handle specific cases such as invalid token format
                throw new BadRequestException("Invalid token format", ex);
            }
            catch (Exception ex)
            {
                // Catch unexpected errors
                throw new ApplicationException("Token revocation failed", ex);
            }
        }
    }
}
