using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponData))]
public class WeaponDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "_spreadPattern", "_spreadCurveX", "_spreadCurveY");

        WeaponData data = (WeaponData)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Specific Pattern Settings", EditorStyles.boldLabel);

        if (data.RecoilType == WeaponRecoilType.Array)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spreadPattern"), true);
            EditorGUILayout.HelpBox("Using Legacy Array Pattern", MessageType.Info);
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spreadCurveX"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spreadCurveY"));
            EditorGUILayout.HelpBox("Using Procedural Animation Curves", MessageType.Info);
        }

        serializedObject.ApplyModifiedProperties();
    }
}