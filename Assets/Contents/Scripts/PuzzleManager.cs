using LuviKunG.Attribute;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public sealed class PuzzleManager : MonoBehaviour
    {
        [Header("Configurations")]
        [SerializeField, ScriptableObjectField(typeof(PuzzleSettings))]
        private PuzzleSettings settings = default;
        [SerializeField, ScriptableObjectField(typeof(PuzzleGemRandomizer))]
        private PuzzleGemRandomizer gemRandomizer = default;
        [SerializeField, ScriptableObjectField(typeof(PuzzleGemStyles))]
        private PuzzleGemStyles gemStyle = default;

        [Header("References")]
        [SerializeField]
        private GemLayout gemLayout = default;

        public GemData[,] gems = null;

        public void Initialize()
        {
            gemRandomizer.Initialize();
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