using System.ComponentModel.DataAnnotations;

namespace Adopcat.API.Models
{
    public class FacebookUserViewModel
    {
        [Required]
        public string Name { get; set; }
        public string FacebookId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PictureUrl { get; set; }
    }
}