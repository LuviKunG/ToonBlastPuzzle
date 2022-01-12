using System;
using UnityEngine;

namespace LuviKunG.Attribute
{
    /// <summary>
    /// Make object field require object reference from asset.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class AssetFieldAttribute : PropertyAttribute
    {
        public AssetFieldAttribute() { }
    }
}