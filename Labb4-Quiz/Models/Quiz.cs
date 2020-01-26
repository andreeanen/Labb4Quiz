using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Labb4_Quiz
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        public virtual IList<Question> Questions { get; set; }
        public virtual User User { get; set; }
    }
}
