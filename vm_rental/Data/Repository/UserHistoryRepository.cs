using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;
using vm_rental.ViewModels.Account;

namespace vm_rental.Data.Repository
{
    public class UserHistoryRepository : Repository<UserHistory>, IUserHistoryRepository
    {
        public UserHistoryRepository(vmDbContext context) : base(context)
        {

        }
        public UserHistory CreateUserHistory(ClientViewModel clientVM, User user)
        {
            UserHistory userHistory = new UserHistory(
                clientVM.userName, 
                clientVM.email, 
                clientVM.password, 
                clientVM.firstName,
                clientVM.lastName, 
                clientVM.phone)
            {
                CreatedByNavigation = user
            };

            this.Add(userHistory);

            return userHistory;
        }
    }
}
