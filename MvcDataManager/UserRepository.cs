using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using MvcDataManager.Model;

namespace MvcDataManager
{
    public interface IUserRepository : IController
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        ActionResult Register(ApplicationUser user, string password, string view, string redirectView,
            string redirectCntroller = "");

        ActionResult SignIn(string userName, string password, IEnumerable<Claim> claims, bool isPersistent, string view,
            string redirectView, string redirectController = "");

        ActionResult SignOff(string redirectView, string redirectController = "");

        IEnumerable<SecurityQuestion> GetSecurityQuestions();

        ActionResult AuthenticateUserSecurityAnswer(ForgotPassword model, string view);

        ActionResult ResetPassword(ResetPassword model, string view, string redirectView,
            string redirectController = "");
    }

    public class UserRepository : Controller, IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IRepository<SecurityQuestion> _securityQuestionRepository;
        public UserRepository(IDataProtectionProvider dataProtectionProvider, IUnitOfWork unitOfWork)
        {
            _userStore = new UserStore<ApplicationUser>(unitOfWork._dbContext);
            _unitOfWork = unitOfWork;
            _securityQuestionRepository = new RepositoryBase<SecurityQuestion>(unitOfWork);
            _userStore.UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        }

        public ApplicationUser GetById(string id)
        {
            return _userStore.UserManager.FindById(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userStore.UserManager.Users;
        }

        public ActionResult Register(ApplicationUser user, string password, string view, string redirectView, string redirectCntroller = "")
        {
            var result = _userStore.UserManager.Create(user, password);
            if (result.Succeeded)
                return string.IsNullOrEmpty(redirectCntroller)
                    ? RedirectToActionPermanent(redirectView)
                    : RedirectToActionPermanent(redirectView, redirectCntroller);
            result.Errors.ToList().ForEach(x => ModelState.AddModelError("CreateUser", x));
            return View(view, user);
        }

        public ActionResult SignIn(string userName, string password, IEnumerable<Claim> claims, bool isPersistent, string view, string redirectView, string redirectController = "")
        {
            var user = _userStore.UserManager.Find(userName, password);
            if (user == null)
            {
                ModelState.AddModelError("User authentication", "Login failed. Incorrect user credentials");
                return View(view);
            }
            HelperMethods<ApplicationUser>.SignIn(_userStore.UserManager, user, claims,
                isPersistent);
            return string.IsNullOrEmpty(redirectController) ? RedirectToAction(redirectView) : RedirectToAction(redirectView, redirectController);
        }

        public ActionResult SignOff(string redirectView, string redirectController = "")
        {
            HelperMethods<ApplicationUser>.SignOut();
            return string.IsNullOrEmpty(redirectController)
                ? RedirectToActionPermanent(redirectView)
                : RedirectToActionPermanent(redirectView, redirectController);
        }

        public IEnumerable<SecurityQuestion> GetSecurityQuestions()
        {
            return _securityQuestionRepository.GetAll();
        }

        public ActionResult AuthenticateUserSecurityAnswer(ForgotPassword model, string view)
        {
            var user = _userStore.UserManager.FindByName(model.UserName);
            model.SecurityQuestions = _securityQuestionRepository.GetAll();
            if (user == null)
            {
                ModelState.AddModelError("User authentication", "User authentication failed. Please enter correct user name");
                return View(view, model);
            }
            int questionId;
            int.TryParse(model.SecurityQuestionId, out questionId);

            var userAuthentication = user.SecurityQuestionId == questionId && user.SecurityAnswer == model.Answer;

            if (userAuthentication)
            {
                var changePassword = new ResetPassword
                {
                    UserName = Helper.Encrypt(model.UserName)
                };
                return RedirectToAction("ResetPassword", changePassword);
            }

            ModelState.AddModelError("User authentication", "User authentication failed. Please enter correct answer");
            return View(view, model);
        }

        public ActionResult ResetPassword(ResetPassword model, string view, string redirectView,
            string redirectController = "")
        {
            var userName = Helper.Decrypt(model.UserName);
            var user = _userStore.UserManager.FindByName(userName);
            if (user == null)
            {
                ModelState.AddModelError("User authentication", "User authentication failed. Invalid username");
                return View(view, model);
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("User authentication", "New password and Confirm password does not match");
                return View(view, model);
            }
            var token = _userStore.UserManager.GeneratePasswordResetToken(user.Id);
            var resetuserPasswordResult = _userStore.UserManager.ResetPassword(user.Id, token, model.NewPassword);
            if (resetuserPasswordResult.Succeeded)
                return string.IsNullOrEmpty(redirectController)
                    ? RedirectToActionPermanent(redirectView)
                    : RedirectToActionPermanent(redirectView, redirectController);
            resetuserPasswordResult.Errors.ToList().ForEach(x => ModelState.AddModelError("User authentication", x));
            return View(view, model);
        }
    }
}
