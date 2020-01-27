using System.ComponentModel.DataAnnotations.Schema;

namespace Labb4_Quiz
{
    public enum UserStatus { User, Admin }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
        public UserStatus UserStatus { get; set; }
        //[ForeignKey("QuizId")]
        //public int QuizId { get; set; }
        //public virtual Quiz Quiz { get; set; }
    }
}
