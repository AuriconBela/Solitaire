namespace Solitaire.Model;

public class Board
{
    private readonly CellType[,] _board = new CellType[Constants.BoardSize, Constants.BoardSize];

    public Board()
    {
        Init();
    }


    public CellType this[int column, int row]
    {
        get => _board[column, row];
    }

    private void Init()
    {
        // Set banned cells (corners of the 7x7 grid that are outside the cross shape)
        _board[0, 0] = CellType.Banned;
        _board[0, 1] = CellType.Banned;
        _board[1, 0] = CellType.Banned;
        _board[1, 1] = CellType.Banned;

        _board[0, 5] = CellType.Banned;
        _board[0, 6] = CellType.Banned;
        _board[1, 5] = CellType.Banned;
        _board[1, 6] = CellType.Banned;

        _board[5, 0] = CellType.Banned;
        _board[5, 1] = CellType.Banned;
        _board[6, 0] = CellType.Banned;
        _board[6, 1] = CellType.Banned;

        _board[5, 5] = CellType.Banned;
        _board[5, 6] = CellType.Banned;
        _board[6, 5] = CellType.Banned;
        _board[6, 6] = CellType.Banned;

        // Set one random cell as empty (the starting position)
        var emptyCell = RandomEmptyCell();
        _board[emptyCell.X, emptyCell.Y] = CellType.Empty;
    }

    private Point RandomEmptyCell()
    {
        Random random = new();
        Point emptyCell;

        do
        {
            int x = random.Next(0, Constants.BoardSize);
            int y = random.Next(0, Constants.BoardSize);
            emptyCell = new Point(x, y);
        }
        while (this[emptyCell.X,emptyCell.Y] == CellType.Banned);

        return emptyCell;
    }
}
