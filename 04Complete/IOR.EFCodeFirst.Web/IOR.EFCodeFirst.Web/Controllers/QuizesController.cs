using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using IOR.EFCodeFirst.Web.Data;
using IOR.EFCodeFirst.Web.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IOR.EFCodeFirst.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizesController : ControllerBase
    {
        private readonly QuizesDbContext _dbContext;

        public QuizesController(QuizesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<QuizWithTags>), 200)]
        public async Task<IActionResult> GetQuizes()
        {
            var quizes = await _dbContext.Quizes
                .Include(q => q.QuizTags)
                    .ThenInclude(qt => qt.Tag)
                .ToListAsync();
            var quizesWithTags = quizes
                .Select(q => QuizWithTags.MapFrom(q))
                .ToList(); ;
                
            return Ok(quizesWithTags);
        }

        public class QuizWithTags 
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public IEnumerable<string> Tags { get; set; }

            internal static QuizWithTags MapFrom(QuizEntity quiz)
                => new QuizWithTags
                {
                    Id = quiz.Id,
                    Title = quiz.Title,
                    Tags = quiz.QuizTags.Select(qt => qt.Tag.Name)
                };
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetQuiz(int id)
        {
            var quiz = await _dbContext
                .Quizes
                .Include(qz => qz.Questions)
                    .ThenInclude(qs => qs.Answers)
                .FirstOrDefaultAsync(qz => qz.Id == id);
                
            return Ok(quiz);
        }

        [HttpPost]
        [Route("{title}")]
        public async Task<IActionResult> PostQuiz(string title)
        {
            _dbContext.Quizes.Add(new QuizEntity
            {
                Title = title
            });

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}/Question/{question}")]
        public async Task<IActionResult> PutQuestion(int id, string question)
        {
            var quiz = await _dbContext.Quizes.FindAsync(id);
            if (quiz == null) return BadRequest();

            var newQuestion = new QuestionEntity
            {
                Question = question,
                Quiz = quiz
            };
            await _dbContext.Questions.AddAsync(newQuestion);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("Question/{id}/Answer")]
        public async Task<IActionResult> PutAnswer(int id, [FromBody] AnswerPutModel model)
        {
            var question = await _dbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null) return BadRequest();

            var newAnswer = new AnswerEntity
            {
                Answer = model.Answer,
                IsCorrect = model.IsCorrect,
                Question = question
            };

            _dbContext.Answers.Add(newAnswer);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        public class AnswerPutModel
        {
            public string Answer { get; set; }
            public bool IsCorrect { get; set; }
        }

        [HttpPut]
        [Route("Tag/{name}")]
        [ProducesResponseType(typeof(NewTagResponse), 200)]
        public async Task<IActionResult> PutTag(string name)
        {
            var newTag = new TagEntity
            {
                Name = name
            };

            _dbContext.Tags.Add(newTag);
            await _dbContext.SaveChangesAsync();
            return Ok(new NewTagResponse
            {
                Id = newTag.Id,
                Name = newTag.Name
            });
        }
        public class NewTagResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [HttpPut]
        [Route("{id}/Tag/{tagId}")]
        public async Task<IActionResult> PutTagToQuiz(int id, int tagId)
        {
            var quiz = await _dbContext.Quizes.FindAsync(id);
            if (quiz == null) return BadRequest();
            var tag = await _dbContext.Tags.FindAsync(tagId);
            if (tag == null) return BadRequest();

            _dbContext.QuizTags
                .Add(new QuizTagEntity
                {
                    Quiz = quiz,
                    Tag = tag
                });

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}