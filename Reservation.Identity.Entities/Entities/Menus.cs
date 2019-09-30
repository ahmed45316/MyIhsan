using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIhsan.Identity.Entities.Entities
{
    public partial class Menus
    {
        public string Id { get; set; }
        public string ScreenNameAr { get; set; }
        public string ScreenNameEn { get; set; }
        public string Href { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Parameters { get; set; }
        public bool? IsStop { get; set; }
        public string ParentId { get; set; }
        public bool? ItsOrder { get; set; }
        public string Icon { get; set; }
        [ForeignKey("ParentId")]
        public virtual Menus Parent { get; set; }
        public virtual ICollection<Menus> Children { get; set; }
    }
}
