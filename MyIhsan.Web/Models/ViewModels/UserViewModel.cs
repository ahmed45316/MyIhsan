using MyIhsan.LanguageResources.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyIhsan.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "usernameVal")]
        [Display(Name = "username", ResourceType = typeof(Auth))]
        [Remote("CheckUserNameExist", "Security", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName= "usernameExist", ErrorMessageResourceType =typeof(Auth))]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "emailVal")]
        [Display(Name = "email", ResourceType = typeof(Auth))]
        [EmailAddress(ErrorMessageResourceName= "emailRegx", ErrorMessageResourceType =typeof(Auth))]
        [Remote("CheckEmailExist", "Security", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName = "mailExist", ErrorMessageResourceType = typeof(Auth))]
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; } = false;
        public string SecurityStamp { get; set; }
        [Display(Name = "mobile", ResourceType = typeof(Auth))]
        [Remote("CheckPhoneExist", "Security", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName = "mobileExist", ErrorMessageResourceType = typeof(Auth))]
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; } = false;
        public bool? TwoFactorEnabled { get; set; } = false;
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool? LockoutEnabled { get; set; } = false;
        public int? AccessFailedCount { get; set; } = 0;
        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "passVal")]
        [Display(Name = "pass", ResourceType = typeof(Auth))]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "passRegx")]
        public string PasswordHash { get; set; }
        [Display(Name = "confirmPass", ResourceType = typeof(Auth))]
        [System.ComponentModel.DataAnnotations.CompareAttribute("PasswordHash", ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "confirmPassRegx")]
        public string ConfirmPasswordHash { get; set; }
    }
}