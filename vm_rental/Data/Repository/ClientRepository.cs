using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;
 
namespace vm_rental.Data.Repository
{
   public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(vmDbContext context) : base(context){}


        //Join Operations
        public IEnumerable<Client> GetAllWithUsers()
        {
            return _context.Client.Include(c => c.User);
        }

    }
}
