namespace ToonBlastPuzzle
{
    /// <summary>
    /// Gem dissolve data.
    /// It will need to hold pair value of coordinate and data for dissolve gem calculations.
    /// </summary>
    public struct GemDissolveData
    {
        public GemData gemData;
        public int x;
        public int y;

        public GemDissolveData(GemData gemData, int x, int y)
        {
            this.gemData = gemData;
            this.x = x;
            this.y = y;
        }

        public GemDissolveData(GemSlot gemSlot)
        {
            this.gemData = gemSlot.gemData;
            this.x = gemSlot.x;
            this.y = gemSlot.y;
        }
    }
}