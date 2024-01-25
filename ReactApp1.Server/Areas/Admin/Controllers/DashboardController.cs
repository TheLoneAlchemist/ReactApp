using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data;

namespace ReactApp1.Server.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class DashboardController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            Thread.Sleep(8000);
            return Ok(_context.Users.ToList());
        }
    }
}
