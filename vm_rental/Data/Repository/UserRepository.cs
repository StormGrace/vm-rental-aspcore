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
        public UserRepository(vm_usage_reportsContext context) : base(context)
        {
            
        }


    }
}
