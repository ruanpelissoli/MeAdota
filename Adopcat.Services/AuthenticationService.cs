using Adopcat.Data.Interfaces;
using Adopcat.Model;
using Adopcat.Services.Exceptions;
using Adopcat.Services.Interfaces;
using Adopcat.Services.Util;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Adopcat.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService 
    {
        private ITokenRepository _tokenRepository;
        private IUserRepository _userRepository;
        
        public AuthenticationService(ILoggingService log, ITokenRepository tokenRepository, IUserRepository userRepository) : base(log)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        public async Task<Token> GetByAccessToken(string accessToken)
        {
            return await TryCatch(async () =>
            {
                return await _tokenRepository.FindAsync(x => x.Access_token.Equals(accessToken) && x.ExpiresUtc >= DateTime.UtcNow);
            });
        }

        public async Task<string> GenerateToken(string email, string password)
        {
            return await TryCatch(async () =>
            {
                password = Cryptography.GetMD5Hash(password);
                var user = _userRepository.GetAll(x => x.Email == email && x.Password == password && x.IsActive).FirstOrDefault();
                if (user != null)
                {
                    await KillExpiredTokens(user);
                    string accessToken = CreateToken(email, password);
                    var token = new Token()
                    {
                        Access_token = accessToken,
                        Token_type = "Bearer",
                        IssuedUtc = DateTime.UtcNow,
                        UserId = user.Id
                    };

                    token.ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(1));

                    await _tokenRepository.CreateAsync(token);
                    return token.Access_token;
                }

                throw new UnauthorizedException("Unauthorized");
            });
        }

        public async Task KillToken(int idToken)
        {
            await TryCatch(async () =>
            {
                await _tokenRepository.DeleteAsync(t => t.Id == idToken);
            });
        }

        private async Task KillExpiredTokens(User user)
        {
            await TryCatch(async () =>
            {
                if (user != null)
                {
                    await _tokenRepository.DeleteAsync(t => t.UserId == user.Id && t.ExpiresUtc < DateTime.UtcNow);
                }
            });
        }

        public async Task RefreshToken(Token token)
        {
            await TryCatch(async () =>
            {
                if (token != null)
                {
                     token.ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(1));
                    await _tokenRepository.UpdateAsync(token);
                }
            });
        }

        public async Task ChangePassword(int idUser, string newPassword)
        {
            await TryCatch(async () =>
            {
                var user = _userRepository.GetAll(u => u.Id == idUser).FirstOrDefault();
                user.Password = Cryptography.GetMD5Hash(newPassword);
                await _userRepository.UpdateAsync(user);
            });
        }

        private const string _alg = "HmacSHA256";
        private const string _salt = "O117QJOnjxMLlsx42SxTh";

        private static string CreateToken(string username, string password)
        {
            var ticks = DateTime.UtcNow.Ticks;
            string hash = string.Join(":", new string[] { username, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));

                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { username, ticks.ToString() });
            }

            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
            return token.Replace("=", "");
        }

        private static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hmac.Hash);
            }
        }
    }
}
