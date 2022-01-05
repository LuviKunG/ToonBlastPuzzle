using System;
using UnityEngine;

namespace LuviKunG.EditorLayout
{
    public class EditorColorScope : IDisposable
    {
        public Color color = default;
        private Color cacheColor = default;

        public EditorColorScope(Color color)
        {
            this.color = color;
            cacheColor = GUI.color;
            GUI.color = color;
        }
        public void Dispose()
        {
            GUI.color = cacheColor;
        }
    }
}