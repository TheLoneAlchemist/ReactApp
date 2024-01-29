using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ReactApp1.Server.Areas.Admin.Controllers;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Test
{
    public class DashboardControllerTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public DashboardControllerTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MEMDB").Options;
        }

        [Fact]
        public async void GetAllUserTest()
        {

            //Arrange
            ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);
            context.Users.AddRange(new List<ApplicationUser>
            {
                new ApplicationUser() { FirstName="TheLone", LastName="Alchemist", Email="tlt@gmail.com", PasswordHash="Password", UserName= "tlt@gmail.com" },
                new ApplicationUser() { FirstName="TheLone", LastName="Alchemist", Email="tlt@gmail.com", PasswordHash="Password", UserName= "tlt@gmail.com" },
            });
            await context.SaveChangesAsync();

            //Act
            DashboardController controller = new DashboardController(context);
           var restult =  await controller.GetUsers();

            //Assert

            Assert.NotNull(restult);
           var okObj =  Assert.IsType<OkObjectResult>(restult);
            object jsonObj = okObj.Value;

            var obj = jsonObj as List<ApplicationUser>;
            Assert.IsType<List<ApplicationUser>> (obj);
            Assert.True(obj.Any());

        }
    }
}
