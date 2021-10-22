using System;
using System.Linq;

namespace HexGrid
{
    public readonly struct HexRange
    {
        public HexInt Pos { get; }

        public int Range { get; }

        public HexRange(HexInt pos, int range)
        {
            if (range < 0)
                throw new ArgumentOutOfRangeException(nameof(range));
            (Pos, Range) = (pos, range);
        }

        public ReadOnlyMemory<HexInt> GetPoints()
        {
            Memory<HexInt> result = new HexInt[3 * Range * (1 + Range) + 1];
            int i = 0;
            for (int x = -Range; x <= Range; ++x)
            {
                var yMax = Math.Min(Range, Range - x);
                for (int y = Math.Max(-Range, -Range - x); y <= yMax; ++y)
                {
                    result.Span[i++] = Pos + new HexInt(x, y, -x - y);
                }
            }
            return result;
        }

        public static ReadOnlyMemory<HexInt> Intersect(params HexRange[] pos)
        {
            if (pos.Length == 0)
                return ReadOnlyMemory<HexInt>.Empty;
            var maxRange = pos.Max(p => p.Range);
            
            Memory<HexInt> result = new HexInt[3 * maxRange * (1 + maxRange) + 1];
            int i = 0;

            var xMin = pos.Max(p => p.Pos.X - p.Range);
            var xMax = pos.Min(p => p.Pos.X + p.Range);
            var yMin = pos.Max(p => p.Pos.Y - p.Range);
            var yMax = pos.Min(p => p.Pos.Y + p.Range);
            var zMin = pos.Max(p => p.Pos.Z - p.Range);
            var zMax = pos.Min(p => p.Pos.Z + p.Range);

            for (int x = xMin; x <= xMax; ++x)
            {
                var yLimit = Math.Min(yMax, -x - zMin);
                for (int y = Math.Max(yMin, -x - zMax); y <= yLimit; ++y)
                    result.Span[i++] = new HexInt(x, y, -x - y);
            }

            return result[..i];
        }
    }
}