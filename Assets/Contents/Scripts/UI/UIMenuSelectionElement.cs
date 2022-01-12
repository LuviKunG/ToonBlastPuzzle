using LuviKunG.Pooling;
using LuviKunG.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

namespace ToonBlastPuzzle
{
    public sealed class UIMenuSelectionElement : UIBehaviourBase, IPoolable, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI m_text;

        public string text
        {
            get => m_text.text;
            set => m_text.text = value;
        }

        public UnityEvent onClick;

        public bool isPoolActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}