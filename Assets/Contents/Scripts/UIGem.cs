using LuviKunG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ToonBlastPuzzle
{
    public sealed class UIGem : UIBehaviourBase
    {
        [SerializeField]
        private Image imageGem = default;

        public void SetGemSprite(Sprite sprite)
        {
            imageGem.sprite = sprite;
        }

        public void Move(Vector2 position)
        {

        }
    }
}