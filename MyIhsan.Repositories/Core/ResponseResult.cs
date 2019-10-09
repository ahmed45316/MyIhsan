using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Core
{
    public class ResponseResult : Result
    {
        public Exception Exception { get; }

        public new HttpStatusCode Status { get; }

        public ResponseResult()
        {
           
        }
    }
}
