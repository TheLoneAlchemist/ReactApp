using System.Security.Claims;

namespace ReactApp1.Server.Interfaces
{
    public interface IJWTService
    {
        /// <summary>
        /// Generate Access Token 
        /// </summary>
        /// <param name="claims"></param>
        /// <returns>Access Token</returns>
        Task<string> GenerateTokenAsync(IEnumerable<Claim> claims);
        Task<string> GenerateRefreshTokenAsync();

        Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string Token);
    }
}
