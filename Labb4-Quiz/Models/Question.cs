using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb4_Quiz
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionContent { get; set; }
        [ForeignKey("QuizId")]
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public virtual IList<Answer> Answers { get; set; }

    }
}
