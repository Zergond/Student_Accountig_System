using BLL.Identity.Models;
using DAL.Entities.Identity;
using DAL.Entities.Models;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentAccountingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static BLL.Identity.Service;

namespace BLL.Providers
{
    public class AccountProvider : IAccountProvider
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IStudentRepository _studentRepository;

        public AccountProvider(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authManager,IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
            _studentRepository = studentRepository;
        }

        public async Task<SignInStatus> Login(LoginViewModel model, string returnUrl)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            return result;
        }


        public async Task<bool> HasBeenVerifiedAsync()
        {
            return await _signInManager.HasBeenVerifiedAsync();
        }


        public async Task<SignInStatus> VerifyCode(VerifyCodeViewModel model)
        {
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            return result;
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {         
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                Student student = new Student
                {
                    Age = model.Age,
                    Name = model.Name,
                    LastName = model.LastName,
                    StudyDate = model.StudyDate,
                    RegisteredDate = DateTime.Now
                };
                _studentRepository.Add(student);
                _studentRepository.SaveChanges();

                await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return result;
            }
            
            return result;
        }

        public async Task<string> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return "Error";
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return result.Succeeded ? "ConfirmEmail" : "Error";
        }

        public async Task<bool> ForgotPassword(ForgotPasswordViewModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
            {
                return true;
            }
            return false;
        }


        public async Task<string> GetUserIdByEmail(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                return user.Id;
            }
            return "-1";
        }

        public async Task<bool> UserWithNameExists(string email)
        {
            return await _userManager.FindByNameAsync(email) != null;
        }

        public async Task<IdentityResult> ResetPassword(string email, string code, string password)
        {
            var userId = await GetUserIdByEmail(email);
            return await _userManager.ResetPasswordAsync(userId, code, password);
        }

        public async Task<string> GetVerifiedUserIdAsync()
        {
            return await _signInManager.GetVerifiedUserIdAsync();
        }

        public async Task<IEnumerable<string>> GetValidTwoFactorProvidersAsync(string userId)
        {
            return await _userManager.GetValidTwoFactorProvidersAsync(userId);
        }

        public async Task<bool> SendCode(string selectedProvider)
        {
            return await _signInManager.SendTwoFactorCodeAsync(selectedProvider);
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo)
        {
            return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _authManager.GetExternalLoginInfoAsync();
        }

        public async Task<IdentityResult> CreateUserAsync(ExternalLoginConfirmationViewModel model)
        {
            var user = new AppUser { UserName = model.Email, Email = model.Email };
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> AddLoginAsync(string email, UserLoginInfo login)
        {
            return await _userManager.AddLoginAsync(email, login);
        }

        public async Task SignInAsync(string email, bool isPersistent, bool rememberBrowser)
        {
            AppUser user = await GetUserByEmail(email);
            await _signInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }

        private async Task<AppUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public void LogOff()
        {
            _authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

    }
}
