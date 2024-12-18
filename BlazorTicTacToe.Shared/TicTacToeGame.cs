﻿
namespace BlazorTicTacToe.Shared;

public class TicTacToeGame 
{
    public string? PlayerXId { get; set; }
    public string ?PlayerOId { get; set; }
    public string? CurrentPlayerId { get; set; }
    public string? CurrentPlayerSymbol => CurrentPlayerId == PlayerXId ? "X" : "O";
    public bool IsGameStarted { get; set; } = false;
    public bool IsGameOver{ get; set; } = false;
    public bool IsDraw{ get; set; } = false;
    public string Winner { get; set; } = string.Empty;
    public List<List<string>> BoardList { get; set; } = new List<List<string>>(3);

    public TicTacToeGame()
    {
        InitializeBoard();
    }
    public void StartGame()
    {
        CurrentPlayerId = PlayerXId;
        IsGameStarted = true;
        IsGameOver = false;
        Winner = string.Empty;
        IsDraw = false; 
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        BoardList.Clear();

        for (int i = 0; i < 5; i++)
        {
            var row = new List<string>(5);
            for (int j = 0; j < 5; j++)
            {
                row.Add(string.Empty);
            }

            BoardList.Add(row);
        }
    }

    public void TogglePlayer()
    {
        CurrentPlayerId = CurrentPlayerId == PlayerXId ? PlayerOId : PlayerXId;
    }

    //public bool MakeMove(int row, int col, string playerId)
    //{
    //    if (playerId != CurrentPlayerId ||
    //        row < 0 || row >= 3 || col < 0 ||
    //        col >= 3 || BoardList[row][col] != string.Empty)
    //    {
    //        return false;
    //    }

    //    BoardList[row][col] = CurrentPlayerSymbol;
    //    TogglePlayer();
    //    return true;
    //}
    public bool MakeMove(int row, int col, string playerId)
    {
        Console.WriteLine($"Trying to make move: Player {playerId} on row {row}, col {col}");

        if (playerId != CurrentPlayerId)
        {
            Console.WriteLine($"Player {playerId} is not the current player ({CurrentPlayerId}).");
            return false;
        }

        if (row < 0 || row >= 5 || col < 0 || col >= 5)
        {
            Console.WriteLine($"Invalid move: row {row}, col {col} is out of bounds.");
            return false;
        }

        if (BoardList[row][col] != string.Empty)
        {
            Console.WriteLine($"Cell at row {row}, col {col} is already occupied.");
            return false;
        }

        // Если все проверки прошли успешно
        BoardList[row][col] = CurrentPlayerSymbol;
        TogglePlayer();
        return true;
    }

    //public string CheckWinner()
    //{
    //    for (int i = 0;i < 3;i++)
    //    {
    //        if (!string.IsNullOrEmpty(BoardList[i][0]) 
    //            && BoardList[i][0] == BoardList[i][1]
    //            && BoardList[i][1] == BoardList[i][2])
    //        {
    //            return BoardList[i][0];
    //        }

    //        if (!string.IsNullOrEmpty(BoardList[0][i])
    //          && BoardList[0][i] == BoardList[1][i]
    //          && BoardList[1][i] == BoardList[2][i])
    //        {
    //            return BoardList[0][i];
    //        }

    //        // Diagonals

    //        if (!string.IsNullOrEmpty(BoardList[0][0])
    //        && BoardList[0][0] == BoardList[1][1]
    //        && BoardList[1][1] == BoardList[2][2])
    //        {
    //            return BoardList[0][0];
    //        }

    //        if (!string.IsNullOrEmpty(BoardList[0][2])
    //          && BoardList[0][2] == BoardList[1][1]
    //          && BoardList[1][1] == BoardList[2][0])
    //        {
    //            return BoardList[0][2];
    //        }
    //    }

    //    return string.Empty;
    //}
    public string CheckWinner()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (!string.IsNullOrEmpty(BoardList[i][j]))
                {
                    // Проверяем горизонталь (ряд)
                    if (j <= 5 - 5 && BoardList[i][j] == BoardList[i][j + 1] && BoardList[i][j] == BoardList[i][j + 2] && BoardList[i][j] == BoardList[i][j + 3] && BoardList[i][j] == BoardList[i][j + 4])
                    {
                        return BoardList[i][j];
                    }

                    // Проверяем вертикаль (столбец)
                    if (i <= 5 - 5 && BoardList[i][j] == BoardList[i + 1][j] && BoardList[i][j] == BoardList[i + 2][j] && BoardList[i][j] == BoardList[i + 3][j] && BoardList[i][j] == BoardList[i + 4][j])
                    {
                        return BoardList[i][j];
                    }

                    // Проверяем диагональ (левый верх — правый низ)
                    if (i <= 5 - 5 && j <= 5 - 5 && BoardList[i][j] == BoardList[i + 1][j + 1] && BoardList[i][j] == BoardList[i + 2][j + 2] && BoardList[i][j] == BoardList[i + 3][j + 3] && BoardList[i][j] == BoardList[i + 4][j + 4])
                    {
                        return BoardList[i][j];
                    }

                    // Проверяем диагональ (правый верх — левый низ)
                    if (i <= 5 - 5 && j >= 4 && BoardList[i][j] == BoardList[i + 1][j - 1] && BoardList[i][j] == BoardList[i + 2][j - 2] && BoardList[i][j] == BoardList[i + 3][j - 3] && BoardList[i][j] == BoardList[i + 4][j - 4])
                    {
                        return BoardList[i][j];
                    }
                }
            }
        }

        return string.Empty;
    }
    public bool CheckDraw()
    {
        return IsDraw = BoardList.All(row => row.All(cell => !string.IsNullOrEmpty(cell)));
    }
}
