using LuviKunG.Attribute;
using System.Collections;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public sealed class PuzzleManager : MonoBehaviour
    {
        [Header("Configurations")]
        [SerializeField, AssetField]
        private PuzzleSettings settings = default;
        [SerializeField, AssetField]
        private PuzzleGemRandomizer gemRandomizer = default;
        [SerializeField, AssetField]
        private PuzzleGemStyles gemStyle = default;

        [Header("References")]
        [SerializeField]
        private GemLayout gemLayout = default;

        public GemData[,] gems = null;

        public IEnumerator InitializeAsync()
        {
            yield return gemRandomizer.InitializeAsync();
            yield return gemStyle.InitializeAsync();
            gemLayout.button.SetPrefabGem(gemStyle.prefabGem);
            yield break;
        }

        public void CreatePuzzle()
        {
            gems = new GemData[settings.width, settings.height];
            gemLayout.button.SetGemSize(gemStyle.gemSize);
            gemLayout.button.SetLayoutSize(settings.width, settings.height);
            RandomAllGems(ref gems);
        }

        private void RandomAllGems(ref GemData[,] gems)
        {
            for (int y = 0; y < gems.GetLength(1); ++y)
            {
                for (int x = 0; x < gems.GetLength(0); ++x)
                {
                    gemRandomizer.RandomizeGem(ref gems, out GemData gemData);
                    gems[x, y] = gemData;
                }
            }
        }
    }

    public abstract class PuzzleRuleBase : ScriptableObject
    {

    }
}