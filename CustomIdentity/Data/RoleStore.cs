using CustomORM;
using Microsoft.AspNetCore.Identity;

namespace CustomIdentity.Models
{
    public class RoleStore : IRoleStore<AppRole>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Repository<AppRole> _repositoryRoles;
        public RoleStore()
        {
            _repositoryRoles = new Repository<AppRole>();
            _unitOfWork = new UnitOfWork();
        }
        public async Task<IdentityResult> CreateAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _repositoryRoles.Insert(role);
            _unitOfWork.SaveChanges();
            return await Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(AppRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<AppRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (roleId == null)
            {
                throw new ArgumentNullException(nameof(roleId));
            }
            var result = _repositoryRoles.Get(Convert.ToInt32(roleId));
            return await Task.FromResult(result);
        }

        public async Task<AppRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedRoleName == null)
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }
            var roles = _repositoryRoles.GetAll();
            var result = roles.FirstOrDefault(roles => roles.ToString() == normalizedRoleName);
            return await Task.FromResult(result);
        }

        public Task<string> GetNormalizedRoleNameAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var result = _repositoryRoles.Get(role.Id).Name;
            if (String.IsNullOrEmpty(result))
            {
                throw new ArgumentException("This user name not found", nameof(role));
            }
            return Task.FromResult(result);
        }

        public async Task<string> GetRoleIdAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var result = _repositoryRoles.Get(role.Id).Id.ToString();
            if (String.IsNullOrEmpty(result))
            {
                throw new ArgumentException("This role not have password hash", nameof(role));
            }
            return await Task.FromResult(result);
        }

        public async Task<string> GetRoleNameAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var roleName = _repositoryRoles.Get(role.Id).Name;
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("This user not have role name", nameof(role));
            }
            return await Task.FromResult(roleName);
        }

        public async Task SetNormalizedRoleNameAsync(AppRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            if (string.IsNullOrWhiteSpace(normalizedName)) throw new ArgumentNullException(nameof(normalizedName));
            role.Name = normalizedName;
            await Task.FromResult<object>(null);
        }

        public async Task SetRoleNameAsync(AppRole role, string roleName, CancellationToken cancellationToken)
        {
            await SetNormalizedRoleNameAsync(role, roleName, cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(AppRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
