using MeAdota.Model;
using System.Threading.Tasks;

namespace MeAdota.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Token GetByAccessToken(string accessToken);
        Task<string> GenerateTokenByFacebook(string email, string facebookUserId);
        Task<string> GenerateToken(string email, string password);
        Task KillToken(int idToken);
        void RefreshToken(Token token);
        Task ChangePassword(int idUser, string newPassword);
    }
}
