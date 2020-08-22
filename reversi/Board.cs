using System;
using System.IO;
using System.Linq;

namespace reversi
{
    public class Board
    {
        public static char[,] MakeBoard(int size, bool load)
        {
            char[,] boardMatrix = new char[size, size];
            // populate board matrix from save.txt
            if (load == true)
            { 
                string [] array = File.ReadAllLines("save.txt");
                string savedstring = string.Join("", array);
                char[] charArray = savedstring.ToCharArray();

                int i = 0;
                while (i < (size * size))
                {
                    for (int y = 0; y < size; y++)
                    {
                        for (int x = 0; x < size; x++)
                        {
                            boardMatrix[x, y] = charArray[i];
                            i++;
                        }
                    }
                }
                Console.WriteLine("Board successfully loaded.");
                return boardMatrix;
            }
            // populate board matrix with starting positions
            else
            {
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        // populate X chars
                        if ((x == ((size/2)-1) & y == ((size / 2) - 1)) || (x == (size / 2) & y == (size / 2)))
                        {
                            boardMatrix[x, y] = 'X';
                        }
                        // populate O chars
                        else if ((x == ((size / 2) - 1) & y == (size / 2)) || (x == (size / 2) & y == ((size / 2) - 1)))
                        {
                            boardMatrix[x, y] = 'O';
                        }
                        // populate empty space chars
                        else boardMatrix[x, y] = ' ';
                    }
                }
                Console.WriteLine("Board successfully created.");
                return boardMatrix;
            }  
        }
        public static void DrawMap(int size)
        {
            // render map of all board positions for reference when placing moves
            Console.WriteLine("\nHere is a map of all the potential board positions. When placing a legal move, enter the number coordinate of where you would like to place your piece:\n");
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.Write(string.Format("|{0}{1}", x, y));
                    if (x == size - 1)
                    {
                        Console.Write("|\n");
                    }
                }
            }
        }
    }
}
