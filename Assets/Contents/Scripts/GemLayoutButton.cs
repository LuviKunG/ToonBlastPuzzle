using LuviKunG.Attribute;
using LuviKunG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ToonBlastPuzzle
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GemLayoutButton : UIBehaviourBase
    {
        [Header("Components")]
        [SerializeField]
        private GridLayoutGroup gridLayoutGroup = default;
        [SerializeField]
        private RectTransform rectTransformGem = default;

        [Header("Prefabs")]
        [SerializeField, PrefabField(typeof(UIGem))]
        private UIGem prefabGem = default;
        [SerializeField, PrefabField(typeof(UIGemButton))]
        private UIGemButton prefabGemButton = default;

        private Vector2 gemSize;

        public UIGemButton[,] buttons = null;

        public void SetGemSize(float size)
        {
            SetGemSize(size, size);
        }

        public void SetGemSize(float width, float height)
        {
            if (width <= 0.0f || height <= 0.0f)
                throw new GameDevelopmentException("Cannot set gem size with negative or zero.");
            gemSize = new Vector2(width, height);
            gridLayoutGroup.cellSize = gemSize;
        }

        public void SetLayoutSize(int width, int height)
        {
            ClearGrid();
            buttons = new UIGemButton[width, height];
            if (width <= 0 || height <= 0)
                throw new GameDevelopmentException("Cannot create gem grid size with negative or zero.");
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = width;
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    UIGemButton button = Instantiate(prefabGemButton, gridLayoutGroup.transform);
                    button.name = $"Gem Button [{x}, {y}]";
                    buttons[x, y] = button;
                }
            }
            int gemCount = width * height * 2;
            for (int i = 0; i < gemCount; ++i)
            {
                UIGem gem = Instantiate(prefabGem, rectTransformGem);
                gem.rectTransform.sizeDelta = gemSize;
                gem.name = $"Gem [{i}]";
            }
        }

        public void ClearGrid()
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

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (gridLayoutGroup == null)
                gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }
#endif
    }
}