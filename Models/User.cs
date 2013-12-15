using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LMSServices.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Comment { get; set; }
        public string LogOnMessage { get; set; }
        public string LogOffMessage { get; set; }
        public DateTime LastLoginDT { get; set; }
        public int ChangePassword { get; set; }
        public DateTime ChangePasswordDT { get; set; }
        public int InActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int RemainingLeaveDays { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public DbSet<User> AppUser { get; set; }
    }
}
