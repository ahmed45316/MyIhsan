using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Entities.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsDeleted { get; set; } = false;
        public bool IsBlock { get; set; } = false;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuRole> Menu { get; set; }
    }
}
