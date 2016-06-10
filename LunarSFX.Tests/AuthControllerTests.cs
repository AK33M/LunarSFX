using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunarSFX.Controllers;
using Rhino.Mocks;
using Microsoft.Owin.Security;

namespace LunarSFX.Tests
{
    [TestClass]
    public class AuthControllerTests
    {
        private AuthController _authController;
        private AppSignInManager _appSignInManager;
        private AppUserManager _appUserManager;
        private IAuthenticationManager _authenticationManager;

        [TestMethod]
        public void SetUp()
        {
            _appSignInManager = MockRepository.GenerateMock<AppSignInManager>();
            _appUserManager = MockRepository.GenerateMock<AppUserManager>();
            _authenticationManager = MockRepository.GenerateMock<IAuthenticationManager>();

            _authController = new AuthController(_appUserManager, _appSignInManager, _authenticationManager);
        }

        [TestMethod]
        public void Login_IsLoggedIn_True_Test()
        {
            // arrange


            // act
            

            // assert
            
        }
    }
}
