using LuviKunG.UI;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public abstract class UIGem : UIBehaviourBase
    {
        public abstract void SetGem(ref GemData gemData);
        public abstract void Move(Vector2 position);
    }
}