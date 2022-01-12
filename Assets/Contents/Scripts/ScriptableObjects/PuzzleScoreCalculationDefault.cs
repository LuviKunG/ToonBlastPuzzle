using System.Collections.Generic;
using UnityEngine;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Score Calculation/Default")]
    public sealed class PuzzleScoreCalculationDefault : PuzzleScoreCalculationData
    {
        [SerializeField]
        private int m_scoreBlock;
        [SerializeField]
        private float m_scoreMultiplierPerDissolveCombo;

        public override int CalculateScore(ref List<GemSlot> list, int dissolveComboCount)
        {
            if (list == null || list.Count == 0)
                return 0;
            return Mathf.FloorToInt((list.Count * m_scoreBlock) * dissolveComboCount);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (m_scoreBlock < 0)
                m_scoreBlock = 0;
            if (m_scoreMultiplierPerDissolveCombo < 1.0f)
                m_scoreMultiplierPerDissolveCombo = 1.0f;
        }
#endif
    }
}