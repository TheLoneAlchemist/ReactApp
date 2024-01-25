using Microsoft.AspNetCore.Mvc;
using Moq;
using ReactApp1.Server.Areas.Admin.Controllers;
using ReactApp1.Server.Data;
using ReactApp1.Server.Interfaces;
using ReactApp1.Server.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Test
{
    public class LoginTest
    {
        private readonly Mock<IAuthService> _authService;
        private readonly AccountController accountController;


        public LoginTest()
        {
            _authService = new Mock<IAuthService>();
            accountController = new AccountController(_authService.Object);
        }


        [Fact]
        public async void Login_Check_False()
        {
            var loginDto = new LoginDTO { Email = "admin@admin.com", Password = "Admin@123" };

            var result = await accountController.Login(loginDto);

            Assert.NotNull(result);

            var iAction = Assert.IsType<OkObjectResult>(result);

            object jsonObj = iAction.Value;
            Assert.Equal(false,jsonObj.GetType().GetProperty("status").GetValue(jsonObj, null));
            
            
        }


    }
}
