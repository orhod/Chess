namespace ChessLogic
{
    public class Result
    {
        // Properties
        public Player Winner { get; }
        public EndReason Reason { get; }

        public Result(Player winner, EndReason Reason)
        {
            this.Winner = winner;
            this.Reason = Reason;
        }
        public static Result Win(Player winner)
        {
            return new Result(winner, EndReason.Checkmate);
        }
        public static Result Draw(EndReason reason)
        {
            return new Result(Player.None, reason);
        }

    }
}
