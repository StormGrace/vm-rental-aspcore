using System;
using System.Collections.Generic;

namespace vm_rental.Data.Model
{
    public partial class User
    {
        public User()
        {
            ClientDiscountHistory = new HashSet<ClientDiscountHistory>();
            ClientHistory = new HashSet<ClientHistory>();
            MachineComponentHistory = new HashSet<MachineComponentHistory>();
            MachineComponentTypeHistory = new HashSet<MachineComponentTypeHistory>();
            MachineHistory = new HashSet<MachineHistory>();
            MachinesUsers = new HashSet<MachinesUsers>();
            Order = new HashSet<Order>();
            ProductHistory = new HashSet<ProductHistory>();
            ProductSupplierHistory = new HashSet<ProductSupplierHistory>();
            UserClaim = new HashSet<UserClaim>();
            UserHistory = new HashSet<UserHistory>();
            UserLogin = new HashSet<UserLogin>();
            UserToken = new HashSet<UserToken>();
            UsersRoles = new HashSet<UsersRoles>();
        }

        //public int UserId { get; set; }
        public int ClientId { get; set; }
        //public string Username { get; set; }
        //public string Email { get; set; }
        //public byte EmailConfirmed { get; set; }
        //public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string PhoneNumber { get; set; }
        //public string SecurityStamp { get; set; }
        //public byte 
        //public override bool LockoutEnabled { get; set; }
        //public DateTimeOffset? LockoutEnd { get; set; }
        //public int AccessFailedCount { get; set; }
        //public string ConcurrencyStamp { get; set; }
        public byte IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<ClientDiscountHistory> ClientDiscountHistory { get; set; }
        public virtual ICollection<ClientHistory> ClientHistory { get; set; }
        public virtual ICollection<MachineComponentHistory> MachineComponentHistory { get; set; }
        public virtual ICollection<MachineComponentTypeHistory> MachineComponentTypeHistory { get; set; }
        public virtual ICollection<MachineHistory> MachineHistory { get; set; }
        public virtual ICollection<MachinesUsers> MachinesUsers { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ProductHistory> ProductHistory { get; set; }
        public virtual ICollection<ProductSupplierHistory> ProductSupplierHistory { get; set; }
        public virtual ICollection<UserClaim> UserClaim { get; set; }
        public virtual ICollection<UserHistory> UserHistory { get; set; }
        public virtual ICollection<UserLogin> UserLogin { get; set; }
        public virtual ICollection<UserToken> UserToken { get; set; }
        public virtual ICollection<UsersRoles> UsersRoles { get; set; }
    }
}
