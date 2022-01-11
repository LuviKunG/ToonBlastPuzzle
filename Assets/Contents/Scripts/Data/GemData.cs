namespace ToonBlastPuzzle
{
    /// <summary>
    /// Gem Data.
    /// </summary>
    public struct GemData
    {
        public GemColor color;
        public GemPower power;

        public GemData(GemColor color, GemPower power)
        {
            this.color = color;
            this.power = power;
        }

        public void Reset()
        {
            color = GemColor.None;
            power = GemPower.None;
        }

        public bool IsValid()
        {
            if (power == GemPower.None && color == GemColor.None)
                return false;
            return true;
        }

        public override string ToString()
        {
            return $"{{color:{color},power:{power}}}";
        }
    }
}