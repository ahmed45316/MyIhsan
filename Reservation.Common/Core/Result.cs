using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Reservation.Common.Core
{
    public class Result : IResult
    {
        public object Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
