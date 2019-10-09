using MyIhsan.LanguageResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Parameters
{
    public class LoginParameters
    {
        [Required(ErrorMessageResourceType = typeof(Translate), ErrorMessageResourceName = "userNameRequired")]
        [Display(Name = "Login_UserName", ResourceType = typeof(Translate))]
        public string Username { get; set; }
        [Required(ErrorMessageResourceType = typeof(Translate), ErrorMessageResourceName = "passwordRequired")]
        [Display(Name = "Login_Password", ResourceType = typeof(Translate))]
        public string Password { get; set; }
        [Display(Name = "Login_Remember", ResourceType = typeof(Translate))]
        public bool IsSavedPassword { get; set; }
        public bool Status { get; set; }
    }
}
