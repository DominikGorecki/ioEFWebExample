using IOR.EFCodeFirst.Web.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOR.EFCodeFirst.Web.Data
{
    public static class QuizesDbContextConfig
    {
        public static void ConfigQuizEntity(this ModelBuilder modelBuilder)
        {
            var quizBuilder = modelBuilder.Entity<QuizEntity>();
            quizBuilder.ToTable("Quizes");
            quizBuilder.HasKey(q => q.Id);

            // The next two lines are not necessary because the entity prop  
            // matches up perfectly with the Column name of the table.
            // If the column name were different, it would be changed here.
            quizBuilder.Property(q => q.Id)
                .HasColumnName("Id");

            quizBuilder.Property(q => q.Title)
                .HasColumnName("Title")
                .HasMaxLength(100) // varchar(100)
                .IsRequired(); // not null
        }
    }
}
