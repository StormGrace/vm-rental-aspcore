using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vm_rental.Data.Repository.Interface;
using vm_rental.Data.Model;
using System;

namespace vm_rental.Data.Repository
{
   public class UserRepository : Repository<User>, IUserRepository, IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>, IUserAuthenticationTokenStore<User>
  {
        public UserRepository(VmDbContext context) : base(context){ }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
          this.Add(user);
          return Task.FromResult(IdentityResult.Success);
        }

        public bool UsernameExists(string username)
        {
          bool usernameExists = false;

          if (username != null)
          {
            usernameExists = _context.User.Any(em => em.UserName.ToString() == username); ;
          }

          return usernameExists;
        }

        public bool EmailExists(string email)
        {
          bool emailExists = false;

          if (email != null)
          {
            emailExists = _context.UserHistory.Any(em => em.Email.ToString() == email);
          }

          return emailExists;
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
          return Task.FromResult(_context.User.FirstOrDefault(u => u.Id.Equals(userId)));
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
          return Task.FromResult(_context.User.FirstOrDefault(u => u.UserName.ToUpper().Equals(normalizedUserName)));
        }

        public void Dispose()
        {
           
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

    public User CreateUser(Client client)
    {
      throw new NotImplementedException();
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
        user.EmailConfirmed = confirmed;
      });
    }

    public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
      return Task.FromResult(_context.User.FirstOrDefault(u => u.Email.ToUpper().Equals(normalizedEmail)));
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
        user.NormalizedEmail = normalizedEmail;
      });
    }

    public Task SetTokenAsync(User user, string loginProvider, string name, string value, CancellationToken cancellationToken)
    {
      UserToken userToken = new UserToken
      {
        LoginProvider = loginProvider,
        Name = name,
        Value = value,
        User = user,
        UserId = user.Id  
      };

      _context.Add(userToken);
      _context.SaveChanges();

      return Task.Run(() =>
      {
        return Task.CompletedTask;
      });
    }

    public Task RemoveTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
    {
      UserToken token = _context.UserToken.FirstOrDefault(u => u.LoginProvider.Equals(loginProvider) && u.Name.Equals(name));

      _context.Remove(token);
      _context.SaveChanges();

      return Task.CompletedTask;
    }

    public Task<string> GetTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
    {
      UserToken token = _context.UserToken.FirstOrDefault(u => u.LoginProvider.Equals(loginProvider) && u.Name.Equals(name));

      return Task.FromResult(token.Value);
    }
  }
}
