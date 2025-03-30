using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using roweb.API.Controllers;
using roweb.API.Models;
using roweb.Domain.Helper;
using Xunit;

namespace roweb.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Login_ReturnsToken_WhenCredentialsAreValid()
        {
            var userEmail = "test@yahoo.com";
            var userPassword = "Password123!";
            var user = new ApplicationUser { Id = "1", UserName = userEmail, Email = userEmail };

            var mockUserManager = MockUserManager<ApplicationUser>();
            mockUserManager.Setup(u => u.FindByEmailAsync(userEmail)).ReturnsAsync(user);
            mockUserManager.Setup(u => u.CheckPasswordAsync(user, userPassword)).ReturnsAsync(true);

            var inMemorySettings = new Dictionary<string, string>
            {
                { "JwtSettings:Secret", "wb346ct93br3tvc23iybfn4uxnhouxb3t6tbcr93cbxinoml" },
                { "JwtSettings:Issuer", "TestIssuer" },
                { "JwtSettings:Audience", "TestAudience" }
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var controller = new AccountController(mockUserManager.Object, configuration);

            var loginModel = new LoginModel
            {
                Email = userEmail,
                Password = userPassword
            };

            var result = await controller.Login(loginModel);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var tokenObj = okResult.Value as dynamic;
            Assert.NotNull(tokenObj);

        }

        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            return mgr;
        }
    }
}
