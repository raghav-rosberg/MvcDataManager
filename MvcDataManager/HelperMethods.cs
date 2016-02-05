using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace MvcDataManager
{
    public class HelperMethods<T> where T : IdentityUser
    {
        public static async Task SignInAsync(UserManager<T> userManager, T user, IEnumerable<Claim> claims, bool isPersistent)
        {
            if (userManager == null) throw new ArgumentNullException("userManager");
            if (user == null) throw new ArgumentNullException("user");
            var authnManager = HttpContext.Current.GetOwinContext().Authentication;
            var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            claims.ToList().ForEach(identity.AddClaim);
            authnManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        public static void SignIn(UserManager<T> userManager, T user, IEnumerable<Claim> claims, bool isPersistent)
        {
            if (userManager == null) throw new ArgumentNullException("userManager");
            if (user == null) throw new ArgumentNullException("user");
            var authnManager = HttpContext.Current.GetOwinContext().Authentication;
            authnManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            if (claims != null)
                claims.ToList().ForEach(identity.AddClaim);
            authnManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        public static void SignOut()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut();
        }
    }
}