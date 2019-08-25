using System.Collections.Generic;
using vm_rental.Data.Repository.Interface.Common;
using vm_rental.Data.Model;

namespace vm_rental.Data.Repository.Interface
{
    public interface IClientRepository : IRepository<Client>
    {
        void CreateClient(Client client);
        IEnumerable<Client> GetAllWithUsers();
    }
}
