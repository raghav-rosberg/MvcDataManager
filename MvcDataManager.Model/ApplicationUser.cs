using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcDataManager.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            DateCreated = DateTime.Now;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicUrl { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public bool Activated { get; set; }

        public int? SecurityQuestionId { get; set; }
        [ForeignKey("SecurityQuestionId")]
        public virtual SecurityQuestion SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }

        public string DisplayName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
