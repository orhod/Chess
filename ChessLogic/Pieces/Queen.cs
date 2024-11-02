namespace ChessLogic
{
    // Inheret from Piece
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen;
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
        public Queen(Player color)
        {
            this.Color = color;
        }

        // Copy Function To generate more pieces
        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        // Return All possible moves for the Queen
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }

    }
}
