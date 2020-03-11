using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOR.EFCodeFirst.Web.Data.Models
{
    public class QuestionEntity
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int QuestionOrder { get; set; }

        public int QuizId { get; set; }
        public QuizEntity Quiz { get; set; }
    }
}
