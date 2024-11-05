namespace ChessLogic
{
    internal class NormalMove : Move
    {
        // Properties
        public override MoveType Type => MoveType.Normal;
        public override Position FromPos { get; }
        public override Position ToPos { get; }
        /*
         * In : Position (Where the piece is), Position (Where the piece should be)
         * Out: -
         * Do : Constructor to create a normal move
         */
        public NormalMove(Position fromPos, Position toPos)
        {
            this.FromPos = fromPos;
            this.ToPos = toPos;
        }
        /*
         * In : Board (Game board)
         * Out: bool
         * Do : Execute a move and return if a capture was made or a pawn moved
         */
        public override bool Execite(Board board)
        {
            Piece piece = board[FromPos];
            bool campture = !board.IsEmpty(ToPos);
            board[ToPos] = piece;
            board[FromPos] = null;
            piece.HasMoved = true;

            return campture || piece.Type == PieceType.Pawn;
        }
    }
}
