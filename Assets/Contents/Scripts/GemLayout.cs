using LuviKunG.UI;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public sealed class GemLayout : UIBehaviourBase
    {
        [SerializeField]
        private GemLayoutButton gemLayoutButton;
        public ref GemLayoutButton button => ref gemLayoutButton;
    }
}