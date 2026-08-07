using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(BattleButton))]
[CanEditMultipleObjects]
public class BattleButtonEditor : ButtonEditor
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
