using MyIhsan.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Service.Dtos
{
    public class MenuDto : IPrimaryKeyField<string>
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
        public List<MenuDto> Children { get; set; }
    }
}
