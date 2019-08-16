using vm_rental.Data.Model;
using vm_rental.ViewModels;


namespace vm_rental.Data.Interface
{
    public interface IClientHistoryRepository : IRepository<ClientHistory>
    {
        ClientHistory CreateClientHistory(string firmName, string firstName, string lastName, string email, string phone, string state, string city, bool isBusinessClient, Client client, User user);
    }
}
