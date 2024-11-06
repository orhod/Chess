namespace ChessLogic
{
    public class GameState
    {
        // Properties
        public Board Board {  get; }
        public Result Result { get; private set; } = null;
        public Player CurrentPlayer { get; private set; } 

        private int noCaptureOrPawnMove = 0;
        private string stateString;
        private readonly Dictionary<string, int> stateHistory = new Dictionary<string, int>();

        /*
         * In : Player (Current player), Board (Game board)
         * Out: -
         * Do : Constructor to create a game state
         */
        public GameState(Player player, Board board)
        {
            this.CurrentPlayer = player;
            this.Board = board;

            stateString = new StateString(Board, CurrentPlayer).ToString();
            stateHistory[stateString] = 1;

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
         * Do : Make a move and update the captured or pawn moved counter
         */
        public void MakeMove(Move move) 
        {
            Board.SetPawnSkipPosition(CurrentPlayer, null);
            bool captureOrPwanMove = move.Execite(Board);
            if (captureOrPwanMove)
            {
                noCaptureOrPawnMove = 0;
            }
            else
            {
                noCaptureOrPawnMove++;
            }
            CurrentPlayer = CurrentPlayer.Opponent();
            UpdateStateString();
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
            else if (Board.InsufficientMaterial())
            {
                Result = Result.Draw(EndReason.InsuffcientMaterial);
            }
            else if(FiftyMoveRule())
            {
                Result = Result.Draw(EndReason.FiftyMoveRule);
            }
            else if (ThreefoldRepetition())
            {
                Result = Result.Draw(EndReason.ThreefoldRepatition);
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
        /*
         * In : -
         * Out: Bool
         * Do : Check for the fifty move rule (50 moves for each player => 100 total)
         */
        private bool FiftyMoveRule()
        {
            return noCaptureOrPawnMove == 100;
        }
        /*
         * In : - 
         * Out: -
         * Do : Update the state string and add it to the state history dictionary
         *      If the state string already exists in the dictionary, increment the value
         */
        private void UpdateStateString()
        {
            stateString = new StateString(Board, CurrentPlayer).ToString();

            if (!stateHistory.ContainsKey(stateString))
            {
                stateHistory[stateString] = 1;
            }
            else
            {
                stateHistory[stateString]++;

            }
        }
        /*
         * In : -
         * Out: bool
         * Do : Check for threefold Repetition (same board state happend 3 time)
         */
        private bool ThreefoldRepetition()
        {
            return stateHistory[stateString] == 3;
        }
    }
}
