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

            /* Alternative to configuring in Questions 
            quizBuilder
                .HasMany<QuestionEntity>(qz => qz.Questions)
                .WithOne(qst => qst.Quiz)
                .HasForeignKey(qst => qst.QuizId);
                */
        }

        public static void ConfigQuestionsEntity(this ModelBuilder modelBuilder)
        {
            var questionsBuilder = modelBuilder.Entity<QuestionEntity>();
            questionsBuilder.ToTable("Questions");
            questionsBuilder.HasKey(q => q.Id);

            questionsBuilder.Property(q => q.Question)
                .HasColumnName("Question")
                .HasMaxLength(100)
                .IsRequired();

            questionsBuilder.Property(q => q.QuestionOrder)
                .HasColumnName("QuestionOrder")
                .IsRequired();

            questionsBuilder.Property(q => q.QuizId)
                .HasColumnName("QuizId")
                .IsRequired();

            questionsBuilder.HasOne(qst => qst.Quiz)
                .WithMany(quiz => quiz.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(qst => qst.QuizId);
        }

        public static void ConfigAnswersEntity(this ModelBuilder modelBuilder)
        {
            var answersBuilder = modelBuilder.Entity<AnswerEntity>();
            answersBuilder.ToTable("Answers");
            answersBuilder.HasKey(a => a.Id);

            answersBuilder.Property(a => a.Answer)
                .HasColumnName("Answer")
                .HasMaxLength(255)
                .IsRequired();

            answersBuilder.Property(a => a.IsCorrect)
                .HasColumnName("IsCorrect")
                .IsRequired();

            answersBuilder.Property(a => a.QuestionId)
                .HasColumnName("QuestionId")
                .IsRequired();

            answersBuilder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.QuestionId);
        }

        public static void ConfigTagEntity(this ModelBuilder modelBuilder)
        {
            var tagBuilder = modelBuilder.Entity<TagEntity>();
            tagBuilder
                .ToTable("Tag")
                .HasKey("Id");

            tagBuilder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();
        }

        public static void ConfigQuizTagEntity(this ModelBuilder modelBuilder)
        {
            var quizTagBuilder = modelBuilder.Entity<QuizTagEntity>();
            quizTagBuilder
                .ToTable("Quizes_Tags")
                .HasKey(qt => new { qt.QuizId, qt.TagId });

            quizTagBuilder.HasOne(qt => qt.Quiz)
                .WithMany(qz => qz.QuizTags)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(qt => qt.QuizId);

            quizTagBuilder.HasOne(qt => qt.Tag)
                .WithMany(t => t.QuizTags)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(qt => qt.TagId);

        }
    }
}
