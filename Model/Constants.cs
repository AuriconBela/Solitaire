namespace Solitaire.Model;

public static class Constants
{
    public static int BoardSize = 7;

    public static Color Background = Color.OrangeRed;
    public static Color Marble = Color.Aquamarine;
    public static Color Grid = Color.Gray;
    public static Color Boundary = Color.Black;
    public static Color MarbleLine = Color.DarkBlue;

    public static Brush BackGroundBrush = new SolidBrush(Background);
    public static Brush MarbleBrush = new SolidBrush(Marble);
    public static Pen GridPen = new (Grid, 1);
    public static Pen BoundaryPen = new (Boundary, 3);
    public static Pen MarblePen = new (MarbleLine, 2);

    public static int GridLineWidth = 1;
}
