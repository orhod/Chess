namespace ChessLogic
{
    // Inheret from Piece
    public class Knight : Piece
    {
        // Properties
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        /*
         * In : Player (Color of the piece)
         * Out: -
         * Do : Constructor to create a Knight
         */
        public Knight(Player color)
        {
            this.Color = color;
        }

        /*
         * In : -
         * Out: A piece of the same kind
         * Do :  Copy a knight
         */
        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        /*
         * In : Position (Where the piece placed), Board (Game board)
         * Out: enumerator of positions
         * Do : Check all the potential positions a knight can move to
         */
        private static IEnumerable<Position> PotentialMoves(Position from)
        {
            foreach(Direction vDir in new Direction[] {Direction.Up, Direction.Down}) 
            { 
                foreach(Direction hDire in new Direction[] {Direction.Left, Direction.Right })
                {
                    yield return from + 2 * vDir + hDire;
                    yield return from + 2 * hDire + vDir;  
                }
            }
        }
        /*
         * In : Position (Where the piece placed), Board (Game board)
         * Out: enumerator of positions 
         * Do : Get all possible moves the knight can do
         */
        private IEnumerable<Position> PossibleMoves(Position from,Board board) 
        {
            return PotentialMoves(from).Where(pos => Board.OnBoard(pos) && (board.IsEmpty(pos) || board[pos].Color != Color));
        }
        /*
         * In : Position (Where the piece placed), Board (Game board)
         * Out: enumerator of moves
         * Do: Get all the moves a knight can do
         */
        public override IEnumerable<Move> GetMoves(Position from , Board board)
        {
            return PossibleMoves(from,board).Select(to => new NormalMove(from,to));
        }
    }
}
