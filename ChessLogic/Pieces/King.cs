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
            
        }
    }

}
