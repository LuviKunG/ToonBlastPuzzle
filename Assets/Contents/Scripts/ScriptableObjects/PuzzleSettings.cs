using UnityEngine;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Settings")]
    public sealed class PuzzleSettings : ScriptableObject
    {
        [SerializeField]
        private int _width;
        [SerializeField]
        private int _height;

        public int width => _width;
        public int height => _height;

        private void OnValidate()
        {
            if (_width < 0)
                _width = 0;
            if (_height < 0)
                _height = 0;
        }
    }
}