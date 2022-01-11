using LuviKunG.Pooling;
using LuviKunG.UI;
using UnityEngine.EventSystems;

namespace ToonBlastPuzzle
{
    public class UIGemButton : UIBehaviourBase, IPoolable, IPointerClickHandler
    {
        public delegate void OnButtonGemClickedDelegate(UIGemButton gemButton);

        public bool isAvailable = default;
        public int x = default;
        public int y = default;
        public OnButtonGemClickedDelegate onButtonGemClicked;

        public bool isPoolActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!isAvailable)
                return;
            onButtonGemClicked?.Invoke(this);
        }
    }
}