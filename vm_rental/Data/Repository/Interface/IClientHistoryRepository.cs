using vm_rental.Data.Repository.Interface.Common;
using vm_rental.Data.Model;

namespace vm_rental.Data.Repository.Interface
{
    public interface IClientHistoryRepository: IRepository<ClientHistory>, IHistoryRepository<Client>
    {
        
    }
}
