using Adopcat.Data.Interfaces;
using Adopcat.Model;
using Adopcat.Services.Interfaces;
using Adopcat.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<User> GetByEmail(string email)
        {
            return await TryCatch(async () =>
            {
                return await _userRepository.FindAsync(r => r.Email == email && r.IsActive);
            });
        }

        public async Task<List<User>> GetAllActive()
        {
            return await TryCatch(async () =>
            {
                return await _userRepository.GetAllAsync(u => u.IsActive);
            });
        }

        public async Task<User> GetById(int id)
        {
            return await TryCatch(async () =>
            {
                return await _userRepository.FindAsync(r => r.Id == id);
            });
        }

        public async Task<bool> Deactivate(int idUser)
        {
            return await TryCatch(async () =>
            {
                var model = _userRepository.GetAll(u => u.Id == idUser).FirstOrDefault();
                model.IsActive = false;
                await _userRepository.UpdateAsync(model);
                return true;
            });
        }

        public async Task<User> UpdateOrCreateAsync(User model)
        {
            return await TryCatch(async () =>
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

                if (model.Id == 0)
                    return await _userRepository.CreateAsync(user);

                await _userRepository.UpdateAsync(user);
                return user;
            });
        }

        public async Task<bool> EmailExists(int idUser, string email)
        {
            return await TryCatch(async () =>
            {
                var list = await _userRepository.GetAllAsync(u => u.Email == email && u.Id != idUser && u.IsActive);
                return list.Any();
            });
        }
    }
}
