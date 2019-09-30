using MyIhsan.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Identity.Service.Dtos
{
    public class RoleDto : IPrimaryKeyField<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlock { get; set; }
        public int AspNetUsersRolesCount { get; set; }
    }
}
