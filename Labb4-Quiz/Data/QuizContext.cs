using Microsoft.EntityFrameworkCore;

namespace Labb4_Quiz
{
    public class QuizContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos("https://localhost:8081",
                                    "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                                    databaseName: "Quiz");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                        .ToContainer("Questions");

            modelBuilder.Entity<Answer>()
                        .ToContainer("Answers");

            modelBuilder.Entity<Quiz>()
                        .ToContainer("Quizzes");

            modelBuilder.Entity<User>()
                        .ToContainer("Users");

            modelBuilder.Entity<Score>()
                        .ToContainer("Scores");

            modelBuilder.Entity<Score>()
                        .HasKey(s => s.ScoreId)
                        .HasName("ScoreId");
        }
    }
}
