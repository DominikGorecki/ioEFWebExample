using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetQuizes()
        {
            var quizes = await _dbContext.Quizes.ToListAsync();
            return Ok(quizes);
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



    }
}