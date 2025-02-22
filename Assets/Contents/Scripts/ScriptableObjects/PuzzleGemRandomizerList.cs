﻿using System.Collections;
using UnityEngine;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Rules Gem Randomizer/List")]
    public sealed class PuzzleGemRandomizerList : PuzzleGemRandomizerData
    {
        [SerializeField]
        private GemColor[] availableColors;

        public override void RandomizeGem(ref GemSlot[,] gems, out GemData gem)
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