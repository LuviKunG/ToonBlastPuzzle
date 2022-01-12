using LuviKunG.UI;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public sealed class UIPanelGameplayHeader : UIBehaviourBase
    {
        [SerializeField]
        private UIPanelGameplayScore m_gameplayScore;
        public UIPanelGameplayScore gameplayScore => m_gameplayScore;

        [SerializeField]
        private UIButton m_buttonBack;
        public UIButton buttonBack => m_buttonBack;
    }
}