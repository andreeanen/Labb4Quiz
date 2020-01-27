using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb4_Quiz
{
    class Program
    {
        static void Main(string[] args)
        {

            var quizContext = new QuizContext();
            quizContext.Database.EnsureDeleted();
            var isCreated = quizContext.Database.EnsureCreated();
            var connected = quizContext.Database.CanConnect();
            Console.WriteLine($"can connect {connected} and is created {isCreated}");
            var quiz2 = new Quiz
            {
                Questions = new List<Question> {
                new Question {
                    QuestionContent = "what is your name?",
                    Answers= new List<Answer> {
                        new Answer { AnswerContent="i don't know", IsCorrect=false}
                    } } }
            };
            quizContext.Quizzes.Add(quiz2);
            quizContext.SaveChanges();
            foreach (var item in quizContext.Questions.ToList())
            {
                Console.WriteLine(item.Answers.ToString());
            }


            Console.ReadKey(true);
        }
    }
}
