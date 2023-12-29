using ReactApp1.Server.Constants;
using ReactApp1.Server.Models.DTOs;
using System.Security.Claims;

namespace ReactApp1.Server.Interfaces
{
    public interface IAuthService
    {
        Task<(bool, string)> RegisterAsync(RegisterDTO registerModel, UserRole userRole);
        /// <summary>
        /// Return Access token and Refresh token on success other wise String.Empty
        /// </summary>
        /// <param name="loginModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Access Token and Refresh Token </returns>
        Task<(string,string)> LoginAsync(LoginDTO loginModel, CancellationToken cancellationToken);
        Task<(bool, string)> LogoutAsync();

       /// <summary>
       /// Generate new JWT and Refresh token
       /// </summary>
       /// <param name="token">Expired JWT token</param>
       /// <returns>JWT token , Refresh Token & status code </returns>
        Task<(string, string, int)> Refresh(string token, string refreshtkn);

        /// <summary>
        /// Revoke the current refresh token
        /// </summary>
        /// <returns> 200(OK) if everything is good else unauthorized </returns>
        Task<int> Revoke();
        
    }
}
