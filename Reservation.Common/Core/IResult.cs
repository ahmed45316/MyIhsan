using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Reservation.Common.Core
{
    public interface IResult
    {
        object Data { get; set; }
        HttpStatusCode Status { get; set; }
        string Message { get; set; }
        bool Success { get; set; }
    }
}
