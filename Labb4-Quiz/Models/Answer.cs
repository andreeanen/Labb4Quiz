using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb4_Quiz
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

    }
}
