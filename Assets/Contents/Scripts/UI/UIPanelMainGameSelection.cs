using LuviKunG.Attribute;
using LuviKunG.Pooling;
using LuviKunG.UI;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

namespace ToonBlastPuzzle
{
    public sealed class UIPanelMainGameSelection : UIBehaviourBase
    {
        [Header("Buttons")]
        [SerializeField]
        private UIButton m_buttonSelectLevel;
        public UIButton buttonSelectLevel => m_buttonSelectLevel;

        [SerializeField]
        private UIButton m_buttonSelectComboRule;
        public UIButton buttonSelectComboRule => m_buttonSelectComboRule;

        [SerializeField]
        private UIButton m_buttonSelectScoreCalculation;
        public UIButton buttonSelectScoreCalculation => m_buttonSelectScoreCalculation;

        [SerializeField]
        private UIButton m_buttonSelectGemRandomizer;
        public UIButton buttonSelectGemRandomizer => m_buttonSelectGemRandomizer;

        [SerializeField]
        private UIButton m_buttonSelectGemStyle;
        public UIButton buttonSelectGemStyle => m_buttonSelectGemStyle;

        [SerializeField]
        private UIButton m_buttonPlay;
        public UIButton buttonPlay => m_buttonPlay;

        [Header("Menu Selections")]
        [SerializeField]
        private UIPanelMenuSelection m_menuSelectionLevel;
        public UIPanelMenuSelection menuSelectionLevel => m_menuSelectionLevel;

        [SerializeField]
        private UIPanelMenuSelection m_menuSelectionComboRule;
        public UIPanelMenuSelection menuSelectionComboRule => m_menuSelectionComboRule;

        [SerializeField]
        private UIPanelMenuSelection m_menuSelectionScoreCalculation;
        public UIPanelMenuSelection menuSelectionScoreCalculation => m_menuSelectionScoreCalculation;

        [SerializeField]
        private UIPanelMenuSelection m_menuSelectionGemRandomizer;
        public UIPanelMenuSelection menuSelectionGemRandomizer => m_menuSelectionGemRandomizer;

        [SerializeField]
        private UIPanelMenuSelection m_menuSelectionGemStyle;
        public UIPanelMenuSelection menuSelectionGemStyle => m_menuSelectionGemStyle;

        public void Initialize()
        {
            m_menuSelectionLevel.Initialize();
            m_menuSelectionComboRule.Initialize();
            m_menuSelectionScoreCalculation.Initialize();
            m_menuSelectionGemRandomizer.Initialize();
            m_menuSelectionGemStyle.Initialize();
            m_menuSelectionLevel.Hide();
            m_menuSelectionComboRule.Hide();
            m_menuSelectionScoreCalculation.Hide();
            m_menuSelectionGemRandomizer.Hide();
            m_menuSelectionGemStyle.Hide();
        }
    }
}