using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOR.EFCodeFirst.Web.Data
{
    public class QuizesDbContext : DbContext
    {
        public QuizesDbContext(DbContextOptions options)  : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // We'll add code here
        }
    }
}
