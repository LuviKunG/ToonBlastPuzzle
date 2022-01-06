using UnityEngine;
using UnityEngine.EventSystems;

namespace LuviKunG.UI
{
    /// <summary>
    /// Base of the UI elements which include RectTransform access.
    /// </summary>
    public class UIBehaviourBase : UIBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;
        public ref RectTransform rectTransform => ref _rectTransform;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            _rectTransform = GetComponent<RectTransform>();
        }
#endif

        protected override void OnTransformParentChanged()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}
