namespace ChessLogic
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        // Set position
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        // Getter of squareColor
        public Player SquareColor()
        {
            if((Row + Column) % 2 == 0)
            {
                return Player.White;
            }
            else
            {
                return Player.Black;
            }
        }
        // Hash code
        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
        // Overload the + operator to retrun a new positon
        public static Position operator +(Position pos,Direction dir)
        {
            return new Position(pos.Row + dir.RowDel, pos.Column + dir.ColumnDel);
        }
    }
}
