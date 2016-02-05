using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcDataManager.Model
{
    public class UserLogin
    {
        [DisplayName("UserName"), Required]
        public string UserName { get; set; }
        [DisplayName("Password"), Required]
        public string Password { get; set; }
    }
}