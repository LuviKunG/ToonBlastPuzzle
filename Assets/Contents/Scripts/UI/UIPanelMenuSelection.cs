using LuviKunG.Attribute;
using LuviKunG.Pooling;
using LuviKunG.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace ToonBlastPuzzle
{
    public sealed class UIPanelMenuSelection : UIBehaviourBase
    {
        public delegate void MenuSelectionHandler();

        [Header("UI")]
        [SerializeField]
        private UIButton m_buttonClose;
        public UIButton buttonClose => m_buttonClose;

        [Header("Transform")]
        [SerializeField]
        private RectTransform m_rectTransformElements;

        [Header("Prefabs")]
        [SerializeField, AssetField]
        private UIMenuSelectionElement m_prefabElement;

        private PoolObject<UIMenuSelectionElement> m_poolElements;
        private List<UIMenuSelectionElement> m_listElements;

        public void Initialize()
        {
            m_poolElements = new PoolObject<UIMenuSelectionElement>((index) =>
            {
                UIMenuSelectionElement element = Instantiate(m_prefabElement, m_rectTransformElements);
                element.isPoolActive = false;
                return element;
            });
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void AddElement(string text, MenuSelectionHandler onSelect)
        {
            UIMenuSelectionElement element = m_poolElements.Pick();
            element.isPoolActive = true;
            element.text = text;
            element.onClick.RemoveAllListeners();
            element.onClick.AddListener(() => { onSelect?.Invoke(); });
        }

        public void ClearAllElements()
        {
            for (int i = 0; i < m_listElements.Count; i++)
            {
                m_listElements[i].isPoolActive = false;
                m_listElements[i].onClick.RemoveAllListeners();
            }
            m_listElements.Clear();
        }
    }
}