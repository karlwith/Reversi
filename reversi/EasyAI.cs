using System;

namespace reversi
{
    public class EasyAI
    {
        public static char[,] MakeMove(char[,] boardMatrix, int size)
        {
            // loop through all board positions until valid move is found
            for (int xcoord = 0; xcoord < size; xcoord++)
            {
                for (int ycoord = 0; ycoord < size; ycoord++) 
                {
                    // look left
                    for (int x = xcoord; x > 0; x--)
                    {    
                        // look all positions to left for friendly char
                        if (boardMatrix[x, ycoord] == 'O')
                        {
                            // look 1 position to left for opposing char
                            if (boardMatrix[xcoord - 1, ycoord] == 'X')
                            {
                                int xstop = x;
                                // change positions
                                for (int j = xcoord; j > xstop; j--)
                                {
                                    boardMatrix[j, ycoord] = 'O';
                                }
                                Console.WriteLine("Computer entered a move at {0}{1}", xcoord, ycoord);
                                return boardMatrix;
                            }
                        }
                    }
                    // look right
                    for (int x = xcoord; x < Math.Sqrt(boardMatrix.Length); x++)
                    {
                        // look all positions to the right for friendly char
                        if (boardMatrix[x, ycoord] == 'O')
                        { 
                            // look 1 position to the right for opposing char
                            if (boardMatrix[xcoord + 1, ycoord] == 'X')
                            {
                                int xstop = x;
                                // change positions
                                for (int j = xcoord; j < xstop; j++)
                                {
                                    boardMatrix[j, ycoord] = 'O';
                                }
                                Console.WriteLine("Computer entered a move at {0}{1}", xcoord, ycoord);
                                return boardMatrix;
                            }       
                        }
                    }
                    // look up
                    for (int y = ycoord; y >= 0; y--)
                    {
                        // look all positions up for friendly char
                        if (boardMatrix[xcoord, y] == 'O')
                        { 
                            // look 1 position up for opposing char
                            if (boardMatrix[xcoord, ycoord - 1] == 'X')
                            {     
                                int ystop = y;
                                // change positions
                                for (int j = ycoord; j > ystop; j--)
                                {
                                    boardMatrix[xcoord, j] = 'O';
                                }
                                Console.WriteLine("Computer entered a move at {0}{1}", xcoord, ycoord);
                                return boardMatrix;
                            }
                        }
                    }
                    // look down
                    for (int y = ycoord; y < Math.Sqrt(boardMatrix.Length); y++)
                    {
                        // look all positions below for friendly char
                        if (boardMatrix[xcoord, y] == 'O')
                        { 
                            // look 1 position below for opposing char
                            if (boardMatrix[xcoord, ycoord + 1] == 'X')         
                            {
                                int ystop = y;
                                // change positions
                                for (int j = ycoord; j < ystop; j++)
                                {
                                    boardMatrix[xcoord, j] = 'O';
                                }
                                Console.WriteLine("Computer entered a move at {0}{1}", xcoord, ycoord);
                                return boardMatrix;
                            }
                        }    
                    }
                }               
            }
            // else end game
            EndOfGame.Score(boardMatrix);
            return boardMatrix;
        }    
    }
}
