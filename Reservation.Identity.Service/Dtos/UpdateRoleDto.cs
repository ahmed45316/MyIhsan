using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Identity.Service.Dtos
{
    public class UpdateRoleDto
    {
        public bool IsBlock { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public int AspNetUsersRoleCount { get; set; }
    }
}
