namespace Solitaire.Model;

public class Game
{
    public Board Board { get; set; } = new Board();
    public int Score { get; set; }
}
