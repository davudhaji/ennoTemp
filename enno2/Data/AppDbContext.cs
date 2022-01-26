using enno2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace enno2.Data
{
    public class AppDbContext: IdentityDbContext
    {
        
        public AppDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Team> teams { get; set; }

    }
}
