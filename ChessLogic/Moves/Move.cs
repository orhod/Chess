namespace ChessLogic
{
    // abstract class
    public abstract class Move
    {
        // Properties
        public abstract MoveType Type { get; }
        public abstract Position FromPos { get; }
        public abstract Position ToPos { get; }
        /*
         * In : Board (Game board)
         * Out: bool
         * Do : Abstract method to execute a move 
         *      bool value is used to check if a capture was made or a pawn moved
         */
        public abstract bool Execite(Board board);
        /*
         * In : Board
         * Out: bool
         * Do : Check if A move is leagal
         */
        public virtual bool IsLeagal(Board board)
        {
            Player player = board[FromPos].Color;
            Board boardCopy = board.copy();
            Execite(boardCopy);
            return !boardCopy.IsInCheck(player);
        }
    }
}
