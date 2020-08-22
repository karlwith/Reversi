using System;
using System.IO;


namespace reversi
{
    public class GameCore
    {   
        public static void Main(string[] args)
        {
            Console.WriteLine(" ______    _______  __   __  _______  ______    _______  ___  ");
            Console.WriteLine("|    _ |  |       ||  | |  ||       ||    _ |  |       ||   | ");
            Console.WriteLine("|   | ||  |    ___||  |_|  ||    ___||   | ||  |  _____||   | ");
            Console.WriteLine("|   |_||_ |   |___ |       ||   |___ |   |_||_ | |_____ |   | ");
            Console.WriteLine("|    __  ||    ___||       ||    ___||    __  ||_____  ||   | ");
            Console.WriteLine("|   |  | ||   |___  |     | |   |___ |   |  | | _____| ||   | ");
            Console.WriteLine("| __|  |_||_______|  |___|  |_______||___|  |_||_______||___| ");
            GameStart();
        }
        
        public static void GameStart()
        {
            // determine size of board and new or loaded board state
            var (size, load) = BoardInit();
            // determing 1v1 or 1vcomputer
            string mode = PlayersInit();
            // print game rules 
            Help.PrintHelp(size); 
            // initialize board matrix
            char[,] boardMatrix = Board.MakeBoard(size, load);
            // start turn loops which includes options to exit and save
            TurnLoop(mode, size, boardMatrix);
        }

        public static (int, bool) BoardInit()
        {
            // initialize game state
            Console.WriteLine("\nTo start a new game enter 'new', otherwise to load a previously saved game, enter 'load':");
            string init = Console.ReadLine();
            int size;
            bool loop = true;
            bool load = true;
            while (loop == true)
            {
                // choose board size 
                if (init == "new")
                {
                    Console.WriteLine("Enter an even number between 4 and 8 inclusive for the board size. E.g. for an 8x8 board enter '8':");
                    size = Convert.ToInt32(Console.ReadLine());
                    load = false;
                    while (loop == true)
                    {
                        if (((size % 2) == 0) & (size <= 9) & (size >= 4))
                        {
                            return (size, load);
                        }
                        else
                        {
                            Console.WriteLine("I didn't quite get that. Enter a valid number:");
                            size = Convert.ToInt32(Console.ReadLine());
                        }
                    }
                }
                // load from save.txt
                else if (init == "load")
                {              
                    string[] lines = File.ReadAllLines("save.txt");
                    size = Convert.ToInt32(Math.Sqrt(lines.Length));
                    load = true;
                    return (size, load);
                }
                Console.WriteLine("I didn't quite get that. Enter 'new' or 'load':");
                init = Convert.ToString(Console.ReadLine());
            }
            size = 8;
            return (size, load);
        }
        public static string PlayersInit()
        {
            // set game mode
            Console.WriteLine("To play against another human enter 'h1', easy computer enter 'c1' or hard computer enter 'c2'.");
            string mode = Console.ReadLine();
            bool flag = true;
            do
            {
                if ((mode != "h1") & (mode != "c1") & (mode != "c2"))
                {
                    Console.WriteLine("I didn't quite get that. Enter a valid game mode.");
                    mode = Console.ReadLine();
                }
                else
                {
                    return mode;
                }
            } while (flag);
            return mode;
        }
        public static void TurnLoop(string mode, int size, char[,] boardMatrix)
        {
            // set player char arrays for looping though
            char[] activeplayer = new char[] { 'X', 'O' };
            char[] idleplayer = new char[] { 'O', 'X' };
            int[] playerid = new int[] { 1, 2 };
            bool gameover = false;

            // loop for 2 human players
            if (mode == "h1")
            {
                while (gameover == false)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        // render board map 
                        Board.DrawMap(size);
                        // render in board state
                        Console.WriteLine("\n");
                        for (int y = 0; y < size; y++)
                        {
                            for (int x = 0; x < size; x++)
                            {
                                Console.Write(string.Format("|{0}", boardMatrix[x, y]));
                                if (x == size - 1)
                                {
                                    Console.Write("|\n");
                                }
                            }
                        }
                        // prompt and handle user input
                        Console.WriteLine("\nPlayer {0}, your pieces are denoted by '{1}'. Enter a 2 digit co-ordinate to place, 'help' to view rules again, 'save' to save the current board state and 'end' to view the current score and finish:", playerid[i], activeplayer[i]);
                        string req = Console.ReadLine();
                        bool input = Int32.TryParse(req, out int placing);
                        // print game rules
                        if (req == "help")
                        {
                            Help.PrintHelp(size);
                        }
                        // save board matrix to txt file
                        else if (req == "save")
                        {
                            using (StreamWriter sr = new StreamWriter("save.txt"))
                            {
                                foreach (var item in boardMatrix)
                                {
                                    sr.WriteLine(item);
                                }
                            }
                            Console.WriteLine("The game has been saved to save.txt.");
                        }
                        // end game - display score
                        else if (req == "end")
                        {
                            EndOfGame.Score(boardMatrix);
                        }
                        // action move request if valid number input
                        else if (input)
                        {
                            int xcoord = placing / 10;
                            int ycoord = placing % 10;
                            boardMatrix = HumanMove.LookAndPlace(xcoord, ycoord, boardMatrix, activeplayer[i], idleplayer[i]);
                            // end of game test
                            int count = 0;
                            foreach (char square in boardMatrix)
                            {
                                if (square == ' ')
                                {
                                    count++;
                                }  
                            }
                            if (count == 0)
                            {
                                EndOfGame.Score(boardMatrix);
                            }
                        }
                        else
                        {
                            Console.Write("\nInput not recognised.");
                        }
                    }
                }    
            }
            // loop for 1 computer player, 1 human
            else if (mode == "c1")
            {
                while (gameover == false)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        // render in board state console
                        Console.WriteLine("\n");
                        for (int y = 0; y < size; y++)
                        {
                            for (int x = 0; x < size; x++)
                            {
                                Console.Write(string.Format("|{0}", boardMatrix[x, y]));
                                if (x == size - 1)
                                {
                                    Console.Write("|\n");
                                }
                            }
                        }
                        // prompt and handle human input
                        Console.WriteLine("\nPlayer 1, your pieces are denoted by 'X'. Enter a 2 digit co-ordinate to place, 'help' to view rules again, 'save' to save the current board state and 'end' to view the current score and finish:");
                        string req = Console.ReadLine();
                        bool input = Int32.TryParse(req, out int placing);
                        // print game rules
                        if (req == "help")
                        {
                            Help.PrintHelp(size);
                        }
                        // save board state to txt file
                        else if (req == "save")
                        {
                            using (StreamWriter sr = new StreamWriter("save.txt"))
                            {
                                foreach (var item in boardMatrix)
                                {
                                    sr.WriteLine(item);
                                }
                            }
                            Console.WriteLine("The game has been saved to save.txt.");
                        }
                        // end game - show scores
                        else if (req == "end")
                        {
                            EndOfGame.Score(boardMatrix);
                        }
                        // action move request if valid number input
                        else if (input)
                        {
                            int xcoord = placing / 10;
                            int ycoord = placing % 10;
                            boardMatrix = HumanMove.LookAndPlace(xcoord, ycoord, boardMatrix, 'X', 'O');
                        }
                        else
                        {
                            Console.Write("\nInput not recognised.");
                        }
                        // computer move
                        boardMatrix = EasyAI.MakeMove(boardMatrix, size);
                        // test for full board, if so end game
                        foreach (char square in boardMatrix)
                        {
                            if (square == ' ')
                            {
                                break;
                            }
                            else
                            {
                                EndOfGame.Score(boardMatrix);
                            }
                        }
                    }
                }
            }
        }
    }
}


