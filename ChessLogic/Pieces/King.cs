namespace ChessLogic
{
    // Inheret from Piece
    public class King : Piece
    {
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

        // Constractor
        public King(Player color)
        {
            this.Color = color;
        }

        // Copy Function To generate more pieces
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        // Return All positions a king can move to
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
        // Return the possible moves the king
        public override IEnumerable<Move> GetMoves(Position from , Board board)
        {
            foreach(Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from , to);
            }
            
        }
    }

}
