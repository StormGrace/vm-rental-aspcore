using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.ViewModels.Account;

namespace vm_rental.Data.Interface
{
    public interface IUserHistoryRepository : IRepository<UserHistory>
    {
        UserHistory CreateUserHistory(ClientViewModel clientVM, User user);
    }
}