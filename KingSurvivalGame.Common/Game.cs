using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvivalGame.Common
{
    public class Game : BasicGame
    {
        public bool KingIsOnTheMove { get { return MovesCount % 2 == 0; } }
        public int KingTurns { get { return MovesCount / 2; } }
        public bool GameIsFinished { get; protected set; }

        protected char[,] field = 
        {
            { 'U', 'L', ' ', ' ', '0', ' ', '1', ' ', '2', ' ', '3', ' ', '4', ' ', '5', ' ', '6', ' ', '7', ' ', ' ', 'U', 'R' },
            { ' ', ' ', ' ', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', ' ', ' ', ' ' },
            { '0', ' ', '|', ' ', 'A', ' ', ' ', ' ', 'B', ' ', ' ', ' ', 'C', ' ', ' ', ' ', 'D', ' ', ' ', ' ', '|', ' ', '0' },
            { '1', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '1' },
            { '2', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '2' },
            { '3', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '3' },
            { '4', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '4' },
            { '5', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '5' },
            { '6', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '6' },
            { '7', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'K', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '7' },
            { ' ', ' ', '|', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '|', ' ', ' ' },
            { 'D', 'L', ' ', ' ', '0', ' ', '1', ' ', '2', ' ', '3', ' ', '4', ' ', '5', ' ', '6', ' ', '7', ' ', ' ', 'D', 'R' },
        };

        public int KingYPosition { get; protected set; }

        protected int[,] pawnsPositions = 
        {
            { 2, 4 }, { 2, 8 }, { 2, 12 }, { 2, 16 }
        };

        protected int[] startingPositionKing = { 9, 10 };

        protected bool[,] pawnsMoves = 
        {
            { true, true }, { true, true }, { true, true }, { true, true }
        };

        protected bool[] kingMoves = { true, true, true, true };

        protected int[] GetPawnDestination(int[] currentCoordinates, char checkDirection, char currentPawn)
        {
            int[] displasmentDownLeft = { 1, -2 };
            int[] displasmentDownRight = { 1, 2 };
            
            int[] newCoords = new int[2];
            
            if (checkDirection == 'L')
            {
                newCoords[0] = currentCoordinates[0] + displasmentDownLeft[0];
                newCoords[1] = currentCoordinates[1] + displasmentDownLeft[1];
                
                if (CheckIfInBoard(newCoords) && field[newCoords[0], newCoords[1]] == ' ')
                {
                    char sign = field[currentCoordinates[0], currentCoordinates[1]];
                    field[currentCoordinates[0], currentCoordinates[1]] = ' ';
                    field[newCoords[0], newCoords[1]] = sign;
                    MovesCount++;
                   
                    switch (currentPawn)
                    {
                        case 'A':
                            pawnsMoves[0, 0] = true;
                            pawnsMoves[0, 1] = true;
                            break;
                        case 'B':
                            pawnsMoves[1, 0] = true;
                            pawnsMoves[1, 1] = true;
                            break;
                        case 'C':
                            pawnsMoves[2, 0] = true;
                            pawnsMoves[2, 1] = true;
                            break;
                        case 'D':
                            pawnsMoves[3, 0] = true;
                            pawnsMoves[3, 1] = true;
                            break;
                        default:
                            throw new ArgumentException("No such pawn!");
                    }

                    return newCoords;
                }
                else
                {
                    bool allAreFalse = true;

                    switch (currentPawn)
                    {
                        case 'A':
                            pawnsMoves[0, 0] = false;
                            break;
                        case 'B':
                            pawnsMoves[1, 0] = false;
                            break;
                        case 'C':
                            pawnsMoves[2, 0] = false;
                            break;
                        case 'D':
                            pawnsMoves[3, 0] = false;
                            break;
                        default:
                            throw new ArgumentException(string.Format("No such pawn '{0}'!", currentPawn));
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (pawnsMoves[i, j] == true)
                            {
                                allAreFalse = false;
                                break;
                            }
                        }
                    }

                    if (allAreFalse)
                    {
                        GameIsFinished = true;
                    }

                    return null;
                }
            }
            else
            {
                newCoords[0] = currentCoordinates[0] + displasmentDownRight[0];
                newCoords[1] = currentCoordinates[1] + displasmentDownRight[1];
                
                if (CheckIfInBoard(newCoords) && field[newCoords[0], newCoords[1]] == ' ')
                {
                    char sign = field[currentCoordinates[0], currentCoordinates[1]];
                    field[currentCoordinates[0], currentCoordinates[1]] = ' ';
                    field[newCoords[0], newCoords[1]] = sign;
                    MovesCount++;
                    switch (currentPawn)
                    {
                        case 'A':
                            pawnsMoves[0, 0] = true;
                            pawnsMoves[0, 1] = true;
                            break;

                        case 'B':
                            pawnsMoves[1, 0] = true;
                            pawnsMoves[1, 1] = true;
                            break;

                        case 'C':
                            pawnsMoves[2, 0] = true;
                            pawnsMoves[2, 1] = true;
                            break;

                        case 'D':
                            pawnsMoves[3, 0] = true;
                            pawnsMoves[3, 1] = true;
                            break;

                        default:
                            Console.WriteLine("ERROR!");
                            break;
                    }
                    return newCoords;
                }
                else
                {
                    bool allAreFalse = true;
                    switch (currentPawn)
                    {
                        case 'A':
                            pawnsMoves[0, 1] = false;
                            break;
                        case 'B':
                            pawnsMoves[1, 1] = false;
                            break;
                        case 'C':
                            pawnsMoves[2, 1] = false;
                            break;
                        case 'D':
                            pawnsMoves[3, 1] = false;
                            break;
                        default:
                            Console.WriteLine("ERROR!");
                            break;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (pawnsMoves[i, j] == true)
                            {
                                allAreFalse = false;
                            }
                        }
                    }

                    if (allAreFalse)
                    {
                        GameIsFinished = true;
                    }
                    return null;
                }
            }
        }

        protected int[] checkNextKingPosition(int[] currentCoordinates, char firstDirection, char secondDirection)
        {
            int[] displasmentDownLeft = { 1, -2 };
            int[] displasmentDownRight = { 1, 2 };
            int[] displasmentUpLeft = { -1, -2 };
            int[] displasmentUpRight = { -1, 2 };
            int[] newCoords = new int[2];

            if (firstDirection == 'U')
            {
                if (secondDirection == 'L')
                {
                    newCoords[0] = currentCoordinates[0] + displasmentUpLeft[0];
                    newCoords[1] = currentCoordinates[1] + displasmentUpLeft[1];
                    if (CheckIfInBoard(newCoords) && field[newCoords[0], newCoords[1]] == ' ')
                    {
                        char sign = field[currentCoordinates[0], currentCoordinates[1]];
                        field[currentCoordinates[0], currentCoordinates[1]] = ' ';
                        field[newCoords[0], newCoords[1]] = sign;
                        MovesCount++;
                        for (int i = 0; i < 4; i++)
                        {
                            kingMoves[i] = true;
                        }
                        KingYPosition = newCoords[0];
                        //CheckIfKingExited();
                        return newCoords;
                    }
                    else
                    {
                        kingMoves[0] = false;
                        bool allAreFalse = true;
                        for (int i = 0; i < 4; i++)
                        {
                            if (kingMoves[i] == true)
                            {
                                allAreFalse = false;
                            }
                        }
                        if (allAreFalse)
                        {
                            GameIsFinished = true;
                            Console.WriteLine("King loses!");
                            return null;
                        }
                        Console.WriteLine("You can't go in this direction! ");
                        return null;
                    }
                }
                else
                {
                    newCoords[0] = currentCoordinates[0] + displasmentUpRight[0];
                    newCoords[1] = currentCoordinates[1] + displasmentUpRight[1];
                    if (CheckIfInBoard(newCoords) && field[newCoords[0], newCoords[1]] == ' ')
                    {
                        char sign = field[currentCoordinates[0], currentCoordinates[1]];
                        field[currentCoordinates[0], currentCoordinates[1]] = ' ';
                        field[newCoords[0], newCoords[1]] = sign;
                        MovesCount++;
                        for (int i = 0; i < 4; i++)
                        {
                            kingMoves[i] = true;
                        }
                        KingYPosition = newCoords[0];
                        //CheckIfKingExited();
                        return newCoords;
                    }
                    else
                    {
                        kingMoves[1] = false;
                        bool allAreFalse = true;
                        for (int i = 0; i < 4; i++)
                        {
                            if (kingMoves[i] == true)
                            {
                                allAreFalse = false;
                            }
                        }
                        if (allAreFalse)
                        {
                            GameIsFinished = true;
                            Console.WriteLine("King loses!");
                            return null;
                        }
                        Console.WriteLine("You can't go in this direction! ");
                        return null;
                    }
                }
            }
            else
            {
                if (secondDirection == 'L')
                {
                    newCoords[0] = currentCoordinates[0] + displasmentDownLeft[0];
                    newCoords[1] = currentCoordinates[1] + displasmentDownLeft[1];
                    if (CheckIfInBoard(newCoords) && field[newCoords[0], newCoords[1]] == ' ')
                    {
                        char sign = field[currentCoordinates[0], currentCoordinates[1]];
                        field[currentCoordinates[0], currentCoordinates[1]] = ' ';
                        field[newCoords[0], newCoords[1]] = sign;
                        MovesCount++;
                        for (int i = 0; i < 4; i++)
                        {
                            kingMoves[i] = true;
                        }
                        KingYPosition = newCoords[0];
                        //CheckIfKingExited();
                        return newCoords;
                    }
                    else
                    {
                        kingMoves[2] = false;
                        bool allAreFalse = true;
                        for (int i = 0; i < 4; i++)
                        {
                            if (kingMoves[i] == true)
                            {
                                allAreFalse = false;
                            }
                        }
                        if (allAreFalse)
                        {
                            GameIsFinished = true;
                            Console.WriteLine("King loses!");
                            return null;
                        }
                        Console.WriteLine("You can't go in this direction! ");
                        return null;
                    }
                }
                else
                {
                    newCoords[0] = currentCoordinates[0] + displasmentDownRight[0];
                    newCoords[1] = currentCoordinates[1] + displasmentDownRight[1];
                    if (CheckIfInBoard(newCoords) && field[newCoords[0], newCoords[1]] == ' ')
                    {
                        char sign = field[currentCoordinates[0], currentCoordinates[1]];
                        field[currentCoordinates[0], currentCoordinates[1]] = ' ';
                        field[newCoords[0], newCoords[1]] = sign;
                        MovesCount++;
                        for (int i = 0; i < 4; i++)
                        {
                            kingMoves[i] = true;
                        }
                        KingYPosition = newCoords[0];
                        //CheckIfKingExited();
                        return newCoords;
                    }
                    else
                    {
                        kingMoves[3] = false;
                        bool allAreFalse = true;
                        for (int i = 0; i < 4; i++)
                        {
                            if (kingMoves[i] == true)
                            {
                                allAreFalse = false;
                            }
                        }
                        if (allAreFalse)
                        {
                            GameIsFinished = true;
                            Console.WriteLine("King loses!");
                            return null;
                        }
                        Console.WriteLine("You can't go in this direction! ");
                        return null;
                    }
                }
                // checkForKingExit();
            }
        }

        public bool CheckIfKingExited()
        {
            if (KingYPosition == 2)
            {
                return true;
            }
            return false;
        }

        public string GetGridAsString()
        {
            StringBuilder consoleOutput = new StringBuilder();
            consoleOutput.AppendLine();

            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    int[] coordinates = { row, col };
                    bool isCellIn = CheckIfInBoard(coordinates);
                    char currentCellContent = field[row, col];

                    if (currentCellContent == ' ' && isCellIn)
                    {
                        if (row % 2 == 0)
                        {
                            if (col % 2 == 0)
                            {
                                consoleOutput.Append('+');
                            }
                            else
                            {
                                consoleOutput.Append('-');
                            }
                        }
                        else
                        {
                            if (col % 2 == 0)
                            {
                                consoleOutput.Append('-');
                            }
                            else
                            {
                                consoleOutput.Append('+');
                            }
                        }
                    }
                    else
                    {
                        consoleOutput.Append(currentCellContent);
                    }
                }
                consoleOutput.AppendLine();
            }
            consoleOutput.AppendLine();

            return consoleOutput.ToString();
        }

        private bool PlayPawnMove(char figure, char direction)
        {
            int unitNumber = ((int)figure) - 65;
            int[] oldCoordinates = new int[2];
            int[] coords = new int[2];

            oldCoordinates[0] = pawnsPositions[unitNumber, 0];
            oldCoordinates[1] = pawnsPositions[unitNumber, 1];

            coords = GetPawnDestination(oldCoordinates, direction, figure);
            
            if (coords != null)
            {
                pawnsPositions[unitNumber, 0] = coords[0];
                pawnsPositions[unitNumber, 1] = coords[1];
                return true;
            }

            return false;
        }

        private bool PlayKingMove(char yAxisDirection, char xAxisDirection)
        {
            int[] oldCoordinates = new int[2];
            int[] coords = new int[2];
            oldCoordinates[0] = startingPositionKing[0];
            oldCoordinates[1] = startingPositionKing[1];
            coords = checkNextKingPosition(oldCoordinates, yAxisDirection, xAxisDirection);

            if (coords != null)
            {
                startingPositionKing[0] = coords[0];
                startingPositionKing[1] = coords[1];
                return true;
            }

            return false;
        }

        public bool ExecuteCommand(string cmd)
        {
            char startLetter = cmd[0];
            int[] oldCoordinates = new int[2];
            int[] coords = new int[2];

            if (startLetter == 'K')
            {
                return PlayKingMove(cmd[1], cmd[2]);
            }
            else if (startLetter == 'A' || startLetter == 'B' || startLetter == 'C' || startLetter == 'D')
            {
                return PlayPawnMove(startLetter, cmd[2]);
            }
            else
            {
                throw new ArgumentException("Attempting to move a non existing figure!");
            }


        }
    }
}
