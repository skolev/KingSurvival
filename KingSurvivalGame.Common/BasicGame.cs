using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvivalGame.Common
{
    /// <summary>
    /// Server as a base for the game. It holds the logic for game field size.
    /// Validating commands and checking if a given location is within the border of the game grid.
    /// </summary>
    public class BasicGame
    {
        protected int[,] edgesOfTheGameField = 
        {
            { 2, 3 }, { 2, 11 }, { 9, 4 }, { 9, 11 }
        };

        /// <summary>
        /// Keeps a count of both the moves made by the king and those made by any of the pawns.
        /// </summary>
        public int MovesCount { get; set; }

        protected string[] validKingInputs = { "KUL", "KUR", "KDL", "KDR" };

        protected string[] validAPawnInputs = { "AL", "AR" };

        protected string[] validBPawnInputs = { "BL", "BR" };

        protected string[] validCPawnInputs = { "CL", "CR" };

        protected string[] validDPawnInputs = { "DL", "DR" };

        protected bool CheckIfInBoard(int[] positionCoodinates)
        {
            int positonRow = positionCoodinates[0];
            bool isRowInBoard = (positonRow >= edgesOfTheGameField[0, 0]) && (positonRow <= edgesOfTheGameField[3, 0]);
            int positonCol = positionCoodinates[1];
            bool isColInBoard = (positonCol >= edgesOfTheGameField[0, 1]) && (positonCol <= edgesOfTheGameField[3, 1]);
            
            return isRowInBoard && isColInBoard;
        }

        /// <summary>
        /// Makes sure that the passed command is known to the game engine.
        /// </summary>
        /// <param name="cmd">It takes a single string command, that gets evaluated.</param>
        /// <returns>bool stating the result of the validation.</returns>
        public bool ValidateCommand(string cmd)
        {
            if (MovesCount % 2 == 0)
            {
                return validKingInputs.Contains(cmd);
            }
            else
            {
                char startLetter = cmd[0];
                switch (startLetter)
                {
                    case 'A':
                        return validAPawnInputs.Contains(cmd);
                    case 'B':
                        return validBPawnInputs.Contains(cmd);
                    case 'C':
                        return validCPawnInputs.Contains(cmd);
                    case 'D':
                        return validDPawnInputs.Contains(cmd);

                    default:
                        return false;
                }
            }
        }
    }
}
