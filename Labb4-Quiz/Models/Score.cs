using System;
using System.ComponentModel.DataAnnotations;

namespace Labb4_Quiz
{
    public class Score
    {
        [Key]
        public Guid ScoreId { get; }
        public int ScorePerQuiz { get; set; }

        public string UserNamePerQuiz { get; set; }
        public virtual User User { get; set; }
    }
}
