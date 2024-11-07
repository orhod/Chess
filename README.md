# Chess Game Project

A fully functional Chess game built with C#. The game includes all standard chess rules and features, an intuitive user interface, and menus for an immersive playing experience.

## Features

### Moves
- **Normal Moves**: All standard moves for each piece.
- **Castling**: Both kingside and queenside castling.
- **Double Pawn Move**: Pawns can move two squares from their initial position.
- **En Passant**: Special capture move for pawns.
- **Pawn Promotion**: Pawns reaching the last rank can be promoted to any piece (Queen, Rook, Bishop, or Knight) with a selection menu displaying each piece.

### End Game Conditions
- **Checkmate**: When a player's king is in check and cannot escape.
- **Stalemate**: When a player has no legal moves but is not in check.
- **Fifty-Move Rule**: Draw declared if no pawn moves or captures occur in 50 consecutive moves.
- **Insufficient Material**: Draw when neither player has enough material to checkmate.
- **Threefold Repetition**: Draw when the same position occurs three times in a game.

### User Interface
- **Chess Board and Pieces**: Clean and visually engaging board design with recognizable pieces.
- **Pause Menu**:
  - **Continue**: Resume the game.
  - **Restart**: Start a new game.
  - **Exit**: Return to the main menu.
- **End of Game Menu**: Displays the reason for the game ending and the winning player (if applicable).
- **Pawn Promotion Menu**: Showcases each piece with an image to make the promotion choice clear and easy.

## Getting Started

Clone the repository to start playing:

```bash
git clone https://github.com/your-username/chess-game.git
cd chess-game
