using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcDataManager
{
    #region User Store

    public interface IUserStore<T, TU>
        where T : IdentityUser
        where TU : DbContext, new()
    {
        UserManager<T> UserManager { get; set; }
    }

    public class UserStore<T, TU> : IUserStore<T, TU>
        where T : IdentityUser
        where TU : DbContext, new()
    {
        public UserManager<T> UserManager { get; set; }

        public UserStore()
        {
            var dbContext = Activator.CreateInstance<TU>();
            UserManager = new UserManager<T>(new UserStore<T>(dbContext))
            {
                PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 8,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true
                }
            };
        }

        public UserStore(PasswordValidator passwordValidator)
        {
            var dbContext = Activator.CreateInstance<TU>();
            UserManager = new UserManager<T>(new UserStore<T>(dbContext))
            {
                PasswordValidator = passwordValidator ?? new PasswordValidator
                {
                    RequiredLength = 8,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true
                }
            };
        }
    }

    #endregion
}
