using Solitaire.GameFlow;
using Solitaire.Model;
using Solitaire.Presentation;

namespace Solitaire;

public partial class Form1 : Form
{
    private readonly Game _game;
    private readonly BoardDrawer _boardDrawer;
    private State _state = new NormalState();
    
    public Form1() : this(new Game())
    {
    }

    public Form1(Game game)
    {
        _game = game;
        _boardDrawer = new BoardDrawer(_game);
        InitializeComponent();
        SetupForm();
    }

    private void SetupForm()
    {
        // Set form properties for optimal drawing
        SetStyle(ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.UserPaint | 
                ControlStyles.DoubleBuffer | 
                ControlStyles.ResizeRedraw, true);

        // Set minimum form size based on board requirements
        var requiredSize = _boardDrawer.GetRequiredSize();
        MinimumSize = new Size(requiredSize.Width + 100, requiredSize.Height + 150);
        Size = new Size(requiredSize.Width + 200, requiredSize.Height + 200);
        
        // Set background color
        BackColor = Constants.Background;
        Text = "Solitaire Game";
        
        // Center the form on screen
        StartPosition = FormStartPosition.CenterScreen;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        _boardDrawer.DrawBoard(e.Graphics, ClientSize);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        
        // Get the cell position from mouse click
        var cellPosition = _boardDrawer.GetCellFromPosition(e.Location, ClientSize);
        if (cellPosition.HasValue)
        {
            HandleCellClick(cellPosition.Value);
        }
    }

    private void HandleCellClick(Point cellPosition)
    {
        if (_state.CanClick(cellPosition, _game.Board))
        {
            _state.Select(cellPosition, _game.Board, OnRemoval);
            _state = _state.NextState();
        }
        Invalidate();
    }

    private void OnRemoval()
    {
        _game.Score += 1;
    }
}
