﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Repositories.Core
{
    public class Result 
    {
        public object Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
