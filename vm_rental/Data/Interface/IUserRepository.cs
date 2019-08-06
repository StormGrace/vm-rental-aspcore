﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Model;

namespace vm_rental.Data.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User CreateUser(Client client);
    }
    
}
