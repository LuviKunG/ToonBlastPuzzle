using System;
using UnityEngine;

namespace LuviKunG.Attribute
{
    /// <summary>
    /// Makes object field require object reference from prefab.
    /// </summary>
    public class PrefabFieldAttribute : PropertyAttribute
    {
        public Type type = default;
        public PrefabFieldAttribute() { }
        public PrefabFieldAttribute(Type type)
        {
            this.type = type;
        }
    }
}
