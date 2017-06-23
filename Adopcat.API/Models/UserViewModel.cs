using System.ComponentModel.DataAnnotations;

namespace Adopcat.API.Models
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string PictureUrl { get; set; }
        public byte[] Picture { get; set; }
        public bool ReceiveNotifications { get; set; }
        public string RegistrationId { get; set; }
    }
}