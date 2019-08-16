using vm_rental.Data.Model;


namespace vm_rental.Data.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User CreateUser(Client client);
    }
    
}
