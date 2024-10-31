namespace ChessLogic
{
    // Inheret from Piece
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }

        // Constractor
        public King(Player color)
        {
            this.Color = color;
        }

        // Copy Function To generate more pieces
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }

}
