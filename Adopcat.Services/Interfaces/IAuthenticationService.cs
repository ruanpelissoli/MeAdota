using Adopcat.Model;

namespace Adopcat.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Token GetByAccessToken(string accessToken);

        string GenerateToken(string email, string password);

        void KillToken(long idToken);

        void RefreshToken(Token token);

        void ChangePassword(int idUser, string newPassword);
    }
}
