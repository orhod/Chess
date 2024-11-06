namespace ChessLogic
{
    public class DoublePawn : Move
    {
        // Properties
        public override MoveType Type => MoveType.DoublePawn;
        public override Position FromPos { get; }
        public override Position ToPos { get; }
        private readonly Position skippedPos;
        /*
         * In : Position from, Position to
         * Out: -
         * Do : Constructor for the DoublePawn move
         */
        public DoublePawn(Position from, Position to)
        {
            FromPos = from;
            ToPos = to;
            skippedPos = new Position((from.Row + to.Row) / 2, from.Column);
        }
        /*
         * In : Board
         * Out: bool (ture) (pawn was moved)
         * Do : Execute the DoublePawn move
         */
        public override bool Execite(Board board)
        {
            Player player = board[FromPos].Color;
            board.SetPawnSkipPosition(player, skippedPos);
            new NormalMove(FromPos, ToPos).Execite(board);
            return true;
        }
    }
}
