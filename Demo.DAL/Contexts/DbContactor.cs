using Azure.Messaging;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class DbContactor : IdentityDbContext<ApplicationUser>
    {
        public DbContactor(DbContextOptions<DbContactor> options):base(options) 
        { }

    
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
