using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcDataManager.Model
{
    public class ForgotPassword
    {
        [DisplayName("UserName"), Required]
        public string UserName { get; set; }

        [DisplayName("SecurityQuestionId"), Required]
        public string SecurityQuestionId { get; set; }

        [DisplayName("Answer"), Required]
        public string Answer { get; set; }

        [DisplayName("SecurityQuestions")]
        public IEnumerable<SecurityQuestion> SecurityQuestions { get; set; }
    }
}