using System;
using vm_rental.Data.Model;
using vm_rental.Data.Interface;
using vm_rental.ViewModels;

namespace vm_rental.Data.Repository
{
    public class ClientHistoryRepository : Repository<ClientHistory>, IClientHistoryRepository
    {
        public ClientHistoryRepository(vmDbContext context) : base(context)
        {

        }
        public ClientHistory CreateClientHistory(string firmName, string firstName, string lastName, string email, string phone, string state, string city, bool isBusinessClient, Client client, User user)
        {
            ClientHistory clientHistory = new ClientHistory(
                 firmName, 
                 firstName + " " + lastName, 
                 email, 
                 phone, 
                 state,
                 city,
                 Convert.ToByte(isBusinessClient))
            {
                Client = client,
                CreatedByNavigation = user
            };

            this.Add(clientHistory);

            return clientHistory;
        }
    }
}
