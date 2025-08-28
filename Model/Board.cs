namespace Solitaire.Model;

public class Board
{
    private readonly CellType[,] _board = new CellType[Constants.BoardSize, Constants.BoardSize];
    private Point? _selected;

    public Board()
    {
        Init();
    }


    public CellType this[int column, int row]
    {
        get => _board[column, row];
        set => _board[column, row] = value;
    }

    public Point? Selected { get => _selected; set => _selected = value; }

    public bool IsReachableFrom(Point startPoint, Point targetPoint)
    {
        if (this[targetPoint.X, targetPoint.Y] != CellType.Empty)
        {
            return false;
        }
        if (targetPoint == startPoint)
        {
            return false;
        }

        var cellToBeRemoved = CellBetween(startPoint, targetPoint);
        var isRemoveAble = cellToBeRemoved is not null && this[cellToBeRemoved.Value.X, cellToBeRemoved.Value.Y] != CellType.Empty;
        return (IsHorizontallyReachableFrom(startPoint, targetPoint) ||
               IsVerticallyReachableFrom(startPoint, targetPoint) ||
               IsDiagonallyReachableFrom(startPoint, targetPoint)) &&
               isRemoveAble;
    }

    public static Point? CellBetween(Point startPoint, Point targetPoint)
    {
        if (IsHorizontallyReachableFrom(startPoint, targetPoint))
        {
            if (startPoint.X > targetPoint.X)
            {
                return startPoint with { X = startPoint.X - 1 };
            };
            return startPoint with { X = startPoint.X + 1 };
        }
        if (IsVerticallyReachableFrom(startPoint, targetPoint))
        {
            if (startPoint.Y > targetPoint.Y)
            {
                return startPoint with { Y = startPoint.Y - 1 };
            };
            return startPoint with { Y = startPoint.Y + 1 };
        }
        if (IsDiagonallyReachableFrom(startPoint, targetPoint))
        {
            int x, y;
            if (startPoint.X > targetPoint.X)
            {
                x = startPoint.X - 1;
            }
            else
            {
                x = startPoint.X + 1;
            }

            if (startPoint.Y > targetPoint.Y)
            {
                y = startPoint.Y - 1;
            }
            else
            {
                y = startPoint.Y + 1;
            }
            return new(x, y);
        }
        return null;
    }

    private static bool IsHorizontallyReachableFrom(Point startPoint, Point targetPoint)
    {
        return Math.Abs(startPoint.X - targetPoint.X) == 2 &&
               Math.Abs(startPoint.Y - targetPoint.Y) == 0;
    }

    private static bool IsVerticallyReachableFrom(Point startPoint, Point targetPoint)
    {
        return Math.Abs(startPoint.Y - targetPoint.Y) == 2 &&
               Math.Abs(startPoint.X - targetPoint.X) == 0;
    }

    private static bool IsDiagonallyReachableFrom(Point startPoint, Point targetPoint)
    {
        return Math.Abs(startPoint.Y - targetPoint.Y) == 2 &&
               Math.Abs(startPoint.X - targetPoint.X) == 2;
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
        while (this[emptyCell.X, emptyCell.Y] == CellType.Banned);

        return emptyCell;
    }
}
