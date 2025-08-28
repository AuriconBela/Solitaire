# Solitaire Game

My implementation of the classic Peg Solitaire (also known as Marble Solitaire) built with C# and Windows Forms, featuring polished graphics and smooth gameplay.

## 🎮 Game Overview

Peg Solitaire is a classic board game where the objective is to remove as many marbles as possible by jumping over them with other marbles. The game starts with a cross-shaped board filled with marbles, except for one empty space in the center.

### Game Rules
- **Goal**: Remove as many marbles as possible by jumping over them
- **Movement**: Select a marble and jump over an adjacent marble to an empty space
- **Direction**: Marbles can jump horizontally, vertically, or diagonally
- **Distance**: Jumps must be exactly 2 spaces (jumping over one marble)
- **Removal**: The marble that was jumped over is removed from the board
- **Winning**: The fewer marbles remaining, the better your score

## 🚀 Features

### Visual Features
- **Antialiased Graphics**: Smooth, professional-looking rendering
- **Gradient Marbles**: 3D-effect marbles with gradient shading
- **Cross-shaped Board**: Traditional Solitaire board layout with proper boundaries
- **Selection Highlighting**: Yellow border indicates selected marble
- **Responsive Design**: Board automatically centers and scales with window size

### Gameplay Features
- **Interactive Selection**: Click to select marbles and target positions
- **Valid Move Detection**: Only allows legal moves according to Solitaire rules
- **Score Tracking**: Real-time score display at the bottom of the screen
- **State Management**: Robust game state handling for selections and moves
- **Random Start**: Each game begins with a randomly placed empty space

### Technical Features
- **.NET 8.0**: Built on the latest .NET framework
- **Windows Forms**: Native Windows application with optimal performance
- **Double Buffering**: Smooth, flicker-free rendering
- **Constructor Injection**: Clean, testable architecture
- **Separation of Concerns**: Well-organized code structure

## 🛠️ Technical Architecture

### Project Structure
```
Solitaire/
├── Model/
│   ├── Board.cs          # Game board logic and state
│   ├── Game.cs           # Main game state and score
│   ├── CellType.cs       # Enumeration for cell types
│   └── Constants.cs      # Visual constants and styling
├── Presentation/
│   └── BoardDrawer.cs    # Rendering and visual presentation
├── GameFlow/
│   └── State.cs          # Game state management (Normal/Selected)
├── Form1.cs              # Main form and user interaction
└── Program.cs            # Application entry point
```

### Key Components

#### BoardDrawer Class
- **Purpose**: Handles all visual rendering of the game board
- **Features**: Antialiasing, centering, score display, selection highlighting
- **Responsibilities**: Drawing marbles, grid, boundaries, and score

#### State Pattern Implementation
- **NormalState**: Default state, allows selecting occupied marbles
- **SelectedState**: Marble selected, allows jumping to valid empty spaces
- **Benefits**: Clean separation of game logic and clear state transitions

#### Board Class
- **Responsibilities**: Game logic, move validation, marble placement
- **Methods**: `IsReachableFrom()`, `CellBetween()`, position validation
- **State**: 7×7 grid with cross-shaped valid area

## 🎯 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Windows operating system
- Visual Studio 2022 (recommended) or VS Code

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/AuriconBela/Solitaire.git
   ```
2. Navigate to the project directory:
   ```bash
   cd Solitaire
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

### Alternative Setup
- Open `Solitaire.sln` in Visual Studio
- Press F5 to build and run

## 🎮 How to Play

1. **Start**: The game begins with a cross-shaped board filled with marbles and one empty space
2. **Select**: Click on a marble you want to move (it will be highlighted in yellow)
3. **Jump**: Click on an empty space that's exactly 2 spaces away (horizontally, vertically, or diagonally)
4. **Score**: Each successful jump removes a marble and increases your score
5. **Continue**: Keep jumping until no more moves are possible
6. **Goal**: Try to remove as many marbles as possible!

### Visual Cues
- **Aquamarine Marbles**: Available marbles to select
- **Yellow Highlight**: Currently selected marble
- **Light Gray Cells**: Valid board positions
- **Orange-Red Background**: Areas outside the game board

## 🎨 Customization

The game's visual appearance can be easily customized through the `Constants.cs` file:

```csharp
// Colors
public static Color Background = Color.OrangeRed;      // Background color
public static Color Marble = Color.Aquamarine;        // Marble color
public static Color SelectedHighlight = Color.Yellow; // Selection highlight
public static Color GameScore = Color.Black;          // Score text color

// Sizes
public static int BoardSize = 7;                      // Board dimensions
public static Font ScoreFont = new("Arial", 16, FontStyle.Bold);
```

## 🔧 Development

### Architecture Patterns
- **State Pattern**: Clean game state management
- **Dependency Injection**: Constructor injection for BoardDrawer
- **Separation of Concerns**: Clear separation between model, view, and game logic

### Code Quality Features
- **Nullable Reference Types**: Enabled for better null safety
- **Modern C# Features**: Records, pattern matching, and implicit usings
- **Clean Code**: Well-documented methods and clear naming conventions

## 📋 System Requirements

- **OS**: Windows 10 or later
- **Framework**: .NET 8.0 Runtime
- **Memory**: 100MB RAM
- **Storage**: 50MB available space
- **Display**: 800×600 minimum resolution

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

### Development Guidelines
- Follow the existing code style and patterns
- Add unit tests for new functionality
- Update documentation for any new features
- Ensure all builds pass before submitting

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 🎯 Future Enhancements

- [ ] High score tracking and persistence
- [ ] Multiple difficulty levels
- [ ] Undo/Redo functionality
- [ ] Game statistics and analytics
- [ ] Custom board sizes and shapes
- [ ] Sound effects and animations
- [ ] Online leaderboards

## 🐛 Known Issues

No known issues at this time. If you encounter any problems, please create an issue on GitHub.

## 🙏 Acknowledgments

- Classic Peg Solitaire game for inspiration
- .NET and Windows Forms teams for the excellent framework
- Contributors and testers

---

**Enjoy the game!** 🎉
