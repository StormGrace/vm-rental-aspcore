using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vm_rental.Data.Repository.Interface;
using vm_rental.Data.Model;
using System;

namespace vm_rental.Data.Repository
{
   public class UserRepository : Repository<User>, IUserRepository
  {
        private readonly new VmDbContext _context;

        public UserRepository(VmDbContext context) : base(context)
        {
          _context = context;
        }

        public void CreateUser(User user)
        {
          Add(user);
        }

        public User FindByIdAsync(string id)
        {
          return FindFirst(user => user.Id.Equals(id));
        }

        public User FindByNameAsync(string username)
        {
          return FindFirst(user => user.UserName.Equals(username));
        }

        public User FindByEmailAsync(string email)
        {
          return FindFirst(user => user.Email.ToUpper().Equals(email));
        }

        public void ConfirmEmail(string username)
        {
          User user = FindByNameAsync(username);

          user.EmailConfirmed = true;

          Update(user);
        }

        public bool UsernameExists(string username)
        {
          bool exists = false;

          if (username != null)
          {
            exists = Exists(user => user.UserName.Equals(username));
          }

          return exists;
        }

        public bool EmailExists(string email)
        {
          bool exists = false;

          if (email != null)
          {
            exists = _context.UserHistory.Any(em => em.Email.ToString() == email);
          }

          return exists;
        }
  }
}
