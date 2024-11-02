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
        // Return the leagal moves for a piece in position
        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if(Board.IsEmpty(pos)|| Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }
            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        }
        // Makeing move
        public void MakeMove(Move move) 
        { 
            move.Execite(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
        }
    }
}
