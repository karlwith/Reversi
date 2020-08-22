using System;
using System.Collections.Generic;
using System.Text;

namespace reversi
{
    class EndOfGame
    {
        public static void Score(char [,] boardMatrix)
        {
            // calculate number of each char on the board
            int scorex = 0;
            int scoreo = 0;
            foreach (char square in boardMatrix)
            {
                if (square == 'X')
                {
                    scorex++;
                }
                if (square == 'O')
                {
                    scoreo++;
                }
            }
            // print the score and winner
            Console.WriteLine("\nX had {0} square(s) and O had {1} square(s).", scorex, scoreo);
            if (scorex < scoreo)
            {
                Console.WriteLine("The player using O won.");
            }
            else
            {
                Console.WriteLine("The player using X won.");
            }
            // prompt to loop back and start a new game / load previous saved game
            Console.WriteLine("To start a new game enter 'new', otherwise 'exit' to finish:");
            string req = Console.ReadLine();
            if (req == "new")
            {
                GameCore.GameStart();
            }
            if (req == "exit")
            {
                Environment.Exit(1);
            }
        }
    }
}
