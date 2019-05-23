using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Reservation.Identity.Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        public string ImagePath { get; set; }
        [DefaultValue(false)]
        public bool IsBlocked { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public Guid AdminId { get; set; }
        public Guid VendorId { get; set; }
        public Guid ClientId { get; set; }
    }
}
