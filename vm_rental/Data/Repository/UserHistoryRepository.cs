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

        public bool UsernameExists(string username)
        {
          bool usernameExists = false;

          if (username != null)
          {
            usernameExists = _context.UserHistory.Any(em => em.Username.ToString() == username); ;
          }

          return usernameExists;
        }

        public bool EmailExists(string email)
            {
              bool emailExists = false;

              if (email != null)
              {
                emailExists = _context.UserHistory.Any(em => em.UserEmail.ToString() == email);
              }

              return emailExists;
            }
        }
}
