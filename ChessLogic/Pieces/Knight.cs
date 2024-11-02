namespace ChessLogic
{
    // Inheret from Piece
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        // Knight constractor
        public Knight(Player color)
        {
            this.Color = color;
        }

        // Copy Function To generate more pieces
        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        // Calculate all of the moves for knights
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
        // Retrun all the posible moves the knight can do
        private IEnumerable<Position> PossibleMoves(Position from,Board board) 
        {
            return PotentialMoves(from).Where(pos => Board.OnBoard(pos) && (board.IsEmpty(pos) || board[pos].Color != Color));
        }
        // Return all the the moves a knight can do
        public override IEnumerable<Move> GetMoves(Position from , Board board)
        {
            return PossibleMoves(from,board).Select(to => new NormalMove(from,to));
        }
    }
}
