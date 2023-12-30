using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReactApp1.Server.Constants;
using ReactApp1.Server.Data;
using ReactApp1.Server.Interfaces;
using ReactApp1.Server.Models.Common;
using ReactApp1.Server.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReactApp1.Server.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IJWTService _jwtService;
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            //   ILogger logger,
            IMapper mapper
,
            ApplicationDbContext context,
            IJWTService jwtService,
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            //  _logger = logger;
            _mapper = mapper;
            _context = context;
            _jwtService = jwtService;
            _contextAccessor = contextAccessor;
        }




        public async Task<(string, string)> LoginAsync(LoginDTO loginModel, CancellationToken cancellationToken)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(loginModel.Email);
                if (userExist == null)
                {
                    return ("Invalid Login! Please Try again", string.Empty);
                }

                var signinResult = await _signInManager.PasswordSignInAsync(userExist, loginModel.Password, false, lockoutOnFailure: false);//lockout will be true
                if (!signinResult.Succeeded)
                {

                    return ("Invalid Login! Please Try again", string.Empty);
                }

                var userRoles = _userManager.GetRolesAsync(userExist);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userExist.Email),
                    new Claim(ClaimTypes.GivenName,userExist.FirstName),

                };
                foreach (var role in await userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }


                string accessToken = await _jwtService.GenerateTokenAsync(claims);

                if (String.IsNullOrEmpty(accessToken))
                {
                    return (string.Empty, string.Empty);

                }

                string refreshToken = await _jwtService.GenerateRefreshTokenAsync();
                if (String.IsNullOrEmpty(refreshToken))
                {
                    return (string.Empty, string.Empty);
                }
                await _context.Entry(userExist).Reference(z => z.RefreshToken).LoadAsync();
                RefreshToken refreshTkn;
                if (userExist.RefreshToken is null)
                {
                    refreshTkn = new RefreshToken { CreatedAt = DateTime.UtcNow, Token=refreshToken, RefreshTokenExpiry=DateTime.UtcNow.AddMinutes(4)};
                    userExist.RefreshToken=refreshTkn;
                    
                }
                else
                {
                    refreshTkn = userExist.RefreshToken;
                refreshTkn.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(4);
                refreshTkn.Token = refreshToken;
                    refreshTkn.IsRevoked = false;
                    refreshTkn.CreatedAt=DateTime.UtcNow;
                }

                await _userManager.UpdateAsync(userExist);


                return (accessToken, refreshToken);

            }
            catch (Exception e)
            {

                return (string.Empty, string.Empty);
            }
        }


        public async Task<(string, string, int)> Refresh(string token, string refreshtkn)
        {
            try
            {

                var principal = await _jwtService.GetClaimsPrincipalAsync(token);

                if (principal?.Identity?.Name == null)
                {
                    return (string.Empty, string.Empty, StatusCodes.Status401Unauthorized);
                }

                var user = await _userManager.FindByNameAsync(principal.Identity.Name);

                await _context.Entry(user)                          //Explicit Loading of Refresh Token
                              .Reference(z => z.RefreshToken)
                              .LoadAsync();

                                                                        
                                                                   
                if (user is null
                    || user.IsDeleted == true
                    || user.RefreshToken.IsRevoked == true
                    || user.RefreshToken.Token != refreshtkn
                    || user.RefreshToken.RefreshTokenExpiry < DateTime.UtcNow)
                {
                    return (string.Empty, string.Empty, StatusCodes.Status401Unauthorized);

                }


                var refreshtoken = user.RefreshToken;
                refreshtoken.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(4);
                refreshtoken.Token = await _jwtService.GenerateRefreshTokenAsync();
                var accessToken = await _jwtService.GenerateTokenAsync(principal.Claims);
                await _context.SaveChangesAsync();

                return (accessToken, refreshtoken.Token, StatusCodes.Status200OK);

            }
            catch (Exception e)
            {

                throw;
            }
        }


        public async Task<int> Revoke()
        {
            var username = _contextAccessor.HttpContext.User.Identity.Name;
            if (username is null)
            {
                return (StatusCodes.Status401Unauthorized);
            }

            var user = await _userManager.FindByEmailAsync(username);
            if (username is null)
            {
                return (StatusCodes.Status401Unauthorized);
            }
            await _context.Entry(user).Reference(x => x.RefreshToken).LoadAsync();

            user.RefreshToken.Token = null;
            user.RefreshToken.IsRevoked = true;
            await _userManager.UpdateAsync(user);
            return (StatusCodes.Status200OK);
        }


        public Task<(bool, string)> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, string)> RegisterAsync(RegisterDTO registerModel, UserRole userRole)
        {
            try
            {
                if (registerModel.Password != registerModel.ConfirmPassword)
                {
                    return (false, "Both Password must be same!");
                }
                var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
                if (userExists != null)
                {
                    return (false, "Email Already has been registered!");

                }

                var user = _mapper.Map<RegisterDTO, ApplicationUser>(registerModel);
                user.UserName = registerModel.Email;
                var UCreateResult = await _userManager.CreateAsync(user, registerModel.Password);
                if (!UCreateResult.Succeeded)
                {
                    var errors = UCreateResult.Errors.Select(x => x.Description).ToList();
                    return (false, String.Join(',', errors));
                }

                if (!await _roleManager.RoleExistsAsync(userRole.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName: userRole.ToString()));
                }

                if (await _roleManager.RoleExistsAsync(userRole.ToString()))
                {
                    await _userManager.AddToRoleAsync(user, userRole.ToString());
                }

                return (true, "User Created Successfully!");
            }
            catch (Exception e)
            {
                // _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
