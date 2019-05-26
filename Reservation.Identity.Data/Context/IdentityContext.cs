using Microsoft.EntityFrameworkCore;
using Reservation.Common.DbContextBase;
using Reservation.Identity.Data.SeedData;
using Reservation.Identity.Entities.Entities;

namespace Reservation.Identity.Data.Context
{
    public class IdentityContext : BaseContext<IdentityContext>
    {
        private readonly IDataInitialize _dataInitialize;
        public IdentityContext(DbContextOptions<IdentityContext> options, IDataInitialize dataInitialize) : base(options)
        {
            _dataInitialize = dataInitialize;
        }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUsersRole> AspNetUsersRoles { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed Data
            modelBuilder.Entity<AspNetUser>().HasData(_dataInitialize.AddSystemAdmin());
            modelBuilder.Entity<AspNetRole>().HasData(_dataInitialize.AddDefaultRole());
            modelBuilder.Entity<AspNetUsersRole>().HasData(_dataInitialize.AddUserRole());
            #endregion
        }
    }
}
