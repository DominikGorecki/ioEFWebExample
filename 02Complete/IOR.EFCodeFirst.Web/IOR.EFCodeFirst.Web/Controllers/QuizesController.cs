﻿using System;
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
    }
}