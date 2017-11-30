using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;
using DAL.Entities.Identity;
using DAL.Entities;
using System.Net.Mail;

namespace BLL.Identity
{
    public  class Service
    {
        public class EmailService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                var from = "azimut965@gmail.com";
                var pass = "980704pashok";

                // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(from, pass);

                var mail = new MailMessage(from, message.Destination);
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.IsBodyHtml = true;
                return client.SendMailAsync(mail); ;
            }
        }

        public class SmsService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                // Plug in your SMS service here to send a text message.
                return Task.FromResult(0);
            }
        }

        // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
        public class ApplicationUserManager : UserManager<DAL.Entities.Identity.AppUser>
        {
            public ApplicationUserManager(IUserStore<AppUser> store, IDataProtectionProvider dataProtectionProvider)
                : base(store)
            {
                // Configure validation logic for usernames
                UserValidator = new UserValidator<AppUser>(this)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Configure validation logic for passwords
                PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                };

                // Configure user lockout defaults
                UserLockoutEnabledByDefault = true;
                DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                MaxFailedAccessAttemptsBeforeLockout = 5;

                // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
                // You can write your own provider and plug it in here.
                RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<AppUser>
                {
                    MessageFormat = "Your security code is {0}"
                });
                RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AppUser>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });
                EmailService = new EmailService();
                SmsService = new SmsService();
                if (dataProtectionProvider != null)
                {
                    UserTokenProvider =
                        new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
                }
            }         
        }

        public class ApplicationRoleManager : RoleManager<AppRole>
        {
            public ApplicationRoleManager(IRoleStore<AppRole,string> store)
                :base(store)
            {

            }
        }

        public class ApplicationRoleStore : RoleStore<AppRole>
        {
            public ApplicationRoleStore(AppDBContext context)
                :base(context)
            {

            }
        }
        public class ApplicationUserStore : UserStore<AppUser>
        {
            public ApplicationUserStore(AppDBContext context)
                : base(context)
            {
            }
        }
        // Configure the application sign-in manager which is used in this application.
        public class ApplicationSignInManager : SignInManager<AppUser, string>
        {
            public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
                : base(userManager, authenticationManager)
            {
            }

            public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
            {
                return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
            }

            public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
            {
                return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
            }
        }

        
    }
}
