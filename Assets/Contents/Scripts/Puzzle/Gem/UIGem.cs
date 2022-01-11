using LuviKunG.Pooling;
using LuviKunG.UI;
using System;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class UIGem : UIBehaviourBase, IPoolable
    {
        public abstract bool isPoolActive { get; set; }
        public abstract void SetGem(ref GemData gemData);
        public abstract void Move(Vector3 position);
        public abstract void PlayGemBreak();
        public virtual void SetPosition(Vector3 position)
        {
            rectTransform.position = position;
        }

    }
}