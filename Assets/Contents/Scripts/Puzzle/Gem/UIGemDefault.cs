using UnityEngine;
using UnityEngine.UI;

namespace ToonBlastPuzzle
{
    public sealed class UIGemDefault : UIGem
    {
        [Header("Components")]
        [SerializeField]
        private Image imageSprite;

        [Header("Sprites")]
        [SerializeField]
        private PairGemColorSprite[] listColor;
        [SerializeField]
        private Sprite spritePowerHorizontal;
        [SerializeField]
        private Sprite spritePowerVertical;
        [SerializeField]
        private Sprite spritePowerAxis;
        [SerializeField]
        private Sprite spritePowerGroup;
        [SerializeField]
        private PairGemColorSprite[] listPowerColor;

        public override bool isPoolActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public override void SetGem(ref GemData gemData)
        {
            switch (gemData.power)
            {
                case GemPower.None:
                    imageSprite.sprite = PairGemColorSprite.GetSprite(ref listColor, gemData.color);
                    return;
                case GemPower.Horizontal:
                    imageSprite.sprite = spritePowerHorizontal;
                    return;
                case GemPower.Vertical:
                    imageSprite.sprite = spritePowerVertical;
                    return;
                case GemPower.Axis:
                    imageSprite.sprite = spritePowerAxis;
                    return;
                case GemPower.Group:
                    imageSprite.sprite = spritePowerGroup;
                    return;
                case GemPower.Color:
                    imageSprite.sprite = PairGemColorSprite.GetSprite(ref listPowerColor, gemData.color);
                    return;
                default: return;
            }
        }

        public override void Move(Vector2 position)
        {

        }
    }
}