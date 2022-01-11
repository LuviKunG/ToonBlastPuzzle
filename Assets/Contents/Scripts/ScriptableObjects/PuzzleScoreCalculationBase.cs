﻿using System.Collections.Generic;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class PuzzleScoreCalculationBase : ScriptableObject
    {
        public abstract int CalculateScore(ref List<GemSlot> list, int dissolveComboCount);
    }
}