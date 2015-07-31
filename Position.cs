namespace GameOfFifteen
{
    struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
            };
        }

        public override string ToString()
        {
            return string.Format("Position[{0}, {1}]", X, Y);
        }
    }
}