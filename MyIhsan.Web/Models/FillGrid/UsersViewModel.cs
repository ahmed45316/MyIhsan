using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIhsan.Web.Models.FillGrid
{
    public class UsersViewModel
    {
        public string NameEn { get; set; }
        public string Name { get; set; }
        public string CityId { get; set; }
        public string CountryId { get; set; }
        public bool? Gender { get; set; }
        public string Address { get; set; }
        public string TelNumber { get; set; }
        public bool? IsBlock { get; set; }
        public bool? IsDeleted { get; set; }
        public string UserName { get; set; }
        public long? AccessFailedCount { get; set; }
        public bool? LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string Email { get; set; }
        public long? Id { get; set; }
        public string CountryName { get; set; }
        public string Roles { get; set; }
    }
}