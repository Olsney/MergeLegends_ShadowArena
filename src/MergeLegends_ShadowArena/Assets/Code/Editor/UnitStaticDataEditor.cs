using System.Collections.Generic;
using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(UnitStaticData))]
    public class UnitStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var data = (UnitStaticData)target;
            serializedObject.Update();

            SerializedProperty generation = serializedObject.FindProperty(nameof(UnitStaticData.Generation));

            EditorGUI.BeginChangeCheck();

            DrawAllExcept(generation.name);
            DrawGeneration(data, generation);

            bool changed = EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();

            if (changed && data.Generation.AutoGenerate)
                Generate(data);
        }

        private void DrawAllExcept(string excludedName)
        {
            SerializedProperty it = serializedObject.GetIterator();
            bool enterChildren = true;

            while (it.NextVisible(enterChildren))
            {
                enterChildren = false;

                if (it.name == excludedName)
                    continue;

                using (new EditorGUI.DisabledScope(it.name == "m_Script"))
                    EditorGUILayout.PropertyField(it, true);
            }
        }

        private void DrawGeneration(UnitStaticData data, SerializedProperty generation)
        {
            generation.isExpanded = EditorGUILayout.Foldout
                (generation.isExpanded, generation.displayName, true);

            if (generation.isExpanded == false)
                return;

            EditorGUI.indentLevel++;

            SerializedProperty child = generation.Copy();
            SerializedProperty end = generation.GetEndProperty();
            bool enterChildren = true;

            while (child.NextVisible(enterChildren) && SerializedProperty.EqualContents(child, end) == false)
            {
                enterChildren = false;
                EditorGUILayout.PropertyField(child, true);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUI.DisabledScope(data.Generation.AutoGenerate))
                {
                    if (GUILayout.Button("Generate"))
                    {
                        serializedObject.ApplyModifiedProperties();
                        Generate(data);
                    }
                }

                if (GUILayout.Button("Reset overrides & generate") &&
                    EditorUtility.DisplayDialog("Reset overrides",
                        "Снять Override со всех уровней и пересчитать значения?", "Да", "Отмена"))
                {
                    serializedObject.ApplyModifiedProperties();
                    Undo.RecordObject(data, "Reset unit overrides");

                    foreach (UnitStatsByLevelIndexSetup setup in data.UnitStatsByLevelIndexSetups)
                        setup.Override = false;

                    Generate(data);
                }
            }

            EditorGUI.indentLevel--;
        }

        private static void Generate(UnitStaticData data)
        {
            Undo.RecordObject(data, "Generate unit stats");
            data.UnitStatsByLevelIndexSetups ??= new List<UnitStatsByLevelIndexSetup>();
            data.Generation.Apply(data.UnitStatsByLevelIndexSetups);
            EditorUtility.SetDirty(data);
        }
    }
}