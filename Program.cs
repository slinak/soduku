using System;
using System.Linq;
using System.Collections.Generic;

namespace InterviewApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] incompleteBoard = CreateBoardFromUserInput();

            Console.WriteLine("Initial Board:");
            PrintBoard(incompleteBoard);

            Console.WriteLine("Solved Board:");
            SolveBoard(incompleteBoard);
 
            Console.ReadLine();
        }

        public static int[][] CreateBoardFromUserInput()
        {
            int[][] board = new int[9][];

            Console.WriteLine("Input rows as a single string, do not separate values.");
            Console.WriteLine("Unknown values should be entered as 0");
            Console.WriteLine("Press Enter after entering each row");
            Console.WriteLine("Ex [ 1, 9, 8, 3, 0, 2, 5, 6, 7 ] is input as 198302567");
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(String.Format("Row {0}:", i));
                var tmp = Console.ReadLine();
                board[i] = ProcessBoardLineInput(tmp);
            }

            Console.WriteLine("Line limit reached");

            return board;
        }

        public static int[] ProcessBoardLineInput(string input)
        {
            return input.ToCharArray().Select(c => Int32.Parse(c.ToString())).ToArray();
        }

        public static void PrintBoard(int[][] board)
        {
            for (int i = 0; i < 9; i++)
            {
                int[] line = board[i];
                Console.WriteLine(String.Format("{0} {1} {2} | {3} {4} {5} | {6} {7} {8}", line[0], line[1], line[2], line[3], line[4], line[5], line[6], line[7], line[8]));
                if (i == 2 || i == 5) 
                    Console.WriteLine("---------------------");
            }
        }

        public static void SolveBoard(int[][] board)
        {
            int rows, columns;
            rows = columns = 0;
            int[] possibleValues = new int[]{};


            if (IsFull(board))               
                PrintBoard(board);
            else
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                        if (board[x][y] == 0)
                        {
                            rows = x;
                            columns = y;
                        }

            possibleValues = GetPossibleValues(board, rows, columns);

            for (int x = 1; x < 10; x++)
                if(possibleValues[x] != 0)
                {
                    board[rows][columns] = possibleValues[x];
                    SolveBoard(board);
                }
            
            board[rows][columns] = 0;
        }

        public static int[] GetPossibleValues(int[][] board, int i, int j)
        {
            int[] possibleValues = new int[10];
            
            for(int x = 1; x < 10; x++)
                possibleValues[x] = 0;

            for(int y = 0; y < 9; y++)
                if (board[i][y] != 0)
                    possibleValues[board[i][y]] = 1;

            for (int a = 0; a < 9; a++)
                if (board[a][j] != 0)
                    possibleValues[board[a][j]] = 1;

            int k = 0;
            int l = 0;
            
            if (i >= 0 && i <= 2)
                k = 0;
            else if (i >= 3 && i <=5)
                k = 3;
            else
                k = 6;

            if (j >= 0 && j <= 2)
                l = 0;
            else if (j >= 3 && j <=5)
                l = 3;
            else 
                l = 6;

            for (int x = k; x < k + 3; x++)
                for (int y = l; y < l + 3; y++)
                    if (board[x][y] != 0 )
                        possibleValues[board[x][y]] = 1;

            for (int x = 1; x < 10; x++)
                if (possibleValues[x] == 0)
                    possibleValues[x] = x;
                else
                    possibleValues[x] = 0;
            
            return possibleValues;
        }


        public static bool IsFull(int[][] board)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if(board[i][j] == 0)
                        return false;

            return true;
        }
    }
}