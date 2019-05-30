using Reservation.Common.IdentityInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Service.Dtos
{
    public class UserDto : IUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; } = false;
        public bool TwoFactorEnabled { get; set; } = false;
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; } = false;
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsBlock { get; set; } = false;
        public string AdminId { get; set; }
        public string VendorId { get; set; }
        public string ClientId { get; set; }
        public string Roles { get; set; }
    }
}
