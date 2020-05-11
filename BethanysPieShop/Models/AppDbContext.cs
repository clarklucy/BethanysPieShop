using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class AppDbContext : DbContext        
    {
        //DbContext must have an instance of DbContextOptions for it to execute - see ctor below
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {

        }

        //entities the DbContext will manage. These map to tables in the database.

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
