using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;
using Soundville.Infrastructure.WindsorCastle;

namespace Soundville.Presentation.Identity
{
    public class CustomUserStore : IUserPasswordStore<ApplicationUser>, IUserStore<ApplicationUser>, IUserLoginStore<ApplicationUser>
    {
        private readonly IUserDomainService _userDomainService;

        public CustomUserStore()
        {
            _userDomainService = IoC.ContainerInstance.Resolve<IUserDomainService>();
        }

        public Task CreateAsync(ApplicationUser appUser)
        {
            var user = new User
            {
                Email = appUser.Email,
                PasswordHash = appUser.PasswordHash
            };

            appUser.Id = _userDomainService.Create(user).Id.ToString();

            return Task.FromResult(0);
        }

        public Task UpdateAsync(ApplicationUser appUser)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var id = int.Parse(userId);
            User user = _userDomainService.GetById(id);
            var appUser = new ApplicationUser
            {
                Id = user.Id.ToString(),
                UserName = user.Email,
                PasswordHash = user.PasswordHash
            };

            return Task.FromResult(appUser);
        }

        public Task<ApplicationUser> FindByNameAsync(string email)
        {
            ApplicationUser appUser = null;
            User user = _userDomainService.GetByEmail(email);
            if (user == null)
            {
                return Task.FromResult(appUser);
            }

            appUser = new ApplicationUser
            {
                Id = user.Id.ToString(),
                UserName = user.Email,
                PasswordHash = user.PasswordHash
            };

            return Task.FromResult(appUser);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
    }
}
