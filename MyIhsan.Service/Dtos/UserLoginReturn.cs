﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Service.Dtos
{
    public class UserLoginReturn 
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenValidTo { get; set; }
        public string UserId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
