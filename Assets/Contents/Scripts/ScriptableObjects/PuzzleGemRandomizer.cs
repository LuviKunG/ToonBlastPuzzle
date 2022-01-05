using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class PuzzleGemRandomizer : ScriptableObject
    {
        public abstract void Initialize();
        public abstract void RandomizeGem(ref GemData[,] gems, out GemData gem);
    }
}