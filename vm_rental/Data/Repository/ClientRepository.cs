using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using vm_rental.Data.Model;
using vm_rental.Data.Repository.Interface;

namespace vm_rental.Data.Repository
{
   public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(VmDbContext context) : base(context){}

        public void CreateClient(Client client)
        {
           Add(client);
        }

        //Join Operations
        public IEnumerable<Client> GetAllWithUsers()
        {
            return _context.Client.Include(c => c.User);
        }
    }
}
