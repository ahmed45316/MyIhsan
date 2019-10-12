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
        public short? Gender { get; set; }
        public string Address { get; set; }
        public string TelNumber { get; set; }
        public short? IsBlock { get; set; }
        public short? IsDeleted { get; set; }
        public string UserName { get; set; }
        public long? AccessFailedCount { get; set; }
        public short? LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public short? TwoFactorEnabled { get; set; }
        public short? PhoneNumberConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public short? EmailConfirmed { get; set; }
        public string Email { get; set; }
        public long? Id { get; set; }
        public string CountryName { get; set; }
        public string Roles { get; set; }
    }
}