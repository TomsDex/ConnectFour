namespace C4_Game
{ 
    public class Player
    {
        public bool IsPlayerOne { get; set; }

        public Player(bool isPlayerOne)
        {
            IsPlayerOne = DetermineIfPlayerOne(isPlayerOne);
        }

        private static bool DetermineIfPlayerOne(bool isPlayerOne) { return isPlayerOne; }

        /// <summary>
        /// Processes and returns user input
        /// </summary>
        /// <returns>The player's selected column if it is a valid input</returns>
        internal byte PlayerTurn() 
        {
            return ValidUserInput(); 
        }

        /// <summary>
        /// Ensures the user has entered a valid column number
        /// or has selected u to undo
        /// </summary>
        /// <returns>The column number, or 18 if undo</returns>
        internal static byte ValidUserInput()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char input = keyInfo.KeyChar;

                if (input.ToString() == "u") { return 18; } //If user presses U signal an undo

                //Otherwise
                if (char.IsDigit(input)) //Checks that input is digit
                {
                    byte choice = (byte)(input - '0'); //Converts char to byte
                    if (choice >= 0 && choice <= 6) return choice; //Returns if valid column number
                }
                //otherwise reprompt
                Console.WriteLine("Please enter a column between 0 and 6!");
            }
        }

        /// <summary>
        /// Prompts user to input column number
        /// </summary>
        internal void OutputPlayerInputPrompt()
        {
            if (IsPlayerOne)
            {
                Console.Write("Player 1 (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("), enter your column!");

                Console.Write("Or, Player 2 (");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("O");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("), press U to undo!");
            }
            else
            {
                Console.Write("Player 2 (");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("O");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("), enter your column!");

                Console.Write("Or, Player 1 (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("), press U to undo!");
            }
        }
    }
}
