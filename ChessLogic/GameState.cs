namespace ChessLogic
{
    // Current game state
    public class GameState
    {
        public Board Board {  get; }
        public Player CurrentPlayer { get; private set; }

        public GameState(Player player, Board board)
        {
            this.CurrentPlayer = player;
            this.Board = board;
        }
    }
}
