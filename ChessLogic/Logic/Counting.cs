namespace ChessLogic
{
    public class Counting
    {
        // properties
        private readonly Dictionary<PieceType, int> whiteCount = new Dictionary<PieceType, int>();
        private readonly Dictionary<PieceType, int> blackCount = new Dictionary<PieceType, int>();
        // Total count of pieces
        public int TotalCount { get; private set; }
        /*
         * In : -
         * Out: -
         * Do : Constructor
         */
        public Counting()
        {
            foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                whiteCount[type] = 0;
                blackCount[type] = 0;
            }
        }
        /*
         * In : Player Color, PieceType
         * Out: -
         * Do : Increment the count of the pieces
         */
        public void Increment(Player color, PieceType type)
        {
            if (color == Player.White)
            {
                whiteCount[type]++;
            }
            else if (color == Player.Black)
            {
                blackCount[type]++;
            }

            TotalCount++;
        }
        /*
         * In : PieceType
         * Out: int
         * Do : Get the count of the white pieces
         */
        public int WhitePieces(PieceType type)
        {
            return whiteCount[type];
        }
        /*
         * In : PieceType
         * Out: int
         * Do : Get the count of the black pieces
         */
        public int BlackPieces(PieceType type)
        {
            return blackCount[type];
        }

    }
}
