using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.ViewModels.Account;

namespace vm_rental.Data.Interface
{
    public interface IClientHistoryRepository : IRepository<ClientHistory>
    {
        ClientHistory CreateClientHistory(ClientViewModel clientVM, Client client, User user);
    }
}
