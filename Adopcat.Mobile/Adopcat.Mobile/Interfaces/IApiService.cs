using Adopcat.Mobile.Helpers;
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

        [Get("/auth/logout")]
        Task Logout();
        #endregion

        #region User
        [Get("/user")]
        Task<User> GetUser(int id);

        [Post("/user")]
        Task CreateUser(User model);
        #endregion

        #region Poster
        [Get("/poster")]
        Task<Poster> GetPoster(int id);

        [Get("/poster")]
        Task<List<Poster>> GetPosters([Header("Authorization")] string token);
        #endregion
    }

}
