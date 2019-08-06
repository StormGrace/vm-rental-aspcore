using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.ViewModels.Account;

namespace vm_rental.Data.Interface
{
    public interface IClientRepository : IRepository<Client>
    {
        Client CreateClient(ClientViewModel client);
        IEnumerable<Client> GetAllWithUsers();
    }
}
