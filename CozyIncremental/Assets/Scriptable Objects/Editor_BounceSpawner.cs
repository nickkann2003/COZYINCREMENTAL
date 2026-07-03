using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_BounceSpawner))]
public class Editor_BounceSpawner : Editor
{
    public override void OnInspectorGUI()
    {
        // Pull the latest values from the object
        serializedObject.Update();

        // Find properties
        SerializedProperty prefabProp = serializedObject.FindProperty("prefab");

        SerializedProperty spawnTypeProp = serializedObject.FindProperty("spawnType");

        SerializedProperty startProp = serializedObject.FindProperty("start");

        SerializedProperty radiusProp = serializedObject.FindProperty("radius");

        SerializedProperty horizontalProp = serializedObject.FindProperty("horizontal");
        SerializedProperty verticalProp = serializedObject.FindProperty("vertical");

        SerializedProperty arcDegreesProp = serializedObject.FindProperty("arcDegrees");
        SerializedProperty minDistanceProp = serializedObject.FindProperty("minDistance");
        SerializedProperty maxDistanceProp = serializedObject.FindProperty("maxDistance");

        EditorGUILayout.PropertyField(prefabProp);

        EditorGUILayout.PropertyField(spawnTypeProp);

        EditorGUI.indentLevel++;

        // Conditionally draw the other fields based on the enum state
        switch (spawnTypeProp.enumValueFlag)
        {
            case 0:
                EditorGUILayout.PropertyField(startProp);
                break;
            case 1:
                EditorGUILayout.PropertyField(radiusProp);
                break;
            case 2:
                EditorGUILayout.PropertyField(horizontalProp);
                EditorGUILayout.PropertyField(verticalProp);
                break;
        }
        EditorGUI.indentLevel--;

        EditorGUILayout.PropertyField(arcDegreesProp);
        EditorGUILayout.PropertyField(minDistanceProp);
        EditorGUILayout.PropertyField(maxDistanceProp);


        // Apply changes made in the inspector to the ScriptableObject asset
        serializedObject.ApplyModifiedProperties();
    }
}