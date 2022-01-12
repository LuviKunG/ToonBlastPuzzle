using UnityEngine;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Gem Styles")]
    public sealed class PuzzleGemStyleData : ScriptableObject
    {
        [SerializeField]
        private float _gemSize;
        public float gemSize => _gemSize;

        [SerializeField]
        private UIGem _prefabGem;
        public UIGem prefabGem => _prefabGem;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_gemSize < 0)
                _gemSize = 0;
        }
#endif
    }
}