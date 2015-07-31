using System;

namespace GameOfFifteen
{
    class ConsoleColorSetter : IDisposable
    {
        private readonly ConsoleColor _originalColor;

        public ConsoleColorSetter(ConsoleColor color)
        {
            _originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public void Dispose()
        {
            Console.ForegroundColor = _originalColor;
        }
    }
}