using Reservation.Identity.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Data.SeedData
{
   public interface IDataInitialize
    {
        AspNetUser[] AddSystemAdmin();
        AspNetRole[] AddDefaultRole();
        AspNetUsersRole[] AddUserRole();
        Menu[] addMenus();
    }
}
