using System.Data.Common;

namespace C4_Game
{
    public class Game
    {
        public char[,] Board { get; set; }

        public Game()
        {
            Board = CreateEmptyBoard();
        }

        /// <summary>
        /// Creates a new blank board for creation of a new game
        /// </summary>
        private char[,] CreateEmptyBoard()
        {
            return new char[,]
            {          //layout of empty board
                { 'e', 'e', 'e', 'e', 'e', 'e', 'e' },
                { 'e', 'e', 'e', 'e', 'e', 'e', 'e' },
                { 'e', 'e', 'e', 'e', 'e', 'e', 'e' },
                { 'e', 'e', 'e', 'e', 'e', 'e', 'e' },
                { 'e', 'e', 'e', 'e', 'e', 'e', 'e' },
                { 'e', 'e', 'e', 'e', 'e', 'e', 'e' }
            };
        }

        /// <summary>
        /// Writes the board to the console
        /// </summary>
        private void OutputBoard()
        {
            Console.WriteLine(" 0 1 2 3 4 5 6"); //Column nos

            for (int row = 0; row < 6; row++) //For each row
            {
                Console.Write(row); 
                Console.Write("|"); //Left hand side

                for (int column = 0; column < 7; column++) //For each column
                {
                    if (Board[row, column] == 'e') Console.Write(" "); //If (row,column) is empty, write a space

                    else
                    {
                        if (Board[row, column] == 'X') Console.ForegroundColor = ConsoleColor.Red;
                        if (Board[row, column] == 'O') Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(Board[row, column]); //If (row,column) has a value, write the value
                        Console.ForegroundColor = ConsoleColor.White;
                    }


                    Console.Write("|"); //Border between spaces
                }
                Console.WriteLine(); //Go to next row
            }
        }

        /// <summary>
        /// Checks if the space below the parameters is empty
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns>False if the space below is not 'e' or is off the bottom of the board</returns>
        private bool SpaceBelowIsEmpty(byte row, byte column)
        {
            if (row == 5) return false; //Bottom row
            else if (Board[row + 1, column] == 'e') return true; //Space below is empty
            else return false; //Space below is not empty
        }

        /// <summary>
        /// Checks if the column is full
        /// </summary>
        /// <param name="column"></param>
        /// <returns>True if the character in row 0 is not 'e'</returns>
        private bool ColumnIsFull(byte column) { return Board[0, column] != 'e'; }

        /// <summary>
        /// Checks if whole board is full
        /// </summary>
        /// <returns>True if top row is full </returns>
        private bool WholeBoardIsFull()
        {
            return Board[0, 0] != 'e'
                && Board[0, 1] != 'e'
                && Board[0, 2] != 'e'
                && Board[0, 3] != 'e'
                && Board[0, 4] != 'e'
                && Board[0, 5] != 'e'
                && Board[0, 6] != 'e';
        }

        /// <summary>
        /// Contains input validation to check if the given coordinate is off the map.
        /// This is used during win logic checking to prevent crashes when checking 
        /// coordinates which are not on the board
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns>The char in the given position, otherwise 'e' if off the map</returns>
        private char ReturnPositionalChar(int row, int column)
        {
            try { return Board[row, column]; }
            catch (IndexOutOfRangeException) { return 'e'; } //Return empty if the given coordinate does not exist
        }

        /// <summary>
        /// Finds which row the last token in that column fell into
        /// </summary>
        /// <param name="column"></param>
        /// <returns>The row number</returns>
        private byte GetRowNumber(byte column)
        {
            byte row = 0;

            //Finds the lowest empty space in the column
            while (SpaceBelowIsEmpty(row, column)) row++;

            //row would remain at 0 if the top row was full or empty if the second row was full
            //Therefore if the selected position is empty, move down one row to the newly placed token
            //This prevents unnecessarly moving down when the top row is not full
            if (ReturnPositionalChar(row, column) == 'e') row++;

            return row;
        }

        /// <summary>
        /// Contains win logic
        /// </summary>
        /// <returns>True if a player has connected four tokens</returns>
        private bool PlayerHasWon(byte column)
        {
            byte row = GetRowNumber(column); //Get full coords of token last placed

            //Positions in relation to new token are as follows (8 is new token)
            //  0   1   2
            //  3  (8)  4
            //  5   6   7

            List<char> surroundings = new(8) //Checks all tokens around new input and stores them in list
            {
                ReturnPositionalChar(row - 1,    column - 1), // Checks 0
                ReturnPositionalChar(row - 1,    column),     // Checks 1
                ReturnPositionalChar(row - 1,    column + 1), // Checks 2
                ReturnPositionalChar(row,        column - 1), // Checks 3
                ReturnPositionalChar(row,        column + 1), // Checks 4
                ReturnPositionalChar(row + 1,    column - 1), // Checks 5
                ReturnPositionalChar(row + 1,    column),     // Checks 6
                ReturnPositionalChar(row + 1,    column + 1), // Checks 7
                ReturnPositionalChar(row,        column)      // Checks 8
            };

            //For each value in surroundings which is not the new token
            for (int i = 0; i < 8; i++)
            {
                //If there is a three in a row within the list (as shown above)
                if (surroundings[i] == surroundings[8] && surroundings[7 - i] == surroundings[8])
                {
                    switch (i) //Return true if any of the three in a row in the grid continue to be a four in a row
                    {
                        case 0:
                            if (ReturnPositionalChar(row - 2, column - 2) == surroundings[8] ||
                                    ReturnPositionalChar(row + 2, column + 2) == surroundings[8]) return true;
                            else break;

                        case 1:
                            if (ReturnPositionalChar(row - 2, column) == surroundings[8] ||
                                    ReturnPositionalChar(row + 2, column) == surroundings[8]) return true;
                            else break;

                        case 2:
                            if (ReturnPositionalChar(row - 2, column + 2) == surroundings[8] ||
                                    ReturnPositionalChar(row + 2, column - 2) == surroundings[8]) return true;
                            else break;

                        case 3:
                            if (ReturnPositionalChar(row, column - 2) == surroundings[8] ||
                                    ReturnPositionalChar(row, column + 2) == surroundings[8]) return true;
                            else break;
                    }
                }

                //If any specific surrounding token is the same as the new token
                if (surroundings[i] == surroundings[8])
                {
                    switch (i) //Return true if the two consecutive tokens in the same direction are also the same as the new token and the immediate token
                    {
                        case 0:
                            if (ReturnPositionalChar(row - 2, column - 2) == surroundings[8] &&
                                    ReturnPositionalChar(row - 3, column - 3) == surroundings[8]) return true;
                            else break;

                        case 1:
                            if (ReturnPositionalChar(row - 2, column) == surroundings[8] &&
                                    ReturnPositionalChar(row - 3, column) == surroundings[8]) return true;
                            else break;

                        case 2:
                            if (ReturnPositionalChar(row - 2, column + 2) == surroundings[8] &&
                                    ReturnPositionalChar(row - 3, column + 3) == surroundings[8]) return true;
                            else break;

                        case 3:
                            if (ReturnPositionalChar(row, column - 2) == surroundings[8] &&
                                    ReturnPositionalChar(row, column - 3) == surroundings[8]) return true;
                            else break;

                        case 4:
                            if (ReturnPositionalChar(row, column + 2) == surroundings[8] &&
                                    ReturnPositionalChar(row, column + 3) == surroundings[8]) return true;
                            else break;

                        case 5:
                            if (ReturnPositionalChar(row + 2, column - 2) == surroundings[8] &&
                                    ReturnPositionalChar(row + 3, column - 3) == surroundings[8]) return true;
                            else break;

                        case 6:
                            if (ReturnPositionalChar(row + 2, column) == surroundings[8] &&
                                    ReturnPositionalChar(row + 3, column) == surroundings[8]) return true;
                            else break;

                        case 7:
                            if (ReturnPositionalChar(row + 2, column + 2) == surroundings[8] &&
                                    ReturnPositionalChar(row + 3, column + 3) == surroundings[8]) return true;
                            else break;

                    }
                }
            }
            //Else no one has won yet
            return false;
        }

        /// <summary>
        /// Drops token, checks if the token has dropped to the bottom of the board and clears the console before rewriting board
        /// </summary>
        /// <param name="column"></param>
        /// <param name="isPlayerOne"></param>
        /// <returns>True if the column was not full</returns>
        private bool TokenHasDropped(byte column, bool isPlayerOne)
        {
            if (ColumnIsFull(column))
            {
                Console.WriteLine("This column is full!");
                return false;
            }
            else
            {
                byte row = 0;

                //Checks the row below
                while (SpaceBelowIsEmpty(row, column)) row++;

                //Places char if the space below is not empty
                if (isPlayerOne) { Board[row, column] = 'X'; }
                else { Board[row, column] = 'O'; }

                Console.Clear();
                OutputBoard();
                return true;
            }
        }

        /// <summary>
        /// Checks if a turn can be undone
        /// </summary>
        /// <returns>True if the turn before has not been an undo</returns>
        private bool UndoCanBePerformed(byte column)
        {
            if (column == 9 || column == 18) //If no token has been placed or last turn was an undo
            {
                return false;
            }
            else { return true; }
        }

        /// <summary>
        /// Contains logic to undo the previous turn
        /// </summary>
        private void UndoTurn(byte lastColumn)
        {
            byte row = GetRowNumber(lastColumn); //Find the row which the last placed token was put in
            Board[row, lastColumn] = 'e'; //Mark space as empty
        }

        /// <summary>
        /// Checks if any game ending condition has been met
        /// </summary>
        /// <returns>F for a full board, W for a win or C for a continue</returns>
        private char IsEndOfGame(byte column)
        {
            if (PlayerHasWon(column)) return 'W'; //first check for if player has won
            if (WholeBoardIsFull()) return 'F'; //then check if board is full
            return 'C'; //otherwise continue

        }

        /// <summary>
        /// Overall game logic
        /// </summary>
        internal void StartGame()
        {
            //Initial setup
            Console.WriteLine("Game start!");
            OutputBoard();

            Player playerOne = new(true);
            Player playerTwo = new(false);
            bool isPlayerOneTurn = true;
            char GameStatus = 'C'; //Continues game
            byte columnInput = 9; //Initialisation value
            byte previousColumnInput = 9; //Remembers last valid token input. Initialisation value

            while (GameStatus == 'C') //Turn cycle continues while no one has won
            {
                Player currentPlayer = isPlayerOneTurn ? playerOne : playerTwo; //Switches player depending on value of isPlayerOneTurn

                currentPlayer.OutputPlayerInputPrompt(UndoCanBePerformed(previousColumnInput));
                
                columnInput = currentPlayer.PlayerTurn();

                if (columnInput == 18) //Undo has been selected
                {
                    if (UndoCanBePerformed(previousColumnInput)) //Last turn was not an undo
                    {
                        UndoTurn(previousColumnInput);
                        Console.Clear();
                        OutputBoard();
                        Console.WriteLine("Turn has been undone!");
                        isPlayerOneTurn = !isPlayerOneTurn; //Alternates turn

                    }
                    else
                    {
                        Console.WriteLine("Nothing to undo!");
                    }
                }
                else 
                { 
                    if (!TokenHasDropped(columnInput, isPlayerOneTurn)) 
                    {
                        
                    }
                    else
                    {
                        isPlayerOneTurn = !isPlayerOneTurn; //Alternates turn
                    }
                }

                previousColumnInput = columnInput; //Remembers last valid column input
            }

            //If the last input was not an undo
            if (columnInput != 18)
            {
                //Check if anyone has won 
                GameStatus = IsEndOfGame(columnInput);
            }

            if (GameStatus == 'F') //Board is full
            {
                Console.WriteLine("The game has ended in a draw! The board is full.");
            }
            if (GameStatus == 'W') //Player has won
            {
                if (!isPlayerOneTurn) Console.WriteLine("Player 1 wins!"); //Output inverse because the value is switched before it is passed in
                else Console.WriteLine("Player 2 wins!");
            }

            //Replay option
            Console.WriteLine("Hit enter to play again!");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Game newGame = new Game();
                newGame.StartGame();
            }
        }        
    }
}
