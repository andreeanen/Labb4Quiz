using System;
using System.Collections.Generic;

namespace Labb4_Quiz
{
    class Program
    {
        static QuizContext quizContext;

        static void Main(string[] args)
        {
            quizContext = new QuizContext();

            quizContext.Database.EnsureDeleted();
            var isCreated = quizContext.Database.EnsureCreated();
            //var connected = quizContext.Database.CanConnect();
            //Console.WriteLine($"can connect {connected} and is created {isCreated}");

            RegisterAdmins();
            LoadQuestions();

            StartPage();

            Console.WriteLine("Press any key to close the application...");
            Console.ReadKey(true);
        }

        private static void RegisterAdmins()
        {
            var admin1= new User
            {
                UserId = 1,
                Name = "Tijana",
                Password = "AVerySecurePassword!",
                UserStatus = UserStatus.Admin
            };

            var admin2 = new User
            {
                UserId = 2,
                Name = "Andreea",
                Password = "AnotherVerySecurePassword!",
                UserStatus = UserStatus.Admin
            };

            quizContext.Users.Add(admin1);
            quizContext.Users.Add(admin2);
            quizContext.SaveChanges();
        }

        private static void StartPage()
        {
            Console.WriteLine("Welcome, hello kitty, nice to see you bla bla bla...");
        }

        private static void LoadQuestions()
        {
            List<string> questions = new List<string>
            {
                "Which author created the character Maggie Tulliver?",
                "James Herriot was an animal doctor. Which of the four words below is correctly spelt?",
                "Where in Europe could you see Barbary macaques living wild?",
                "Which island was awarded the George Cross by King George VI after World War II?",
                "What is unusual about the reproduction process of the seahorse?",
                "In which musical would you hear the songs 'Mr Cellophane' and 'When You're Good to Mama'?",
                "Who invented the seed drill and the horse drawn hoe?",
                "Where in the world would you find a building called the Atomium?",
                "What is a gooseberry fool?",
                "In which movie does Katharine Hepburn say to Humphrey Bogart 'Dear, Dear, What is your first name?'?",
                "Which author has written Il nome della rosa (1980)?",
                "What is the name of Japanese poetry form consisting of 5+7+5 syllables?",
                "Which author(s) created the book series The Mortal Instruments?",
                "Who wrote this classic children's fiction novel? Black Beauty (1877)?",
                "Who wrote this Pulitzer Prize winning or nominated book? The Accidental Tourist?",
                "Which author(s) created the book series Goosebumps?",
                "Who is the author of Humboldt's Gift",
                "Who is the author of Madame Bovary",
                "What is the capital of American Samoa",
                "This island Lolland is part of which country? Lolland",
                "What is the capital of Ghana?"
            };

            List<string> incorrectAnswers1 = new List<string>
            {
                "Charlotte Bronte", "Vetinary", "Sardinia", "Cyprus", "The eggs are laid on the seabed", "Porgy and Bess", "Benjamin Franklin",
                "Budapest Hungary", "An unwanted third person with a couple", "On Golden Pond", "Italo Calvino", "Caricature", "Stephen King", "Mark Twain",
                "Norman Mailer", "Henning Mankell", "Toni Morrisson", "Marcel Proust", "Suva", "Spain", "Belgrade"
            };

            List<string> incorrectAnswers2 = new List<string>
            {
                "Jane Austen", "Vetinary", "Southern France", "Corsica", "Seahorses are hermaphrodite", "Cabaret", "Charles Babbage", "Belgrade Serbia",
                "A person who is mad about gooseberries", "Bringing Up Baby", "Milan Kundera", "Climax", "Robert Jordan", "E.B. White", "Peter Taylor",
                "Roger Hargreaves", "George Orwell", "Bram Stoker", "Ngerulmud (Melekeok)", "United States", "Tallinn"
            };

            List<string> incorrectAnswers3 = new List<string>
            {
                "Emily Bronte", "Veterinry", "Lanzarotte", "Jersey", "The eggs are laid in shells discarded by other animals.", "Show Boat",
                "Michael Faraday", "Bucharest Romania", "An open gooseberry tart", "Look Whose Coming to Dinner", "Carlo Collodi", "Essay",
                "Philip Pullman", "Beatrix Potter", "William Kennedy", "Lemony Snicket", "J.D. Salinger", "Mary Wollenstonecraft Shelley", "Majuro",
                "Denmark", "Bucharest"
            };

            List<string> correctAnswers = new List<string>
            {
                "George Eliot", "Veterinary", "Gibraltar", " Malta", "The male carries the young", "Chicago", "Jethro Tull", "Brussels Belgium",
                "A dessert with yoghurt or custard", "The African Queen", "Umberto Eco", "Haiku", "Cassandra Clare", "Anna Sewell", "Anne Tyler",
                "R.L. Stine", "Saul Bellow", "Gustave Flaubert", "Pago Pago", "South Africa", "Accra"

            };

            for (int i = 0; i < questions.Count; i++)
            {
                var question = new Question
                {
                    QuestionId = i + 1,
                    QuestionContent = questions[i],
                    Answers = new List<Answer>
                    {
                        new Answer { AnswerId = 4 * i + 1, AnswerContent = correctAnswers[i], IsCorrect = true },
                        new Answer { AnswerId = 4 * i + 2, AnswerContent = incorrectAnswers1[i], IsCorrect = false },
                        new Answer { AnswerId = 4 * i + 3, AnswerContent = incorrectAnswers2[i], IsCorrect = false },
                        new Answer { AnswerId = 4 * i + 4, AnswerContent = incorrectAnswers3[i], IsCorrect = false },
                    },
                    IsApproved = true
                };
                quizContext.Questions.Add(question);
                quizContext.SaveChanges();
            }

            //var quiz2 = new Quiz
            //{
            //    Questions = new List<Question> {
            //    new Question {
            //        QuestionContent = "what is your name?",
            //        Answers= new List<Answer> {
            //            new Answer { AnswerContent="i don't know", IsCorrect=true},
            //            new Answer { AnswerContent="i don't know2", IsCorrect=false},
            //            new Answer { AnswerContent="i don't know3", IsCorrect=false},
            //            new Answer { AnswerContent="i don't know4", IsCorrect=false}
            //        } } }
            //};
            //quizContext.Quizzes.Add(quiz2);
            //quizContext.SaveChanges();
        }
    }
}
