using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AngularCrud.Server.Models;

namespace AngularCrud.Server.Data
{
    public class AngularCrudServerContext : DbContext
    {
        public AngularCrudServerContext (DbContextOptions<AngularCrudServerContext> options)
            : base(options)
        {
        }

        public DbSet<AngularCrud.Server.Models.Employee> Employee { get; set; } = default!;
    }
}
