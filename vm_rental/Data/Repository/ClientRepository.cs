using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;
using vm_rental.ViewModels.Account;

namespace vm_rental.Data.Repository
{
   public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(vmDbContext context) : base(context){}
        public Client CreateClient(ClientViewModel clientVM)
        {
            Client client = new Client();

            this.Add(client);

            return client;
        }

        //Join Operations
        public IEnumerable<Client> GetAllWithUsers()
        {
            return _context.Client.Include(c => c.User);
        }

    }
}
