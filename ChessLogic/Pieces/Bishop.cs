namespace ChessLogic
{
    // Inheret from Piece
    public class Bishop : Piece
    {
        // Properties
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }
        private static readonly Direction[] dirs =
        [
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
        public Bishop(Player color)
        {
            this.Color = color;
        }
        /*
         * In : -
         * Out: A piece of the same kind
         * Do :  Copy a bishop
         */
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        /*
         * In : Position (Where the piece placed), Board (Game board)
         * Out: enumerator of moves
         * Do: Get all the moves a bishop can do
         */
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }


    }
}
