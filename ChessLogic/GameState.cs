using System.Numerics;

namespace ChessLogic
{
    public class GameState
    {
        // Properties
        public Board Board {  get; }
        public Result Result { get; private set; } = null;
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
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLeagal(Board));
        }
        /*
         * In : Move (The move to be made)
         * Out: -
         * Do : Make a move
         */
        public void MakeMove(Move move) 
        {
            Board.SetPawnSkipPosition(CurrentPlayer, null);
            move.Execite(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameover();
            
        }
        /*
         * In : Player
         * Out: IEnumerable<Move>
         * Do : Get all leagal moves for a player
         */
        public IEnumerable<Move> AllLeagalMovesFor(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionsfor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });
            return moveCandidates.Where(move => move.IsLeagal(Board));
        }
        /*
         * In : -
         * Out: -
         * Do : Check if game is over and change the resson to the currect one
         */
        private void CheckForGameover()
        {
            if (!AllLeagalMovesFor(CurrentPlayer).Any())
            {
                if (Board.IsInCheck(CurrentPlayer))
                {
                    Result = Result.Win(CurrentPlayer.Opponent());
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
        }
        /*
         * In : -
         * Out: bool
         * Do : Check if Game is over
         */
        public bool IsGameOver()
        {
            return Result != null;
        }
    }
}
