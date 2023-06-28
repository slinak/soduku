using System;
using System.Linq;
using System.Collections.Generic;

namespace InterviewApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] incompleteBoard = new int[][]
                {
                    new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2}, 
                    new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                    new int[] {1, 9, 8, 3, 0, 2, 5, 6, 7},
                    new int[] {8, 5, 0, 7, 6, 1, 4, 0, 3},
                    new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                    new int[] {7, 0, 3, 9, 2, 4, 8, 5, 6},
                    new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                    new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                    new int[] {3, 0, 0, 2, 8, 6, 1, 7, 9},
                };
            Console.WriteLine("Initial Board:");
            PrintBoard(incompleteBoard);

            Console.WriteLine("Solved Board:");
            SolveBoard(incompleteBoard);
            /*
            int[][] solvedBoard = new int[][]
                {
                    new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2}, 
                    new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                    new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                    new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                    new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                    new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                    new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                    new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                    new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
                };
                */
            Console.ReadLine();
        }

        public static void PrintBoard(int[][] board)
        {
            foreach (var line in board)
                Console.WriteLine(String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", line[0], line[1], line[2], line[3], line[4], line[5], line[6], line[7], line[8]));
        }

        public static void SolveBoard(int[][] board)
        {
            int rows = 0;
            int columns = 0;
            int[] possibilities = new int[]{};

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
            
            possibilities = GetPossibleEntries(board, rows, columns);

            for (int x = 1; x < 10; x++)
                if(possibilities[x] != 0)
                {
                    board[rows][columns] = possibilities[x];
                    SolveBoard(board);
                }
            
            board[rows][columns] = 0;
        }

        public static int[] GetPossibleEntries(int[][] board, int i, int j)
        {
            int[] possibilities = new int[10];
            
            for(int x = 1; x < 10; x++)
                possibilities[x] = 0;

            for(int y = 0; y < 9; y++)
                if (board[i][y] != 0)
                    possibilities[board[i][y]] = 1;

            for (int a = 0; a < 9; a++)
                if (board[a][j] != 0)
                    possibilities[board[a][j]] = 1;

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
                        possibilities[board[x][y]] = 1;

            for (int x = 1; x < 10; x++)
                if (possibilities[x] == 0)
                    possibilities[x] = x;
                else
                    possibilities[x] = 0;
            
            return possibilities;
        }


        public static bool IsFull(int[][] board)
        {
            for(int i = 0; i < 9; i++)
                for(int j = 0; j < 9; j++)
                    if(board[i][j] == 0)
                        return false;
            
            return true;
        }
    }
}