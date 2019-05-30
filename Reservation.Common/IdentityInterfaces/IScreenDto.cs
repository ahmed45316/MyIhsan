using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.IdentityInterfaces
{
    public interface IScreenDto
    {
        string Id { get; set; }
        string ScreenNameAr { get; set; }
        string ScreenNameEn { get; set; }
    }
}
