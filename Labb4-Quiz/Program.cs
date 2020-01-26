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
            var isCreated = quizContext.Database.EnsureCreated();
            var connected = quizContext.Database.CanConnect();
            Console.WriteLine($"can connect {connected} and is created {isCreated}");
            var quiz = new Quiz
            {
                Questions = new List<Question> {
                new Question {
                    QuestionContent = "mama",
                    Answers= new List<Answer> {
                        new Answer { AnswerContent="yes"}
                    } } }
            };
            quizContext.Quizzes.Add(quiz);
            quizContext.SaveChanges();
            foreach (var item in quizContext.Questions.ToList())
            {
                Console.WriteLine(item.Answers.ToString());
            }


            Console.ReadKey(true);
        }
    }
}
