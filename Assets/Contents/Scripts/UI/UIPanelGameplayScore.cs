using LuviKunG.UI;
using TMPro;
using UnityEngine;

namespace ToonBlastPuzzle
{
    public sealed class UIPanelGameplayScore : UIBehaviourBase
    {
        [SerializeField]
        private TextMeshProUGUI m_textValue;

        public void SetScore(int score)
        {
            m_textValue.text = score.ToString("N0");
        }
    }
}