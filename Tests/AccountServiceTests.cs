using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Tests
{
    public class FakeSignInManager : SignInManager<ApplicationUser>
    {
        public FakeSignInManager()
                : base(new FakeUserManager(),
                     new Mock<IHttpContextAccessor>().Object,
                     new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                     new Mock<IOptions<IdentityOptions>>().Object,
                     new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                     new Mock<IAuthenticationSchemeProvider>().Object,
                     new Mock<IUserConfirmation<ApplicationUser>>().Object)
        { }
    }

    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<ApplicationUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<ApplicationUser>>().Object,
              new IUserValidator<ApplicationUser>[0],
              new IPasswordValidator<ApplicationUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        { }
    }

    public class AccountServiceTests
    {
        [Fact]
        public async Task AddUser_Successful()
        {
            // Arrange
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            var user = new RegisterDto() { Name = "test" };
            userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var res = await accountService.Register(user);

            // Assert
            userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
            Assert.True(res.Succeeded);
        }

        [Fact]
        public async Task AddUser_WithError()
        {
            // Arrange
            var exceptionMessage = "User already exists";
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            var user = new RegisterDto() { Name = "test" };
            userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError() { Description = exceptionMessage }));
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var res = await accountService.Register(user);

            // Assert
            userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
            Assert.False(res.Succeeded);
            Assert.Equal(exceptionMessage, res.Errors.FirstOrDefault()?.Description);
        }

        [Fact]
        public async Task EditUser_Successful()
        {
            // Arrange
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            string userName = "test";
            var user = new EditUserDto() { Name = userName };
            userManagerMock.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser() { Name = userName });
            userManagerMock.Setup(userManager => userManager.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var res = await accountService.Edit(user, userName);

            // Assert
            userManagerMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Once);
            userManagerMock.Verify(x => x.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()), Times.Never);
            userManagerMock.Verify(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(res.Name, user.Name);
        }

        [Fact]
        public async Task EditUserWithPassword_Successful()
        {
            // Arrange
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            string userName = "test";
            var user = new EditUserDto() { Name = userName, Password = "testPass" };
            userManagerMock.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser() { Name = userName });
            userManagerMock.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("testToken");
            userManagerMock.Setup(userManager => userManager.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var res = await accountService.Edit(user, userName);

            // Assert
            userManagerMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Once);
            userManagerMock.Verify(x => x.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()), Times.Once);
            userManagerMock.Verify(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal(res.Name, user.Name);
        }

        [Fact]
        public async Task EditUser_WithError()
        {
            // Arrange
            var exceptionMessage = "Test Exception";
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            string userName = "test";
            var user = new EditUserDto() { Name = userName };
            userManagerMock.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser() { Name = userName });
            userManagerMock.Setup(userManager => userManager.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError() { Description = exceptionMessage }));
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => accountService.Edit(user, userName));

            // Assert
            userManagerMock.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.Equal(exceptionMessage, exception.Message);
        }

        [Fact]
        public async Task EditUserWithPassword_WithError()
        {
            // Arrange
            var exceptionMessage = "Test Exception";
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            string userName = "test";
            var user = new EditUserDto() { Name = userName, Password = "testPass" };
            userManagerMock.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser() { Name = userName });
            userManagerMock.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("testToken");
            userManagerMock.Setup(userManager => userManager.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError() { Description = exceptionMessage }));
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => accountService.Edit(user, userName));

            // Assert
            userManagerMock.Verify(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Never);
            Assert.Equal(exceptionMessage, exception.Message);
        }

        [Fact]
        public async Task Login_Successful()
        {
            // Arrange
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            var user = new LoginDto();
            signInManagerMock.Setup(signInManagerMock => signInManagerMock.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var res = await accountService.Login(user);

            // Assert
            signInManagerMock.Verify(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            Assert.True(res);
        }

        [Fact]
        public async Task Login_Failed()
        {
            // Arrange
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            var user = new LoginDto();
            signInManagerMock.Setup(signInManagerMock => signInManagerMock.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var res = await accountService.Login(user);

            // Assert
            signInManagerMock.Verify(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            Assert.False(res);
        }

        [Fact]
        public async Task Logout_HasBeenCalled()
        {
            // Arrange
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            var user = new LoginDto();
            signInManagerMock.Setup(signInManagerMock => signInManagerMock.SignOutAsync());
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            await accountService.Logout();

            // Assert
            signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_Successful()
        {
            // Arrange
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            string userName = "testUser";
            userManagerMock.Setup(userManagerMock => userManagerMock.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManagerMock.Setup(userManagerMock => userManagerMock.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);
            signInManagerMock.Setup(signInManagerMock => signInManagerMock.SignOutAsync());
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var res = await accountService.Delete(userName);

            // Assert
            userManagerMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<ApplicationUser>()), Times.Once);
            signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
            Assert.True(res);
        }

        [Fact]
        public async Task DeleteUser_Exception()
        {
            // Arrange
            var exceptionMessage = "Test Exception";
            var signInManagerMock = new Mock<FakeSignInManager>();
            var userManagerMock = new Mock<FakeUserManager>();
            string userName = "testUser";
            userManagerMock.Setup(userManagerMock => userManagerMock.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManagerMock.Setup(userManagerMock => userManagerMock.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError() { Description = exceptionMessage }));
            signInManagerMock.Setup(signInManagerMock => signInManagerMock.SignOutAsync());
            var accountService = new AccountService(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => accountService.Delete(userName));

            // Assert
            userManagerMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
            userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<ApplicationUser>()), Times.Once);
            signInManagerMock.Verify(x => x.SignOutAsync(), Times.Never);
            Assert.Equal(exceptionMessage, exception.Message);
        }
    }
}