using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;

namespace vm_rental.Data.Repository
{
    public class UserHistoryRepository : Repository<UserHistory>, IUserHistoryRepository
    {
        public UserHistoryRepository(vmDbContext context) : base(context)
        {

        }
    }
}
