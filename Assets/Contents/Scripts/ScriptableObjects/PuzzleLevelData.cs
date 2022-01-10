using UnityEngine;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Level Data")]
    public sealed class PuzzleLevelData : ScriptableObject//, ISerializationCallbackReceiver
    {
        [SerializeField]
        private int m_width = 1;
        public int width => m_width;

        [SerializeField]
        private int m_height = 1;
        public int height => m_height;

        [SerializeField]
        private bool[] m_patternFlatter = default;

        public bool[,] GetPattern()
        {
            bool[,] pattern = new bool[m_width, m_height];
            for (int y = 0; y < m_height; ++y)
                for (int x = 0; x < m_width; ++x)
                    pattern[x, y] = m_patternFlatter[y * m_width + x];
            return pattern;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (m_width < 1)
                m_width = 1;
            if (m_height < 1)
                m_height = 1;
        }
#endif
    }
}