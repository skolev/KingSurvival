using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvivalGame.Common
{
    /// <summary>
    /// Main game logic is present here.
    /// </summary>
    public class Game : BasicGame
    {
        // SERVES AS GAME FIELD
        protected char[,] gameField = 
        {
            { 'U', 'L', ' ', '0', '1', '2', '3', '4', '5', '6', '7', ' ', 'U', 'R' },
            { ' ', ' ', ' ', '_', '_', '_', '_', '_', '_', '_', '_', ' ', ' ', ' ' },
            { '0', ' ', '|', 'A', ' ', 'B', ' ', 'C', ' ', 'D', ' ', '|', ' ', '0' },
            { '1', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '1' },
            { '2', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '2' },
            { '3', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '3' },
            { '4', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '4' },
            { '5', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '5' },
            { '6', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|', ' ', '6' },
            { '7', ' ', '|', ' ', ' ', ' ', 'K', ' ', ' ', ' ', ' ', '|', ' ', '7' },
            { ' ', ' ', '|', '_', '_', '_', '_', '_', '_', '_', '_', '|', ' ', ' ' },
            { 'D', 'L', ' ', '0', '1', '2', '3', '4', '5', '6', '7', ' ', 'D', 'R' },
        };

        // HOLDS THE POSITIN OF EVERY PAWN AT ANY TIME OF THE GAME
        protected int[,] pawnsPositions = 
        {
            { 2, 3 }, { 2, 5 }, { 2, 7 }, { 2, 9 }
        };

        // HOLDS THE POSITION OF THE KING AT ANY TIME OF THE GAME
        protected int[] kingPosition = { 9, 6 };
        
        // STATES IF WHICH DIRECTIONS CAN EACH OF THE PAWNS MOVE
        protected bool[,] pawnsAvaiableMoveDirections = 
        {
            { true, true }, { true, true }, { true, true }, { true, true }
        };

        // STATES IN WHICH DIRECTIONS CAN THE KING MOVE
        protected bool[] kingAvailableMoveDirections = { true, true, true, true };

        // ----------------------------------------------------------------------------------- METHODS

        // ----------------------------------------------------------------------- PUBLIC

        /// <summary>
        /// Checks if it is the king's turn and returns the result as bool
        /// </summary>
        public bool KingIsOnTheMove { get { return MovesCount % 2 == 0; } }

        /// <summary>
        /// Calculates the moves that the king has made for the entire game and returns them as an integer.
        /// </summary>
        public int KingTurns { get { return MovesCount >> 1; } }

        /// <summary>
        /// Keeps the current game state.
        /// </summary>
        public bool GameIsFinished { get; protected set; }

        /// <summary>
        /// Keeps the current king's position along the Y axis.
        /// </summary>
        public int KingYPosition { get; protected set; }

        /// <summary>
        /// Checks if all the pawns have reached a stuck state. If that is so - it marks the game as finished.
        /// </summary>
        /// <returns>Returns the state of all the pawns as bool</returns>
        public bool CheckIfAllPawnsAreStuck()
        {
            for (int pawn = 0; pawn < 4; pawn++)
            {
                for (int direction = 0; direction < 2; direction++)
                {
                    if (pawnsAvaiableMoveDirections[pawn, direction] == true)
                    {
                        return false;
                    }
                }
            }

            GameIsFinished = true;
            return true;
        }

        /// <summary>
        /// This method asks for the current king's y axis position and returns true if it is equal to the top most row.
        /// </summary>
        /// <returns>bool variable</returns>
        public bool CheckIfKingExited()
        {
            if (KingYPosition == 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Visits every cell of the game field and stores it's char value.
        /// </summary>
        /// <returns>string holding all the chars in the field</returns>
        public string GetGridAsString()
        {
            StringBuilder consoleOutput = new StringBuilder();
            consoleOutput.AppendLine();

            for (int row = 0; row < gameField.GetLength(0); row++)
            {
                for (int col = 0; col < gameField.GetLength(1); col++)
                {
                    int[] coordinates = { row, col };
                    bool isCellIn = CheckIfInBoard(coordinates);
                    char currentCellContent = gameField[row, col];

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

        /// <summary>
        /// Performs a single move with one of the pawns givan as parameter as well as the desired direction.
        /// </summary>
        /// <param name="figure">Determines which pawn is to be moved (A, B, C, D)</param>
        /// <param name="direction">Pawns always go down so there is only left(L) and right(R) directions available</param>
        /// <returns>bool if the move succeeded</returns>
        public bool PlayPawnMove(char figure, char direction)
        {
            int unitNumber = ((int)figure) - 65;
            int[] currentCoordinates = new int[2];
            int[] destination = new int[2];

            currentCoordinates[0] = pawnsPositions[unitNumber, 0];
            currentCoordinates[1] = pawnsPositions[unitNumber, 1];

            destination = GetPawnDestination(currentCoordinates, direction);

            if (ValidateDestination(destination))
            {
                pawnsPositions[unitNumber, 0] = destination[0];
                pawnsPositions[unitNumber, 1] = destination[1];

                char sign = gameField[currentCoordinates[0], currentCoordinates[1]];
                gameField[currentCoordinates[0], currentCoordinates[1]] = ' ';
                gameField[destination[0], destination[1]] = sign;

                EnablePawnMoves(figure);
                MovesCount++;

                return true;
            }
            else
            {
                DisablePawnMovesAtDirection(figure, direction);
            }

            return false;
        }

        /// <summary>
        /// Performs a single move with the king in a desired direction.
        /// </summary>
        /// <param name="yAxisDirection">States if the king will move up(U) or down(D)</param>
        /// <param name="xAxisDirection">States if the king will move left(L) or right(R)</param>
        /// <returns>bool if the move secceeded</returns>
        public bool PlayKingMove(char yAxisDirection, char xAxisDirection)
        {
            int[] currentCoordinates = new int[2];
            int[] destination = new int[2];

            currentCoordinates[0] = kingPosition[0];
            currentCoordinates[1] = kingPosition[1];

            destination = GetKingDestination(currentCoordinates, yAxisDirection, xAxisDirection);

            if (ValidateDestination(destination))
            {
                kingPosition[0] = destination[0];
                kingPosition[1] = destination[1];

                char sign = gameField[currentCoordinates[0], currentCoordinates[1]];
                gameField[currentCoordinates[0], currentCoordinates[1]] = ' ';
                gameField[destination[0], destination[1]] = sign;
                MovesCount++;

                for (int i = 0; i < 4; i++)
                {
                    kingAvailableMoveDirections[i] = true;
                }

                KingYPosition = destination[0];

                return true;
            }

            if (yAxisDirection == 'U')
            {
                if (xAxisDirection == 'L')
                {
                    kingAvailableMoveDirections[0] = false;
                }
                else
                {
                    kingAvailableMoveDirections[1] = false;
                }
            }
            else
            {
                if (xAxisDirection == 'L')
                {
                    kingAvailableMoveDirections[2] = false;
                }
                else
                {
                    kingAvailableMoveDirections[3] = false;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the king is still able to move. If he is not - it marks the geam as finished
        /// </summary>
        /// <returns>bool king's state regarding his ability to move</returns>
        public bool CheckIfKingIsStuck()
        {
            for (int i = 0; i < 4; i++)
            {
                if (kingAvailableMoveDirections[i] == true)
                {
                    return false;
                }
            }

            GameIsFinished = true;
            return true;
        }

        //--------------------------------------------------------------------- PRIVATE

        // RETURNS THE NEW DESTINATIONOF THE PAWN THAT GETS MOVED
        protected int[] GetPawnDestination(int[] currentCoordinates, char direction)
        {
            int[] displasmentDownLeft = { 1, -1 };
            int[] displasmentDownRight = { 1, 1 };

            int[] newCoords = new int[2];

            if (direction == 'L')
            {
                newCoords[0] = currentCoordinates[0] + displasmentDownLeft[0];
                newCoords[1] = currentCoordinates[1] + displasmentDownLeft[1];
            }
            else
            {
                newCoords[0] = currentCoordinates[0] + displasmentDownRight[0];
                newCoords[1] = currentCoordinates[1] + displasmentDownRight[1];
            }

            return newCoords;
        }

        // SETS A GIVEN DIRECTION AS INACCESSABLE FOR THE GIVES PAWN
        protected void DisablePawnMovesAtDirection(char currentPawn, char direction)
        {
            if (currentPawn > 'D')
            {
                throw new ArgumentException("No such pawn!");
            }

            int pawnNumber = ((int)currentPawn) - 65;
            pawnsAvaiableMoveDirections[0, direction == 'L' ? 0 : 1] = false;
        }

        // ENABLES A GIVEN PAWN TO MOVE IN ALL DIRECTIONS
        protected void EnablePawnMoves(char currentPawn)
        {
            if (currentPawn > 'D')
            {
                throw new ArgumentException("No such pawn!");
            }

            int pawnNumber = ((int)currentPawn) - 65;
            pawnsAvaiableMoveDirections[pawnNumber, 0] = true;
            pawnsAvaiableMoveDirections[pawnNumber, 1] = true;
        }

        // RETURNS THE NEW DESTINATION FOR THE KING
        protected int[] GetKingDestination(int[] currentCoordinates, char yAxisDirection, char xAxisDirection)
        {
            int[] displasmentDownLeft = { 1, -1 };
            int[] displasmentDownRight = { 1, 1 };
            int[] displasmentUpLeft = { -1, -1 };
            int[] displasmentUpRight = { -1, 1 };

            int[] newCoords = new int[2];

            if (yAxisDirection == 'U')
            {
                if (xAxisDirection == 'L')
                {
                    newCoords[0] = currentCoordinates[0] + displasmentUpLeft[0];
                    newCoords[1] = currentCoordinates[1] + displasmentUpLeft[1];
                }
                else
                {
                    newCoords[0] = currentCoordinates[0] + displasmentUpRight[0];
                    newCoords[1] = currentCoordinates[1] + displasmentUpRight[1];
                }
            }
            else
            {
                if (xAxisDirection == 'L')
                {
                    newCoords[0] = currentCoordinates[0] + displasmentDownLeft[0];
                    newCoords[1] = currentCoordinates[1] + displasmentDownLeft[1];
                }
                else
                {
                    newCoords[0] = currentCoordinates[0] + displasmentDownRight[0];
                    newCoords[1] = currentCoordinates[1] + displasmentDownRight[1];
                }
            }

            return newCoords;
        }

        // VALIDATES THE NEW DESTINATION FOR BOTH PAWNS AND THE KING
        private bool ValidateDestination(int[] destination)
        {
            if (CheckIfInBoard(destination) && gameField[destination[0], destination[1]] == ' ')
            {
                return true;
            }
            
            return false;
        }
    }
}
