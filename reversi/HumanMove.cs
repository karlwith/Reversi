using System;

namespace reversi
{
    public class HumanMove
    {
        public static char[,] LookAndPlace (int xcoord, int ycoord, char[,] boardMatrix, char activeplayer, char idleplayer)
        {
            bool loop = true;
            do
            {
                // look left
                for (int x = xcoord; x >= 0; x--)
                {
                    // look all positions to left for friendly char
                    if (boardMatrix[x, ycoord] == activeplayer)
                    {
                        // look 1 position to the left for an opposing char
                        if (boardMatrix[xcoord - 1, ycoord] == idleplayer)
                        {
                            int xstop = x;
                            // change positions
                            for (int j = xcoord; j > xstop; j--)
                            {
                                boardMatrix[j, ycoord] = activeplayer;
                            }
                            return boardMatrix;
                        }
                    }
                }
                // look right
                for (int x = xcoord; x < Math.Sqrt(boardMatrix.Length); x++)
                {
                    // look all positions to the right for friendly char
                    if (boardMatrix[x, ycoord] == activeplayer)
                    {
                        // look 1 position to the right for an opposing char
                        if (boardMatrix[xcoord + 1, ycoord] == idleplayer)
                        {
                            int xstop = x;
                            // change positions
                            for (int j = xcoord; j < xstop; j++)
                            {
                                boardMatrix[j, ycoord] = activeplayer;
                            }
                            return boardMatrix;
                        }
                    }
                }
                // look up
                for (int y = ycoord; y >= 0; y--)
                {
                    // look all positions above for friendly char
                    if (boardMatrix[xcoord, y] == activeplayer)
                    {
                        // look 1 position above for opposing char
                        if (boardMatrix[xcoord, ycoord - 1] == idleplayer)
                        {
                            int ystop = y;
                            // change positions
                            for (int j = ycoord; j > ystop; j--)
                            {
                                boardMatrix[xcoord, j] = activeplayer;
                            }
                            return boardMatrix;
                        }
                    }
                }
                // look down
                for (int y = ycoord; y < Math.Sqrt(boardMatrix.Length); y++)
                {
                    // look all positions below for friendly char
                    if (boardMatrix[xcoord, y] == activeplayer)
                    {
                        // look 1 position below for opposing char
                        if (boardMatrix[xcoord, ycoord + 1] == idleplayer)
                        {
                            int ystop = y;
                            // change positions
                            for (int j = ycoord; j < ystop; j++)
                            {
                                boardMatrix[xcoord, j] = activeplayer;
                            }
                            return boardMatrix;
                        }
                    }
                }
                Console.WriteLine("\nInvalid move.");
                Console.WriteLine("Enter a move that complies to the rules.");
                int placing = Convert.ToInt32(Console.ReadLine());
                xcoord = placing / 10;
                ycoord = placing % 10;
            } while (loop == true);
            return boardMatrix;
        }
    }
}
