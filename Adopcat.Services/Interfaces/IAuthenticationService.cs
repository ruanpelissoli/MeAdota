using Adopcat.Model;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Token GetByAccessToken(string accessToken);
        Task<string> GenerateToken(string email, string password);
        Task KillToken(int idToken);
        void RefreshToken(Token token);
        Task ChangePassword(int idUser, string newPassword);
    }
}
