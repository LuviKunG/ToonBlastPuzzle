using UnityEngine;
using System.Collections.Generic;
using System;

namespace ToonBlastPuzzle
{
    [CreateAssetMenu(menuName = "Toon Blast Puzzle/Level Data")]
    public sealed class PuzzleLevelData : ScriptableObject//, ISerializationCallbackReceiver
    {
        [SerializeField]
        private int m_width = 1;
        [SerializeField]
        private int m_height = 1;

        [SerializeField]
        private bool[] m_patternFlatter = default;

        private bool[,] m_pattern = default;
        public bool[,] pattern => m_pattern;

        public void OnBeforeSerialize()
        {
            m_width = m_pattern.GetLength(0);
            m_height = m_pattern.GetLength(1);
            m_patternFlatter = new bool[m_width * m_height];
            for (int y = 0; y < m_height; ++y)
                for (int x = 0; x < m_width; ++x)
                    m_patternFlatter[y * m_width + x] = m_pattern[x, y];
        }

        public void OnAfterDeserialize()
        {
            m_pattern = new bool[m_width, m_height];
            for (int y = 0; y < m_height; ++y)
                for (int x = 0; x < m_width; ++x)
                    m_pattern[x, y] = m_patternFlatter[y * m_width + x];
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