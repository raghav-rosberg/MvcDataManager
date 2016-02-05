using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcDataManager.Model
{
    public class UserInfo
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ProfilePicUrl { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public bool Activated { get; set; }

        [DisplayName("SecurityQuestion"), Required]
        public int SecurityQuestionId { get; set; }

        public IEnumerable<SelectListItem> SecurityQuestions { get; set; }

        [Required]
        public string SecurityAnswer { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}