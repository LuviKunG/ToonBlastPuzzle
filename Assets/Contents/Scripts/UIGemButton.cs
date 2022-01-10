using LuviKunG.Pooling;
using LuviKunG.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ToonBlastPuzzle
{
    public class UIGemButton : UIBehaviourBase, IPoolable, IPointerClickHandler
    {
        public bool isAvailable;

        public bool isPoolActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!isAvailable)
                return;
            Debug.Log(name);
        }
    }
}