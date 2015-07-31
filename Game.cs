using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfFifteen
{
    class Game
    {
        public int[,] Grid { get; set; }

        public int MoveCount { get; private set; }
        public Position CurrentPosition { get; private set; }

        public static readonly Position Up = new Position {X = 0, Y = 1};
        public static readonly Position Down = new Position { X = 0, Y = -1 };
        public static readonly Position Left = new Position { X = 1, Y = 0 };
        public static readonly Position Right = new Position { X = -1, Y = 0 };

        public Game(int dimension = 4)
        {
            this.Grid = new int[dimension,dimension];

            FillGrid();

            this.CurrentPosition = new Position(this.Width - 1, this.Height - 1);
        }

        public bool IsPositionCorrect(int x, int y)
        {
            return GetCorrectValue(x, y) == this[x, y];
        }

        public int GetCorrectValue(int x, int y)
        {
            if (x == Width - 1 && y == Height - 1)
                return -1;

            return this.Width*y + x + 1;
        }

        private void FillGrid()
        {
            foreach (var cell in Cells)
            {
                this[cell.X, cell.Y] = this.GetCorrectValue(cell.X, cell.Y);
            }
        }

        public static Game NewGame(int gridSize = 4)
        {
            var game = new Game(gridSize);
            game.Scramble(7 * game.Width * game.Height);
            game.MoveCount = 0;
            return game;
        }

        public void Scramble(int count = 100)
        {
            var random = new Random();

            var directions = new[]
            {
                Up, 
                Down, 
                Left, 
                Right
            };

            while (count > 0)
            {
                count--;

                var d = directions[random.Next(4)];

                this.MoveBy(d.X, d.Y);
            }
        }

        public bool MoveBy(int x, int y)
        {
            return MoveBy(new Position {X = x, Y = y});
        }

        public bool MoveBy(Position moveBy)
        {
            var current = this.CurrentPosition;
            var newPos = current + moveBy;
            if (!CanMoveTo(newPos))
                return false;

            this.MoveCount++;
            var source = this[current];
            var dest = this[newPos];


            this[current] = dest;
            this[newPos] = source;

            this.CurrentPosition = newPos;

            return true;
        }

        public int this[Position p]
        {
            get { return this[p.X, p.Y]; }
            set { this[p.X, p.Y] = value; }
        }

        public int this[int x, int y]
        {
            get { return this.Grid[x, y]; }
            set { this.Grid[x, y] = value; }
        }

        public bool Won()
        {
            return Cells.All(n => IsPositionCorrect(n.X, n.Y));
        }

        public int Height
        {
            get { return this.Grid.GetUpperBound(1) + 1; }
        }

        public int Width
        {
            get { return this.Grid.GetUpperBound(0) + 1; }
        }
        
        private bool CanMoveTo(Position pos)
        {
            return pos.X < Width && pos.Y < Height &&
                   pos.X >= 0 && pos.Y >= 0;
        }

        private IEnumerable<Position> Cells
        {
            get
            {
                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        yield return new Position(x, y);
                    }
                }
            }
        }
    }
}