using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb4_Quiz
{
    class Program
    {
        static QuizContext quizContext;
        static List<int> questionIdList = new List<int>();
        static void Main(string[] args)
        {
            quizContext = new QuizContext();

            quizContext.Database.EnsureDeleted();
            quizContext.Database.EnsureCreated();

            RegisterAdmins();
            LoadQuestions();

            User currentUser = StartPage();
            PrintMenu(currentUser);

            Console.WriteLine("Press any key to close the application...");
            Console.ReadKey(true);
        }

        private static void PrintMenu(User currentUser)
        {
            bool isInputValid = false;
            while (!isInputValid)
            {
                Console.WriteLine("Choose what you would like to do by typing the number in front of the option." +
                                  "\n1.Play a quiz" +
                                  "\n2.Add a new question to the quiz" +
                                  "\n3.Quit");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        PlayQuiz(currentUser);
                        isInputValid = true;
                        break;
                    case "2":
                        AddNewQuestionFromUser();
                        isInputValid = true;
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Your input is incorrect! Please try again..");
                        break;
                }
            }
        }

        private static void AddNewQuestionFromUser()
        {
            Console.WriteLine("Please type the content of your question and then press enter." +
                              "\nQuestion:");
            string questionContent = Console.ReadLine().Trim(' ');
            Console.WriteLine("Please type the correct answer for the question you wrote before." +
                              "\nCorrect answer:");
            string correctAnswer = Console.ReadLine().Trim();
            bool isWrongAnswersStringCorrect = false;
            while (!isWrongAnswersStringCorrect)
            {
                Console.WriteLine("Please type 3 wrong answers for your question and divide them by comma (,)." +
                              "\nWrong answers:");
                string wrongAnswers = Console.ReadLine().Trim(' ');
                var splitAnswers = wrongAnswers.Split(",");
                //List<int> questionIdList = new List<int>();
                foreach (var question in quizContext.Questions)
                {
                    questionIdList.Add(question.QuestionId);
                }
                var numberOfQuestionInDatabase = questionIdList.Count();
                if ((splitAnswers.Length == 3) && (correctAnswer != splitAnswers[0].Trim()) && (correctAnswer != splitAnswers[1].Trim()) && (correctAnswer != splitAnswers[2].Trim()))
                {
                    isWrongAnswersStringCorrect = true;
                    var newQuestionsFromUser = new Question
                    {
                        QuestionId = numberOfQuestionInDatabase + 1,
                        IsApproved = false,
                        QuestionContent = questionContent,
                        Answers = new List<Answer>
                    {
                        new Answer { AnswerId = 4 * numberOfQuestionInDatabase + 1, AnswerContent = correctAnswer, IsCorrect = true },
                        new Answer { AnswerId = 4 * numberOfQuestionInDatabase + 2, AnswerContent = splitAnswers[0], IsCorrect = false },
                        new Answer { AnswerId = 4 * numberOfQuestionInDatabase + 3, AnswerContent = splitAnswers[1], IsCorrect = false },
                        new Answer { AnswerId = 4 * numberOfQuestionInDatabase + 4, AnswerContent = splitAnswers[2], IsCorrect = false },
                    }

                    };
                    quizContext.Questions.Add(newQuestionsFromUser);
                    quizContext.SaveChanges();
                    Console.WriteLine("Your question was submited and an administrator will publish it.");
                }
                else
                {
                    Console.WriteLine("You need to write 3 wrong answers divided by comma (,) that are different from the corect answer." +
                                      "\nPlease try again..");
                }
            }

        }

        private static void PlayQuiz(User currentUser)
        {
            Console.WriteLine("The quiz starts right now. Good luck!");

            foreach (var question in quizContext.Questions)
            {
                if (question.IsApproved)
                {
                    questionIdList.Add(question.QuestionId);
                }
            }

            int numberOfQuestions = 10;
            Random random = new Random();
            List<int> random10Questions = new List<int>();
            for (int i = 0; i < numberOfQuestions; i++)
            {
                int randomId = random.Next(1, questionIdList.Count + 1);
                if (!random10Questions.Contains(randomId))
                {
                    random10Questions.Add(randomId);
                }
                else
                {
                    i--;
                }
            }
            List<int> quizIdList = new List<int>();
            foreach (var quiz in quizContext.Quizzes)
            {
                quizIdList.Add(quiz.QuizId);
            }
            var numberOfQuizesInDatabase = quizIdList.Count();

            Quiz newQuiz = new Quiz { QuizId = numberOfQuizesInDatabase + 1, Questions = new List<Question>() };  // TODO: Fix QuizId
            foreach (var randomQuestion in random10Questions)
            {
                foreach (var question in quizContext.Questions)
                {
                    if (question.QuestionId == randomQuestion)
                    {
                        newQuiz.Questions.Add(question);
                    }
                }
            }
            quizContext.Quizzes.Add(newQuiz);
            quizContext.SaveChanges();
            int score = 0;
            foreach (var question in quizContext.Quizzes.ToList().Last().Questions)
            {
                string correctAnswer = AskQuestion(question);

                //int currentScore = 0;
                Console.Write("\nWhat is the correct answer?\nEnter A, B, C or D: ");
                string usersAnswer = Console.ReadLine().Trim().ToUpper();
                if (usersAnswer == correctAnswer)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nCorrect answer!!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    score++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nIncorrect answer...");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("The correct answer is:" + correctAnswer);
                }

                Console.WriteLine("Current score: " + score);

                currentUser.Score = score;
                quizContext.Users.Update(currentUser);
                quizContext.SaveChanges();
            }
            Console.WriteLine($"\nCongratulations! Your final score is: {score}.");
        }

        private static string AskQuestion(Question question)
        {
            Console.WriteLine($"\n{question.QuestionContent}");

            List<int> randomAnswersOrder = ShuffleAnswersOrder();

            return PrintAnswers(question, randomAnswersOrder);
        }

        private static string PrintAnswers(Question question, List<int> randomAnswersOrder)
        {
            List<string> abcd = new List<string> { "A", "B", "C", "D" };
            string correctAnswer = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{abcd[i]} - {question.Answers[randomAnswersOrder[i]].AnswerContent}");
                if (question.Answers[randomAnswersOrder[i]].IsCorrect)
                {
                    correctAnswer = abcd[i];
                }
            }
            return correctAnswer;
        }

        private static List<int> ShuffleAnswersOrder()
        {
            List<int> randomAnswersOrder = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int randomAnswerIndex = random.Next(0, 4);
                if (!randomAnswersOrder.Contains(randomAnswerIndex))
                {
                    randomAnswersOrder.Add(randomAnswerIndex);
                }
                else
                {
                    i--;
                }
            }
            return randomAnswersOrder;
        }

        private static void RegisterAdmins()
        {
            var admin1 = new User
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

        private static User StartPage()
        {
            Console.WriteLine("Welcome to play the best quiz of the year!" +
                              "\nPlease enter your username:");
            string userNameInput = Console.ReadLine();
            List<int> userIdList = new List<int>();
            foreach (var item in quizContext.Users)
            {
                userIdList.Add(item.UserId);
            }
            var numberOfUsersInDatabase = userIdList.Count();

            var newUser = new User
            {
                UserId = numberOfUsersInDatabase + 1,
                Name = userNameInput,
                Score = 0,
                UserStatus = UserStatus.User
            };
            quizContext.Users.Add(newUser);
            quizContext.SaveChanges();

            return newUser;
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

        }
    }
}
