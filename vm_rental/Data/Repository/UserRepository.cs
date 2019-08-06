using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;

namespace vm_rental.Data.Repository
{
   public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(vmDbContext context) : base(context)
        {
            
        }
        public User CreateUser(Client client)
        {
            User user = new User() { Client = client };

            this.Add(user);

            return user;
        }
    }
}
