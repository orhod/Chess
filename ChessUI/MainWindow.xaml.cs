using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Images for the pieces
        private readonly Image[,] pieceImages = new Image[8,8];
        // Highlight rectangles
        private readonly Rectangle[,] highlight = new Rectangle[8, 8];
        // The move cache
        private Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
        // The current game state
        private GameState gameState;
        // The selected position
        private Position selectedPos = null;
        /*
         *  Do : Constructor for the main window
         */
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            this.gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }
        /*
         * In : -
         * Out: -
         * Do : Initialize the board with images
         */
        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for(int c = 0; c < 8; c++)
                {
                    // Create Images for the pieces and add them to the grid
                    Image image = new Image();
                    this.pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    // Create rectangles to represent highlighted squers
                    Rectangle highlight = new Rectangle();
                    this.highlight[r, c] = highlight;
                    HighLightGrid.Children.Add(highlight);
                }

            }
        }
        /*
         * In : Board
         * Out: -
         * Do : Draw the board on the screen with the correct images 
         */
        private void DrawBoard(Board board)
        {
            for(int r = 0;r < 8; r++)
            { 
                for(int c = 0;c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r,c].Source = Images.GetImage(piece);
                }
            }
        }
        /*
         * In : object sender (the board grid), MouseButtonEventArgs e (the mouse event)
         * Out: -
         * Do : Handle the mouse down event on the board grid and get the position of the click
         */
        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(IsEndGameMenuOnScrean())
            {
                return;
            }
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);
            if(selectedPos ==  null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }

        }
        /*
         * In : Point
         * Out: Position
         * Do: Convert the point to a position on the board
         */
        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y/squareSize);
            int col = (int)(point.X/squareSize);
            return new Position(row, col);

        }
        /*
         * In : Position
         * Out: -
         * Do : Logic for when a from position is selected 
         *      show the possible moves for the piece
         */
        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);
            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlight();
            }
        }

        /*
         * In : IEnumerable<Move> (possible moves)
         * Out: -
         * Do : Cache the moves in the move cache
         */
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }
        /*
         * In : Position
         * Out: -
         * Do : Logic for when a to position is selected
         *      move the piece to the selected position
         */
        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHighlight();

            if(moveCache.TryGetValue(pos, out Move move))
            {
                if(move.Type == MoveType.PawnPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }
        /*
         * In : Move
         * Out: -
         * Do : Handle the move and update the game state
         *      redraw the board and set the cursor to the player color
         *      If Game Over Show End screan menu
         */
        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }
        /*
         * In : Position from, Position to
         * Out: -
         * Do : Handle the pawn promotion
         *      show the promotion menu and handle the promotion
         */
        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row,to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            PromotionMenu promMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new PawnPromotion(from, to, type);
                HandleMove(promMove);
            };
        }

        /*
         * In : -
         * Out: -
         * Do : Show all the possible moves for the selected piece
         */
        private void ShowHighlight()
        {
            Color color = Color.FromArgb(125, 53, 192, 250);
            foreach(Position to in this.moveCache.Keys)
            {
                highlight[to.Row,to.Column].Fill = new SolidColorBrush(color);
            }
        }
        /*
         * In : -
         * Out: -
         * Do : Hide all the possible moves for the selected piece
         *      after the move is made or the piece is deselected
         */
        private void HideHighlight()
        {
            foreach (Position to in this.moveCache.Keys)
            {
                highlight[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }
        /*
         * In : Player
         * Out: -
         * Do : Set the cursor to the player color
         */
        private void SetCursor(Player player)
        {
            if(player == Player.White)
            {
                Cursor = ChessCursors.WhiteCursor;
            }
            else
            {
                Cursor = ChessCursors.BlackCursor;

            }
        }
        /*
         * In : -
         * Out: bool
         * Do : Ceck if the end game menu is on screan
         */
        private bool IsEndGameMenuOnScrean()
        {
            return MenuContainer.Content != null;
        }
        /*
         * In : -
         * Out: -
         * Do : handle the game over screan
         */
        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += Option =>
            {
                if (Option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }
        /*
         * In : -
         * Out: -
         * Do : Restart the Game
         */
        private void RestartGame()
        {
            HideHighlight();
            moveCache.Clear();
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }
    }
}