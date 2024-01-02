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
        public byte PlayerTurn() 
        {
            return IsValidUserColumnInput(); 
        }

        /// <summary>
        /// Ensures the user has entered a valid column number 
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>The column number</returns>
        public static byte IsValidUserColumnInput()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char input = keyInfo.KeyChar;

                if (char.IsDigit(input)) //Checks that input is digit
                {
                    byte choice = (byte)(input - '0'); //Converts char to byte
                    if (choice >= 0 && choice <= 6) return choice; //Returns if valid column number
                }
                //otherwise reprompt
                Console.WriteLine("Please enter a column between 0 and 6!");
            }
        }

    }
}
