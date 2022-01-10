﻿using System.Collections;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class PuzzleGemRandomizer : ScriptableObject
    {
        public abstract IEnumerator InitializeAsync();
        public abstract void RandomizeGem(ref GemSlot[,] gems, out GemData gem);
    }
}