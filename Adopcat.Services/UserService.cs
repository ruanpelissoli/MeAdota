using Adopcat.Data.Interfaces;
using Adopcat.Model;
using Adopcat.Services.Interfaces;
using Adopcat.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adopcat.Services
{
    public class UserService : BaseService, IUserService
    {
        private IUserRepository _userRepository;
        
        private readonly IApplicationParameterRepository _applicationParameterRepository;

        public UserService(ILoggingService log, IUserRepository userRepository, IApplicationParameterRepository applicationParameterRepository) : base(log)
        {
            _userRepository = userRepository;
            _applicationParameterRepository = applicationParameterRepository;
        }

        public User GetByEmail(string email)
        {
            return TryCatch(() =>
            {
                return _userRepository.GetAll(r => r.Email == email && r.IsActive).FirstOrDefault();
            });
        }

        public List<User> GetAllActive()
        {
            return TryCatch(() =>
            {
                return _userRepository.GetAll(u => u.IsActive).ToList();
            });
        }

        public User GetById(int id)
        {
            return TryCatch(() =>
            {
                return _userRepository.GetAll(r => r.Id == id).FirstOrDefault();
            });
        }

        public bool Deactivate(int idUser)
        {
            return TryCatch(() =>
            {
                var model = _userRepository.GetAll(u => u.Id == idUser).FirstOrDefault();
                model.IsActive = false;
                _userRepository.Update(model);
                return true;
            });
        }

        public User UpdateOrCreate(User model)
        {
            return TryCatch(() =>
            {
                var user = new User();
                if (model.Id > 0)
                {
                    user = _userRepository.GetAll(u => u.Id == model.Id).FirstOrDefault();
                }
                else
                {
                    user.IsActive = true;
                    user.CreatedAt = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(model.Password))
                    user.Password = Cryptography.GetMD5Hash(model.Password);

                user.Name = model.Name;
                user.Email = model.Email;

                if (user.Id == 0)
                    return _userRepository.Create(user);                

                _userRepository.Update(user);
                return user;
            });
        }

        public bool EmailExists(int idUser, string email)
        {
            return TryCatch(() =>
            {
                return _userRepository.GetAll(u => u.Email == email && u.Id != idUser && u.IsActive).Any();
            });
        }
    }
}
