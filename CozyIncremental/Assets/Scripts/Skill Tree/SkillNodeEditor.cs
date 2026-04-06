using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillTreeNode))]
public class SkillNodeEditor : Editor
{
    SerializedProperty damageUpgradeProperty;
    private void OnEnable()
    {
        damageUpgradeProperty = serializedObject.FindProperty("damageUpgrade");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SkillTreeNode script = (SkillTreeNode)target;
        if (script.upgradeTypes.HasFlag(UpgradeTypes.DAMAGE))
        {
            EditorGUILayout.PropertyField(damageUpgradeProperty, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
