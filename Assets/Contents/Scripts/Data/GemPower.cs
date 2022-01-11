namespace ToonBlastPuzzle
{
    /// <summary>
    /// Gem Blast or Gem Power.
    /// </summary>
    public enum GemPower : byte
    {
        /// <summary>
        /// No Special Power.
        /// </summary>
        None,
        /// <summary>
        /// Blast every gems on horizontal.
        /// </summary>
        Horizontal,
        /// <summary>
        /// Blast every gems on vertical.
        /// </summary>
        Vertical,
        /// <summary>
        /// Blast every gems on both horizontal and vertical.
        /// </summary>
        Axis,
        /// <summary>
        /// Blast every gems around.
        /// </summary>
        Bomb,
        /// <summary>
        /// Blast every gems that color is matched.
        /// </summary>
        Color,
    }
}