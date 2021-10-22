using System;

namespace HexGrid
{
    public readonly partial struct HexInt
    {
        public bool Valid => X + Y + Z == 0;

        public HexInt GetNeighbour(int direction)
        {
            if (direction is < 0 or >= 6)
                throw new ArgumentOutOfRangeException(nameof(direction));
            return this + Directions.Span[direction];
        }

        public HexInt GetDiagonal(int direction)
        {
            if (direction is < 0 or >= 6)
                throw new ArgumentOutOfRangeException(nameof(direction));
            return this + Diagonals.Span[direction];
        }

        public static ReadOnlyMemory<HexInt> Directions { get; } =
            new []
            {
                new HexInt(+1, -1, 0), new HexInt(+1, 0, -1), new HexInt(0, +1, -1),
                new HexInt(-1, +1, 0), new HexInt(-1, 0, +1), new HexInt(0, -1, +1),
            };
        
        public static ReadOnlyMemory<HexInt> Diagonals { get; } =
            new []
            {
                new HexInt(+2, -1, -1), new HexInt(+1, +1, -2), new HexInt(-1, +2, -1),
                new HexInt(-2, +1, +1), new HexInt(-1, -1, +2), new HexInt(+1, -2, +1),
            };

        public static implicit operator HexFloat(HexInt value)
            => new HexFloat(value.X, value.Y, value.Z);
        
        public (int col, int row) ToOffset(OffsetCoordinateSystem system)
        {
            return system switch
            {
                OffsetCoordinateSystem.OddR => (X + (Z - (Z & 1)) / 2, Z),
                OffsetCoordinateSystem.EvenR => (X + (Z + (Z & 1)) / 2, Z),
                OffsetCoordinateSystem.OddQ => (X, Z + (X - (X & 1)) / 2),
                OffsetCoordinateSystem.EvenQ => (X, Z + (X + (X & 1)) & 2),
                _ => throw new NotImplementedException(),
            };
        }

        public static HexInt FromOffset(int col, int row, OffsetCoordinateSystem system)
        {
            return system switch
            {
                OffsetCoordinateSystem.OddR => FromAxial(col - (row - (row&1)) / 2, row),
                OffsetCoordinateSystem.EvenR => FromAxial(col - (row + (row&1)) / 2, row),
                OffsetCoordinateSystem.OddQ => FromAxial(col, row - (col - (col&1)) / 2),
                OffsetCoordinateSystem.EvenQ => FromAxial(col, row - (col + (col&1)) / 2),
                _ => throw new NotImplementedException(),
            };
        }

        public static HexInt FromOffset((int col, int row) p, OffsetCoordinateSystem system)
            => FromOffset(p.col, p.row, system);
        
        public static ReadOnlyMemory<HexInt> GetLine(HexInt start, HexInt end)
        {
            var n = Distance(start, end);
            var fraction = (HexFloat)(end - start) * (1f / n);
            HexFloat pos = start;
            Memory<HexInt> result = new HexInt[n + 1];
            result.Span[0] = start;
            for (int i = 1; i < n; ++i)
            {
                pos += fraction;
                result.Span[i] = pos.Round();
            }
            result.Span[n] = end;
            return result;
        }

        public static ReadOnlyMemory<HexInt> GetRange(HexInt pos, int range)
            => new HexRange(pos, range).GetPoints();
    }
}