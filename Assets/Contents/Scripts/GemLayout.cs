using LuviKunG.Attribute;
using LuviKunG.Pooling;
using LuviKunG.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ToonBlastPuzzle
{
    public class GemLayout : UIBehaviourBase
    {
        [Header("Components")]
        [SerializeField]
        private GridLayoutGroup gridLayoutGroup = default;

        [Header("Transforms")]
        [SerializeField]
        private RectTransform m_rectTransformGem = default;
        public ref RectTransform rectTranformGem => ref m_rectTransformGem;

        [SerializeField]
        private RectTransform m_rectTransformButton = default;
        public ref RectTransform rectTranformButton => ref m_rectTransformButton;

        [Header("Prefabs")]
        [SerializeField, AssetField]
        private UIGemButton prefabGemButton = default;

        public UIGemButton[,] buttons = null;

        private float m_gemSize;
        public float gemSize
        {
            get => m_gemSize;
            set
            {
                m_gemSize = value;
                gridLayoutGroup.cellSize = new Vector2(m_gemSize, m_gemSize);
            }
        }

        private PoolObject<UIGemButton> poolGemButton;

        public void Initialize(float gemSize)
        {
            this.gemSize = gemSize;
            poolGemButton = new PoolObject<UIGemButton>((index) =>
            {
                UIGemButton gemButton = Instantiate(prefabGemButton, m_rectTransformButton);
                gemButton.isPoolActive = false;
                gemButton.name = $"Gem Button ({index})";
                return gemButton;
            });
        }

        public void CreateLayout(ref GemSlot[,] gemSlots)
        {
            ClearLayout();
            int width = gemSlots.GetLength(0), height = gemSlots.GetLength(1);
            buttons = new UIGemButton[width, height];
            if (width <= 0 || height <= 0)
                throw new GameDevelopmentException("Cannot create gem grid size with negative or zero.");
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = width;
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    UIGemButton button = poolGemButton.Pick();
                    button.name = $"Gem Button [{x}, {y}]";
                    button.isPoolActive = true;
                    button.isAvailable = gemSlots[x, y].isAvailable;
                    buttons[x, y] = button;
                }
            }
            Canvas.ForceUpdateCanvases();
        }

        public void ClearLayout()
        {
            if (buttons != null)
            {
                for (int y = 0; y < buttons.GetLength(1); ++y)
                    for (int x = 0; x < buttons.GetLength(0); ++x)
                        if (buttons[x, y] != null)
                            Destroy(buttons[x, y]);
                buttons = null;
            }
        }
    }
}