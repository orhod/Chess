namespace ChessLogic
{
    // Inheret from Piece
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        // Constractor
        public Pawn (Player color)
        {
            this.Color = color;
        }
        // Copy Function To generate more pieces
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
