namespace ChessLogic
{
    public class Board
    {
        // The board
        private readonly Piece[,] pieces = new Piece[8, 8];

        private readonly Dictionary<Player, Position> PawnSkipPositions = new Dictionary<Player, Position>
        {
            {Player.Black, null },
            {Player.White, null }
        };

        // Getter and Setter by row and column
        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        // Getter and Setter by Position
        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }
        /*
         * In: -
         * Out: Board
         * Do : Create a new board with the initial pieces
         */
        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }

        /*
         * In : -
         * Out: -
         * Do : Add the initial pieces to the board
         */
        private void AddStartPieces()
        {
            // Adding The black pieces
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
            }
            // Adding The white pieces
            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new Bishop(Player.White);
            this[7, 3] = new Queen(Player.White);
            this[7, 4] = new King(Player.White);
            this[7, 5] = new Bishop(Player.White);
            this[7, 6] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);
            for (int i = 0; i < 8; i++)
            {
                this[6, i] = new Pawn(Player.White);
            }

        }
        /*
         * In : Position
         * Out: Boolean
         * Do : Check if a position is on the board
         */
        public static bool OnBoard(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }
        /*
         * In : Position
         * Out: Boolean
         * Do : Check if a position has a piece on it
         */
        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }
        /*
         * In : -
         * Out: IEnumerable<Position>
         * Do : Check of all positions with a piece
         */
        public IEnumerable<Position> PiecePositions()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Position pos = new Position(r, c);
                    if (!IsEmpty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }
        /*
         * In : Player (color)
         * Out: IEnumerable<Position>
         * Do : Get all the places where pieces of a player are placed
         */
        public IEnumerable<Position> PiecePositionsfor(Player player)
        {
            return PiecePositions().Where(pos => this[pos].Color == player);
        }
        /*
         * In : Player
         * Out: bool
         * Do : Check if the player is in check
         */
        public bool IsInCheck(Player player)
        {
            return PiecePositionsfor(player.Opponent()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.CanCaptureOpponentKing(pos, this);
            });
        }
        /*
         * In : -
         * Out: Board
         * Do : Copy the Board
         */
        public Board copy()
        {
            Board copy = new Board();
            foreach (Position pos in PiecePositions())
            {
                copy[pos] = this[pos].Copy();
            }
            return copy;
        }
        /*
         * In : Player
         * Out: Position
         * Do : get The skiped position of the pawn
         */
        public Position GetPawnSkipPosition(Player player)
        {
            return PawnSkipPositions[player];
        }
        /*
         * In : Player, Position
         * Out: -
         * Do : Save in Dictionary the skiped position 
         */
        public void SetPawnSkipPosition(Player player, Position pos)
        {
            PawnSkipPositions[player] = pos;
        }
        /*
         * In : Player , PieceType
         * Out: Position
         * Do : find the first position containing a piece type in color
         */
        private Position FindPiece(Player color, PieceType type)
        {
            return PiecePositionsfor(color).First(pos => this[pos].Type == type);
        }
        /*
         * In : -
         * Out: Counting
         * Do : Count the pieces on the board
         */
        public Counting CountPieces()
        {
            Counting counting = new Counting();
            foreach (Position pos in PiecePositions())
            {
                Piece piece = this[pos];
                counting.Increment(piece.Color, piece.Type);
            }
            return counting;
        }
        /*
         * In : -
         * Out: bool
         * Do : Check if there is insufficient material to checkmate
         */
        public bool InsufficientMaterial()
        {
            Counting counting = CountPieces();
            return IsKingBishopVSKing(counting) || IsKingBishopVSKingBishop(counting) ||
                   IsKingKnightVSKing(counting) || IsKingVSKing(counting);
        }
        /*
         * In : Counting
         * Out: bool
         * Do : Check if there is a king vs king
         */
        private static bool IsKingVSKing(Counting counting)
        {
            return counting.TotalCount == 2;
        }
        /*
         * In : Counting
         * Out: bool
         * Do : Check if there is a king and a bishop vs king
         */
        private static bool IsKingBishopVSKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.WhitePieces(PieceType.Bishop) == 1 || counting.BlackPieces(PieceType.Bishop) == 1);
        }
        /*
         * In : Counting
         * Out: bool
         * Do : Check if there is a king and a knight vs king
         */
        private static bool IsKingKnightVSKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.WhitePieces(PieceType.Knight) == 1 || counting.BlackPieces(PieceType.Knight) == 1);
        }
        /*
         * In : Counting
         * Out: bool
         * Do : Check if there is a king and a bishop vs king and a bishop (where the bishops are on the same color)
         */
        private bool IsKingBishopVSKingBishop(Counting counting)
        {
            if (counting.TotalCount != 4)
            {
                return false;
            }
            if (counting.WhitePieces(PieceType.Bishop) != 1 || counting.BlackPieces(PieceType.Bishop) != 1)
            {
                return false;
            }
            Position whiteBishopPos = FindPiece(Player.White, PieceType.Bishop);
            Position BlackBishopPos = FindPiece(Player.Black, PieceType.Bishop);

            return whiteBishopPos.SquareColor() == BlackBishopPos.SquareColor();
        }



    }
}
