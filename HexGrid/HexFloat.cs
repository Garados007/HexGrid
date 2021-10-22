using System;

namespace HexGrid
{
    public readonly partial struct HexFloat
    {
        public bool Valid => Math.Abs(X + Y + Z) <= 1e-6;

        public static HexFloat Lerp(HexFloat a, HexFloat b, float t)
            => new HexFloat(
                Lerp(a.X, b.X, t),
                Lerp(a.Y, b.Y, t),
                Lerp(a.Z, b.Z, t)
            );

        private static float Lerp(float a, float b, float t)
            => a + (b - a) * t;

        public HexInt Round()
            => new HexInt(
                (int)Math.Round(X), 
                (int)Math.Round(Y), 
                (int)Math.Round(Z)
            );

        public HexInt Floor()
            => new HexInt(
                (int)Math.Floor(X), 
                (int)Math.Floor(Y), 
                (int)Math.Floor(Z)
            );

        public HexInt Ceiling()
            => new HexInt(
                (int)Math.Ceiling(X), 
                (int)Math.Ceiling(Y), 
                (int)Math.Ceiling(Z)
            );
        
        public HexInt Truncate()
            => new HexInt(
                (int)Math.Truncate(X), 
                (int)Math.Truncate(Y), 
                (int)Math.Truncate(Z)
            );
        
        public static explicit operator HexInt(HexFloat value)
            => value.Round();
    }
}