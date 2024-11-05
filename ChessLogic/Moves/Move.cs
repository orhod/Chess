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
         * Out: -
         * Do : Abstract method to execute a move
         */
        public abstract void Execite(Board board);
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
