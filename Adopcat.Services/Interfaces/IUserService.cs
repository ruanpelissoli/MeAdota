using Adopcat.Model;
using System.Collections.Generic;

namespace Adopcat.Services.Interfaces
{
    public interface IUserService
    {
        User GetByEmail(string email);

        User GetById(int id);

        List<User> GetAllActive();
        bool Deactivate(int idUser);

        User UpdateOrCreate(User model);

        bool EmailExists(int idUser, string email);
    }
}
