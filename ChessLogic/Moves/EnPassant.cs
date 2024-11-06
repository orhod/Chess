namespace ChessLogic
{
    public class EnPassant : Move
    {
        // Properties
        public override MoveType Type => MoveType.EnPassant;
        public override Position FromPos { get; }
        public override Position ToPos { get; }
        private readonly Position capturedPos;
        /*
         * In : Position from, Position to
         * Out: -
         * Do : Constructor for the EnPassant move
         */
        public EnPassant(Position from, Position to)
        {
            FromPos = from;
            ToPos = to;
            capturedPos = new Position(from.Row, to.Column);
        }
        /*
         * In : Board
         * Out: bool (ture) (capture was made)
         * Do : Execute the EnPassant move
         */
        public override bool Execite(Board board)
        {
            board[capturedPos] = null;
            new NormalMove(FromPos, ToPos).Execite(board);
            return true;
        }
    }
}
