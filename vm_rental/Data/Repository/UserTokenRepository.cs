using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.Data.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace vm_rental.Data.Repository
{
  public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
  {
    public UserTokenRepository(VmDbContext context) : base(context) { }

    public bool isTokenValid(string provider, string purpose, string token, User user)
    {   
      return _context.UserToken.Any(u => (u.UserId.Equals(user.Id) && u.LoginProvider.Equals(provider) && u.Name.Equals(purpose) && u.Value.Equals(token)));
    }
  }
}
