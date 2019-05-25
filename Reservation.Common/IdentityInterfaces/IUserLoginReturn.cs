using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.IdentityInterfaces
{
    public interface IUserLoginReturn
    {
        string Token { get; set; }
        string RefreshToken { get; set; }
        DateTime TokenValidTo { get; set; }
        string AdminId { get; set; }
        string VendorId { get; set; }
        string ClientId { get; set; }
        string UserId { get; set; }
        string NameEn { get; set; }
        string NameAr { get; set; }
        string ImagePath { get; set; }
    }
}
