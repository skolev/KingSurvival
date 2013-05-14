using System;

namespace KingSurvivalGame
{
    class KingSurvivalGameUI
    {
        static void Main()
        {
            KingPawsGame game = new KingPawsGame();
            game.InteractWithUser(game.Counter);
            Console.WriteLine("\nThank you for using this game!\n\n");
        }
    }
}