using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Reservation.Common.Core
{
    public interface IResponseResult : IResult
    {
        Exception Exception { get; }

        new HttpStatusCode Status { get; }

        IResponseResult GetRepositoryActionResult(object result = null,
            HttpStatusCode status = HttpStatusCode.BadRequest, Exception exception = null,
            string message = null);
    }
}
