using UnityEditor;
using UnityEngine;

namespace LuviKunG.Attribute
{
    using LuviKunG.EditorLayout;
    [CustomPropertyDrawer(typeof(ScriptableObjectFieldAttribute))]
    public class ScriptableObjectFieldAttributeDrawer : PropertyDrawer
    {
        private const float BUTTON_WIDTH = 50.0f;

        private int pickerControlID = -1;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ScriptableObjectFieldAttribute scriptableObjectAttribute = (ScriptableObjectFieldAttribute)attribute;
            if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == pickerControlID)
            {
                property.objectReferenceValue = EditorGUIUtility.GetObjectPickerObject();
                pickerControlID = -1;
            }
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                Rect rectField = EditorGUI.PrefixLabel(position, label);
                if (scriptableObjectAttribute.type == null)
                {
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
                    Rect rectPropertyField = new Rect(rectField.x, rectField.y, rectField.width - BUTTON_WIDTH, rectField.height);
                    Rect rectButton = new Rect(rectField.x + rectPropertyField.width, rectField.y, BUTTON_WIDTH, rectField.height);
                    if (property.objectReferenceValue == null || !AssetDatabase.Contains(property.objectReferenceValue))
                    {
                        using (new EditorColorScope(Color.red))
                        {
                            EditorGUI.PropertyField(rectPropertyField, property, GUIContent.none);
                        }
                    }
                    else
                    {
                        EditorGUI.PropertyField(rectPropertyField, property, GUIContent.none);
                    }
                    if (GUI.Button(rectButton, "Select", EditorStyles.miniButton))
                    {
                        pickerControlID = GUIUtility.GetControlID(FocusType.Passive);
                        EditorGUIUtility.ShowObjectPicker<ScriptableObject>(property.objectReferenceValue, false, $"t:{scriptableObjectAttribute.type.Name}", pickerControlID);
                    }
                }
            }
            else
            {
                EditorGUI.HelpBox(position, $"Cannot use {nameof(PrefabFieldAttribute)} to the field that didn't from object type.", MessageType.Error);
            }
        }
    }
}