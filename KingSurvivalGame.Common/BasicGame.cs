using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvivalGame.Common
{
    public class BasicGame
    {
        protected int[,] edges = 
        {
            { 2, 4 }, { 2, 18 }, { 9, 4 }, { 9, 18 }
        };

        public int Counter { get; set; }

        protected string[] validKingInputs = { "KUL", "KUR", "KDL", "KDR" };

        protected string[] validAPawnInputs = { "ADL", "ADR" };

        protected string[] validBPawnInputs = { "BDL", "BDR" };

        protected string[] validCPawnInputs = { "CDL", "CDR" };

        protected string[] validDPawnInputs = { "DDL", "DDR" };

        protected bool CheckIfInBoard(int[] positionCoodinates)
        {
            int positonRow = positionCoodinates[0];
            bool isRowInBoard = (positonRow >= edges[0, 0]) && (positonRow <= edges[3, 0]);
            int positonCol = positionCoodinates[1];
            bool isColInBoard = (positonCol >= edges[0, 1]) && (positonCol <= edges[3, 1]);
            
            return isRowInBoard && isColInBoard;
        }

        protected bool ValidateCommand(string checkedString)
        {
            if (Counter % 2 == 0)
            {
                return validKingInputs.Contains(checkedString);
            }
            else
            {
                char startLetter = checkedString[0];
                switch (startLetter)
                {
                    case 'A':
                        return validAPawnInputs.Contains(checkedString);
                    case 'B':
                        return validBPawnInputs.Contains(checkedString);
                    case 'C':
                        return validCPawnInputs.Contains(checkedString);
                    case 'D':
                        return validDPawnInputs.Contains(checkedString);

                    default:
                        return false;
                }
            }
        }
    }
}
