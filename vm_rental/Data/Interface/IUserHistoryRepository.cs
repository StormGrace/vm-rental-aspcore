using vm_rental.Data.Model;
using vm_rental.ViewModels;


namespace vm_rental.Data.Interface
{
    public interface IUserHistoryRepository : IRepository<UserHistory>
    {
        UserHistory CreateUserHistory(string username, string email, string password, string firstName, string lastName, string phone, User user);
        bool UsernameExists(string username);
        bool EmailExists(string email);      
    }
}