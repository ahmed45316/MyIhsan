using Reservation.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.Parameters
{
    public class GetAllUserParameters: BaseParam
    {
        public string UserName { get; set; }
    }
}
