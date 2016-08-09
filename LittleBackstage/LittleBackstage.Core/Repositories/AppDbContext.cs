using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBackstage.Core.Models;
using LittleBackstage.Infrastructure;

namespace LittleBackstage.Core.Repositories
{
   public class AppDbContext : DbContext, IDependencyPerRequest
    {
        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<ForExcel> ForExcels { get; set; }
    }
}
