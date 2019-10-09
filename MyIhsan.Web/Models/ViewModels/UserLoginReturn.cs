using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIhsan.Web.Models.ViewModels
{
    public class UserLoginReturn 
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenValidTo { get; set; }
        public string AdminId { get; set; }
        public string VendorId { get; set; }
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string ImagePath { get; set; }
    }
}