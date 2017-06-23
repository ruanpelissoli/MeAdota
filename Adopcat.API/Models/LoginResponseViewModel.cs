using Adopcat.Model;

namespace Adopcat.API.Models
{
    public class LoginResponseViewModel
    {
        public string AuthToken { get; set; }
        public int UserId { get; set; }
    }
}