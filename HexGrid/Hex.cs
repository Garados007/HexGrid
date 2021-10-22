using System;

namespace HexGrid
{
    /// <summary>
    /// A single coordinate in the Hex Grid. This use cubic coordinates internaly.
    /// </summary>
    public readonly partial struct Hex<T>
        where T : struct
    {
        public T X { get; }

        public T Y { get; }

        public T Z { get; }

        public Hex(T x, T y, T z)
        {
            (X, Y, Z) = (x, y, z);
        }

        public static Hex<T> operator +(Hex<T> left, Hex<T> right)
        {
            return new Hex<T>();
        }

        public static Hex<T> Zero { get; } = new Hex<T>();
    }
}
