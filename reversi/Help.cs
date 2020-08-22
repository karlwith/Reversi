using System;
using System.Collections.Generic;
using System.Text;

namespace reversi
{
    class Help
    {
        public static void PrintHelp(int size)
        {
            Board.DrawMap(size);
            Console.WriteLine("\nA legal move is one placed in the same axis as an existing square of the same character while also being adjacent to an oppositions character. Diagonal placing relations are not supported. This placing will change all of the squares in between to the players character.");
            Console.Write("The object of the game is to have the most squares covered with your character (X or O). When there are no more empty squares or valid moves the game is ended.");
            Console.Write("The state of the board can be saved and loaded.");
        }
    }
}
