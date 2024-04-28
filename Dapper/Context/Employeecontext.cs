using Dapper.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dapper.Context
{
    public class Employeecontext :DbContext
    {
        
            public Employeecontext(DbContextOptions options) : base(options)
            {

            }
            public DbSet<Employee> Employees { set; get; }
            public DbSet<Company> companies { set; get; }

        
    }
}
