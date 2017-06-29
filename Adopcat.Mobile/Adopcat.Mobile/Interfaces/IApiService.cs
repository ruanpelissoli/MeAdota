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
        Task Logout([Header("Authorization")] string token);
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

        [Put("/user")]
        Task UpdateUser(string email, User model, [Header("Authorization")] string token);
        #endregion

        #region Poster
        [Get("/poster")]
        Task<PosterOutput> GetPoster(int id, [Header("Authorization")] string token);

        [Get("/poster")]
        Task<List<PosterOutput>> GetPosters([Header("Authorization")] string token);

        [Get("/poster/filter")]
        Task<List<PosterOutput>> GetFilteredPosters(string city, int? petType, bool? castrated, bool? dewormed, [Header("Authorization")] string token);

        [Get("/poster/my")]
        Task<List<PosterOutput>> GetMyPosters([Header("Authorization")] string token);

        [Post("/poster")]
        Task<PosterOutput> CreatePoster(PosterInput poster, [Header("Authorization")] string token);

        [Put("/poster")]
        Task UpdatePoster(PosterInput poster, [Header("Authorization")] string token);

        [Delete("/poster")]
        Task<PosterOutput> DeletePoster(int id, [Header("Authorization")] string token);
        #endregion

        #region Download
        [Get("/download")]
        Task<byte[]> Download(string url);
        #endregion

        #region Reports
        [Post("/reports")]
        Task<Reports> CreateReport(Reports model, [Header("Authorization")] string token);
        #endregion

        #region Log
        [Post("/log")]
        Task<List<SystemLog>> CreateLog(SystemLog log, [Header("Authorization")] string token);
        #endregion
    }

}
