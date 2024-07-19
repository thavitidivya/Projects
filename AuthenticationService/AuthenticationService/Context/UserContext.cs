using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthenticationService.Context
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> Authuserss { get; set; }
    }
}

