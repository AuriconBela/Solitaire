# BoardDrawer Utility Class

## Overview
The `BoardDrawer` class is a drawing utility for the Solitaire game that renders the game board on a Windows Forms application. It's designed to be constructor-injected into `Form1` and provides complete visual representation of the game state.

## Features

### Core Functionality
- **Cross-shaped Board Rendering**: Draws the traditional Solitaire board layout with proper cross shape
- **Accurate Cross Boundary**: Draws a precise outline following the actual cross shape contours (not a simple rectangle)
- **Score Display**: Shows the current game score at the bottom of the screen with centered, bold text
- **Cell Type Visualization**:
  - `Occupied` cells: Display marble icons using the marble brush from Constants
  - `Empty` cells: Show as light gray cells with grid lines
  - `Banned` cells: Not drawn (background color shows through)
  - **Selected cells**: Highlighted with yellow border when Board.Selected is not null
- **Boundary Drawing**: Draws a precise cross-shaped boundary that follows the exact contours of valid cells using the boundary pen from Constants

### Customization Properties
- `CellSize`: Size of each cell in pixels (default: 50)
- `MarbleRadius`: Radius of marble icons (default: 20)
- `BoardOffset`: Offset from form edges (default: 50, 50)

### Interactive Features
- `GetCellFromPosition()`: Converts mouse coordinates to board cell coordinates
- `GetRequiredSize()`: Returns minimum form size needed to display the board and score area
- **Selection Highlighting**: Automatically highlights selected marbles based on Board.Selected property
- **Dynamic Score Display**: Automatically updates and displays the current game score

## Usage

### Constructor Injection
```csharp
public Form1(Game game)
{
    _game = game;
    _boardDrawer = new BoardDrawer(_game);
    InitializeComponent();
    SetupForm();
}
```

### Drawing Implementation
```csharp
protected override void OnPaint(PaintEventArgs e)
{
    base.OnPaint(e);
    _boardDrawer.DrawBoard(e.Graphics, ClientSize);
}
```

### Mouse Interaction
```csharp
protected override void OnMouseClick(MouseEventArgs e)
{
    var cellPosition = _boardDrawer.GetCellFromPosition(e.Location, ClientSize);
    if (cellPosition.HasValue)
    {
        HandleCellClick(cellPosition.Value);
    }
}
```

## Centering Features
The BoardDrawer now includes automatic centering functionality:
- **`CalculateCenteredOffset(Size clientSize)`**: Calculates the proper offset to center the board within the given client area
- **Dynamic Positioning**: Board position automatically adjusts when the form is resized
- **Maintained Interaction**: Mouse click detection works correctly with the centered board
- **Backward Compatibility**: Original methods without client size parameter still work for legacy usage

## Constants Integration
The class uses all visual constants from the `Constants` class:
- `Background`: Background color for banned areas
- `Grid`: Color for cell grid lines
- `Boundary`: Color for the board boundary
- `Marble`: Color for marble filling
- `MarbleLine`: Color for marble borders
- `SelectedHighlight`: Color for highlighting selected marbles
- `GameScore`: Color for score text
- `BackGroundBrush`: Used for form background
- `MarbleBrush`: Used for drawing marbles
- `GameScoreBrush`: Used for drawing score text
- `GridPen`: Used for cell borders
- `BoundaryPen`: Used for board boundary
- `MarblePen`: Used for marble borders
- `SelectedHighlightPen`: Used for highlighting selected marbles (thicker, yellow border)
- `ScoreFont`: Bold Arial font for score display
- `ScoreAreaHeight`: Reserved height at bottom for score display (50px)

## Technical Implementation

### Coordinate System
- Uses a standard 2D coordinate system with (0,0) at top-left
- Board coordinates are translated to screen coordinates using `BoardOffset`
- Each cell occupies a square area of `CellSize` × `CellSize` pixels
- Score area reserves 50px at the bottom of the form for score display
- Board centering accounts for score area when calculating vertical positioning

### Drawing Optimization
- Form uses double buffering for smooth rendering
- Only draws valid (non-banned) cells
- Uses proper disposal patterns for graphics resources
- **Antialiasing enabled** for smooth edges and professional appearance
- **Gradient effects** for enhanced visual appeal
- **Rounded corners** using custom GraphicsPath for modern UI design

### Visual Enhancement
- Marbles are drawn as filled circles with gradient effects and dark blue borders for better visibility
- Grid lines provide clear cell separation with rounded corners for smoother appearance
- Light gray background for valid cells distinguishes them from banned areas
- **Antialiasing**: All graphics are rendered with smooth antialiasing for crisp, professional appearance
- **Gradient Marbles**: Marbles use gradient fills for 3D-like appearance
- **Rounded Cell Corners**: Cells have subtle rounded corners for modern look

## Dependencies
- `Solitaire.Model.Game`: Source of board data
- `Solitaire.Model.Constants`: Visual styling constants
- `System.Drawing`: Graphics rendering
- `System.Windows.Forms`: Form integration
