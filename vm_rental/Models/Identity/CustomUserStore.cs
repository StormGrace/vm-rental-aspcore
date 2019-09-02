using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.Data.Repository.Interface;

namespace vm_rental.Models.Identity
{
  public class CustomUserStore : IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User> 
  {
    private readonly IUserRepository _userRepository;

    public CustomUserStore(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
      _userRepository.CreateUser(user);

      return Task.FromResult(IdentityResult.Success);
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Id.ToString());
    }

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.UserName = userName);
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.NormalizedUserName = normalizedName);
    }

    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
      return Task.FromResult(IdentityResult.Success); //To Implement
    }

    public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
      return Task.FromResult(IdentityResult.Success); //To Implement
    }

    public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      return Task.FromResult(_userRepository.FindByIdAsync(userId));
    }

    public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      return Task.FromResult(_userRepository.FindByNameAsync(normalizedUserName));
    }

    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        user.PasswordHash = passwordHash;
      });
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        return user.PasswordHash;
      });
    }

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        bool flag = false;

        if (String.IsNullOrEmpty(user.PasswordHash))
        {
          flag = true;
        }

        return Task.FromResult(flag);
      });
    }

    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        user.Email = email;
      });
    }

    public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        return user.Email;
      });
    }

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        return user.EmailConfirmed;
      });
    }

    public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        return user.EmailConfirmed = confirmed;
      });
    }

    public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
      return Task.FromResult(_userRepository.FindByEmailAsync(normalizedEmail) as User);
    }

    public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        return user.Email.ToUpper();
      });
    }

    public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        return user.NormalizedEmail = normalizedEmail;
      });
    }

    public void Dispose(){}
  }
}
