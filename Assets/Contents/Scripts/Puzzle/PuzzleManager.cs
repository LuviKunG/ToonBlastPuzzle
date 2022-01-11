using LuviKunG.Attribute;
using LuviKunG.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public sealed class PuzzleManager : MonoBehaviour
    {
        [Header("Configurations")]
        [SerializeField, AssetField]
        private PuzzleLevelData levelData = default;
        [SerializeField, AssetField]
        private PuzzleComboRuleBase comboRule = default;
        [SerializeField, AssetField]
        private PuzzleScoreCalculationBase scoreCalculation = default;
        [SerializeField, AssetField]
        private PuzzleGemRandomizer gemRandomizer = default;
        [SerializeField, AssetField]
        private PuzzleGemStyles gemStyle = default;

        [Header("References")]
        [SerializeField]
        private UIGemLayout uiGemLayout = default;
        [SerializeField]
        private UIGameplayScore uiScore = default;

        public GemSlot[,] gems = null;

        public PoolObject<GemSlot> poolGemSlot;
        public PoolObject<UIGem> poolUIGem;

        private List<GemData> comboResolveGems;

        private int m_currentScore;

        /// <summary>
        /// Initialize Async of puzzle manager.
        /// </summary>
        /// <returns>Yield instruction.</returns>
        public IEnumerator InitializeAsync()
        {
            m_currentScore = 0;
            comboResolveGems = new List<GemData>();
            poolGemSlot = new PoolObject<GemSlot>((index) =>
            {
                GemSlot gemSlot = new GemSlot();
                gemSlot.isPoolActive = false;
                return gemSlot;
            });
            poolUIGem = new PoolObject<UIGem>((index) =>
            {
                UIGem gem = Instantiate(gemStyle.prefabGem, uiGemLayout.rectTranformGem);
                gem.isPoolActive = false;
                gem.name = $"Gem ({index})";
                gem.rectTransform.sizeDelta = new Vector2(gemStyle.gemSize, gemStyle.gemSize);
                return gem;
            });
            uiGemLayout.Initialize(gemStyle.gemSize);
            uiGemLayout.onGemSelected = OnGemSelected;
            uiScore.SetScore(m_currentScore);
            yield return gemRandomizer.InitializeAsync();
            yield return gemStyle.InitializeAsync();
        }

        /// <summary>
        /// Calculate drop down gems.
        /// </summary>
        /// <param name="gems">Reference of gem slots.</param>
        private void DropDownGems(ref GemSlot[,] gems)
        {
            int width = gems.GetLength(0), height = gems.GetLength(1);
            for (int y = height - 1; y > 0; --y)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (gems[x, y].isAvailable && !gems[x, y].IsValid())
                    {
                        int i = y - 1;
                        do
                        {
                            if (gems[x, i].isAvailable)
                            {
                                if (gems[x, i].IsValid())
                                {
                                    gems[x, y].SwapGem(gems[x, i]);
                                    gems[x, y].gem?.Move(uiGemLayout.buttons[x, y].rectTransform.position);
                                    break;
                                }
                                else
                                {
                                    i--;
                                    continue;
                                }
                            }
                            else
                            {
                                i--;
                                continue;
                            }
                        }
                        while (i >= 0);
                    }
                }
            }
        }

        /// <summary>
        /// Create new gems in white space.
        /// Will generate other special power gem when available in gems data.
        /// </summary>
        /// <param name="gems">Reference of gem slots.</param>
        /// <param name="gemsData">Reference of list of GemData.</param>
        private void GenerateNewGems(ref GemSlot[,] gems, ref List<GemData> gemsData)
        {
            List<GemSlot> spaceSlots = new List<GemSlot>();
            int width = gems.GetLength(0), height = gems.GetLength(1);
            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x)
                    if (gems[x, y].isAvailable && !gems[x, y].IsValid())
                    {
                        gemRandomizer.RandomizeGem(ref gems, out GemData gemData);
                        gems[x, y].gemData = gemData;
                        UIGem gem = poolUIGem.Pick();
                        gem.isPoolActive = true;
                        gem.rectTransform.SetAsFirstSibling();
                        gem.SetPosition(uiGemLayout.buttons[x, y].rectTransform.position + new Vector3(0, gemStyle.gemSize, 0));
                        gem.Move(uiGemLayout.buttons[x, y].rectTransform.position);
                        gems[x, y].gem = gem;
                        spaceSlots.Add(gems[x, y]);
                    }
            // Remove every first of gemsData until it's none or no more space slot is left.
            while (gemsData.Count > 0 && spaceSlots.Count > 0)
            {
                // Apply first gemsData to a random space slot, then remove both for prevent re-random again.
                int randomIndexSlot = Random.Range(0, spaceSlots.Count);
                spaceSlots[randomIndexSlot].gemData = gemsData[0];
                gemsData.RemoveAt(0);
                spaceSlots.RemoveAt(randomIndexSlot);
            }
        }

        /// <summary>
        /// Create an puzzle depend on level data and gem randomizer.
        /// </summary>
        public void CreatePuzzle()
        {
            bool[,] pattern = levelData.GetPattern();
            gems = new GemSlot[levelData.width, levelData.height];
            for (int y = 0; y < gems.GetLength(1); ++y)
            {
                for (int x = 0; x < gems.GetLength(0); ++x)
                {
                    GemSlot gemSlot = poolGemSlot.Pick();
                    gemSlot.isPoolActive = true;
                    gemSlot.isAvailable = pattern[x, y];
                    gemSlot.x = x;
                    gemSlot.y = y;
                    gems[x, y] = gemSlot;
                }
            }
            uiGemLayout.CreateLayout(ref gems);
            RandomAllGems(ref gems);
            InitialPositionGem(ref gems);
        }

        /// <summary>
        /// Random all gems in all available slots.
        /// </summary>
        /// <param name="gems">Reference of gem slots.</param>
        private void RandomAllGems(ref GemSlot[,] gems)
        {
            for (int y = 0; y < gems.GetLength(1); ++y)
            {
                for (int x = 0; x < gems.GetLength(0); ++x)
                {
                    if (gems[x, y].isAvailable)
                    {
                        gemRandomizer.RandomizeGem(ref gems, out GemData gemData);
                        gems[x, y].gemData = gemData;
                    }
                }
            }
        }

        /// <summary>
        /// Create initial position for gems.
        /// </summary>
        /// <param name="gems">Reference of gem slots.</param>
        private void InitialPositionGem(ref GemSlot[,] gems)
        {
            for (int y = 0; y < gems.GetLength(1); ++y)
            {
                for (int x = 0; x < gems.GetLength(0); ++x)
                {
                    if (gems[x, y].isAvailable)
                    {
                        UIGem gem = poolUIGem.Pick();
                        gem.isPoolActive = true;
                        gem.rectTransform.SetAsFirstSibling();
                        gem.SetPosition(uiGemLayout.buttons[x, y].rectTransform.position);
                        gems[x, y].gem = gem;
                    }
                }
            }
        }

        /// <summary>
        /// Event binding function for gem button when target coordinate gem is selected by player's input.
        /// </summary>
        /// <param name="x">Axis X.</param>
        /// <param name="y">Axis Y.</param>
        private void OnGemSelected(int x, int y)
        {
            List<GemDissolveData> dissolves = new List<GemDissolveData>();
            dissolves.Add(new GemDissolveData(gems[x, y]));
            int dissolveCombo = 0;
            do
            {
                List<GemSlot> dissolveSlots = comboRule.GetCombo(ref gems, ref comboResolveGems, dissolves[0]);
                m_currentScore += scoreCalculation.CalculateScore(ref dissolveSlots, ++dissolveCombo);
                uiScore.SetScore(m_currentScore);
                if (dissolveSlots != null && dissolveSlots.Count > 0)
                {
                    for (int i = 0; i < dissolveSlots.Count; ++i)
                    {
                        if (dissolveSlots[i].gemData.power != GemPower.None)
                            dissolves.Add(new GemDissolveData(dissolveSlots[i]));
                        dissolveSlots[i].ResetGem();
                    }
                }
                dissolves.RemoveAt(0);
            }
            while (dissolves.Count > 0);
            DropDownGems(ref gems);
            GenerateNewGems(ref gems, ref comboResolveGems);
        }
    }
}