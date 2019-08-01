using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.Data.Interface;

namespace vm_rental.Data.Repository
{
    public class ClientHistoryRepository : Repository<ClientHistory>, IClientHistoryRepository
    {
        public ClientHistoryRepository(vmDbContext context) : base(context)
        {

        }
        //Join Operations
 

    }
}
