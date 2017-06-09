using System;
using System.ComponentModel.DataAnnotations;

namespace Adopcat.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string DeviceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
