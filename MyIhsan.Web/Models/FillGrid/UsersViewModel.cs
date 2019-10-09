using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIhsan.Web.Models.FillGrid
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; } 
        public bool TwoFactorEnabled { get; set; } 
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string PasswordHash { get; set; }
        public string TelNumber { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string CityId { get; set; }
        public String Name { get; set; }
        public String NameEn { get; set; }
        public string EntityIdInfo { get; set; }
        public string EntityIdInfoName { get; set; }
        public string Roles { get; set; }
    }
}