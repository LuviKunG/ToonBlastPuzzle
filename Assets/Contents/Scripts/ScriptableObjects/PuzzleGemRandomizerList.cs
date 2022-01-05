using UnityEngine;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Rules Gem Randomizer/List")]
    public sealed class PuzzleGemRandomizerList : PuzzleGemRandomizer
    {
        [SerializeField]
        private GemColor[] availableColors;

        public override void Initialize()
        {
            // Do nothing! this class don't want to be initialize.
        }

        public override void RandomizeGem(ref GemData[,] gems, out GemData gem)
        {
            gem = new GemData();
            gem.color = GetRandomColor();
        }

        private GemColor GetRandomColor()
        {
            int randomIndex = Random.Range(0, availableColors.Length);
            return availableColors[randomIndex];
        }
    }
}