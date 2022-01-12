using System.Collections;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class PuzzleGemRandomizerData : ScriptableObject
    {
        public abstract void RandomizeGem(ref GemSlot[,] gems, out GemData gem);
    }
}