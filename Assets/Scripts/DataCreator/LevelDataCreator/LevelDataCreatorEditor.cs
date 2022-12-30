
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(LevelDataCreator))]
public class LevelDataCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelDataCreator m_LevelDataCreator = (LevelDataCreator)target;

        GUILayout.Space(25f);

        EditorGUI.BeginChangeCheck();

        GUILayout.Label("");
        m_LevelDataCreator.UseOpponent = EditorGUILayout.Toggle("Use Opponent", m_LevelDataCreator.UseOpponent);

        GUILayout.Label("");
        m_LevelDataCreator.UseTimer = EditorGUILayout.Toggle("Use Timer", m_LevelDataCreator.UseTimer);

        GUILayout.Label("");
        m_LevelDataCreator.TimeSecondCount = EditorGUILayout.IntField("Time Count", m_LevelDataCreator.TimeSecondCount);

        GUILayout.Label("Image To Be Created");
        m_LevelDataCreator.CreatedLevelTexture = EditorGUILayout.ObjectField("Level Image", m_LevelDataCreator.CreatedLevelTexture, typeof(Texture2D), true) as Texture2D;
        m_LevelDataCreator.ImageWidth=m_LevelDataCreator.CreatedLevelTexture.width;

        GUILayout.Label("Level Name To Be Created");
        m_LevelDataCreator.CreatedLevelName = EditorGUILayout.TextField("Level Name", m_LevelDataCreator.CreatedLevelName);

        EditorGUI.EndChangeCheck();

        GUILayout.Space(25f);

        if (GUILayout.Button("Save Level"))
        {
            m_LevelDataCreator.SaveLevelData();
        }
        if (GUILayout.Button("Created Pixel Cubes"))
        {
            m_LevelDataCreator.SpawnCubes();
        }
        if (GUILayout.Button("Clear Cubes On Scene"))
        {
            m_LevelDataCreator.ClearCubesOnScene();
        }
    }

}
#endif

