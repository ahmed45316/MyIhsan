using System;
using System.Collections.Generic;

namespace MyIhsan.Identity.Entities.Entities
{
    public partial class AspNetRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsBlock { get; set; }
    }
}
