namespace Reservation.Identity.Entities.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AspNetUser
    {
        [Key]
        [StringLength(256)]
        public string Id { get; set; }

        [StringLength(50)]
        public string EntityIdInfo { get; set; }

        [StringLength(100)]
        public String Name { get; set; }
        [StringLength(100)]
        public String NameEn { get; set; }
        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }
        
        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string TelNumber { get; set; }
        [StringLength(1000)]
        public string Address { get; set; }

        public bool? Gender { get; set; }
        [StringLength(100)]
        public string CountryId { get; set; }
        [StringLength(100)]
        public string CityId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public bool IsBlock { get; set; } = false;
        [StringLength(256)]
        public string AdminId { get; set; }
        [StringLength(256)]
        public string VendorId { get; set; }
        [StringLength(256)]
        public string ClientId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUsersRole> AspNetUsersRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
    }
}
