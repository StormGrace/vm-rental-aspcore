using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.Data.Repository.Interface.Common;

namespace vm_rental.Data.Repository.Interface
{ 
  public interface IUserTokenRepository : IRepository<UserToken>
  {
    bool isTokenValid(string provider, string purpose, string token, User user);
  }
}
