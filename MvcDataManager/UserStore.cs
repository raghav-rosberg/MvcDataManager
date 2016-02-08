using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcDataManager
{
    #region User Store

    public interface IUserStore<T>
        where T : IdentityUser
    {
        UserManager<T> UserManager { get; set; }
    }

    public class UserStore<T> : IUserStore<T>
        where T : IdentityUser
    {
        public UserManager<T> UserManager { get; set; }

        public UserStore(DbContext dbcontext)
        {
            UserManager = new UserManager<T>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<T>(dbcontext))
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

        public UserStore(DbContext dbcontext, PasswordValidator passwordValidator)
        {
            UserManager = new UserManager<T>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<T>(dbcontext))
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
