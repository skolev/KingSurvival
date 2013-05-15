using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvivalGame.Common
{
    public class BasicGame
    {
        protected int[,] edgesOfTheGameField = 
        {
            { 2, 4 }, { 2, 18 }, { 9, 4 }, { 9, 18 }
        };

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
