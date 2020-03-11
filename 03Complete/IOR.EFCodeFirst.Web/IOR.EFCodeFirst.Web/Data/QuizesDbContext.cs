using IOR.EFCodeFirst.Web.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOR.EFCodeFirst.Web.Data
{
    public class QuizesDbContext : DbContext
    {
        public DbSet<QuizEntity> Quizes { get; set; }
        public DbSet<QuestionEntity> Questions { get; set; }
        public DbSet<AnswerEntity> Answers { get; set; }

        public QuizesDbContext(DbContextOptions options)  : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigQuizEntity();
            modelBuilder.ConfigQuestionsEntity();
            modelBuilder.ConfigAnswersEntity();
        }
    }
}
