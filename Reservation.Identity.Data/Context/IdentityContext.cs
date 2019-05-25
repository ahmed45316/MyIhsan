using Microsoft.EntityFrameworkCore;
using Reservation.Common.DbContextBase;
using Reservation.Identity.Entities.Entities;

namespace Reservation.Identity.Data.Context
{
    public class IdentityContext : BaseContext<IdentityContext>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
             
        }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUsersRole> AspNetUsersRoles { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
