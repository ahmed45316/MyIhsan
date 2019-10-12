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
        [Display(Name = "nameEn", ResourceType = typeof(Auth))]
        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "nameEnRegx")]
        public string NameEn { get; set; }

        [Display(Name = "name", ResourceType = typeof(Auth))]
        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "nameRegx")]
        public string Name { get; set; }

        [Display(Name = "country", ResourceType = typeof(Auth))]
        public string CountryId { get; set; }

        public string CountryName { get; set; }

        [Display(Name = "city", ResourceType = typeof(Auth))]
        public string CityId { get; set; }

        public string CityName { get; set; }

        [Display(Name = "gender", ResourceType = typeof(Auth))]
        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "genderRegx")]
        public short? Gender { get; set; }

        [Display(Name = "address", ResourceType = typeof(Auth))]
        public string Address { get; set; }

        [Display(Name = "telNumber", ResourceType = typeof(Auth))]
        public string TelNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "usernameVal")]
        [Display(Name = "username", ResourceType = typeof(Auth))]
        [Remote("CheckUserNameExist", "Security", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName = "usernameExist", ErrorMessageResourceType = typeof(Auth))]
        public string UserName { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        [Display(Name = "mobile", ResourceType = typeof(Auth))]
        [Remote("CheckPhoneExist", "Security", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName = "mobileExist", ErrorMessageResourceType = typeof(Auth))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "passVal")]
        [Display(Name = "pass", ResourceType = typeof(Auth))]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "passRegx")]
        public string PasswordHash { get; set; }

        [Display(Name = "confirmPass", ResourceType = typeof(Auth))]
        [System.ComponentModel.DataAnnotations.CompareAttribute("PasswordHash", ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "confirmPassRegx")]
        public string ConfirmPasswordHash { get; set; }

        [Required(ErrorMessageResourceType = typeof(Auth), ErrorMessageResourceName = "emailVal")]
        [Display(Name = "email", ResourceType = typeof(Auth))]
        [EmailAddress(ErrorMessageResourceName = "emailRegx", ErrorMessageResourceType = typeof(Auth))]
        [Remote("CheckEmailExist", "Security", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName = "mailExist", ErrorMessageResourceType = typeof(Auth))]
        public string Email { get; set; }

        public long? Id { get; set; }
    }
}