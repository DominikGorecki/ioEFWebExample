using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IOR.EFCodeFirst.Web.Data.Models
{
    public class QuizEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<QuestionEntity> Questions { get; set; }
        public virtual ICollection<QuizTagEntity> QuizTags { get; set; }
    }
}
