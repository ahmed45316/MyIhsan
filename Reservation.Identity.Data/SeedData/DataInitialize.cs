using System;
using System.Collections.Generic;
using System.Text;
using Reservation.Identity.Entities.Entities;

namespace Reservation.Identity.Data.SeedData
{
    public class DataInitialize : IDataInitialize
    {
        public AspNetRole[] AddDefaultRole()
        {
            var rolesList = new List<AspNetRole>();
            rolesList.AddRange(new[] {
                new AspNetRole{ Id = "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee",
                Name = "Administrator",
                IsBlock = false,
                IsDeleted = false
                },
            });
            return rolesList.ToArray();
        }

        public AspNetUser[] AddSystemAdmin()
        {
            var usersList = new List<AspNetUser>();
            usersList.AddRange(new[] {
                new AspNetUser{ Id = "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee",
                  SecurityStamp = Guid.NewGuid().ToString(),
                  UserName = "admin",
                  PasswordHash = "ALQ9yNzGkKdXRP8gdol1whMNSIZAlmjXpF6SNHELSKf0N6+aZs24+5h8B4OzpBWrIw==",
                  Email = "admin@A3n.com",
                  PhoneNumber = "+9",
                  IsDeleted = false,
                  IsBlock = false
                },
            });
            return usersList.ToArray();
        }

        public AspNetUsersRole[] AddUserRole()
        {
            var userRoleList = new List<AspNetUsersRole>();
            userRoleList.AddRange(new[] {
                new AspNetUsersRole{ Id = "c21c91c0-5c2f-45cc-ab6d-1d256538a6ee",
                RoleId = "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee",
                UserId = "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee",
                IsBlock = false,
                IsDeleted = false
                },
            });
            return userRoleList.ToArray();
        }
    }
}
