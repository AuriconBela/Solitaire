using Solitaire.Model;

namespace Solitaire.GameFlow;

internal abstract class State
{
    protected Point? Clicked {get;set;}

    internal virtual bool CanClick(Point point, Board board)
    {
        Clicked = point;        
        return true;    
    }
    internal abstract State NextState();
    internal abstract void Select(Point point, Board board, Action? onSuccess);
}

internal class NormalState : State
{
    internal override bool CanClick(Point point, Board board)
    {        
        base.CanClick(point, board);
        return (board.Selected is null) && board[point.X, point.Y] == CellType.Occupied;
    }

    internal override State NextState()
    {
        return new SelectedState();
    }

    internal override void Select(Point point, Board board, Action? onSuccess)
    {
        board.Selected = point;
    }
}

internal class SelectedState : State
{

    internal override bool CanClick(Point point, Board board)
    {        
        base.CanClick(point, board);
        if (board.Selected == point)
        {
            return true;
        }
        if (board[point.X, point.Y] != CellType.Empty)
        {
            return false;
        }
        return board.IsReachableFrom(board.Selected!.Value, point);
    }

    internal override State NextState()
    {
        return new NormalState();
    }

    internal override void Select(Point point, Board board, Action? onSuccess)
    {
        if (point == board.Selected!.Value)
        {
            board.Selected = null;
            return;
        }
        board[point.X, point.Y] = CellType.Occupied;
        board[board.Selected!.Value.X, board.Selected!.Value.Y] = CellType.Empty;

        var removedCell = Board.CellBetween(board.Selected!.Value, point);

        if (removedCell is not null)
        {
            board[removedCell!.Value.X, removedCell!.Value.Y] = CellType.Empty;
            onSuccess?.Invoke();
        }

        board.Selected = null;
    }
}