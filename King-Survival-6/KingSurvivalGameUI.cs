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
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(game.GetGridAsString());
                
                string message;
                message = game.KingIsOnTheMove ? "Please enter king's turn: " : "Please enter pawn's turn: ";
                
                while (true)
                {
                    Console.Write(message);
                    string userInput = GetUserInput();

                    if (userInput == null)
                    {
                        Console.WriteLine("Please enter something!");
                        continue;
                    }

                    if (game.ValidateCommand(userInput))
                    {
                        game.ProcessCommand(userInput);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Illegal move!");
                    }
                }

                if (game.CheckIfKingExited(game.KingYPosition))
                {
                    Console.WriteLine("End!");
                    Console.WriteLine("King wins in {0} moves!", game.KingTurns);
                }
            }

            Console.WriteLine("Game is finished!");
            Console.WriteLine("\nThank you for using this game!\n\n");
        }

        public static string GetUserInput()
        {
            return Console.ReadLine().ToUpper();
        }

    }
}