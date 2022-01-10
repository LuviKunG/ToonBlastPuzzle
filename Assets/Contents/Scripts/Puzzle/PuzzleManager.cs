using LuviKunG.Attribute;
using LuviKunG.Pooling;
using System.Collections;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public sealed class PuzzleManager : MonoBehaviour
    {
        [Header("Configurations")]
        [SerializeField, AssetField]
        private PuzzleLevelData levelData = default;
        [SerializeField, AssetField]
        private PuzzleGemRandomizer gemRandomizer = default;
        [SerializeField, AssetField]
        private PuzzleGemStyles gemStyle = default;

        [Header("References")]
        [SerializeField]
        private GemLayout gemLayout = default;

        public GemSlot[,] gems = null;

        public PoolObject<GemSlot> poolGemSlot;
        public PoolObject<UIGem> poolUIGem;

        public IEnumerator InitializeAsync()
        {
            poolGemSlot = new PoolObject<GemSlot>((index) =>
            {
                GemSlot gemSlot = new GemSlot();
                gemSlot.isPoolActive = false;
                return gemSlot;
            });
            poolUIGem = new PoolObject<UIGem>((index) =>
            {
                UIGem gem = Instantiate(gemStyle.prefabGem, gemLayout.rectTranformGem);
                gem.isPoolActive = false;
                gem.name = $"Gem ({index})";
                gem.rectTransform.sizeDelta = new Vector2(gemStyle.gemSize, gemStyle.gemSize);
                return gem;
            });
            gemLayout.Initialize(gemStyle.gemSize);
            yield return gemRandomizer.InitializeAsync();
            yield return gemStyle.InitializeAsync();
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
                    gems[x, y] = gemSlot;
                }
            }
            gemLayout.CreateLayout(ref gems);
            RandomAllGems(ref gems);
            InitialPositionGem(ref gems);
        }

        /// <summary>
        /// Random all gems in all available slots.
        /// </summary>
        /// <param name="slots">Gem Slots.</param>
        private void RandomAllGems(ref GemSlot[,] slots)
        {
            for (int y = 0; y < slots.GetLength(1); ++y)
            {
                for (int x = 0; x < slots.GetLength(0); ++x)
                {
                    if (slots[x, y].isAvailable)
                        gemRandomizer.RandomizeGem(ref slots, out slots[x, y].gemData);
                }
            }
        }

        private void InitialPositionGem(ref GemSlot[,] slots)
        {
            for (int y = 0; y < slots.GetLength(1); ++y)
            {
                for (int x = 0; x < slots.GetLength(0); ++x)
                {
                    if (slots[x, y].isAvailable)
                    {
                        UIGem gem = poolUIGem.Pick();
                        gem.isPoolActive = true;
                        gem.rectTransform.SetAsFirstSibling();
                        gem.SetGem(ref slots[x, y].gemData);
                        gem.SetPosition(gemLayout.buttons[x, y].rectTransform.position);
                    }
                }
            }
        }
    }
}