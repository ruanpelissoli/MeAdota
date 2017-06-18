using Adopcat.Mobile.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adopcat.Mobile.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IApiService
    {
        #region Auth
        [Post("/auth/login")]
        Task<LoginResponse> Login(Login model);

        [Post("/auth/login/fb")]
        Task<LoginResponse> LoginFacebook(Login model);

        [Get("/auth/logout")]
        Task Logout();
        #endregion

        #region User
        [Get("/user")]
        Task<User> GetUser(int id);

        [Get("/user/byemail")]
        Task<User> GetUserByEmail(string email);

        [Post("/user")]
        Task<User> CreateUser(User model);

        [Post("/user/fb")]
        Task<User> CreateFacebookUser(User model);
        #endregion

        #region Poster
        [Get("/poster")]
        Task<PosterOutput> GetPoster(int id, [Header("Authorization")] string token);

        [Get("/poster")]
        Task<List<PosterOutput>> GetPosters(int userId, [Header("Authorization")] string token);

        [Get("/poster/filter")]
        Task<List<PosterOutput>> GetFilteredPosters(int userId, Filter filter, [Header("Authorization")] string token);

        [Get("/poster/my")]
        Task<List<PosterOutput>> GetMyPosters(int userId, [Header("Authorization")] string token);

        [Post("/poster")]
        Task<PosterOutput> CreatePoster(PosterInput poster, [Header("Authorization")] string token);
        #endregion
    }

}
