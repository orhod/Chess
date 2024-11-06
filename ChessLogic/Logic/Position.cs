namespace ChessLogic
{
    public class Position
    {
        // Properties
        public int Row { get; }
        public int Column { get; }
        /*
         * In : int (row), int (column)
         * Out: -
         * Do : Constructor to create a position
         */
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        /*
         * In : -
         * Out: Player (The color of the square)
         * Do : Return the color of the square
         */
        public Player SquareColor()
        {
            if ((Row + Column) % 2 == 0)
            {
                return Player.White;
            }
            else
            {
                return Player.Black;
            }
        }
        /*
         * In : object
         * Out: Boolean
         * Do : Check if two positions are equal
         */
        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }
        /*
         * In : -
         * Out: int
         * Do : Get the hash code of the position
         */
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
        /*
         * In : Position (The first position), Position (The second position)
         * Out: Boolean
         * Do : Check if two positions are equal
         */
        public static bool operator ==(Position first, Position second)
        {
            return EqualityComparer<Position>.Default.Equals(first, second);
        }
        /*
         * In : Position (The first position), Position (The second position)
         * Out: Boolean
         * Do : Check if two positions are not equal
         */
        public static bool operator !=(Position first, Position second)
        {
            return !(first == second);
        }
        /*
         * In : Position (The position), Direction (The direction)
         * Out: Position
         * Do : Add a direction to a position to get a new position
         */
        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.Row + dir.RowDel, pos.Column + dir.ColumnDel);
        }
    }
}
