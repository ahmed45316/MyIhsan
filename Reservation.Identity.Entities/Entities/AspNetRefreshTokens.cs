using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Reservation.Identity.Entities.Entities
{
    [Table("AspNetRefreshTokens")]
    public class RefreshToken
    {
        [Key]
        [StringLength(450)]
        public string Id { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        [Required]
        [StringLength(450)]
        public string Token { get; set; }

        [StringLength(256)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUser User { get; set; }
    }
}
