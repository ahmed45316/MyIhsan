using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Common.Core
{
    public interface IHandlerResponse
    {
        IResult GetResult(IResponseResult responseResult);
    }
}
