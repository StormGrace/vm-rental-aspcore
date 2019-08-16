using System;
using System.Collections.Generic;
using vm_rental.Data.Model;
using vm_rental.ViewModels;


namespace vm_rental.Data.Interface
{
    public interface IClientRepository : IRepository<Client>
    {
        Client CreateClient();
        IEnumerable<Client> GetAllWithUsers();
    }
}
