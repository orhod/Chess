namespace ChessLogic
{
    // Inheret from Piece
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        // Directions a bishop can move
        private static readonly Direction[] dirs =
        [
            Direction.UpLeft,
            Direction.UpRight,
            Direction.DownLeft,
            Direction.DownRight
        ];

        // Constractor
        public Bishop(Player color)
        {
            this.Color = color;
        }

        // Copy Function To generate more pieces
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        // Return All possible moves for the Bishop
        public override IEnumerable<Move> GetMoves(Position from ,Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));   
        }


    }
}
