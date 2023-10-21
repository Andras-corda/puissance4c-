namespace puissance4
{
    class Program
    {
        static char[,] board = new char[6, 7];
        static char currentPlayer = 'X';
        static bool isGameOver = false;

        static void Main()
        {
            do
            {
                InitializeBoard();
                PlayGame();
                Console.Write("Do you want to play again? (Yes/No): ");
            } while (Console.ReadLine().Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase));
        }

        static void PlayGame()
        {
            bool isGameover = false;

            while (!isGameover)
            {
                DisplayBoard();
                int column = GetValidMove();
                int row = PlaceToken(column);

                if (CheckWin(row, column))
                {
                    DisplayBoard();
                    Console.WriteLine("Player " + currentPlayer + " wins!");
                    isGameover = true;
                }
                else if (IsBoardFull())
                {
                    DisplayBoard();
                    Console.WriteLine("It's a draw!");
                    isGameover = true;
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
        }

        static void InitializeBoard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }

            currentPlayer = 'X';
            isGameOver = false;
        }

        static void DisplayBoard()
        {
            Console.Clear();
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    char token = board[row, col];
                    Console.Write("|");
                    if (token == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" " + token + " ");
                        Console.ResetColor();
                    }
                    else if (token == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" " + token + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  1   2   3   4   5   6   7");
            Console.WriteLine("Current Player: " + currentPlayer);
        }

        static int GetValidMove()
        {
            int column;
            while (true)
            {
                Console.Write("Player " + currentPlayer + ", enter the column (1-7): ");
                if (int.TryParse(Console.ReadLine(), out column) && column >= 1 && column <= 7)
                {
                    column--; 
                    if (board[0, column] == ' ')
                    {
                        return column;
                    }
                    else
                    {
                        Console.WriteLine("Column is full. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid column. Try again.");
                }
            }
        }

        static int PlaceToken(int column)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = currentPlayer;
                    return row;
                }
            }
            return -1;  
        }

        static bool CheckWin(int row, int col)
        {
            char token = board[row, col];

            int consecutiveTokens = 1;
            for (int c = col + 1; c < 7 && board[row, c] == token; c++)
            {
                consecutiveTokens++;
            }
            for (int c = col - 1; c >= 0 && board[row, c] == token; c--)
            {
                consecutiveTokens++;
            }
            if (consecutiveTokens >= 4)
            {
                return true;
            }

            consecutiveTokens = 1;
            for (int r = row + 1; r < 6 && board[r, col] == token; r++)
            {
                consecutiveTokens++;
            }
            if (consecutiveTokens >= 4)
            {
                return true;
            }

            consecutiveTokens = 1;
            for (int r = row + 1, c = col + 1; r < 6 && c < 7 && board[r, c] == token; r++, c++)
            {
                consecutiveTokens++;
            }
            for (int r = row - 1, c = col - 1; r >= 0 && c >= 0 && board[r, c] == token; r--, c--)
            {
                consecutiveTokens++;
            }
            if (consecutiveTokens >= 4)
            {
                return true;
            }

            consecutiveTokens = 1;
            for (int r = row + 1, c = col - 1; r < 6 && c >= 0 && board[r, c] == token; r++, c--)
            {
                consecutiveTokens++;
            }
            for (int r = row - 1, c = col + 1; r >= 0 && c < 7 && board[r, c] == token; r--, c++)
            {
                consecutiveTokens++;
            }
            return consecutiveTokens >= 4;
        }

        static bool IsBoardFull()
        {
            for (int col = 0; col < 7; col++)
            {
                if (board[0, col] == ' ')
                {
                    return false;
                }
            }
            return true;
        }
    }

}