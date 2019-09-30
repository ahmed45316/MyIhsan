using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIhsan.Identity.Entities.Entities
{
    public partial class AspNetUsersRoles
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsBlock { get; set; }
        [ForeignKey("RoleId")]
        public virtual AspNetRoles AspNetRoles { get; set; }
    }
}
