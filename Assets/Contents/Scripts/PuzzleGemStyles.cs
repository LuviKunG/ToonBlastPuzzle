using System;
using UnityEngine;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Gem Styles")]
    public sealed class PuzzleGemStyles : ScriptableObject
    {
        [Serializable]
        public struct GemColorSprite
        {
            public GemColor color;
            public Sprite sprite;
        }

        [SerializeField]
        private float _gemSize;
        public float gemSize => _gemSize;

        [SerializeField]
        private GemColorSprite[] spriteColors;
        [SerializeField]
        private GemColorSprite[] spritePowerColors;

        [SerializeField]
        private Sprite spritePowerHorizontal;
        [SerializeField]
        private Sprite spritePowerVertical;
        [SerializeField]
        private Sprite spritePowerAxis;
        [SerializeField]
        private Sprite spritePowerGroup;

        public void ApplyStyle(ref GemData gemData, ref UIGem uiGem)
        {
            switch (gemData.power)
            {
                case GemPower.None:
                    uiGem.SetGemSprite(GetGemSprite(ref spriteColors, gemData.color));
                    return;
                case GemPower.Color:
                    uiGem.SetGemSprite(GetGemSprite(ref spritePowerColors, gemData.color));
                    return;
                case GemPower.Horizontal:
                    uiGem.SetGemSprite(spritePowerHorizontal);
                    return;
                case GemPower.Vertical:
                    uiGem.SetGemSprite(spritePowerVertical);
                    return;
                case GemPower.Axis:
                    uiGem.SetGemSprite(spritePowerAxis);
                    return;
                case GemPower.Group:
                    uiGem.SetGemSprite(spritePowerGroup);
                    return;
            }
        }

        private Sprite GetGemSprite(ref GemColorSprite[] dictionary, GemColor color)
        {
            for (int i = 0; i < dictionary.Length; ++i)
                if (dictionary[i].color == color)
                    return dictionary[i].sprite;
            return null;
        }

        private void OnValidate()
        {
            if (_gemSize < 0)
                _gemSize = 0;
        }
    }
}