using MyIhsan.LanguageResources.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyIhsan.Web.Models.ViewModels
{
    public class RoleViewModel 
    {
        [Required(ErrorMessageResourceName = "nameRegx",ErrorMessageResourceType =typeof(Auth))]
        [Display(Name = "name", ResourceType = typeof(Auth))]
        [Remote("CheckExistingName", "Security", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceName = "roleExist", ErrorMessageResourceType = typeof(Auth))]
        public string Name { get; set; }
        public string Id { get; set; }
        public int AspNetUsersRoleCount { get; set; }
    }
}