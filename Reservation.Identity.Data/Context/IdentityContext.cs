using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reservation.Identity.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Data.Context
{
    public class IdentityContext: IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            //Database.Migrate();
        }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuRole> MenuRole { get; set; }
    }
}
