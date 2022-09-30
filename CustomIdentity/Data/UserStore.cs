using CustomIdentity.Enums;
using CustomORM;
using Microsoft.AspNetCore.Identity;

namespace CustomIdentity.Models
{
    public class UserStore : IUserStore<AppUser>, IUserPasswordStore<AppUser>, IUserRoleStore<AppUser>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Repository<AppUser> _repositoryUsers;
        private readonly Repository<AppRole> _repositoryRoles;
        public UserStore()
        {
            _repositoryUsers = new Repository<AppUser>();
            _unitOfWork = new UnitOfWork();
            _repositoryRoles = new Repository<AppRole>();
        }

        public Task AddToRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.RoleName = UserRoles.user.ToString();
            return Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _repositoryUsers.Insert(user);
            _unitOfWork.SaveChanges();
            if (user != null)
            {
                return await Task.FromResult(IdentityResult.Success);
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not create user {user.Email}." });
        }

        public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var result = _repositoryUsers.Get(int.Parse(userId));
            return await Task.FromResult(result);
        }

        public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedUserName == null)
            {
                throw new ArgumentNullException(nameof(normalizedUserName));
            }
            var users = _repositoryUsers.GetAll();
            var result = users.FirstOrDefault(users => users.Email == normalizedUserName);
            return await Task.FromResult(result);
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var roles = new List<string>();
            var result = _repositoryRoles.GetAll();
            foreach(var role in result)
            {
                roles.Add(role.Name);
            }
            return roles;
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }

        public Task<IList<AppUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(AppUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsInRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var users = _repositoryUsers.GetAll();
            var newUser = users.FirstOrDefault(u => u.Email == user.Email.ToUpper());
            return newUser.RoleName.ToUpper() == roleName ? await Task.FromResult(true) : await Task.FromResult(false);
        }

        public Task RemoveFromRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(user == null || normalizedName == null)
            {
                throw new ArgumentNullException($"{nameof(user)} or {nameof(normalizedName)} is null!");
            } 
            user.Email = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null || passwordHash == null)
            {
                throw new ArgumentNullException($"{nameof(user)} or {nameof(passwordHash)} is null!");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
