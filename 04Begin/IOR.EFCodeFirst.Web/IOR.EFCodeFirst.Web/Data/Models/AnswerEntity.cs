using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOR.EFCodeFirst.Web.Data.Models
{
    public class AnswerEntity
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public virtual QuestionEntity Question { get; set; }
    }
}
