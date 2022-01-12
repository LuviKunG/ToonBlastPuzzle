using System;
using UnityEngine;

namespace LuviKunG.Attribute
{
    /// <summary>
    /// Make string field into scene selection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StringSceneAttribute : PropertyAttribute
    {
        public bool excludeDisableScene;

        public StringSceneAttribute()
        {
            excludeDisableScene = true;
        }

        public StringSceneAttribute(bool excludeDisableScene)
        {
            this.excludeDisableScene = excludeDisableScene;
        }
    }
}