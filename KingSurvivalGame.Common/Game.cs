using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvivalGame.Common
{
    public class Game : BasicGame
    {
        public bool KingIsOnTheMove { get { return Counter % 2 == 0; } }
        public int KingTurns { get { return Counter / 2; } }

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

        protected int[,] startingPositionsPawns = 
        {
            { 2, 4 }, { 2, 8 }, { 2, 12 }, { 2, 16 }
        };

        protected int[] startingPositionKing = { 9, 10 };

        protected bool[,] pawnsMoves = 
        {
            { true, true }, { true, true }, { true, true }, { true, true }
        };

        protected bool[] kingMoves = { true, true, true, true };

        protected int[] CheckPawnDestination(int[] currentCoordinates, char checkDirection, char currentPawn)
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
                    Counter++;
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
                    /* switch (currentPawn)
                    {
                    case 'A':
                    pawnExistingMoves[0, 0] = false;
                    break;
                    case 'B':
                    pawnExistingMoves[1, 0] = false;
                    break;
                    case 'C':
                    pawnExistingMoves[2, 0] = false;
                    break;
                    case 'D':
                    pawnExistingMoves[3, 0] = false;
                    break;
                    default:
                    Console.WriteLine("ERROR!");
                    break;
                    }*/
                    bool allAreFalse = true;
                    switch (currentPawn)
                    {
                        case 'A':
                            pawnsMoves[0, 0] = false;
                            /*for (int i = 0; i < 2; i++)
                            {
                            if (pawnExistingMoves[0,i] == true)
                            {
                            allAreFalse = false;
                            }
                            }*/
                            break;
                        case 'B':
                            pawnsMoves[1, 0] = false;
                            /*for (int i = 0; i < 2; i++)
                            {
                            if (pawnExistingMoves[1, i] == true)
                            {
                            allAreFalse = false;
                            }
                            }*/
                            break;
                        case 'C':
                            pawnsMoves[2, 0] = false;
                            /*for (int i = 0; i < 2; i++)
                            {
                            if (pawnExistingMoves[2, i] == true)
                            {
                            allAreFalse = false;
                            }
                            }*/
                            break;
                        case 'D':
                            pawnsMoves[3, 0] = false;
                            /*for (int i = 0; i < 2; i++)
                            {
                            if (pawnExistingMoves[3, i] == true)
                            {
                            allAreFalse = false;
                            }
                            }*/
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
                        Console.WriteLine("King wins!");
                        GameIsFinished = true;
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
                    Counter++;
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
                    /*   switch (currentPawn)
                    {
                    case 'A':
                    pawnExistingMoves[0, 1] = false;
                    break;
                    case 'B':
                    pawnExistingMoves[1, 1] = false;
                    break;
                    case 'C':
                    pawnExistingMoves[2, 1] = false;
                    break;
                    case 'D':
                    pawnExistingMoves[3, 1] = false;
                    break;
                    default:
                    Console.WriteLine("ERROR!");
                    break;
                    }*/
                    bool allAreFalse = true;
                    switch (currentPawn)
                    {
                        case 'A':
                            pawnsMoves[0, 1] = false;
                            /*for (int i = 0; i < 2; i++)
                            {
                            if (pawnExistingMoves[0, i] == true)
                            {
                            allAreFalse = false;
                            }
                            }*/
                            break;
                        case 'B':
                            pawnsMoves[1, 1] = false;
                            /*for (int i = 0; i < 2; i++)
                            {
                            if (pawnExistingMoves[1, i] == true)
                            {
                            allAreFalse = false;
                            }
                            }*/
                            break;
                        case 'C':
                            pawnsMoves[2, 1] = false;
                            /*for (int i = 0; i < 2; i++)
                            {
                            if (pawnExistingMoves[2, i] == true)
                            {
                            allAreFalse = false;
                            }
                            }*/
                            break;
                        case 'D':
                            pawnsMoves[3, 1] = false;
                            /*for (int i = 0; i < 2; i++)
                            {
                            if (pawnExistingMoves[3, i] == true)
                            {
                            allAreFalse = false;
                            }
                            }*/
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
                        Console.WriteLine("King wins!");
                        GameIsFinished = true;
                        return null;
                    }
                    Console.WriteLine("You can't go in this direction! ");
                    return null;
                }
            }
        }

        int[] checkNextKingPosition(int[] currentCoordinates, char firstDirection, char secondDirection)
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
                        Counter++;
                        for (int i = 0; i < 4; i++)
                        {
                            kingMoves[i] = true;
                        }
                        checkForKingExit(newCoords[0]);
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
                        Counter++;
                        for (int i = 0; i < 4; i++)
                        {
                            kingMoves[i] = true;
                        }
                        checkForKingExit(newCoords[0]);
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
                        Counter++;
                        for (int i = 0; i < 4; i++)
                        {
                            kingMoves[i] = true;
                        }
                        checkForKingExit(newCoords[0]);
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
                        Counter++;
                        for (int i = 0; i < 4; i++)
                        {
                            kingMoves[i] = true;
                        }
                        checkForKingExit(newCoords[0]);
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

        void checkForKingExit(int currentKingXAxe)
        {
            if (currentKingXAxe == 2)
            {
                Console.WriteLine("End!");
                Console.WriteLine("King wins in {0} moves!", Counter / 2);
                GameIsFinished = true;
            }
        }

        public string GetGridAsString()
        {
            StringBuilder consoleOutput = new StringBuilder();
            consoleOutput.AppendLine();
            //tuka kato cqlo si pravq nekvi shareniiki
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    int[] coordinates = { row, col };
                    bool isCellIn = CheckIfInBoard(coordinates);
                    
                    if (isCellIn)
                    {
                        if (row % 2 == 0)
                        {
                            if (col % 4 == 0)
                            {
                                consoleOutput.Append(field[row, col]);
                            }
                            else if (col % 2 == 0)
                            {
                                consoleOutput.Append(field[row, col]);
                            }
                            else if (col % 2 != 0)
                            {
                                consoleOutput.Append(field[row, col]);
                            }
                        }
                        else if (col % 4 == 0)
                        {
                            consoleOutput.Append(field[row, col]);
                        }
                        else if (col % 2 == 0)
                        {
                            consoleOutput.Append(field[row, col]);
                        }
                        else if (col % 2 != 0)
                        {
                            consoleOutput.Append(field[row, col]);
                        }
                    }
                    else
                    {
                        consoleOutput.Append(field[row, col]);
                    }

                }
                consoleOutput.AppendLine();
            }

            consoleOutput.AppendLine();

            return consoleOutput.ToString();
        }

        public bool ProcessCommand(string cmd)
        {
            char startLetter = cmd[0];
            switch (startLetter)
            {
                case 'A':

                    if (cmd[2] == 'L')
                    {
                        int[] oldCoordinates = new int[2];
                        oldCoordinates[0] = startingPositionsPawns[0, 0];

                        oldCoordinates[1] = startingPositionsPawns[0, 1];

                        int[] coords = new int[2];
                        coords = CheckPawnDestination(oldCoordinates, 'L', 'A');
                        if (coords != null)
                        {
                            startingPositionsPawns[0, 0] = coords[0];
                            startingPositionsPawns[0, 1] = coords[1];
                        }
                    }
                    else
                    {
                        //=='D'
                        int[] oldCoordinates = new int[2];
                        oldCoordinates[0] = startingPositionsPawns[0, 0];

                        oldCoordinates[1] = startingPositionsPawns[0, 1];
                        int[] coords = new int[2];

                        coords = CheckPawnDestination(oldCoordinates, 'R', 'A');
                        if (coords != null)
                        {
                            startingPositionsPawns[0, 0] = coords[0];

                            startingPositionsPawns[0, 1] = coords[1];
                        }
                    }
                    return true;

                case 'B':
                    if (cmd[2] == 'L')
                    {
                        int[] oldCoordinates = new int[2];
                        oldCoordinates[0] = startingPositionsPawns[1, 0];
                        oldCoordinates[1] = startingPositionsPawns[1, 1];

                        int[] coords = new int[2];

                        coords = CheckPawnDestination(oldCoordinates, 'L', 'B');
                        if (coords != null)
                        {
                            startingPositionsPawns[1, 0] = coords[0];

                            startingPositionsPawns[1, 1] = coords[1];
                        }
                    }
                    else
                    {
                        //=='D'
                        int[] oldCoordinates = new int[2];

                        oldCoordinates[0] = startingPositionsPawns[1, 0];

                        oldCoordinates[1] = startingPositionsPawns[1, 1];

                        int[] coords = new int[2];
                        coords = CheckPawnDestination(oldCoordinates, 'R', 'B');
                        if (coords != null)
                        {
                            startingPositionsPawns[1, 0] = coords[0];

                            startingPositionsPawns[1, 1] = coords[1];
                        }
                    }
                    return true;

                case 'C':
                    if (cmd[2] == 'L')
                    {
                        int[] oldCoordinates = new int[2];
                        oldCoordinates[0] = startingPositionsPawns[2, 0];

                        oldCoordinates[1] = startingPositionsPawns[2, 1];
                        int[] coords = new int[2];
                        coords = CheckPawnDestination(oldCoordinates, 'L', 'C');
                        if (coords != null)
                        {
                            startingPositionsPawns[2, 0] = coords[0];
                            startingPositionsPawns[2, 1] = coords[1];
                        }
                    }
                    else
                    {
                        //=='D'
                        int[] oldCoordinates = new int[2];
                        oldCoordinates[0] = startingPositionsPawns[2, 0];
                        oldCoordinates[1] = startingPositionsPawns[2, 1];
                        int[] coords = new int[2];
                        coords = CheckPawnDestination(oldCoordinates, 'R', 'C');
                        if (coords != null)
                        {
                            startingPositionsPawns[1, 0] = coords[0];
                            startingPositionsPawns[1, 1] = coords[1];
                        }
                    }
                    return true;

                case 'D':
                    if (cmd[2] == 'L')
                    {
                        int[] oldCoordinates = new int[2];
                        oldCoordinates[0] = startingPositionsPawns[3, 0];
                        oldCoordinates[1] = startingPositionsPawns[3, 1];
                        int[] coords = new int[2];
                        coords = CheckPawnDestination(oldCoordinates, 'L', 'D');
                        if (coords != null)
                        {
                            startingPositionsPawns[3, 0] = coords[0];
                            startingPositionsPawns[3, 1] = coords[1];
                        }
                    }
                    else
                    {
                        //=='D'
                        int[] oldCoordinates = new int[2];
                        oldCoordinates[0] = startingPositionsPawns[3, 0];
                        oldCoordinates[1] = startingPositionsPawns[3, 1];
                        int[] coords = new int[2];
                        coords = CheckPawnDestination(oldCoordinates, 'R', 'D');
                        if (coords != null)
                        {
                            startingPositionsPawns[3, 0] = coords[0];
                            startingPositionsPawns[3, 1] = coords[1];
                        }
                    }
                    return true;

                case 'K':
                    if (cmd[1] == 'U')
                    {
                        if (cmd[2] == 'L')
                        {
                            int[] oldCoordinates = new int[2];
                            oldCoordinates[0] = startingPositionKing[0];
                            oldCoordinates[1] = startingPositionKing[1];
                            int[] coords = new int[2];
                            coords = checkNextKingPosition(oldCoordinates, 'U', 'L');
                            if (coords != null)
                            {
                                startingPositionKing[0] = coords[0];
                                startingPositionKing[1] = coords[1];
                            }
                        }
                        else
                        {
                            int[] oldCoordinates = new int[2];
                            oldCoordinates[0] = startingPositionKing[0];
                            oldCoordinates[1] = startingPositionKing[1];
                            int[] coords = new int[2];
                            coords = checkNextKingPosition(oldCoordinates, 'U', 'R');
                            if (coords != null)
                            {
                                startingPositionKing[0] = coords[0];
                                startingPositionKing[1] = coords[1];
                            }
                        }
                        return true;
                    }
                    else
                    {
                        //=KD_
                        if (cmd[2] == 'L')
                        {
                            int[] oldCoordinates = new int[2];
                            oldCoordinates[0] = startingPositionKing[0];
                            oldCoordinates[1] = startingPositionKing[1];
                            int[] coords = new int[2];
                            coords = checkNextKingPosition(oldCoordinates, 'D', 'L');
                            if (coords != null)
                            {
                                startingPositionKing[0] = coords[0];
                                startingPositionKing[1] = coords[1];
                            }
                        }
                        else
                        {
                            //==KDD
                            int[] oldCoordinates = new int[2];
                            oldCoordinates[0] = startingPositionKing[0];
                            oldCoordinates[1] = startingPositionKing[1];
                            int[] coords = new int[2];
                            coords = checkNextKingPosition(oldCoordinates, 'D', 'R');
                            if (coords != null)
                            {
                                startingPositionKing[0] = coords[0];
                                startingPositionKing[1] = coords[1];
                            }
                        }
                        return true;
                    }
                default:
                    //  TODO better solution for here...   Console.WriteLine("Sorry, there are some errors, but I can't tell you anything! You broked my program!");
                    return false;
            }

        }
    }
}
