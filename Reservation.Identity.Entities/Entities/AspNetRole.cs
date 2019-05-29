namespace Reservation.Identity.Entities.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AspNetRole
    {
        [Key]
        [StringLength(256)]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsBlock { get; set; } = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUsersRole> AspNetUsersRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuRole> Menu { get; set; }
    }
}