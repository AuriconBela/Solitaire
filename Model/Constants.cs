namespace Solitaire.Model;

public static class Constants
{
    public static int BoardSize = 7;

    public static Color Background = Color.OrangeRed;
    public static Color Marble = Color.Aquamarine;
    public static Color Grid = Color.Gray;
    public static Color Boundary = Color.Black;
    public static Color MarbleLine = Color.DarkBlue;
    public static Color SelectedHighlight = Color.Yellow;
    public static Color GameScore = Color.Black;

    public static Brush BackGroundBrush = new SolidBrush(Background);
    public static Brush MarbleBrush = new SolidBrush(Marble);
    public static Brush GameScoreBrush = new SolidBrush(GameScore);
    public static Pen GridPen = new (Grid, 1);
    public static Pen BoundaryPen = new (Boundary, 3);
    public static Pen MarblePen = new (MarbleLine, 2);
    public static Pen SelectedHighlightPen = new (SelectedHighlight, 4);

    public static Font ScoreFont = new ("Arial", 16, FontStyle.Bold);
    public static int GridLineWidth = 1;
    public static int ScoreAreaHeight = 50;
}
