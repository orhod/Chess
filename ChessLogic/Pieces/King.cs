namespace ChessLogic
{
    // Inheret from Piece
    public class King : Piece
    {
        // Properties
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        private static readonly Direction[] dirs =
        [
            Direction.Up,
            Direction.Right,
            Direction.Left,
            Direction.Down,
            Direction.UpLeft,
            Direction.UpRight,
            Direction.DownLeft,
            Direction.DownRight
        ];
        /*
         * In : Player (Color of the piece)
         * Out: -
         * Do : Constructor to create a Knight
         */
        public King(Player color)
        {
            this.Color = color;
        }
        /*
         * In : postion , board
         * Out: bool
         * Do : Check if There is a rook that can castle
         */
        private static bool IsUnMovedRook(Position pos, Board board)
        {
            if (board.IsEmpty(pos))
            {
                return false;
            }
            Piece piece = board[pos];
            return piece.Type == PieceType.Rook && !piece.HasMoved;
        }
        /*
         * In : IEnumrable of positions , board
         * Out: bool
         * Do : Check if all the positions are empty in the way of the castle
         */
        private static bool AllEmpty(IEnumerable<Position> positions, Board board)
        {
            return positions.All(pos => board.IsEmpty(pos));
        }
        /*
         * In : Position , Board
         * Out: bool
         * Do: Check if the king can castle to king side
         */
        private bool CanCastleKingSide(Position from , Board board)
        {
            if (HasMoved)
            {
                return false;
            }
            Position rookPos = new Position(from.Row, 7);
            Position[] betweenPositions = { new Position(from.Row, 5), new Position(from.Row, 6) };

            return IsUnMovedRook(rookPos,board) && AllEmpty(betweenPositions, board);

        }
        /*
         * In : Position , Board
         * Out: bool
         * Do: Check if the king can castle to queen side
         */
        private bool CanCastleQueenSide(Position from, Board board)
        {
            if (HasMoved)
            {
                return false;
            }
            Position rookPos = new Position(from.Row, 0);
            Position[] betweenPositions = { new Position(from.Row, 1), new Position(from.Row, 2), new Position(from.Row, 3)};

            return IsUnMovedRook(rookPos, board) && AllEmpty(betweenPositions, board);

        }

        /*
         * In : -
         * Out: A piece of the same kind
         * Do :  Copy a king
         */
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        /*
         * In : Position (Where the piece placed), Board (Game board)
         * Out: enumerator of positions
         * Do : Check all the potential positions a king can move to
         */
        private IEnumerable<Position> MovePositions(Position from , Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if (!Board.OnBoard(to))
                {
                    continue;
                }
                if(board.IsEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }
        /*
         * In : Position (Where the piece placed), Board (Game board)
         * Out: enumerator of moves
         * Do: Get all the moves a king can do
         */
        public override IEnumerable<Move> GetMoves(Position from , Board board)
        {
            foreach(Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from , to);
            }
            if (CanCastleKingSide(from, board))
            {
                yield return new Castle(MoveType.CastleKS, from);
            }
            if (CanCastleQueenSide(from, board))
            {
                yield return new Castle(MoveType.castleQS, from);

            }

        }
        /*
         * In : Position , Board
         * Out: bool
         * Do : Check if the king can captucre an opponent king
         */
        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return MovePositions(from, board).Any(to =>
            {
                Piece piece = board[to];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }

}
