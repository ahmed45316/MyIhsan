using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.IdentityInterfaces
{
    public interface IUserDto
    {
        string Id { get; set; }
        string EntityIdInfo { get; set; }
        String Name { get; set; }
        String NameEn { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        string PasswordHash { get; set; }
        string SecurityStamp { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        bool TwoFactorEnabled { get; set; }
        DateTime? LockoutEndDateUtc { get; set; }
        bool LockoutEnabled { get; set; }
        int AccessFailedCount { get; set; }
        string UserName { get; set; }
        string TelNumber { get; set; }
        string Address { get; set; }
        bool? Gender { get; set; }
        string CountryId { get; set; }
        string CityId { get; set; }
        bool IsDeleted { get; set; }
        bool IsBlock { get; set; } 
        string AdminId { get; set; }
        string VendorId { get; set; }
        string ClientId { get; set; }
    }
}
