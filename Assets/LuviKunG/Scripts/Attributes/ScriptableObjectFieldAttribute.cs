using System;
using UnityEngine;

namespace LuviKunG.Attribute
{
    /// <summary>
    /// Makes object field require object reference from scriptable object.
    /// </summary>
    public class ScriptableObjectFieldAttribute : PropertyAttribute
    {
        public Type type = default;
        public ScriptableObjectFieldAttribute() { }
        public ScriptableObjectFieldAttribute(Type type)
        {
            this.type = type;
        }
    }
}
