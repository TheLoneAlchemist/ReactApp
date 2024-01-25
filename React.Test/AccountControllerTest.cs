using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Areas.Admin.Controllers;
using ReactApp1.Server.Data;
using ReactApp1.Server.Interfaces;
using Moq;
using ReactApp1.Server.Models.DTOs;
using ReactApp1.Server.Constants;
using ReactApp1.Server.Services;



namespace React.Test
{

    public delegate (bool, string) CalReturn(bool a, string b);
    public class AccountControllerTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public Mock<IAuthService> AuthService = new Mock<IAuthService>();

        public AccountControllerTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;

        }


        [Fact]
        public async void Resister_Service_Token_Gen_Test()
        {
            ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);
            var regModel = new RegisterDTO() { Email = "admin@test.com", FirstName = "First", LastName = "Last", Password = "pass", ConfirmPassword = "pass" };

            //AuthService.Setup(x => x.RegisterAsync(regModel, UserRole.User)).ReturnsAsync((bool a, string b)=> { return (a, b); });
            AuthService.Setup(x => x.RegisterAsync(It.IsAny<RegisterDTO>(), It.IsAny<UserRole>()))
            .ReturnsAsync((RegisterDTO regModel, UserRole role) =>
                 {
                     if (regModel.Password == regModel.ConfirmPassword)
                     {
                         return (true, "User Created Successfully!");
                     }
                     else
                     {
                         return (false, "Password and Confirm Password do not match.");
                     }
                 });
            var result = await AuthService.Object.RegisterAsync(regModel, UserRole.User);

            //Assert
            Assert.True(result.Item1);
            Assert.Equal("User Created Successfully!", result.Item2);

        }




       


    }
}