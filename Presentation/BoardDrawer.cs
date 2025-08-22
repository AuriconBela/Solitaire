using Solitaire.Model;
using System.Drawing.Drawing2D;

namespace Solitaire.Presentation;

internal class BoardDrawer
{
    private readonly Game _game;
    private int _cellSize = 50;
    private int _marbleRadius = 20;
    private Point _boardOffset = new(50, 50);

    public BoardDrawer(Game game)
    {
        _game = game;
    }

    public int CellSize
    {
        get => _cellSize;
        set => _cellSize = value;
    }

    public int MarbleRadius
    {
        get => _marbleRadius;
        set => _marbleRadius = value;
    }

    public Point BoardOffset
    {
        get => _boardOffset;
        set => _boardOffset = value;
    }

    public void DrawBoard(Graphics graphics, Size clientSize)
    {
        if (graphics == null || _game?.Board == null)
            return;

        // Enable antialiasing for smooth graphics
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        // Clear background with banned cell color
        graphics.Clear(Constants.Background);

        // Calculate centered board offset
        var centeredOffset = CalculateCenteredOffset(clientSize);
        var originalOffset = _boardOffset;
        _boardOffset = centeredOffset;

        // Draw the cross-shaped board
        for (int row = 0; row < Constants.BoardSize; row++)
        {
            for (int col = 0; col < Constants.BoardSize; col++)
            {
                CellType cellType = _game.Board[col, row];

                // Skip banned cells - they are already background color
                if (cellType == CellType.Banned)
                    continue;

                DrawCell(graphics, col, row, cellType);
            }
        }

        // Draw boundary around the cross shape
        DrawBoundary(graphics);

        // Restore original offset
        _boardOffset = originalOffset;
    }

    public void DrawBoard(Graphics graphics)
    {
        // Fallback method that uses the default offset
        DrawBoard(graphics, GetRequiredSize());
    }

    private void DrawCell(Graphics graphics, int col, int row, CellType cellType)
    {
        Rectangle cellRect = GetCellRectangle(col, row);

        // Draw cell background with slight rounded corners for better appearance
        using (Brush cellBrush = new SolidBrush(Color.LightGray))
        {
            // Create a slightly rounded rectangle path
            using (var path = CreateRoundedRectanglePath(cellRect, 2))
            {
                graphics.FillPath(cellBrush, path);
                graphics.DrawPath(Constants.GridPen, path);
            }
        }

        // Draw marble if occupied
        if (cellType == CellType.Occupied)
        {
            DrawMarble(graphics, cellRect);
        }
    }

    private void DrawMarble(Graphics graphics, Rectangle cellRect)
    {
        // Calculate marble position (centered in cell)
        int marbleX = cellRect.X + (cellRect.Width - _marbleRadius * 2) / 2;
        int marbleY = cellRect.Y + (cellRect.Height - _marbleRadius * 2) / 2;
        var marbleRect = new Rectangle(marbleX, marbleY, _marbleRadius * 2, _marbleRadius * 2);

        // Create a gradient brush for better visual effect
        using (var gradientBrush = new LinearGradientBrush(
            marbleRect,
            Color.FromArgb(255, Math.Min(255, Constants.Marble.R + 40),
                               Math.Min(255, Constants.Marble.G + 40),
                               Math.Min(255, Constants.Marble.B + 40)),
            Color.FromArgb(255, Math.Max(0, Constants.Marble.R - 40),
                               Math.Max(0, Constants.Marble.G - 40),
                               Math.Max(0, Constants.Marble.B - 40)),
            LinearGradientMode.ForwardDiagonal))
        {
            // Draw marble as a filled circle with gradient
            graphics.FillEllipse(gradientBrush, marbleRect);
        }

        // Add a slight border to the marble for better visibility
        graphics.DrawEllipse(Constants.MarblePen, marbleRect);
    }

    private void DrawBoundary(Graphics graphics)
    {
        // Create a path that outlines the cross shape
        using (var crossPath = CreateCrossShapePath())
        {
            graphics.DrawPath(Constants.BoundaryPen, crossPath);
        }
    }

    private GraphicsPath CreateCrossShapePath()
    {
        var path = new GraphicsPath();
        
        // The Solitaire cross shape consists of three sections:
        // - Top arm: columns 2-4, rows 0-1 (3×2 cells)
        // - Middle section: columns 0-6, rows 2-4 (7×3 cells)
        // - Bottom arm: columns 2-4, rows 5-6 (3×2 cells)
        // This creates the traditional cross pattern with banned corners
        
        int cellSize = _cellSize;
        int offsetX = _boardOffset.X;
        int offsetY = _boardOffset.Y;
        
        // Start from top-left of the top arm (col 2, row 0)
        Point startPoint = new Point(offsetX + 2 * cellSize, offsetY);
        
        // Top arm outline
        path.StartFigure();
        path.AddLine(startPoint.X, startPoint.Y, offsetX + 5 * cellSize, offsetY); // Top edge of top arm
        path.AddLine(offsetX + 5 * cellSize, offsetY, offsetX + 5 * cellSize, offsetY + 2 * cellSize); // Right edge of top arm
        
        // Right side of middle section
        path.AddLine(offsetX + 5 * cellSize, offsetY + 2 * cellSize, offsetX + 7 * cellSize, offsetY + 2 * cellSize); // Top-right corner
        path.AddLine(offsetX + 7 * cellSize, offsetY + 2 * cellSize, offsetX + 7 * cellSize, offsetY + 5 * cellSize); // Right edge of middle
        path.AddLine(offsetX + 7 * cellSize, offsetY + 5 * cellSize, offsetX + 5 * cellSize, offsetY + 5 * cellSize); // Bottom-right corner
        
        // Bottom arm outline
        path.AddLine(offsetX + 5 * cellSize, offsetY + 5 * cellSize, offsetX + 5 * cellSize, offsetY + 7 * cellSize); // Right edge of bottom arm
        path.AddLine(offsetX + 5 * cellSize, offsetY + 7 * cellSize, offsetX + 2 * cellSize, offsetY + 7 * cellSize); // Bottom edge of bottom arm
        path.AddLine(offsetX + 2 * cellSize, offsetY + 7 * cellSize, offsetX + 2 * cellSize, offsetY + 5 * cellSize); // Left edge of bottom arm
        
        // Left side of middle section
        path.AddLine(offsetX + 2 * cellSize, offsetY + 5 * cellSize, offsetX, offsetY + 5 * cellSize); // Bottom-left corner
        path.AddLine(offsetX, offsetY + 5 * cellSize, offsetX, offsetY + 2 * cellSize); // Left edge of middle
        path.AddLine(offsetX, offsetY + 2 * cellSize, offsetX + 2 * cellSize, offsetY + 2 * cellSize); // Top-left corner
        
        // Complete the outline back to start
        path.AddLine(offsetX + 2 * cellSize, offsetY + 2 * cellSize, offsetX + 2 * cellSize, offsetY); // Left edge of top arm
        path.CloseFigure();
        
        return path;
    }

    private Rectangle GetCellRectangle(int col, int row)
    {
        int x = _boardOffset.X + col * _cellSize;
        int y = _boardOffset.Y + row * _cellSize;
        return new Rectangle(x, y, _cellSize, _cellSize);
    }

    public Point? GetCellFromPosition(Point mousePosition, Size clientSize)
    {
        var centeredOffset = CalculateCenteredOffset(clientSize);

        int col = (mousePosition.X - centeredOffset.X) / _cellSize;
        int row = (mousePosition.Y - centeredOffset.Y) / _cellSize;

        if (col >= 0 && col < Constants.BoardSize &&
            row >= 0 && row < Constants.BoardSize &&
            _game.Board[col, row] != CellType.Banned)
        {
            return new Point(col, row);
        }

        return null;
    }

    public Point? GetCellFromPosition(Point mousePosition)
    {
        // Fallback method that uses the default offset
        return GetCellFromPosition(mousePosition, GetRequiredSize());
    }

    public Size GetRequiredSize()
    {
        return new Size(
            _boardOffset.X * 2 + Constants.BoardSize * _cellSize,
            _boardOffset.Y * 2 + Constants.BoardSize * _cellSize
        );
    }

    private Point CalculateCenteredOffset(Size clientSize)
    {
        int boardWidth = Constants.BoardSize * _cellSize;
        int boardHeight = Constants.BoardSize * _cellSize;

        int offsetX = Math.Max(0, (clientSize.Width - boardWidth) / 2);
        int offsetY = Math.Max(0, (clientSize.Height - boardHeight) / 2);

        return new Point(offsetX, offsetY);
    }

    private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
    {
        var path = new GraphicsPath();
        int diameter = cornerRadius * 2;

        // Top-left corner
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);

        // Top edge
        path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius, rect.Y);

        // Top-right corner
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);

        // Right edge
        path.AddLine(rect.Right, rect.Y + cornerRadius, rect.Right, rect.Bottom - cornerRadius);

        // Bottom-right corner
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);

        // Bottom edge
        path.AddLine(rect.Right - cornerRadius, rect.Bottom, rect.X + cornerRadius, rect.Bottom);

        // Bottom-left corner
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

        // Left edge
        path.AddLine(rect.X, rect.Bottom - cornerRadius, rect.X, rect.Y + cornerRadius);

        path.CloseFigure();
        return path;
    }
}
