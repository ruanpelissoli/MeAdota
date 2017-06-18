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
        private IAuthenticationService _authService;

        public UserService(ILoggingService log, 
                           IUserRepository userRepository, 
                           IApplicationParameterRepository applicationParameterRepository,
                           IAuthenticationService authService) : base(log)
        {
            _userRepository = userRepository;
            _applicationParameterRepository = applicationParameterRepository;
            _authService = authService;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await TryCatch(async () =>
            {
                var query = await _userRepository.GetAllAsync(r => r.Email == email && r.IsActive);
                return query.FirstOrDefault();
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
                return await _userRepository.FindAsync(id);
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
                user.PictureUrl = model.PictureUrl;
                user.FacebookId = model.FacebookId;
                user.Phone = model.Phone;

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

        public async Task<User> GetByToken(string authToken)
        {
            return await TryCatch(async () =>
            {
                var token = _authService.GetByAccessToken(authToken);
                return await _userRepository.FindAsync(token.UserId);                
            });
        }
    }
}
