namespace ChessLogic
{
    // Inheret from Piece
    public class Queen : Piece
    {
        // Properties
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

        /*
         * In : Player (Color of the piece)
         * Out: -
         * Do : Constructor to create a Bishop
         */
        public Queen(Player color)
        {
            this.Color = color;
        }

        /*
         * In : -
         * Out: A piece of the same kind
         * Do :  Copy a queen
         */
        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        /*
         * In : Position (Where the piece placed), Board (Game board)
         * Out: enumerator of moves 
         * Do: Get all the moves a queen can do
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }

    }
}
