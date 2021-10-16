using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(DungeonGeneratorManager))]
public class DungeonGeneratorManagerEditor : Editor
{
    SerializedProperty generationMethod;
    SerializedProperty lowerBound;
    SerializedProperty upperBound;
    SerializedProperty fixedValue;

    private void OnEnable()
    {
        generationMethod = serializedObject.FindProperty(nameof(DungeonGeneratorManager.GenerationMethod));
        lowerBound = serializedObject.FindProperty(nameof(DungeonGeneratorManager.RandomLowerBound));
        upperBound = serializedObject.FindProperty(nameof(DungeonGeneratorManager.RandomUpperBound));
        fixedValue = serializedObject.FindProperty(nameof(DungeonGeneratorManager.FixedValue));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(generationMethod);

        switch ((DungeonGeneratorManager.RandomRoomNumberMethod)generationMethod.enumValueIndex)
        {
            case DungeonGeneratorManager.RandomRoomNumberMethod.Fixed:
                EditorGUILayout.PropertyField(fixedValue);
                break;
            case DungeonGeneratorManager.RandomRoomNumberMethod.Random:
                EditorGUILayout.PropertyField(lowerBound);
                EditorGUILayout.PropertyField(upperBound);
                break;
        }

        base.OnInspectorGUI();

        serializedObject.ApplyModifiedProperties();
    }
}