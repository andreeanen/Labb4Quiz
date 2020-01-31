using System.Collections.Generic;

namespace Labb4_Quiz
{
    public enum UserStatus { User, Admin }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual IList<Score> Scores { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
