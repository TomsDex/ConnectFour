﻿namespace C4.Game;

public class Player(bool isPlayerOne)
{
    private bool IsPlayerOne { get; } = isPlayerOne;


    /// <summary>
    /// Processes and returns user input
    /// </summary>
    /// <returns>The player's selected column if it is a valid input</returns>
    internal static byte PlayerTurn() 
    {
        return ValidUserInput(); 
    }


    /// <summary>
    /// Ensures the user has entered a valid column number
    /// or has selected u to undo
    /// </summary>
    /// <returns>The column number, or 18 if undo</returns>
    private static byte ValidUserInput()
    {
        while (true)
        {
            var keyInfo = Console.ReadKey();
            var input = keyInfo.KeyChar;

            if (input.ToString() == "u") { return 18; } //If user presses U signal an undo

            //Otherwise
            if (char.IsDigit(input)) //Checks that input is digit
            {
                var choice = (byte)(input - '0'); //Converts char to byte
                if (choice <= 6) return choice; //Returns if valid column number
                //choice is never less than zero
            }
            //otherwise prompt again
            Console.WriteLine("Please enter a column between 0 and 6!");
        }
    }


    /// <summary>
    /// Prompts user to input column number
    /// </summary>
    internal void OutputPlayerInputPrompt(bool canUndo)
    {
        if (IsPlayerOne)
        {
            Console.Write("Player 1 (");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("), enter your column!");

            if (!canUndo) return;

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

            if (!canUndo) return;

            Console.Write("Or, Player 1 (");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("), press U to undo!");
        }
    }
}