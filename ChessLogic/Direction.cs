namespace ChessLogic
{
    public class Direction
    {
        public int RowDel { get; }
        public int ColumnDel { get; }


        // Set readonly of all Directions on the board
        // Stragiht moves
        public readonly static Direction Up = new Direction(-1, 0);
        public readonly static Direction Down = new Direction(1, 0);
        public readonly static Direction Left = new Direction(0, -1);
        public readonly static Direction Right = new Direction(0, 1);
        // Diagonal moves
        public readonly static Direction UpLeft = Up + Left;
        public readonly static Direction UpRight = Up + Right;
        public readonly static Direction DownLeft = Down + Left;
        public readonly static Direction DownRight = Down + Right;

        // Constractor
        public Direction(int rowDel, int columnDel)
        {
            this.RowDel = rowDel;
            this.ColumnDel = columnDel;
        }
        // Overidre the operators of + and * to return new direction
        public static Direction operator +(Direction dir1, Direction dir2)
        {
            return new Direction(dir1.RowDel + dir2.RowDel, dir1.ColumnDel + dir2.ColumnDel);
        }
        public static Direction operator *(int scalar, Direction dir)
        {
            return new Direction(scalar * dir.RowDel, scalar * dir.ColumnDel);
        }

    }
}
