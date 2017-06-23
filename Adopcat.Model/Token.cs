using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adopcat.Model
{
    public class Token
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Access_token { get; set; }
        [Required]
        [StringLength(56)]
        public string Token_type { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
