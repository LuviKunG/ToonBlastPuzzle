using System.Collections.Generic;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class PuzzleComboRuleData : ScriptableObject
    {
        public abstract List<GemSlot> GetCombo(ref GemSlot[,] gems, ref List<GemData> gemsData, GemDissolveData dissolveData);
    }
}