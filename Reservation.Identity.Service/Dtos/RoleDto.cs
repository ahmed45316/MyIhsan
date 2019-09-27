using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Identity.Service.Dtos
{
    public class RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlock { get; set; }
    }
}
