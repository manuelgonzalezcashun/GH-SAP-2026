using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ZoneButton))]
[CanEditMultipleObjects]
public class ZoneButtonEditor : ButtonEditor
{
    SerializedProperty descriptionProperty = null;
    protected override void OnEnable()
    {
        base.OnEnable();
        descriptionProperty = serializedObject.FindProperty("description");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(descriptionProperty);

        serializedObject.ApplyModifiedProperties();
    }
}