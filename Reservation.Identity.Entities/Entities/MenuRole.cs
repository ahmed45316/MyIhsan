﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Entities.Entities
{
   public class MenuRole
    {
        [Key]
        [StringLength(256)]
        public string Id { get; set; }
        [StringLength(256)]
        public string RoleId { get; set; }
        [StringLength(256)]
        public string MenuId { get; set; }
        [ForeignKey("RoleId")]
        public virtual AspNetRole AspNetRole { get; set; }
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

    }
}
