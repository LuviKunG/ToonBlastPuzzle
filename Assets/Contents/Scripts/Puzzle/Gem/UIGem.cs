using LuviKunG.Pooling;
using LuviKunG.UI;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class UIGem : UIBehaviourBase, IPoolable
    {
        public abstract bool isPoolActive { get; set; }
        public abstract void SetGem(ref GemData gemData);
        public abstract void Move(Vector2 position);

        public virtual void SetPosition(Vector2 position)
        {
            rectTransform.position = position;
        }
    }
}