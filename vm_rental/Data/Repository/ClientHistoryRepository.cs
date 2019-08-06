using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.Data.Interface;
using vm_rental.ViewModels.Account;

namespace vm_rental.Data.Repository
{
    public class ClientHistoryRepository : Repository<ClientHistory>, IClientHistoryRepository
    {
        public ClientHistoryRepository(vmDbContext context) : base(context)
        {

        }
        public ClientHistory CreateClientHistory(ClientViewModel clientVM, Client client, User user)
        {
            ClientHistory clientHistory = new ClientHistory(
                 clientVM.firmName, 
                 clientVM.firstName + " " + clientVM.lastName, 
                 clientVM.email, 
                 clientVM.phone, 
                 clientVM.state,
                 clientVM.city,
                 Convert.ToByte(clientVM.isBusinessClient))
            {
                Client = client,
                CreatedByNavigation = user
            };

            this.Add(clientHistory);

            return clientHistory;
        }
    }
}
