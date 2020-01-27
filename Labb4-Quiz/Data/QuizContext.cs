using Microsoft.EntityFrameworkCore;

namespace Labb4_Quiz
{
    public class QuizContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseCosmos("https://localhost:8081",
        //                            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
        //                            databaseName: "Quiz");
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=LeQuiz;Integrated Security=True;Pooling=False");
            optionsBuilder.UseLazyLoadingProxies();

        }
    }
}
