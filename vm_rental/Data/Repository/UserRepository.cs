using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vm_rental.Data.Repository.Interface;
using vm_rental.Data.Model;
using System;

namespace vm_rental.Data.Repository
{
   public class UserRepository : Repository<User>, IUserRepository, IUserStore<User>
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
          //Console.WriteLine("bad");
        }

        public User CreateUser(Client client)
        {
          throw new System.NotImplementedException();
        }
  }
}
