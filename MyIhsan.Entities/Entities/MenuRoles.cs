using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIhsan.Entities.Entities
{
    public partial class MenuRoles
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string MenuId { get; set; }
        [ForeignKey("RoleId")]
        public virtual AspNetRoles AspNetRoles { get; set; }
        [ForeignKey("MenuId")]
        public virtual Menus Menus { get; set; }
    }
}
