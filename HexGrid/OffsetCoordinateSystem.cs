namespace HexGrid
{
    /// <summary>
    /// Describes how an offset coordinate system is aligned
    /// </summary>
    public enum OffsetCoordinateSystem : byte
    {
        /// <summary>
        /// shoves odd rows by +½ column
        /// </summary>
        OddR,
        /// <summary>
        /// shoves even rows by +½ column
        /// </summary>
        EvenR,
        /// <summary>
        /// shoves odd columns by +½ row
        /// </summary>
        OddQ,
        /// <summary>
        /// shoves even columns by +½ row
        /// </summary>
        EvenQ,
    }
}