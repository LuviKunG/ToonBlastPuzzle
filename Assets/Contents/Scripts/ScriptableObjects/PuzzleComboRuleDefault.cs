using System.Collections.Generic;
using UnityEngine;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Combo Rules/Default")]
    public sealed class PuzzleComboRuleDefault : PuzzleComboRuleBase
    {
        [SerializeField]
        private int m_minimalDissolveColorCount = default;
        [SerializeField]
        private int m_minimalPowerColorCount = default;
        [SerializeField]
        private int m_minimalPowerAxisCount = default;

        public override List<GemSlot> GetCombo(ref GemSlot[,] gems, ref List<GemData> gemsData, GemDissolveData dissolveData)
        {
            int x = dissolveData.x, y = dissolveData.y;
            int width = gems.GetLength(0), height = gems.GetLength(1);
            List<GemSlot> slots = new List<GemSlot>();
            switch (dissolveData.gemData.power)
            {
                case GemPower.None:
                    // Dissolve all connected color gems.
                    GetComboColor(ref gems, ref slots, x, y, width, height);
                    // If dissolve is success more than m_minimalDissolveColorCount, return all dissolve success slots, otherwise return null as failed.
                    if (slots.Count >= m_minimalDissolveColorCount)
                    {
                        // If dissolve is success more than m_minimalPowerColorCount, spawn power color with the same color.
                        if (slots.Count >= m_minimalPowerColorCount)
                            gemsData.Add(new GemData(slots[0].gemData.color, GemPower.Color));
                        // But if dissolve is success more than m_minimalPowerAxisCount, spawn power axis.
                        else if (slots.Count >= m_minimalPowerAxisCount)
                            gemsData.Add(new GemData(GemColor.None, GemPower.Axis));
                        return slots;
                    }
                    else
                        return null;
                case GemPower.Horizontal:
                    // Dissolve all horizontal gems.
                    for (int i = 0; i < width; ++i)
                        if (gems[i, y].IsValid() && !slots.Contains(gems[i, y]))
                            slots.Add(gems[i, y]);
                    return slots;
                case GemPower.Vertical:
                    // Dissolve all vertical gems.
                    for (int i = 0; i < height; ++i)
                        if (gems[x, i].IsValid() && !slots.Contains(gems[x, i]))
                            slots.Add(gems[x, i]);
                    return slots;
                case GemPower.Axis:
                    // Dissolve all horizontal & vertical gems.
                    for (int i = 0; i < width; ++i)
                        if (gems[i, y].IsValid() && !slots.Contains(gems[i, y]))
                            slots.Add(gems[i, y]);
                    for (int i = 0; i < height; ++i)
                        if (gems[x, i].IsValid() && !slots.Contains(gems[x, i]))
                            slots.Add(gems[x, i]);
                    return slots;
                case GemPower.Bomb:
                    // Dissolve all gems around target gems.
                    return slots;
                case GemPower.Color:
                    // Dissolve all same normal color gems.
                    slots.Add(gems[x, y]);
                    for (int dy = 0; dy < height; ++dy)
                        for (int dx = 0; dx < width; ++dx)
                            if (gems[dx, dy].IsValid() && gems[dx, dy].gemData.power == GemPower.None && dissolveData.gemData.color == gems[dx, dy].gemData.color)
                                slots.Add(gems[dx, dy]);
                    return slots;
                default:
                    return slots;
            }
        }

        /// <summary>
        /// This is stack function is reference base for calculate all combo color nearby the target slots. 
        /// </summary>
        /// <param name="gems">Reference of gem board.</param>
        /// <param name="slots">Cache list that already has some dissolve gems.</param>
        /// <param name="x">Axis X.</param>
        /// <param name="y">Axis Y.</param>
        /// <param name="width">Width of board.</param>
        /// <param name="height">Height of board.</param>
        private void GetComboColor(ref GemSlot[,] gems, ref List<GemSlot> slots, int x, int y, int width, int height)
        {
            if (x - 1 >= 0 && gems[x, y].gemData.color == gems[x - 1, y].gemData.color && gems[x - 1, y].gemData.power == GemPower.None)
                if (TryDissolveGems(ref gems, ref slots, x - 1, y))
                    GetComboColor(ref gems, ref slots, x - 1, y, width, height);
            if (x + 1 < width && gems[x, y].gemData.color == gems[x + 1, y].gemData.color && gems[x + 1, y].gemData.power == GemPower.None)
                if (TryDissolveGems(ref gems, ref slots, x + 1, y))
                    GetComboColor(ref gems, ref slots, x + 1, y, width, height);
            if (y - 1 >= 0 && gems[x, y].gemData.color == gems[x, y - 1].gemData.color && gems[x, y - 1].gemData.power == GemPower.None)
                if (TryDissolveGems(ref gems, ref slots, x, y - 1))
                    GetComboColor(ref gems, ref slots, x, y - 1, width, height);
            if (y + 1 < height && gems[x, y].gemData.color == gems[x, y + 1].gemData.color && gems[x, y + 1].gemData.power == GemPower.None)
                if (TryDissolveGems(ref gems, ref slots, x, y + 1))
                    GetComboColor(ref gems, ref slots, x, y + 1, width, height);
        }

        private bool TryDissolveGems(ref GemSlot[,] gems, ref List<GemSlot> slots, int x, int y)
        {
            if (gems[x, y].IsValid() && !slots.Contains(gems[x, y]))
            {
                slots.Add(gems[x, y]);
                return true;
            }
            else
                return false;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (m_minimalDissolveColorCount < 0)
                m_minimalDissolveColorCount = 0;
            if (m_minimalPowerColorCount < 0)
                m_minimalPowerColorCount = 0;
            if (m_minimalPowerAxisCount < 0)
                m_minimalPowerAxisCount = 0;
        }
#endif
    }
}