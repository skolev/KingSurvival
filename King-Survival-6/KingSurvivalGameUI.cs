using System;
using KingSurvivalGame.Common;

namespace KingSurvivalGame.UI
{
    class KingSurvivalGameUI
    {
        public static void Main()
        {
            Game game = new Game();

            while (!game.GameIsFinished)
            {
                Console.WriteLine(game.GetGridAsString());
                string message;

                if (game.Counter % 2 == 0)
                {
                    message = "Please enter king's turn: ";
                }
                else
                {
                    message = "Please enter pawn's turn: ";
                }

                while (true)
                {
                    Console.Write(message);
                    string userInput = GetUserInput();

                    if (userInput == null)
                    {
                        Console.WriteLine("Please enter something!");
                        continue;
                    }

                    if (ProcessPlayerMove(userInput, game))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command!");
                    }
                }
            }

            Console.WriteLine("Game is finished!");
            Console.WriteLine("\nThank you for using this game!\n\n");
        }

        public static string GetUserInput()
        {
            return Console.ReadLine().ToUpper();
        }

        public static bool ProcessPlayerMove(string cmd, Game game)
        {
            if (game.ProcessCommand(cmd))
            {
                return true;
            }

            return false;
        }

    }
}