using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace ToonBlastPuzzle
{
    [CustomEditor(typeof(PuzzleLevelData))]
    public class PuzzleLevelDataDrawer : Editor
    {
        private const string PROPERTY_PATH_WIDTH = "m_width";
        private const string PROPERTY_PATH_HEIGHT = "m_height";
        private const string PROPERTY_PATH_PATTERN = "m_patternFlatter";

        private SerializedProperty sWidth;
        private SerializedProperty sHeight;
        private SerializedProperty sPattern;

        private void OnEnable()
        {
            sWidth = serializedObject.FindProperty(PROPERTY_PATH_WIDTH);
            sHeight = serializedObject.FindProperty(PROPERTY_PATH_HEIGHT);
            sPattern = serializedObject.FindProperty(PROPERTY_PATH_PATTERN);
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement visualElement = new VisualElement();
            ScrollView scrollView = new ScrollView(ScrollViewMode.Vertical);
            IMGUIContainer imguiContainer = new IMGUIContainer(() =>
            {
                using (new GUILayout.VerticalScope())
                {
                    using (var changeCheckScope = new EditorGUI.ChangeCheckScope())
                    {
                        int uWidth = EditorGUILayout.IntField("Width", sWidth.intValue);
                        sWidth.intValue = uWidth > 0 ? uWidth : 1;
                        int uHeight = EditorGUILayout.IntField("Height", sHeight.intValue);
                        sHeight.intValue = uHeight > 0 ? uHeight : 1;
                        if (GUILayout.Button("Update"))
                            sPattern.arraySize = sWidth.intValue * sHeight.intValue;
                        using (new GUILayout.VerticalScope())
                        {
                            for (int y = 0; y < sHeight.intValue; ++y)
                            {
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUILayout.FlexibleSpace();
                                    for (int x = 0; x < sWidth.intValue; ++x)
                                    {
                                        var sValue = sPattern.GetArrayElementAtIndex(y * sWidth.intValue + x);
                                        sValue.boolValue = GUILayout.Toggle(sValue.boolValue, GUIContent.none);
                                    }
                                    GUILayout.FlexibleSpace();
                                }
                            }
                        }
                        if (changeCheckScope.changed)
                            serializedObject.ApplyModifiedProperties();
                    }
                }
            });
            scrollView.Add(imguiContainer);
            visualElement.Add(scrollView);
            return visualElement;
        }

        public override bool UseDefaultMargins()
        {
            return false;
        }
    }
}