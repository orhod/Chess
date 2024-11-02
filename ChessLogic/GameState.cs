namespace ChessLogic
{
    public class GameState
    {
        // Properties

        public Board Board {  get; }
        public Player CurrentPlayer { get; private set; }
        /*
         * In : Player (Current player), Board (Game board)
         * Out: -
         * Do : Constructor to create a game state
         */
        public GameState(Player player, Board board)
        {
            this.CurrentPlayer = player;
            this.Board = board;
        }
        /*
         * In : Position (Where the piece is placed)
         * Out: enumerator of moves
         * Do : Get all the legal moves for a piece
         */
        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if(Board.IsEmpty(pos)|| Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }
            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        }
        /*
         * In : Move (The move to be made)
         * Out: -
         * Do : Make a move
         */
        public void MakeMove(Move move) 
        { 
            move.Execite(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
        }
    }
}
