using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIhsan.Web.Models.ViewModels
{
    public class AssignUserOnRoleViewModel
    {
        public string RoleId { get; set; }
        public string[] AssignedUser { get; set; }
    }
}