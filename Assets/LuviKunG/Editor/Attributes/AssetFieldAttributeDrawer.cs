using UnityEditor;
using UnityEngine;

namespace LuviKunG.Attribute
{
    using LuviKunG.EditorLayout;

    [CustomPropertyDrawer(typeof(AssetFieldAttribute))]
    public class AssetFieldAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                Rect rectField = EditorGUI.PrefixLabel(position, label);
                if (property.objectReferenceValue == null || !AssetDatabase.Contains(property.objectReferenceValue))
                {
                    using (new EditorColorScope(Color.red))
                    {
                        EditorGUI.PropertyField(rectField, property, GUIContent.none);
                    }
                }
                else
                {
                    EditorGUI.PropertyField(rectField, property, GUIContent.none);
                }
            }
            else
            {
                EditorGUI.HelpBox(position, $"Cannot use {nameof(AssetFieldAttributeDrawer)} to the field that didn't from object type.", MessageType.Error);
            }
        }
    }
}