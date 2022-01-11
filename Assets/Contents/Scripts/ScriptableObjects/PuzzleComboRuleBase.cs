using System.Collections.Generic;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class PuzzleComboRuleBase : ScriptableObject
    {
        public abstract List<GemSlot> GetCombo(ref GemSlot[,] gems, ref List<GemData> gemsData, GemDissolveData dissolveData);
    }
}