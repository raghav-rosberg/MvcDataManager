using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcDataManager.Model
{
    public class ResetPassword
    {
        public string UserName { get; set; }

        [DisplayName("NewPassword"), Required]
        public string NewPassword { get; set; }

        [DisplayName("ConfirmPassword"), Required]
        public string ConfirmPassword { get; set; }
    }
}