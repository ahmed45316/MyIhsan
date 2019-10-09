using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIhsan.Web.Models.ViewModels
{
    public class MenuViewModel
    {
        public string Id { get; set; }
        public string ScreenNameAr { get; set; }
        public string ScreenNameEn { get; set; }
        public string Href { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Parameters { get; set; }
        public string Icon { get; set; }
        public int ItsOrder { get; set; }
        public bool HaveChildren { get { return Children.Count > 0; } }
        public bool HaveParameters { get { return Parameters != null; } }
        public List<MenuViewModel> Children { get; set; }
    }
}