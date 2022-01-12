using LuviKunG.UI;
using UnityEngine.EventSystems;

namespace ToonBlastPuzzle
{
    public sealed class UIButton : UIBehaviourBase, IPointerClickHandler
    {
        public UIEventClick onClick;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}