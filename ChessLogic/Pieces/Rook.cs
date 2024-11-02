namespace ChessLogic
{
    // Inheret from Piece
    public class Rook : Piece
    {
        // Properties
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }
        private static readonly Direction[] dirs =
        [
            Direction.Up,
            Direction.Right,
            Direction.Left,
            Direction.Down
        ];

        /*
         * In : Player (Color of the piece)
         * Out: -
         * Do : Constructor to create a rook
         */
        public Rook(Player color)
        {
            this.Color = color;
        }

        /*
         * In : -
         * Out: A piece of the same kind
         * Do :  Copy a rook
         */
        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        /*
         * In : Position (Where the piece placed), Board (Game board)
         * Out: enumerator of moves 
         * Do: Get all the moves a rook can do
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
