using Adopcat.Data.Interfaces;
using Adopcat.Model;
using Adopcat.Services.Exceptions;
using Adopcat.Services.Interfaces;
using Adopcat.Services.Util;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        public Token GetByAccessToken(string accessToken)
        {
            return TryCatch(() =>
            {
                return _tokenRepository.GetAll(x => x.Access_token.Equals(accessToken) && x.ExpiresUtc >= DateTime.UtcNow).FirstOrDefault();
            });
        }

        public string GenerateToken(string email, string password)
        {
            return TryCatch(() =>
            {
                password = Cryptography.GetMD5Hash(password);
                var user = _userRepository.GetAll(x => x.Email == email && x.Password == password && x.IsActive).FirstOrDefault();
                if (user != null)
                {
                    KillExpiredTokens(user);
                    string accessToken = CreateToken(email, password);
                    var token = new Token()
                    {
                        Access_token = accessToken,
                        Token_type = "Bearer",
                        IssuedUtc = DateTime.UtcNow,
                        UserId = user.Id
                    };

                    token.ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(1));

                    _tokenRepository.Create(token);
                    return token.Access_token;
                }

                throw new UnauthorizedException("Unauthorized");
            });
        }

        public void KillToken(long idToken)
        {
            TryCatch(() =>
            {
                _tokenRepository.Delete(t => t.Id == idToken);
            });
        }

        private void KillExpiredTokens(User user)
        {
            TryCatch(() =>
            {
                if (user != null)
                {
                    _tokenRepository.Delete(t => t.UserId == user.Id && t.ExpiresUtc < DateTime.UtcNow);
                }
            });
        }

        public void RefreshToken(Token token)
        {
            TryCatch(() =>
            {
                if (token != null)
                {
                     token.ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(1));
                    _tokenRepository.Update(token);
                }
            });
        }

        public void ChangePassword(int idUser, string newPassword)
        {
            TryCatch(() =>
            {
                var user = _userRepository.GetAll(u => u.Id == idUser).FirstOrDefault();
                user.Password = Cryptography.GetMD5Hash(newPassword);
                _userRepository.Update(user);
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
