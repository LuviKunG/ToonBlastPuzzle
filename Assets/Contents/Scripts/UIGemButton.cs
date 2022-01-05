using LuviKunG.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ToonBlastPuzzle
{
    public class UIGemButton : UIBehaviourBase, IPointerClickHandler
    {
        public void Initialize()
        {

        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(name);
        }
    }
}