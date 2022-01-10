using System;
using UnityEngine;

namespace ToonBlastPuzzle
{
    [Serializable]
    public struct PairGemColorSprite
    {
        public GemColor color;
        public Sprite sprite;

        public static Sprite GetSprite(ref PairGemColorSprite[] arr, GemColor color)
        {
            for (int i = 0; i < arr.Length; ++i)
                if (arr[i].color == color)
                    return arr[i].sprite;
            return null;
        }
    }
}