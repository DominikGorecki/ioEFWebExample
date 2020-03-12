using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOR.EFCodeFirst.Web.Data.Models
{
    public class QuizTagEntity
    {
        public int QuizId { get; set; }
        public virtual QuizEntity Quiz { get; set; }
        public int TagId { get; set; }
        public virtual TagEntity Tag { get; set; }
    }
}
