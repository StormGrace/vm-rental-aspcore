using System.Linq;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;
using vm_rental.ViewModels;

namespace vm_rental.Data.Repository
{
    public class UserHistoryRepository : Repository<UserHistory>, IUserHistoryRepository
    {
        public UserHistoryRepository(vmDbContext context) : base(context)
        {

        }
        public UserHistory CreateUserHistory(string username, string email, string password, string firstName, string lastName, string phone, User user)
        {
            UserHistory userHistory = new UserHistory(
                username, 
                email, 
                password, 
                firstName,
                lastName, 
                phone)
            {
                CreatedByNavigation = user
            };

            this.Add(userHistory);

            return userHistory;
        }

        public bool EmailExists(string email)
        {
          var emailExists = this._context.UserHistory.Any(em => em.UserEmail.ToString() == email);
          return emailExists;
        }
  }
}
